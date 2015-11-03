/// <reference path="../../../typings/tsd.d.ts" />

import jsyaml=require("../jsyaml/jsyaml2lowLevel")
import defs=require("../definitionSystem")
import hl=require("../highLevelAST")
import ll=require("../lowLevelAST")
import tsStruct=require("../tsStructureParser")
import ts2Def=require("../tsStrut2Def")
import _=require("underscore")
import yaml=require("../jsyaml/yamlAST")
import selector=require("../selectorMatch")
import typeExpression=require("../typeExpressions")
import def=require( "../definitionSystem");
import high=require("../highLevelAST");
import ramlSignature=require("../ramlSignature")
import hlimpl=require("../highLevelImpl")
import su=require("../schemaUtil")
import linterApi=require("./linterApi")
import wrapper=require("../artifacts/raml003parser")
import path=require("path")
import fs=require("fs");
var mediaTypeParser=require("media-typer")

import xmlutil = require('../../util/xmlutil')

type ASTNodeImpl=hlimpl.ASTNodeImpl;
type ASTPropImpl=hlimpl.ASTPropImpl;

class LinterSettings{
    validateNotStrictExamples=true;
}
var settings=new LinterSettings();

interface PropertyValidator{
    validate(node:hl.IAttribute,cb:hl.ValidationAcceptor);
}

interface IShema{
    validate(pObje:any,cb:hl.ValidationAcceptor,strict:boolean);
}
var loophole= require("loophole")
export function evalInSandbox(code:string,thisArg:any,args:any[]) {
    return new loophole.Function(code).call(thisArg,args);
}

var lintWithFile = function (customLinter:string, acceptor:hl.ValidationAcceptor, astNode:hl.IHighLevelNode) {
    if (fs.existsSync(customLinter)) {
        try {
            var content = fs.readFileSync(customLinter).toString();
            var factr = new LinterExtensionsImpl(acceptor);
            evalInSandbox(content, factr, null);
            factr.visit(astNode);
        } catch (e) {
            console.log("Error in custom linter")
            console.log(e);
        }
    }
};
export function lintNode(astNode:hl.IHighLevelNode, acceptor:hl.ValidationAcceptor) {
    var ps = astNode.lowLevel().unit().absolutePath();
    var dr = path.dirname(ps);
    var customLinter = path.resolve(dr, "raml-lint.js");
    lintWithFile(customLinter, acceptor, astNode);
    var dir=path.resolve(dr,".raml")
    if (fs.existsSync(dir)){
        var st=fs.statSync(dir);
        if (st.isDirectory()){
            var files=fs.readdirSync(dir);
            files.forEach(x=>{
                if (x.indexOf("-lint.js")!=-1){
                    lintWithFile(path.resolve(dir,x),acceptor,astNode);
                }
                //console.log(x);
            })
        }
    }
};
export class LinterExtensionsImpl implements linterApi.ErrorFactory<wrapper.BasicNode>,linterApi.Linter{

    constructor(private acceptor:hl.ValidationAcceptor){

    }
    error(w:wrapper.BasicNode,message:string){
        this.acceptor.accept(createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,message,w.highLevel()));
    }
    errorOnProperty(w:wrapper.BasicNode,property: string,message:string){
        var pr=w.highLevel().attr(property);
        this.acceptor.accept(createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,message,pr));
    }
    warningOnProperty(w:wrapper.BasicNode,property: string,message:string){
        var pr=w.highLevel().attr(property);
        this.acceptor.accept(createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,message,pr,true));
    }

    warning(w:wrapper.BasicNode,message:string){
        this.acceptor.accept(createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,message,w.highLevel(),true));
    }
    nodes:{ [name:string]:linterApi.LinterRule<any>[]}={}

    registerRule(nodeType:string,rule: linterApi.LinterRule<any>){
        var q=this.nodes[nodeType];
        if (!q){
            q=[];
            this.nodes[nodeType]=q;
        }
        q.push(rule);
    }

    visit(h:hl.IHighLevelNode){
        var nd=h.definition();
        this.process(nd,h);
        nd.allSuperTypes().forEach(x=>this.process(x,h));
        h.elements().forEach(y=>this.visit(y));
    }
    process(d:hl.ITypeDefinition,h:hl.IHighLevelNode){
        if (d instanceof def.NodeClass) {
            if (!d.getDeclaringNode()) {
                var rules = this.nodes[d.name()];
                if (rules) {
                    rules.forEach(x=>x(h.wrapperNode(), this));
                }
            }
        }
    }
}

