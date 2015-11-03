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
import high=require("../highLevelAST");
import ramlSignature=require("../ramlSignature")
import hlimpl=require("../highLevelImpl")
import search=require("./search")
type ASTNodeImpl=hlimpl.ASTNodeImpl;
type ASTPropImpl=hlimpl.ASTPropImpl;

interface TemplateData{
    [name:string]:hl.ITypeDefinition[]
}
function templateFields(node:hl.IParseResult,d:TemplateData){
    var u=<defs.Universe>node.root().definition().universe();
    node.children().forEach(x=>templateFields(x,d));
    if (node instanceof hlimpl.ASTPropImpl){
        var prop=<ASTPropImpl>node;
        //TODO RECURSIVE PARAMETERS
        var v=prop.value();
        if (typeof v=='string'){
            var strV=<string>v;
            handleValue(strV, d, prop,false,u);
        }
        else{
            node.lowLevel().visit(x=>{
                if (x.value()){
                    var strV=x.value()+"";
                    handleValue(strV,d,prop,true,u);

                }
                return true;
            })
        }
    }
    else if (node instanceof hlimpl.BasicASTNode){
        var v=node.lowLevel().value();
        if (typeof v=='string'){
            var strV=<string>v;
            handleValue(strV, d, null,false,u);
        }
        else{
            node.lowLevel().visit(x=>{
                if (x.value()){
                    var strV=x.value()+"";
                    handleValue(strV,d,null,true,u);

                }
                return true;
            })
        }
    }
}
var handleValue = function (strV:string, d:TemplateData, prop:ASTPropImpl,allwaysString:boolean,u:defs.Universe) {
    var ps = 0;
    while (true) {
        var pos = strV.indexOf("<<", ps);
        if (pos != -1) {
            var end = strV.indexOf(">>", pos);
            var isFull = pos == 0 && end == strV.length - 2;
            var parameterUsage = strV.substring(pos + 2, end);
            ps = pos + 2;
            var directiveIndex = parameterUsage.indexOf("|");
            if (directiveIndex != -1) {
                parameterUsage = parameterUsage.substring(0, directiveIndex);
            }
            parameterUsage = parameterUsage.trim();
            if (parameterUsage=="resourcePathName"||parameterUsage=="methodName"||parameterUsage=="resourcePath"){
                //Handling reserved parameter names;
                continue;
            }

            var q = d[parameterUsage];
            var r = (prop)?prop.property().range():null;
            if (prop){
            if (prop.property().name()=="type"||prop.property().name()=="schema"){
                if (prop.property().domain().name()=="DataElement"){
                    r = u.getType("SchemaString");
                }
            }
            }
            if (!isFull||allwaysString) {
                r = u.getType("StringType");
            }

            //FIX ME NOT WHOLE TEMPLATES
            if (q) {
                q.push(r);
            }
            else {
                d[parameterUsage] = [r]
            }
        }
        else break;
    }
};


export function typeFromNode(node:hl.IHighLevelNode):hl.ITypeDefinition{
    if (!node){
        return null;
    }
    if (node.associatedType()){
        return <hl.ITypeDefinition>node.associatedType()
    }

    var result=new defs.UserDefinedClass(node.name(),<defs.Universe>node.definition().universe(),node,node.lowLevel().unit().path(),"");
    (<ASTNodeImpl>node).setAssociatedType(result);
    //result.setDeclaringNode(node);
    var def=<defs.NodeClass>node.definition();
    if (def.isInlinedTemplates()){
        var usages:TemplateData={}
        templateFields(node,usages);
        Object.keys(usages).forEach(x=>{
            var prop=new defs.UserDefinedProp(x);
            prop._node=node;
            prop.withDomain(result);
            var tp=_.unique(usages[x]).filter(x=>x&&x.name()!="StringType");
            prop.withRange(tp.length==1?tp[0]:<any>node.definition().universe().getType("StringType"));
            prop.withRequired(true)
            prop.unmerge();
        })
    }
    else if (def.getReferenceIs()){
        if (def.universe().version()=="RAML08") {
            result.withAllowAny();
        }
        var p=def.property(def.getReferenceIs());
        if (p){
            p.range().properties().forEach(x=>{
                var prop=new defs.Property(x.name());
                prop.unmerge();
                prop.withDomain(result);
                prop.withRange(x.range());
                prop.withMultiValue(x.isMultiValue());
            });
        }
    }
    else {
        var rp = def.findMembersDeterminer();
        if (rp) {
            var elements = node.elementsOfKind(rp.name());
            elements.forEach(x=> {
                var prop = elementToProp(x);
                prop.withDomain(result)
            })
        }

        //here we should found correct inheritance chain
        var types=node.attributes("type");
        var schema=node.attributes("schema");
        types=types.concat(schema);
        if (node.definition().name()=="GlobalSchema"){
            var vl=node.attributes("value");
            types=types.concat(vl);
        }
        var tp=types.length!=0;
        types.forEach(tp=>{
            var vl=tp.value();
            if (typeof vl=='string'&&vl){
                vl=vl.trim();
                if (vl.charAt(0)=='{'){
                    var et=new defs.ExternalType(node.name(),<defs.Universe>node.definition().universe(),node.lowLevel().unit().path(),"");
                    et.schemaString=vl;
                    et.node=node;
                    var de=<hl.INodeDefinition>node.definition().universe().getType("ObjectField");
                    if (de){
                        result._superTypes.push(de);
                    }
                    result._superTypes.push(et);
                }
                if (vl.charAt(0)=='<'){
                    var et=new defs.ExternalType(node.name(),<defs.Universe>node.definition().universe(),node.lowLevel().unit().path(),"");
                    et.schemaString=vl;
                    et.node=node;
                    var de=<hl.INodeDefinition>node.definition().universe().getType("ObjectField");
                    if (de){
                        result._superTypes.push(de);
                    }
                    result._superTypes.push(et);
                }
            }
            var types:{ [name:string]:hl.ITypeDefinition}={};
            types[result.name()]=result;
            var at:hl.ITypeDefinition=null;
            if (vl!=result.name()) {
                at = typeExpression.getType(node.parent(), vl, types);
            }
            else{
               //at=result;
            }
            if (at){
                result._superTypes.push(at);
            }

        }
        )

        result.addRequirement("type",node.name());
        if (def.getExtendedType()) {
            result._superTypes.push(def.getExtendedType());
        }
        {
            //Adding runtime properties for object types TODO it should be done in more elegant way
            var prop=_.find(node.lowLevel().children(),x=>x.key()=="properties");
            if (prop) {
                var de=<hl.INodeDefinition>node.definition().universe().getType("ObjectField");
                if (de){
                    result._superTypes.push(de);
                }

            }
            else{
                if (node.property()&&node.property().name()=="body"){
                    var de=<hl.INodeDefinition>node.definition().universe().getType("ObjectField");
                    if (de){
                        result._superTypes.push(de);
                    }
                }
                else if (!tp){
                    var de = <hl.INodeDefinition>node.definition().universe().getType("StrElement");
                    if (de&&node.definition().name()!="BodyLike"&&node.definition().name()!="AnnotationType") {
                        result._superTypes.push(de);
                    }
                }
                if (result._superTypes.length==0) {
                    var de = <hl.INodeDefinition>node.definition().universe().getType("DataElement");
                    if (de) {
                        result._superTypes.push(de);
                    }
                }
            }
            var pn=node.definition().universe().getType("ObjectField");
            if (pn) {
                node.lowLevel().children().forEach(x=> {
                    if (x.key()=="facets"){
                        return;
                    }
                    if (x.key()=="annotations"){
                        return;
                    }
                    if (x.key()=="parameters"){
                        return;
                    }
                    if (!(<defs.NodeClass>pn).property(x.key())) {
                        result.fixFacet(x.key(),x);
                    }
                })
            }
        }
    }
    return result;
}


interface MapPropertyInfo{

    regexp:string
    name:string;
}
export function parsePropertyName(name:string):MapPropertyInfo{
    var v={name:"",regexp:null};
    if (name.length>2) {
        name = name.substr(1, name.length - 2);
        var pos=name.lastIndexOf("#");
        if (pos!=-1) {
            v.name = name.substr(pos+1);
            v.regexp=name.substr(0,pos)
        }
        else{
            v.regexp=name;
        }
    }
    return v;
}
export function libraryLocation(definition:defs.NodeClass){
    var node=definition.getDeclaringNode();
    var result=null;
    if (node!=null) {
        var library = node.parent();
        if (library) {
            var libraryAnnotations = library.attributes("annotations");
            libraryAnnotations.forEach(x=> {
                var value = x.value();
                if (value instanceof hlimpl.StructuredValue) {
                    if (value.lowLevel().key() == "LibraryLocation") {
                        var hlv = value.toHighlevel(library);
                        if (hlv) {
                            result= valueOf(hlv)

                        }
                    }
                }
            })
            //console.log(node.parent())
        }
    }
    return result;
}
export function valueOf(hl:hl.IHighLevelNode){
    if (hl) {
        var vl = hl.attr("value");
        if (vl) {
            return vl.value();
        }
    }
    return null;
}

var scriptToValidator={};
var loophole= require("loophole")
function evalInSandbox(code:string,thisArg:any,args:any[]) {
    return new loophole.Function(code).call(thisArg,args);
}

class ValidatorHolder{

    _result:defs.FacetValidator;