export class TypeValidator{


    constructor(private node:hl.IParseResult){

    }

    validate(obj:any, t:hl.ITypeDefinition,cb:hl.ValidationAcceptor,strict:boolean){
        if (t instanceof def.Array){
            this.validateArray(obj,<def.Array>t,cb,strict);
        }
        else if (t instanceof def.Union){
            this.validateUnion(obj,<def.Union>t,cb,strict);
        }
        else if (t instanceof def.NodeClass){
            this.validateClass(obj,<def.NodeClass>t,cb,strict);
        }
        else if (t instanceof def.ValueType){
            this.validateValue(obj,<any>t,cb,strict);
        }
        else{
            throw new Error("Not supported case")
        }
    }
    validateClass(obj:any, t:def.NodeClass,cb:hl.ValidationAcceptor,strict:boolean){
        var supers=t.allSuperTypes();

        this.validateFacets(t, obj, cb, strict);
        supers.forEach(s=>{
           if (s.name()=="StrElement"){
               if (typeof obj!='string'&& typeof obj!='number'&&typeof obj!='boolean'){
                   cb.accept(hlimpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,"String is expected",this.node,!strict))
               }
           }
           if (s.name()=="NumberElement"){
               if (typeof obj!='number'){
                   cb.accept(hlimpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,"Number is expected",this.node,!strict))
               }
           }
           if (s.name()=="BooleanElement"){
                var isOk=obj==true||obj==false;
                if (!isOk){
                    cb.accept(hlimpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,"boolean is expected",this.node,!strict))
                }
           }
           if (s instanceof def.Array){
                this.validate(obj,s,cb,strict)
           }
           if (s instanceof def.Union){
                this.validate(obj,s,cb,strict)
           }
        });
        var props=t.isRuntime()?t.allProperties():t.allRuntimeProperties();
        if (!obj){
            obj={};
        }
        var handled={};
        props.forEach(p=>{

            if (!p.isMerged()){
                var value = obj[p.name()];
                handled[p.name()]=1;
                if (!value) {
                    if (p.isRequired()) {
                        cb.accept(hlimpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, "Required property:" + p.name() + " is missed", this.node, !strict))
                    }
                }
                else {
                    this.validatePropValue(value, p, cb, strict);
                }
            }
        })
        props.forEach(p=>{
            if (p.isMerged()){
                if (p.getKeyRegexp()!=null){
                    Object.keys(obj).forEach(x=>{
                        if (!handled[x]){
                            try {
                                var re=new RegExp(p.getKeyRegexp())
                                if (re.test(x)) {
                                    this.validatePropValue(obj[x],p,cb,strict);
                                    handled[x] = 1;
                                }
                            } catch (e){

                            }
                        }
                    })
                }
            }
        })
        props.forEach(p=>{
            if (p.isMerged()){
                if (p.keyPrefix()!=null){
                    Object.keys(obj).forEach(x=>{
                        if (!handled[x]){
                            this.validatePropValue(obj[x],p,cb,strict);
                            handled[x]=1;
                        }
                    })
                }
            }
        })
        if (typeof obj=='object'&&props.length>0) {
            Object.keys(obj).forEach(x=> {
                if (!handled[x]) {
                    cb.accept(hlimpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, "Unknown property:" + x, this.node, !strict))
                }
            })
        }

    }

    private validateFacets(t, obj, cb, strict) {
        var rof=t.getRepresentationOf();
        var fixedFacets = t.getFixedFacets();

        if (rof){
            t=rof;
            if (t instanceof def.UserDefinedClass){
                fixedFacets=t.getFixedFacets();
            }
        }

        for (var facetKey in fixedFacets) {
            var facet = t.facet(facetKey);
            if (facet) {
                var facetValue = fixedFacets[facetKey];
                var facetValidator = facet.getFacetValidator();
                if (facetValidator) {
                    try {

                        var result = facetValidator(obj, facetValue);
                        if (typeof result == "string") {
                            cb.accept(hlimpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, "" + result, this.node, !strict))
                        }
                    } catch (e) {
                        cb.accept(hlimpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, e.message, this.node, !strict))
                    }
                }
            }
        }
    }

    private validatePropValue(value, p, cb, strict) {
        this.validate(value, p.range(), cb, strict);
        if (p.enumValues(null) && p.enumValues(null).length > 0) {
            if (!_.find(p.enumValues(null), x=>x == value)) {
                cb.accept(hlimpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, "" + p.name() + " should be one of " + p.enumValues(null), this.node, !strict))
            }
        }
    }
    validateArray(obj:any, t:def.Array,cb:hl.ValidationAcceptor,strict:boolean){
        if (obj instanceof Array){
            var arr:any[]=obj;
            this.validateFacets(t, obj, cb, strict);

            arr.forEach(x=>this.validate(x,t.component,cb,strict));
        }
        else{
            cb.accept(hlimpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,"Array expected",this.node,!strict))
        }
    }
    validateUnion(obj:any, t:def.Union,cb:hl.ValidationAcceptor,strict:boolean){
        //FIXME
    }
    validateValue(obj:any, t:def.ValueType,cb:hl.ValidationAcceptor,strict:boolean){
        //FIXME
        if (t.name()=="NumberType"){
            if (typeof obj!='number'){
                var qqq=parseFloat(obj);
                if (!qqq) {
                    cb.accept(hlimpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, "Number is expected", this.node, !strict))
                }
            }
        }
        if (t.name()=="BooleanType"){
            if (typeof obj!='boolean'){
                if (obj!='true'&&obj!='false') {
                    cb.accept(hlimpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, "boolean is expected", this.node, !strict))
                }
            }
        }
    }
}
export class NormalValidator implements PropertyValidator{
    validate(node:hl.IAttribute,cb:hl.ValidationAcceptor){
        var vl=node.value();
        if (node.parent().allowsQuestion()&&node.property().isKey()){
            if (vl != null &&vl.length>0&&vl.charAt(vl.length-1)=='?'){
                vl=vl.substr(0,vl.length-1);
            }
        }
        var pr=node.property();

        var range=pr.range();
        if (range instanceof def.NodeClass){
            var nc=<def.NodeClass>range;
            var rof=nc.getRepresentationOf();
            if (rof){
                nc=rof;
            }
            var ff=nc.getFixedFacets();
            for (var fc in ff){
                var facet=nc.facet(fc);
                if (facet){
                    var val=facet.getFacetValidator();
                    if (val){
                        try{
                          var qq=vl;
                          if (pr.range().isArray()){
                              try {
                                  qq = node.parent().lowLevel().dumpToObject()[node.parent().name()][pr.name()];
                              }catch (e){
                                  //FIXME
                              }
                          }
                          var res=val(qq,ff[fc]);
                          if (typeof res=='string'){
                              cb.accept(createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,""+res, node));
                          }
                        }catch (e){
                            cb.accept(createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,e.message, node));
                        }
                    }
                    //console.log("Facet:"+facet.name())
                }
            }
        }
        var v=cb;
        var validation=pr.range().isValid(node.parent(),vl,pr);
        if (validation instanceof Error){
            if (!(<any>validation).canBeRef){
                v.accept(createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,(<Error>validation).message, node));
                validation=null;
                return;
            }
        }
        if (!validation||validation instanceof Error){//FIXME
            if (pr.name()!='value') {
                if (!checkReference(pr, node, vl, v)) {
                    if (pr.name()=='schema'||pr.name()=='type'){
                        if (vl&&vl.trim()&&(pr.domain().name()=='BodyLike'||pr.domain().name()=="DataElement")){
                            var testSchema=vl.trim().charAt(0);//FIXME
                            if (testSchema!='{'&&testSchema!='<'){
                                return;
                            }
                        }
                        //return ;//
                    }
                    var decl = (<hlimpl.ASTPropImpl>node).findReferencedValue();
                    if (decl instanceof Error) {
                        v.accept(createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,(<Error>decl).message, node));
                    }
                    if (!decl){
                        if (vl) {
                            if (pr.name() == 'schema') {
                                var z=vl.trim();
                                if (z.charAt(0)!='{'&&z.charAt(0)!='<') {
                                    if (vl.indexOf('|') != -1 || vl.indexOf('[]') != -1 || vl.indexOf("(") != -1) {
                                        return;
                                    }
                                }
                            }
                        }
                        if (validation instanceof Error&&vl){
                            v.accept(createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,(<Error>validation).message, node));
                            validation=null;
                            return;
                        }
                        v.accept(createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,"Empty value is not allowed here", node));
                    }
                }
            }
            else{
                var vl=node.value();
                var message="Invalid value schema:"+vl
                if (validation instanceof Error){
                    message=validation.message;
                }
                v.accept(createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, message, node));
            }
        }
        var values=pr.enumOptions();
        if (values&&values.length>0){
            if (!_.find(values, x=>x == vl)) {
                if (vl&&(vl.indexOf("x-")==0)&&pr.name()=="type"){//FIXME move to def system
                    //return true;
                }
                else {
                    v.accept(createIssue(hl.IssueCode.UNRESOLVED_REFERENCE, "Invalid value:" + vl + " allowed values are:" + values.join(","), node));
                }
            }
        }
    }
}

export class UriValidator{
    validate(node:hl.IAttribute,cb:hl.ValidationAcceptor){
        try{
            new UrlParameterNameValidator().parseUrl(node.value());
        }
        catch (e){
            cb.accept(hlimpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, e.message, node,false))
        }
    }
}
export class MediaTypeValidator implements PropertyValidator{
    validate(node:hl.IAttribute,cb:hl.ValidationAcceptor){
        try {
            var v = node.value();
            if (v=="body"){
                if (node.parent().parent()) {
                    if (node.parent().parent().definition().name()=="Response"||node.parent().parent().definition().isAssignableFrom("MethodBase")) {
                        v=node.parent().computedValue("mediaType")
                    }
                }
            }
            if (!v){
                return
            }
            if (v == "*/*") {
                return
            }
            var res = mediaTypeParser.parse(v);
            var types = {
                application: 1,
                audio: 1,
                example: 1,
                image: 1,
                message: 1,
                model: 1,
                multipart: 1,
                text: 1,
                video: 1
            }
            if (!types[res.type]) {
                cb.accept(hlimpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, "Unknown media type 'type'", node))
            }
        }catch (e){
            cb.accept(hlimpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, ""+e.message, node))
        }
        if (node.value()&&node.value()==("multipart/form-data")||node.value()==("application/x-www-form-urlencoded")){
            if (node.parent()&&node.parent().parent()) {
                if (node.parent().parent().property().name() == 'responses') {
                    cb.accept(hlimpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, "Form related media types can not be used in responses", node))
                }
            }
        }
        return;
    }
}
export class SignatureValidator implements PropertyValidator{
    validate(node:hl.IAttribute,cb:hl.ValidationAcceptor){
        var vl=node.value();
        var q = vl?vl.trim():"";
        if (q.length > 0 ) {
            try {
                ramlSignature.validate(vl, node, cb);
            }catch (e){
                cb.accept(hlimpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,"Error during signature parse:"+e.message,node))
            }
            return;
        }
        return;
    }
}
export class UrlParameterNameValidator implements PropertyValidator{