    register(mm:defs.FacetValidator){
        this._result=mm;
    }
}
export function aquireValidator(value:string){
    if (value) {
        var nm = scriptToValidator[value]
        if (nm){
            if (nm==aquireValidator){
                return null;
            }
            return nm;
        }
        try {
            var holder = new ValidatorHolder();
            evalInSandbox(value, holder,[]);
            if (holder._result){
                scriptToValidator[value]=holder._result;
                return holder._result;
            }
            else{
                scriptToValidator[value]=aquireValidator
            }
        }catch (e){
            scriptToValidator[value]=aquireValidator
            // just ignore
        }
        //okey lets prepare validator;
    }
    return null;
}
export function elementToProp(e:hl.IHighLevelNode,toRuntime:boolean=false):defs.Property{
    var nm=e.name();
    var optional=false;
    if (nm.length>0&&nm.charAt(nm.length-1)=='?'){
        nm=nm.substr(0,nm.length-1);
        optional=true;
    }
    var result=new defs.UserDefinedProp(nm);
    result._node=e;
    var annotations=e.attributes("annotations");
    annotations.forEach(annotation=>{
        var value=annotation.value();
        if (value instanceof hlimpl.StructuredValue){
          var highLevel=value.toHighlevel(e);
          if (highLevel) {
              var definition = highLevel.definition();
              if (definition.name()=="FacetInstanceValidator"){
                  var node=definition.getDeclaringNode();
                  var ll=libraryLocation(definition);
                  if (ll=="http://raml.org/library/common.raml"){
                    var value=valueOf(highLevel)
                    var facetValidator=aquireValidator(value);
                    if (facetValidator){
                        result.setFacetValidator(facetValidator)
                    }
                }
              }
            }
        }
    });
    if (nm.length>0){
        if (nm[0]=='['){
            optional=true;
            var info=parsePropertyName(nm);
            if (info.regexp){
                result.withKeyRegexp(info.regexp);
            }
            else{
                result.withKeyRestriction("");
            }
            if (info.name){
                result.withDescription(info.name);
            }
        }
        else{
            result.unmerge();
        }
    }
    var props=e.definition().properties();
    var tp=e.attr("type");
    if (tp){
        var typeName=tp.value();
        if (typeName=="any"){
            result.withMultiValue(true);
        }
        var tpv=typeExpression.getType(e,typeName,{},true);
        if (tpv) {

            tpv = tpv.toRuntime();
            if (tpv instanceof defs.Array){
                var at=<defs.Array>tpv;
                //FIXME
                (<any>at)._af={};
                //var fs=tpv.getFixedFacets();
                //for (var i in fs){
                //    at._af[i]=fs[i];
                //}
                at.findFacets(e,(<any>at)._af);

            }
        }
        result.withRange(<defs.IType>tpv);

        //FIXME
        if (typeName=="pointer"){
            var scope=e.attr("target");
            if (scope){
                try {
                    var sm=selector.parse(e,""+scope.value());
                    result.setSelector(sm);
                }catch (e){
                    //ignoring syntax error here
                }
            }
        }
    }
    //FIXME Literals
    if (nm=="value"&&e.parent()&&e.parent().definition().isAssignableFrom("AnnotationType")){
        result.withCanBeValue()
    }

    e.definition().allProperties().forEach(p=>{
        if (p.name()!="type"){
            if ((<defs.Property>p).describesAnnotation()){
                var annotationName= (<defs.Property>p).describedAnnotation();
                var args:(string|string[])[]=[]
                var vl=e.attributes(p.name()).map(a=>a.value());
                if (vl.length==1) {
                    args.push(vl[0]);
                }
                else{
                    args.push(vl)
                }
                //TODO ANNOTATIONS WITH MULTIPLE ARGUMENTS
                var an:tsStruct.Annotation={
                    name:annotationName,
                    arguments:args
                }
                ts2Def.recordAnnotation(result,an);
            }
        }
    })
    if (optional){
        result.withRequired(false);
    }
    var vn=_.find(e.lowLevel().children(),x=>x.key()=="properties");
    if (vn){
        var of=e.definition().universe().getType("ObjectField");
        var node=new hlimpl.ASTNodeImpl(e.lowLevel(),e.parent(),<hl.INodeDefinition>of,result)
        var nc=new defs.UserDefinedClass("",<defs.Universe>of.universe(),node,e.lowLevel().unit().path(),"");
        nc._superTypes.push(of);
        result.withRange(nc.toRuntime());
    }
    if (result.range()==null){
        result.withRange(new defs.ValueType("String",<defs.Universe>e.definition().universe(),""))
    }
    if (result.range().name()=="ObjectField"&&result.range() instanceof defs.NodeClass){
        var rm=new defs.NodeClass("ObjectField",result.range().universe(),"","");
        rm.withAllowAny();
        result.withRange(rm);
    }
    return result;
}