    private checkBaseUri(node:hl.IAttribute,c, vl, v:hl.ValidationAcceptor) {
        var bu = c.root().attr("baseUri")

        if (bu) {
            var tnv = bu.value();
            try {
                var pNames = this.parseUrl(tnv);
                if (!_.find(pNames, x=>x == vl)) {
                    v.accept(hlimpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, "Unused url parameter", node))
                }
            } catch (e) {

            }
        }
        else {
            v.accept(hlimpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, "Unused url parameter", node))
        }
    }
    parseUrl(value:string):string[]{//FIXME INHERITANCE
        var result=[]
        var temp="";
        var inPar=false;
        var count=0;
        for (var a=0;a<value.length;a++){
            var c=value[a];
            if (c=='{'){
                count++;
                inPar=true;
                continue;
            }
            if (c=='}'){
                count--;
                inPar=false;
                result.push(temp);
                temp="";
                continue;
            }
            if (inPar){
                temp+=c;
            }
        }
        if (count>0){
            throw new Error("Unmatched '{'")
        }
        if (count<0){
            throw new Error("Unmatched '}'")
        }
        return result;
    }

    validate(node:hl.IAttribute,cb:hl.ValidationAcceptor){
        var vl=node.value();
        if (node.parent().property().name()=='baseUriParameters'){
            var c=node.parent().parent();
            this.checkBaseUri(node,c, vl, cb);
            return;
        }
        var c=node.parent().parent();
        var tn=c.name();
        if (c.definition().name()=='Api'){
            this.checkBaseUri(node,c, vl, cb);
        }
        try {
            var pNames=this.parseUrl(tn);
            if (!_.find(pNames,x=>x==vl)){
                cb.accept(hlimpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,"Unused url parameter '"+vl+"'",node))
            }
        } catch (e){

        }
    }
}

export function checkReference(pr:hl.IProperty, astNode:hl.IAttribute, vl:string, cb:hl.ValidationAcceptor):boolean {
    if (!vl){
        return;
    }
    if (vl=='null'){
        if (pr.isAllowNull()){
            return;
        }
    }
    //FIXME this check should not be here
    try {
        if (typeof vl=='string') {
            if(pr.domain().name()=='DataElement') {
                if (pr.name() == "type"||pr.name()=='items') {
                    typeExpression.validate(vl, astNode, cb);
                    return false;
                }
            }
            if (pr.range().name()=="SchemaString"){
                if (pr.range().universe().version()=="RAML10"){
                    if (pr.range() instanceof  defs.ValueType){
                        typeExpression.validate(vl, astNode, cb);
                        return false;
                    }
                }
            }
            if (pr.name() == "schema"||pr.name()=='type') {
                if(pr.domain().name()=='BodyLike'||pr.domain().name()=="DataElement") {
                    var q = vl.trim();
                    if (q.length > 0 && q.charAt(0) != '{' && q.charAt(0) != '<') {
                        typeExpression.validate(vl, astNode, cb);
                        return false;
                    }
                    return;
                }
            }

        }
    } catch (e){
        cb.accept(createIssue(hl.IssueCode.UNRESOLVED_REFERENCE,"Syntax error:"+e.message,astNode))
    }
    var valid = (<def.Property>pr).isValidValue(vl,astNode.parent());
    if (!valid) {
        if (typeof vl=='string') {
            if ((vl.indexOf("x-") == 0) && pr.name() == "type") {//FIXME move to def system
                return true;
            }
        }

        cb.accept(createIssue(hl.IssueCode.UNRESOLVED_REFERENCE,"Unresolved reference:"+vl,astNode));
        return true;
    }
    return false;
};

export class SchemaOrTypeValidator implements PropertyValidator {
    validate(node:hl.IAttribute, cb:hl.ValidationAcceptor) {
        var vl=node.value();
        if (!vl){
            vl="";
        }
        try {
            typeExpression.validate(vl, node, cb);
        } catch (e){
            cb.accept(createIssue(hl.IssueCode.UNRESOLVED_REFERENCE,"Syntax error:"+e.message,node));
        }
    }
}
export class DescriminatorOrReferenceValidator implements PropertyValidator{
    validate(node:hl.IAttribute,cb:hl.ValidationAcceptor){
        var vl=node.value();
        var valueKey=vl;
        var pr=node.property();
        if (typeof vl=='string'){
            checkReference(pr, node, vl,cb);
            if (pr.range() instanceof defs.ReferenceType){
                var t=<defs.ReferenceType>pr.range();
                if (true){
                    var mockNode=jsyaml.createNode(""+vl);
                    mockNode._actualNode().startPosition=node.lowLevel().valueStart();
                    mockNode._actualNode().endPosition=node.lowLevel().valueEnd();
                    var stv=new hlimpl.StructuredValue(mockNode,node.parent(),node.property())
                    var hn = stv.toHighlevel()
                    if (hn) {
                        hn.validate(cb);
                    }
                }
            }
        }
        else{
            var st=<hlimpl.StructuredValue>vl;
            if (st) {
                valueKey=st.valueName();
                var vn = st.valueName();
                if (!checkReference(pr, node, vn,cb)) {
                    var hnode = st.toHighlevel()
                    if(hnode) hnode.validate(cb);
                }
            }
            else{
                valueKey=null;
            }
        }
        if (valueKey) {
            var validation = pr.range().isValid(node.parent(), valueKey, pr);
            if (validation instanceof Error) {
                cb.accept(createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, (<Error>validation).message, node));
                validation = null;
            }
        }
    }
}
/**
 * validates examples
 */
export class ExampleValidator implements PropertyValidator{

    validate(node:hl.IAttribute,cb:hl.ValidationAcceptor){
        //check if we expect to do strict validation

        var strictValidation=this.isStrict(node);
        if (!strictValidation){
            if (!settings.validateNotStrictExamples){
                return;
            }
        }

        var pObj=this.parseObject(node,cb,strictValidation);
        if (!pObj){
            return;
        }
        var schema=this.aquireSchema(node);
        if (schema){
            schema.validate(pObj,cb,strictValidation);
        }
    }

    aquireSchema(node:hl.IAttribute):IShema{
        var sp=node.parent().definition().isAssignableFrom("DataElement");
        if (node.name()=="example"){
            if (node.parent().property().name()=="types") {
                sp = false;
            }
            if (node.parent().parent()) {
                if (node.parent().parent().definition().name() == "Method") {
                    if (node.parent().property().name()=="queryParameters"){

                    }
                    else {
                        sp = true;
                    }
                }
                if (node.parent().parent().definition().name() == "Response") {
                    sp = true;
                }
            }
        }
        if (node.parent().definition().name()=="BodyLike"||sp){
            var sa=node.parent().attr("schema");
            if (!sa){
                sa=node.parent().attr("type");
            }
            if (sa){
                var val=sa.value();
                if (val instanceof hlimpl.StructuredValue){
                    return null;
                }
                var strVal=(""+val).trim();
                var so:su.Schema=null
                if (strVal.charAt(0)=="{"){
                    try {
                        so = su.getJSONSchema(strVal);
                    } catch (e){
                        return null;
                    }
                }
                if (strVal.charAt(0)=="<"){
                    try {
                         so = su.getXMLSchema(strVal);
                    } catch (e){
                        return null;
                    }
                }
                if(so){
                    return {
                        validate(pObje:any,cb:hl.ValidationAcceptor,strict:boolean){
                            try {
                                so.validateObject(pObje);
                            }catch(e){
                                cb.accept(hlimpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,"Example does not conforms to schema:"+e.message,node,!strict));
                                return;
                            }
                            //validate using classical schema;
                        }
                    }
                }
                else{
                    //lets try to get schema from type
                    if (strVal.length>0){
                        var tp=typeExpression.getType(node.parent(),strVal,{});
                        if (tp){
                            return {
                                validate(pObje:any,cb:hl.ValidationAcceptor,strict:boolean){
                                    new TypeValidator(node).validate(pObje,tp,cb,strict);
                                    //validate using typeExpression;
                                }
                            }
                        }
                    }
                }
            }
        }
        return this.getSchemaFromModel(node);
    }
    getSchemaFromModel(node:hl.IAttribute):IShema{
        var p=node.parent();
        if (node.property().name()=="content"){
            p=p.parent();
        }
        var tp=hlimpl.typeFromNode(p);
        if (tp){
            return {
                validate(pObje:any,cb:hl.ValidationAcceptor,strict:boolean){
                    new TypeValidator(node).validate(pObje,tp,cb,strict);
                    //validate using typeExpression;
                }
            }
        }
        return null;
    }

    toObject(h:hl.IAttribute,v:hlimpl.StructuredValue,cb:hl.ValidationAcceptor){
        var res= v.lowLevel().dumpToObject();
        this.testDublication(h,v.lowLevel(),cb);
        if (res["example"]){
            return res["example"];
        }
        if (res["content"]){
            return res["content"];
        }
    }
    testDublication(h:hl.IAttribute, v:ll.ILowLevelASTNode,cb:hl.ValidationAcceptor){
        var map={}
        v.children().forEach(x=>{
            if (x.key()) {
                if (map[x.key()]) {
                    cb.accept(createIssue(hl.IssueCode.KEY_SHOULD_BE_UNIQUE_INTHISCONTEXT, "Keys should be unique", new hlimpl.BasicASTNode(x, h.parent())))
                }
                map[x.key()] = x;
            }
            this.testDublication(h,x,cb)
        })
    }
    private parseObject(node:hl.IAttribute,cb:hl.ValidationAcceptor,strictValidation:boolean):any{
        var pObj:any=null;
        var vl=node.value();
        var mediaType=getMediaType(node);
        if (vl instanceof hlimpl.StructuredValue){
            //validate in context of type/schema
            pObj=this.toObject(node,<hlimpl.StructuredValue>vl,cb);
        }
        else{
            if (mediaType){
                if (isJson(mediaType)){
                    try{
                        pObj=JSON.parse(vl);
                    }catch (e){
                        cb.accept(hlimpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,"Can not parse JSON:"+e.message,node,!strictValidation));
                        return;
                    }
                }
                if (isXML(mediaType)){
                    try {
                        pObj = xmlutil(vl);
                    }
                    catch (e){
                        cb.accept(hlimpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,"Can not parse XML:"+e.message,node,!strictValidation));
                        return;
                    }
                }
            }
            else{
                try{
                    pObj=JSON.parse(vl);
                }catch (e){

                    if (vl.trim().indexOf("<")==0){
                        try {
                            pObj = xmlutil(vl);
                        }
                        catch (e){
                            cb.accept(hlimpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,"Can not parse XML:"+e.message,node,!strictValidation));
                            return;
                        }
                    }
                    else {
                        //cb.accept(hlimpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, "Can not parse JSON:" + e.message, node, !strictValidation));
                        return vl;
                    }
                }
            }
        }
        return pObj;
    }

    private isStrict(node) {
        var strictValidation:boolean = false;
        var strict = node.parent().attr("strict")
        if (strict) {
            if (strict.value() == 'true') {
                strictValidation = true;
            }
        }
        return strictValidation;
    }
}
function isJson(s:string){
    return s.indexOf("json")!=-1;
}
function isXML(s:string){
    return s.indexOf("xml")!=-1;
}
function getMediaType(node:hl.IAttribute){
    if (node.parent().definition().name()=="BodyLike"){
        return node.parent().name();
    }
    return null;
}
var localError = function (node:hl.IParseResult, c, w, message,p:boolean,prop:hl.IProperty) {

    var st = node.lowLevel().start();
    var et = node.lowLevel().end();
    if (node.lowLevel().key() && node.lowLevel().keyStart()) {
        var ks = node.lowLevel().keyStart();
        if (ks > 0) {
            st = ks;
        }
        var ke = node.lowLevel().keyEnd();
        if (ke > 0) {
            et = ke;
        }
    }
    if (et < st) {
        et = st + 1;//FIXME
    }
    if (prop&&!prop.isMerged()&&node.parent()==null){
        var nm= _.find(node.lowLevel().children(),x=>x.key()==prop.name());
        if (nm){
            var ks=nm.keyStart();
            var ke=nm.keyEnd();
            if (ks>0&&ke>ks){
                st=ks;
                et=ke;
            }

        }
    }

    return {
        code: c,
        isWarning: w,
        message: message,
        node: node,
        start: st,
        end: et,
        path: p?(node.lowLevel().unit() ? node.lowLevel().unit().path() : ""):null,
        extras:[],
        unit:node?node.lowLevel().unit():null
    }
};
export function createIssue(c:hl.IssueCode, message:string,node:hl.IParseResult,w:boolean=false):hl.ValidationIssue{
    //console.log(node.name()+node.lowLevel().start()+":"+node.id());
    var original=null;
    var pr:hl.IProperty=null;
    if (node) {
        pr=node.property();

        if (node.lowLevel().unit() != node.root().lowLevel().unit()) {
            original=localError(node,c,w,message,true,pr);
            var v=node.lowLevel().unit();
            if (v) {
                message = message + " " + v.path();
            }
            while(node.lowLevel().unit()!=node.root().lowLevel().unit()){
                pr=node.property();
                node=node.parent();
            }
        }
    }
    if (original){
        if (node.property()&&node.property().name()=="uses"&&node.parent()!=null){
            pr=node.property();//FIXME there should be other cases
        }
    }
    var error=localError(node, c, w, message,false,pr);
    if (original) {
        error.extras.push(original);
    }
    //console.log(error.start+":"+error.end)
    return error;
}
