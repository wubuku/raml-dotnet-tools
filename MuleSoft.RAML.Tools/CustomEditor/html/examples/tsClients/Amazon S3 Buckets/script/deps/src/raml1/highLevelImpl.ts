/// <reference path="../../typings/tsd.d.ts" />

import jsyaml=require("./jsyaml/jsyaml2lowLevel")
import defs=require("./definitionSystem")
import hl=require("./highLevelAST")
import ll=require("./lowLevelAST")
import tsStruct=require("./tsStructureParser")
import ts2Def=require("./tsStrut2Def")
import _=require("underscore")
import yaml=require("./jsyaml/yamlAST")
import selector=require("./selectorMatch")
import typeExpression=require("./typeExpressions")
import def=require( "./definitionSystem");
import high=require("./highLevelAST");
import ramlSignature=require("./ramlSignature")
import builder=require("./ast.core/builder")
import linter=require("./ast.core/linter")
import typeBuilder=require("./ast.core/typeBuilder")
import search=require("./ast.core/search")
import textutil=require('../util/textutil')
import ParserCore = require("./parserCore")
import ModelFactory = require("./modelFactory")

type NodeClass=def.NodeClass;
type IAttribute=high.IAttribute


export function qName(x:hl.IHighLevelNode,context:hl.IHighLevelNode):string{
    var dr=search.declRoot(context);
    var nm=x.name();
    var o=nm;
    var ind=nm.indexOf("<");//TODO SCOPE IT
    if (ind!=-1){
        nm=nm.substring(0,ind);
    }

    while (true){

        var np=x.parent();
        if (!np||np==dr){
            break;
        }
        else{
            if (np.definition().name()=="Library"&&np.parent()){
                nm=np.name()+"."+nm;
            }
            x=np;
        }
    }
    return nm;
}
function insideResourceTypeOrTrait(h:hl.IHighLevelNode){
    var declRoot=h;
    while (true){
        if (declRoot.definition().isInlinedTemplates()){
            return true;
        }
        var np=declRoot.parent();
        if (!np){
            break;
        }
        else{
            declRoot=np;
        }

    }
    return false;
}

var loophole= require("loophole")
export function evalInSandbox(code:string,thisArg:any,args:any[]) {
    return new loophole.Function(code).call(thisArg,args);
}
export class BasicASTNode implements hl.IParseResult {
    private _hashkey : string;
    
    hashkey() {
        if (!this._hashkey) this._hashkey = this.parent() ? this.parent().hashkey() + "/" + this.name() : this.name(); 
        return this._hashkey;
    }
    
    root():hl.IHighLevelNode{
        if (this.parent()){
            return this.parent().root();
        }
        return <any>this;
    }
    private _implicit:boolean=false;
    private values:{[name:string]:any}={}
    _computed:boolean;
    constructor(protected _node:ll.ILowLevelASTNode,private _parent:hl.IHighLevelNode){
        if(_node) {
            _node.setHighLevelParseResult(this);
        }
    }
    knownProperty:hl.IProperty
    needSequence:boolean
    unresolvedRef:string
    checkContextValue(name:string,value:string,thisObj:any):boolean{
        var vl=this.computedValue(name);
        if (vl&&vl.indexOf(value)!=-1){
            return true;//FIXME
        }
        if (!vl){
            try {
                var res = evalInSandbox("return " + name, thisObj, []);
                if (res != undefined) {
                    return "" + res == value;
                }
            }catch (e){
                //ignoring failures here
            }
        }
        return value==vl||value=='false';
    }

    printDetails(indent?:string) : string {
        return (indent? indent : "")+"Unkown\n";
    }

    toRuntimeModel():any{
        var thisObj={};
        //FIXME it should be be done in much cooler way
        //Spec for runtime is also needed
        this.children().forEach(x=>{
            if (x instanceof ASTPropImpl){
                var pr=x;
                var val=pr.value();
                if (val) {
                    var type = pr.property().range();
                    val=this.fillValue(type, val);
                    thisObj[x.name()] = val;
                }
            }

        });
        return thisObj;
    }

    protected fillValue(type:hl.ITypeDefinition, val:any):any {
        type.methods().forEach(m=> {
            if (typeof val == 'string') {
                var newVal:any = {};
                newVal['value'] = new loophole.Function("return this._value");
                newVal._value = val;
                val = newVal;
            }
            var nm = m.name;
            var body = m.text;
            var actualText = body.substring(body.indexOf('{') + 1, body.lastIndexOf('}'))
            var func = new loophole.Function(actualText);
            val[nm] = func;
        })
        val['$$']=this;
        return val;
    }

    validate(v:hl.ValidationAcceptor):void{

        if (this.lowLevel()&&this._parent==null) {
            this.lowLevel().errors().forEach(x=> {
                var em={
                    code:hl.IssueCode.YAML_ERROR,
                    message:x.message,
                    node:null,
                    start:x.mark.position,
                    end:x.mark.position+1,
                    isWarning:false,
                    path:this.lowLevel().unit()==this.root().lowLevel().unit()?null:this.lowLevel().unit().path(),
                    unit:this.lowLevel().unit()
                }
                v.accept(em)
              });
        }

        this.validateIncludes(v);
        if (this.isUnknown()){
            if (this.needSequence){ 
                v.accept(createIssue(hl.IssueCode.UNKNOWN_NODE, "node: " + this.name()+" should be wrapped in sequence", this));
            }
            if (this.unresolvedRef){
                v.accept(createIssue(hl.IssueCode.UNKNOWN_NODE, "reference : " + this.lowLevel().value()+" can not be resolved", this));

            }
            if (this.knownProperty&&this.lowLevel().value()){
                v.accept(createIssue(hl.IssueCode.UNKNOWN_NODE, "property "+this.name()+" can not have scalar value", this));
            }
            else {
                v.accept(createIssue(hl.IssueCode.UNKNOWN_NODE, "Unknown node:" + this.name(), this));
            }
        }
        this.directChildren().forEach(x=>x.validate(v));
    }

    protected validateIncludes(v) {
        if (this.lowLevel()) {
            this.lowLevel().includeErrors().forEach(x=> {
                var em = createIssue(hl.IssueCode.UNABLE_TO_RESOLVE_INCLUDE_FILE, x, this);
                v.accept(em)
            });
        }
    }
    setComputed(name:string,v:any){
        this.values[name]=v;
    }

    computedValue(name:string):any{
        var vl=this.values[name];
        if (!vl&&this.parent()){
            return this.parent().computedValue(name)
        }
        return vl;
    }

    lowLevel():ll.ILowLevelASTNode {
        return this._node;
    }

    expansionSpec():hl.ExpansionSpec {
        return null;
    }
    name(){
        var c=this.lowLevel().key();
        if (!c){
            return "";
        }
        return c;
    }

    parent():hl.IHighLevelNode {
        return this._parent;
    }
    
    setParent(parent: hl.IHighLevelNode) {
        this._parent = parent;
    }
    
    isElement(){
        return false;
    }
    directChildren():hl.IParseResult[] {

        return this.children();
    }

    children():hl.IParseResult[] {
        return [];
    }

    isAttached():boolean {
        return this.parent()!=null;
    }

    isImplicit():boolean {
        return this._implicit;
    }

    isAttr():boolean{
        return false;
    }
    isUnknown():boolean{
        return true;
    }
    id():string{
        if (this._parent){
            var parentId=this.parent().id();
            parentId+="."+this.name();
            var sameName=(<BasicASTNode><any>this.parent()).directChildren().filter(x=>x.name()==this.name());
            if (sameName.length>1){
                var ind=sameName.indexOf(this);
                parentId+="["+ind+"]"
            }
            return parentId;
        }
        return "";
    }

    localId():string{
        return this.name();
    }
    property():hl.IProperty{
        return null;
    }
}

export function createIssue(c:hl.IssueCode, message:string,node:hl.IParseResult,w:boolean=false):hl.ValidationIssue{
    return linter.createIssue(c,message,node,w);
}

export class StructuredValue{

    constructor(private node:ll.ILowLevelASTNode,private _parent:hl.IHighLevelNode,private _pr:hl.IProperty,private kv=null){

    }

    valueName(): string {
        if (this.kv){
            return this.kv;
        }
        return this.node.key();
    }

    children():StructuredValue[]{
        return this.node.children().map(x=>new StructuredValue(x,null,null));
    }

    lowLevel():ll.ILowLevelASTNode{
        return this.node;
    }

    toHighlevel(parent?: hl.IHighLevelNode):hl.IHighLevelNode{
        if (!parent && this._parent) parent = this._parent;
        var vn=this.valueName();
        var cands=this._pr.referenceTargets(parent).filter(x=>qName(x,parent)==vn);
        if (cands&&cands[0]){
            var tp=typeBuilder.typeFromNode(<hl.IHighLevelNode>cands[0])
            var node=new ASTNodeImpl(this.node,parent,<hl.INodeDefinition>tp,this._pr);
            if (this._pr){
                this._pr.childRestrictions().forEach(y=>{
                    node.setComputed(y.name,y.value)
                })
            }
            return node;
        }
        return null;
    }
}

export function genStructuredValue(type: string, name: string, mappings: { key: string; value: string; }[], parent: hl.IHighLevelNode) {
    var map = yaml.newMap(mappings.map(mapping => yaml.newMapping(yaml.newScalar(mapping.key), yaml.newScalar(mapping.value))));
    
    var node = new jsyaml.ASTNode(map, <jsyaml.CompilationUnit> (parent? parent.lowLevel().unit():null), parent? <jsyaml.ASTNode> parent.lowLevel() : null, null, null);

    return new StructuredValue(node, parent, parent? parent.definition().property(type):null, name);
}

function checkPropertyQuard  (n:BasicASTNode, v:hl.ValidationAcceptor) {
    var pr = n.property();
    if (pr) {
        (<defs.Property>pr).getContextRequirements().forEach(x=> {
            if (!n.checkContextValue(x.name, x.value,(<BasicASTNode><any>n.parent()).toRuntimeModel())) {
                v.accept(createIssue(hl.IssueCode.MISSED_CONTEXT_REQUIREMENT, x.name + " should be " + x.value + " to use property " + pr.name(), n))
            }
        });
    }
    return pr;
};



export class ASTPropImpl extends BasicASTNode implements  hl.IAttribute {


    definition():hl.IValueTypeDefinition {
        return this._def;
    }


    constructor(node:ll.ILowLevelASTNode, parent:hl.IHighLevelNode, private _def:hl.IValueTypeDefinition, private _prop:hl.IProperty, private fromKey:boolean = false) {
        super(node, parent)

    }

    owningWrapper():{node:ParserCore.BasicSuperNode; property:string}{
        return {
            node: this.parent().wrapperNode(),
            property: this.name()
        };
    }

    patchType(t:hl.IValueTypeDefinition){
        this._def=t;
    }


    findReferenceDeclaration():hl.IHighLevelNode{
        var targets=this.property().referenceTargets(this.parent());
        var t:hl.IHighLevelNode=_.find(targets,x=>qName(x,this.parent())==this.value())
        return t;
    }
    findReferencedValue(){
        var c=this.findReferenceDeclaration();
        if (c){
            var vl=c.attr("value");
            if (c.definition().name()=="GlobalSchema") {
                if (vl) {
                    var actualValue = vl.value();
                    if (actualValue) {
                        var rf = this._def.isValid(this.parent(),actualValue,vl.property());
                        return rf;
                    }
                }
                return null;
            }
        }
        return c;
    }

    /**
     * TODO Split this method into the cases depending from property kind
     * @param v
     */
    validate(v:hl.ValidationAcceptor):void {
        var pr = checkPropertyQuard(this, v);
        var vl=this.value();
        if (!this.property().range().hasStructure()){
            if (vl instanceof StructuredValue&&!this.property().isSelfNode()){
                //TODO THIS SHOULD BE MOVED TO TYPESYSTEM FOR STS AT SOME MOMENT
                if (this.property().name()=="schema"||this.property().name()=='type'){
                    if (this.property().domain().name()=="BodyLike"){
                        var structValue=<StructuredValue>vl;
                        var node=new ASTNodeImpl(this.lowLevel(),this.parent(),<hl.INodeDefinition>this.parent().definition().universe().getType("ObjectField"),this.property());
                        node.validate(v);
                        return;
                    }
                }
                v.accept(createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,"Scalar is expected here",this))
            }
        }
        if (this.parent().allowsQuestion()&&this.property().isKey()){
            if (vl != null &&vl.length>0&&vl.charAt(vl.length-1)=='?'){
                vl=vl.substr(0,vl.length-1);
            }
        }

        if (typeof vl=='string'&&vl.indexOf("<<")!=-1){
            if (vl.indexOf(">>")>vl.indexOf("<<")){
                if (insideResourceTypeOrTrait(this.parent())){
                    return;
                }
            }
        }
        this.validateIncludes(v);
        if (this.property().range().name() == "MimeType"||
            (this.property().name()=="name"&&this.parent().property().name()=="body")) {//FIXME
            new linter.MediaTypeValidator().validate(this,v);
            return;
        }
        if (this.property().isKey()){
            if (vl.indexOf(" ")!=-1){
                v.accept(createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,"Keys should not have spaces '" + this.value()+"'",this))
            }
        }
        if (this.name() == "signature"&&(this.property().domain().name()=="Resource"||this.property().domain().name()=="Method"||this.property().domain().name()=="MethodBase")) {//FIXME
            if (this.property().domain() instanceof defs.NodeClass) {
                new linter.SignatureValidator().validate(this, v);
                return;
            }
        }
        //TODO TEMP ADDITION
        if (this.property().name()=="example"||this.property().name()=="content"){
            var pn=this.parent().definition().name();
            new linter.ExampleValidator().validate(this, v);

        }

        if (this.property().name()=="name"){
            //TODO MOVE TO DEF SYSTEM
            if (this.parent().property()&&this.parent().property().name()=='uriParameters'){
                new linter.UrlParameterNameValidator().validate(this,v);
                return;

            }
            if (this.parent().property()&&this.parent().property().name()=='baseUriParameters'){
                new linter.UrlParameterNameValidator().validate(this,v);
                return;
            }
        }
        if (this.property().range().name()=="RelativeUri"){
            new linter.UriValidator().validate(this,v);
            return;
        }
        if (this.property().range().name()=="FullUriTemplate"){
            new linter.UriValidator().validate(this,v);
            return;
        }

        if ("pattern" == this.name() && "StringType" == this.definition().name()
            && this.parent().definition().isAssignableFrom("StrElement")) {

            try {
                new RegExp(this.value())
            } catch (Error) {
                v.accept(createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,"Illegal pattern " + this.value(),this))
            }
        }

        if ("name" == this.name() && "StringType" == this.definition().name()
                && (typeof this.value() == "string")
                && (<string>this.value()).indexOf("[") == 0
                && (<string>this.value()).lastIndexOf("]") == (<string>this.value()).length - 1) {

            if(this.parent() instanceof ASTNodeImpl &&
                "properties" == (<ASTNodeImpl>this.parent()).property().name()){

                if (this.parent().parent() instanceof ASTNodeImpl &&
                    "ObjectField" == (<ASTNodeImpl>this.parent().parent()).definition().name()) {

                    try {
                        var cleanedValue = (<string>this.value()).substr(1, (<string>this.value()).length - 2)
                        new RegExp(cleanedValue)
                    } catch (Error) {
                        v.accept(createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,"Illegal pattern " + cleanedValue,this))
                    }
                }
            }
        }
        if (this.property().range().name()=="SchemaString"&&this.property().range() instanceof def.ValueType&&this.property().range().universe().version()=="RAML10"){
            var pn=this.parent().definition().name();
            new linter.SchemaOrTypeValidator().validate(this,v);
            return;
        }
        if (this.property() instanceof def.Property) {
            if ((<def.Property>pr).isTypeExpr()) {
                new linter.SchemaOrTypeValidator().validate(this, v);
                return;
            }
        }
        if (pr.isReference()||pr.isDescriminator()){
            new linter.DescriminatorOrReferenceValidator().validate(this,v);
        }
        else{
            new linter.NormalValidator().validate(this,v);
        }
    }



    toRuntime():any{
        var vl=this.value();
        var valueObj = this.fillValue(this.property().range(), vl);
        if (valueObj['parse']){
            try {
                return valueObj.parse();
            }catch (e){
               return e;
            }
        }
        return valueObj;
    }

    isElement() {
        return false;
    }


    property():hl.IProperty {
        return this._prop;
    }

    convertMultivalueToString(value: string): string {
        //|\n  xxx\n  yyy\n  zzz
        var gap = 0;
        var pos = 2;
        while(value[pos] == ' ') {
            gap++;
            pos++;
        }
        //console.log('gap: ' + gap);
        var lines = textutil.splitOnLines(value);
        lines = lines.map(line=> {
            //console.log('line: ' + line);
            return line.substring(gap, line.length);
        });
        return lines.join('');
    }

    value():any {
        if (this._computed){
            return this.computedValue(this.property().name());
        }
        if (this.fromKey) {
            return this._node.key();
        }

        var actualValue = this._node.value(); //TODO FIXME
        if (this.property().isSelfNode()){
            if (!actualValue||actualValue instanceof jsyaml.ASTNode){
                actualValue=this._node;
                if (actualValue.children().length==0){
                    actualValue=null;
                }
            }
        }
        if (actualValue instanceof jsyaml.ASTNode) {
            return new StructuredValue(<ll.ILowLevelASTNode>actualValue,this.parent(),this._prop);
        }
        if(textutil.isMultiLineValue(actualValue)) {
            var res = this.convertMultivalueToString(actualValue);
            //console.log('converted: [' + textutil.replaceNewlines(res) + ']');
            return res;
        }
        return actualValue;
    }

    name() {
        return this._prop.name();
    }

    printDetails(indent?:string) : string {
        var className = this.definition().name()
        var definitionClassName = this.property().range().name()

        return (indent?indent:"") +
            (this.name() + " : " + className
            + "[" + definitionClassName + "]"
            + "  =  " + this.value()) +
            "\n";
    }

    isAttr():boolean {
        return true;
    }

    isUnknown():boolean {
        return false;
    }

    setValue(value: string|StructuredValue) {
        if (value == this.value()) return;
        var c = new ll.CompositeCommand();
        if(typeof value === 'string') {
            var val = <string>value;
            if (this._prop.isFromParentKey()) {
                c.commands.push(ll.setKey(this._node, val));
            } else {
                if ((!val || val.length == 0) && !this.isEmbedded()) {
                    c.commands.push(ll.removeNode(this._node.parent(), this._node));
                    (<ASTNodeImpl>this.parent()).clearChildrenCache();
                } else {
                    if(!val) val = '';
                    c.commands.push(ll.setAttr(this._node, val));
                }
            }
        } else {
            if (this._prop.isFromParentKey()) {
                throw "couldn't set structured value to a key: " + this._prop.name();
            }
            var sval = <StructuredValue>value;
            c.commands.push(ll.setAttrStructured(this._node, sval));
        }
        this._node.execute(c);
    }

    children():hl.IParseResult[] {
        return [];
    }


    addStringValue(value: string) {
        var sc = jsyaml.createScalar(value);
        var target = <jsyaml.ASTNode>this.lowLevel();
        //console.log('add to target: ' + target.kindName());
        if(target.isScalar()) {
            target = target.parent();
        } else if(target.isMapping()) {
            var ln = <jsyaml.ASTNode>this.lowLevel();
            //target = new jsyaml.ASTNode(target.asMapping().value, <jsyaml.CompilationUnit>ln.unit(), ln, null, null);
        }
        //var llparent = this.lowLevel().parent();
        //console.log('parent: ' + llparent.kind());
        //var attr = new ASTPropImpl(sc, this.parent(), this.property().range(), this.property());
        //this.parent().add(attr);
        var command=new ll.CompositeCommand();
        command.commands.push(ll.insertNode(target, sc, null, true));
        this.lowLevel().execute(command);
        (<ASTNodeImpl>this.parent()).clearChildrenCache();
    }
    
    addStructuredValue(sv: StructuredValue) {
        //var sc = jsyaml.createScalar(value);
        var target = <jsyaml.ASTNode>this.lowLevel();
        //console.log('add to target: ' + target.kindName());
        if(target.isScalar()) {
            target = target.parent();
        } else if(target.isMapping()) {
            var ln = <jsyaml.ASTNode>this.lowLevel();
            //target = new jsyaml.ASTNode(target.asMapping().value, <jsyaml.CompilationUnit>ln.unit(), ln, null, null);
        }
        //var llparent = this.lowLevel().parent();
        //console.log('parent: ' + llparent.kind());
        //var attr = new ASTPropImpl(sc, this.parent(), this.property().range(), this.property());
        //this.parent().add(attr);
        var command=new ll.CompositeCommand();
        command.commands.push(ll.insertNode(target, sv.lowLevel(), null, true));
        this.lowLevel().execute(command);
        (<ASTNodeImpl>this.parent()).clearChildrenCache();
    }


    addValue(value: string|StructuredValue) {
        if(!this.property().isMultiValue()) throw "setValue(string) only apply to multi-values properties";
        if(typeof value == 'string') {
            this.addStringValue(<string>value);
        } else {
            this.addStructuredValue(<StructuredValue>value);
        }
    }

    isEmbedded(): boolean {
        var keyname = (<jsyaml.ASTNode>this.lowLevel()).asMapping().key.value;
        //console.log('propery: ' + this.property().name());
        //console.log('mapping: ' + keyname);
        return this.property().canBeValue() && keyname != this.property().name();
    }

    remove() {
        //if(!this.property().isMultiValue()) throw "setValue(string) only apply to multi-values properties";
        //var sc = jsyaml.createScalar(value);
        var llparent = this.lowLevel().parent();
        //llparent.show('parent:');
        //this.lowLevel().show('attribute:');
        //console.log('parent: ' + llparent.kind());
        //var attr = new ASTPropImpl(sc, this.parent(), this.property().range(), this.property());
        //this.parent().add(attr);
        if(!this.property().isMultiValue() && this.isEmbedded()) {
            // it's embedded value, need to clean scalar instead
            //console.log('embedded!');
            this.setValue('');
        } else {
            var command = new ll.CompositeCommand();

            command.commands.push(ll.removeNode(llparent, this.lowLevel()));
            this.lowLevel().execute(command);
            (<ASTNodeImpl>this.parent()).clearChildrenCache();
        }
    }

    setValues(values: string[]) {
        if(!this.property().isMultiValue()) throw "setValue(string[]) only apply to multi-values properties";
        var node = this.parent();
        if(this.isEmpty()) {
            // nothing to remove so...
        } else {
            var llnode = <jsyaml.ASTNode>node.lowLevel();
            var attrs = node.attributes(this.name());
            attrs.forEach(attr => attr.remove());
        }
        values.forEach(val => node.attrOrCreate(this.name()).addValue(val));
        /*
         if(attrs.length == 1) {
         var anode = <jsyaml.ASTNode>attrs[0].lowLevel();
         //console.log('attribute : ' + anode.kindName());
         //anode.show("ATTR:");
         if(anode.isMapping()) {
         // that's crazy but it means zero length array indeed )
         // nothing to remove so...
         } else {
         attrs.forEach(attr => attr.remove());
         }
         } else {
         attrs.forEach(attr => attr.remove());
         }
         */
    }

    isEmpty(): boolean {
        if(!this.property().isMultiValue()) throw "isEmpty() only apply to multi-values attributes";
        //console.log('remove: ' + this.name());
        var node = this.parent();
        var llnode = <jsyaml.ASTNode>node.lowLevel();
        //node.lowLevel().show('Parent:');
        var attrs = node.attributes(this.name());
        //console.log('attributes: ' + attrs.length);
        if(attrs.length == 0) {
            return true;
        } else if(attrs.length == 1) {
            var anode = <jsyaml.ASTNode>attrs[0].lowLevel();
            //console.log('attribute : ' + anode.kindName());
            //anode.show("ATTR:");
            if(anode.isMapping()) {
                // that's crazy but it means zero length array indeed )
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }


}



var nodeBuilder=new builder.BasicNodeBuilder()
interface NameToInt{
    [name:string]:number
}


export class ASTNodeImpl extends BasicASTNode implements  hl.IHighLevelNode{

    constructor(node:ll.ILowLevelASTNode, parent:hl.IHighLevelNode,private _def:hl.INodeDefinition,private _prop:hl.IProperty){
        super(node,parent)
        if(node) {
            node.setHighLevelNode(this);
        }
    }

    private _expanded=false;
    _children:hl.IParseResult[];
    _allowQuestion:boolean=false;
    _associatedDef:hl.INodeDefinition;

    _subTypesCache:{ [name:string]:hl.ITypeDefinition[]}=null;
    private _wrapperNode:ParserCore.BasicSuperNode;

    wrapperNode():ParserCore.BasicSuperNode{
        if(!this._wrapperNode){
            this._wrapperNode = ModelFactory.buildWrapperNode(this);
        }
        return this._wrapperNode;
    }

    setWrapperNode(node:ParserCore.BasicSuperNode){
        this._wrapperNode = node;
    }

    setAssociatedType(d:hl.INodeDefinition){
        this._associatedDef=d;
    }

    associatedType():hl.INodeDefinition{
        return this._associatedDef;
    }
    private _isAux
    private _auxChecked=false;
    private _knownIds;


    findById(id:string){

        var v=this._knownIds;
        if (!v){
            this._knownIds={};
            var all=search.allChildren(<hl.IHighLevelNode>this);
            all.forEach(x=>this._knownIds[x.id()]=x);
        }
        return this._knownIds[id];
    }

    isAuxilary(){
        if(this._isAux){
            return true;
        }
        if (this._auxChecked){
            return false;
        }
        this._auxChecked=true;
        var mr=_.find(this.lowLevel().children(),x=>x.key()=="masterRef");
        if (mr&&mr.value()){
           this._isAux=true;
           var val=mr.value();
           var unit=(<jsyaml.Project>this.lowLevel().unit().project()).resolve(this.lowLevel().unit().path(),val);
           var api=hl.fromUnit(unit);
           if (api){
               var v=search.allChildren(<hl.IHighLevelNode>api);
               this._knownIds={};
               v.forEach(x=>this._knownIds[x.id()]=x);
           }
        }
    }
    private insideOfDeclaration():boolean{
        if (this.definition().isDeclaration()){
            return true;
        }
        if (this.parent()){
            return (<ASTNodeImpl>this.parent()).insideOfDeclaration()
        }
    }
    private isAllowedId(){
        var r=<ASTNodeImpl>this.root();
        if (r.definition().name()=="Extension"){
            return true;
        }
        if (r.isAuxilary()){

            if (this.insideOfDeclaration()){
                var vl=this.computedValue("decls")
                if (vl=="true") {
                    return true;
                }

            }
            if (r._knownIds){
                var m=r._knownIds[this.id()]!=null;

                return m;
            }
            return false;
        }
        return true;
    }

    printDetails(indent?:string) : string {
        var result : string = ""

        if (!indent) indent = ""

        var classname = this.definition().name()
        var definitionClasName = this.property() ? this.property().range().name() : ""
        var parentPropertyName = this.property() ? this.property().name() : "";

        result += indent + parentPropertyName + " : " + classname + "[" + definitionClasName + "]" + "\n"

        this.children().forEach(child=>{
            result += child.printDetails(indent + "\t")
        })

        return result
    }

    private getExtractedChildren(){
        var r=<ASTNodeImpl>this.root();
        if (r.isAuxilary()){
            if (r._knownIds){
                var i=<hl.IHighLevelNode>r._knownIds[this.id()];
                if (i){
                    return i.children();
                }
            }
            return [];
        }
        return [];
    }

    allowsQuestion():boolean{
        return this._allowQuestion||this.definition().getAllowQuestion();
    }

    findReferences():hl.IParseResult []{
        var rs:hl.IParseResult[]=[];
        search.refFinder(this.root(),this,rs);
        return rs;
    }

    name(){
        var ka=_.find(this.directChildren(),x=>x.property()&&x.property().isKey());
        if (ka&&ka instanceof ASTPropImpl){
            var c= (<ASTPropImpl>ka).value();
            if (c) {
                var io = c.indexOf(':');
                if (io != -1) {//TODO REVIEW
                    return c.substring(0, io);
                }
            }
            return c;
        }
        return super.name();
    }

    findElementAtOffset(n:number):hl.IHighLevelNode{
        return this._findNode(this,n,n);
    }

    isElement(){
        return true;
    }

    private _universe:defs.Universe;
    universe():defs.Universe{
        if (this._universe){
            return this._universe;
        }
        return <any>this.definition().universe()
    }
    setUniverse(u:defs.Universe){
        this._universe=u;
    }

    validate(v:hl.ValidationAcceptor):void {
        if (!this.definition()){
            return;//FIXME
        }
        if (!this.parent()){
            var u=this.universe();
            var tv=u.getTypedVersion();
            if (tv){
                if (tv.indexOf("#%")==0) {
                    if (tv != "#%RAML 0.8" && tv != "#%RAML 1.0") {
                        var i = createIssue(hl.IssueCode.NODE_HAS_VALUE, "Unknown version of RAML expected to see one of '#%RAML 0.8' or '#%RAML 1.0'", this)
                        v.accept(i);

                    }
                    var tl=u.getTopLevel();
                    if (tl){
                        if (tl!=this.definition().name()){
                            var i=createIssue(hl.IssueCode.NODE_HAS_VALUE,"Unknown top level type:"+tl,this)
                            v.accept(i);

                        }
                    }
                }
            }
            linter.lintNode(this,v);
        }

        if (!this.isAllowedId()){
            if ((!this.property())||this.property().name()!="annotations") {
                if (this.definition().name()!="GlobalSchema") {
                    var i = createIssue(hl.IssueCode.ONLY_OVERRIDE_ALLOWED, "This node does not override any node from master api:" + this.id(), this)
                    v.accept(i);
                }
            }
        }
        if (!this.definition().getAllowAny()) {
            super.validate(v);
        }
        else{
            this.validateIncludes(v);
        };
        checkPropertyQuard(this, v);
        if (typeof this.value()=='string'&&!this.definition().allowValue()){
            if (this.parent()) {
                var i = createIssue(hl.IssueCode.NODE_HAS_VALUE, "node " + this.name() + " can not be a scalar", this)
                v.accept(i);
            }
        }
        this.definition().requiredProperties().forEach(x=>{
            var r=x.range();
            if (r instanceof def.Array){
                var ar=<def.Array>r;
                r=ar.component;
            }
            if (r.isValueType()) {
                var nm = this.attr(x.name());
                if (!nm) {

                    var i = createIssue(hl.IssueCode.MISSING_REQUIRED_PROPERTY, "Missing required property " + x.name(), this)
                    v.accept(i);
                }
            }
            else{
                var el = this.elementsOfKind(x.name());
                if (!el||el.length==0) {
                    var i = createIssue(hl.IssueCode.MISSING_REQUIRED_PROPERTY, "Missing required property " + x.name(), this)
                    v.accept(i);
                }
            }
        });

        (<defs.NodeClass>this.definition()).getContextRequirements().forEach(x=>{
            if (!this.checkContextValue(x.name,x.value,this.toRuntimeModel())){
                v.accept(createIssue(hl.IssueCode.MISSED_CONTEXT_REQUIREMENT,x.name+" should be "+x.value+" to use type "+this.definition().name(),this))
            }
        });
        if (this.definition().universe().version()=="RAML08") {
            var m:NameToInt={}
            var els=this.directChildren().filter(x=>x.isElement());
            els.forEach(x=> {
                if ((<BasicASTNode><any>x)["_computed"]){
                    return;
                }
                if (!x.name()){
                    return //handling nodes with no key (documentation)
                }
                var rm=x.lowLevel().parent()?x.lowLevel().parent().end():"";
                var k=x.name()+rm;
                if (m[k]){
                    var i=createIssue(hl.IssueCode.KEY_SHOULD_BE_UNIQUE_INTHISCONTEXT,x.name()+" already exists in this context",x)
                    v.accept(i)
                }
                else{
                    m[k]=1;
                }
            })
        }
        else {
            var m:NameToInt={}
            var els=this.directChildren().filter(x=>x.isElement());
            els.forEach(x=> {
                if ((<BasicASTNode><any>x)["_computed"]){
                    return;
                }
                if (!x.name()){
                    return //handling nodes with no key (documentation)
                }
                if (allowOwerride[x.property().name()]){
                    return;
                }
                var k=x.name()+x.property().name();
                if (m[k]){
                    var i=createIssue(hl.IssueCode.KEY_SHOULD_BE_UNIQUE_INTHISCONTEXT,x.name()+" already exists in this context",x)
                    v.accept(i)
                }
                else{
                    m[k]=1;
                }
            })
        }
        var allLowlevel=this.lowLevel().children();
        var mm=_.groupBy(allLowlevel,x=>x.key());
        var pr=this.directChildren().filter(x=>x.isAttr());
        var gr=_.groupBy(pr,x=>x.name());
        var all=this.directChildren();
        var allG=_.groupBy(all,x=>x.name());

        Object.keys(mm).forEach(x=>{
            if (x) {
                if (mm[x].length > 1&&!allG[x]) {
                    var i = createIssue(hl.IssueCode.PROPERTY_EXPECT_TO_HAVE_SINGLE_VALUE, x + " should have a single value", this)
                    i.start = mm[x][0].keyStart();
                    i.end = mm[x][0].keyEnd();

                    v.accept(i)
                }
            }
        })
        Object.keys(gr).forEach(x=>{
            if (gr[x].length>1&&!gr[x][0].property().isMultiValue()){
                gr[x].forEach(y=>{
                    var i=createIssue(hl.IssueCode.PROPERTY_EXPECT_TO_HAVE_SINGLE_VALUE,y.property().name()+" should have a single value",y)
                    v.accept(i)
                })
            }
        })
        if (this._def&&this.property()&&this.property().name()=="types"){

            var at=this.attributes("type");
            if (at){
                var fn=false;
                at.forEach(x=>{
                    if (x.value()==this.name()){
                        var i=createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,"Recurrent type definition",this.attr("type")?this.attr("type"):this)
                        v.accept(i)
                        fn=true;
                        return;
                    }
                })
                if (fn){
                    return
                }
            }
            try {
                this.traverseDec(this._def, v, 0)
            }catch (e){

            }
            if (!this._associatedDef){
                if (_.find(this.definition().allSuperTypes(),x=>x.name()=="DataElement")){
                    this._associatedDef=<any>typeBuilder.typeFromNode(this);
                }
            }
            if (this._associatedDef){
                var st=this._associatedDef.superTypes();
                var nameMap={};
                st.forEach(x=>{
                    nameMap[x.name()]=x;
                })
                var newSt:hl.ITypeDefinition[]=[]
                for (var nm in nameMap){
                    newSt.push(nameMap[nm]);
                }
                st=newSt;
                if (st.length>1) {
                    if (_.find(st, x=>this.isPrimitive(x))) {
                        if (_.find(st, x=>this.isObject(x))) {
                            var i = createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, "You can not inherit both from types of different kind", this.attr("type") ? this.attr("type") : this)
                            v.accept(i)
                            return;
                        }
                    }
                    if (_.find(st, x=>this.isArray(x))) {
                        if (_.find(st, x=>!this.isArray(x))) {
                            var i = createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, "You can not inherit both from types of different kind", this.attr("type") ? this.attr("type") : this)
                            v.accept(i)
                            return;
                        }
                    }
                    if (_.filter(st,x=>this.isPrimitive(x)).length>1){
                        var i = createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, "You can not inherit from two primitive types", this.attr("type") ? this.attr("type") : this)
                        v.accept(i)
                        return;
                    }
                }
            }
        }
    }

    private isPrimitive(q:hl.ITypeDefinition){
        return !this.isArray(q)&&!this.isObject(q)&&!this.isUnion(q)&&q.name()!="DataElement";
    }
    private isObject(q:hl.ITypeDefinition){
       return q.name()=="ObjectField"||_.find(q.allSuperTypes(),x=>x.name()=='ObjectField')!=null;
    }
    private isArray(q:hl.ITypeDefinition){
        return _.find(q.allSuperTypes(),x=>x instanceof defs.Array)!=null;
    }
    private isUnion(q:hl.ITypeDefinition){
        return _.find(q.allSuperTypes(),x=>x instanceof defs.Union)!=null;
    }

    /**
     * !!!You cannot inherit from types of different kind at the same moment ( kinds are: union types, array types, object types, scalar types )
     * !!!You cannot inherit from types extending union types ( ex: you cannot extend from Pet if Pet = Dog | Cat )
     * You cannot inherit from multiple primitive types
     * !!! You cannot inherit from a type that extends Array type
     * Facets are always inherited
     * You can fix a previously defined facet to a value if the facet is defined on a superclass
     * Properties are only allowed on object types
     * You cannot create cyclic dependencies when inheriting
     * @param d
     * @param v
     * @param level
     * @param visited
     */
    private traverseDec(d:hl.ITypeDefinition,v:hl.ValidationAcceptor,level:number,visited:{[name:string]:hl.ITypeDefinition}={}){
        if (d==null){
            return ;
        }
        if (d instanceof def.Array&&level>0){
            var i = createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, "Extending from types extending array types is not allowed", this.attr("type") ? this.attr("type") : this)
            v.accept(i)
            return;
        }
        if (d instanceof def.Union&&level>0){
            var i = createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, "Extending from types extending union types is not allowed", this.attr("type") ? this.attr("type") : this)
            v.accept(i)
            return;
        }
        if (visited[d.name()]){
            var i = createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, "Recurrent type definition", this.attr("type") ? this.attr("type") : this)
            v.accept(i)
            throw new Error();
        }
        visited[d.name()]=this.definition();
        try {
            if (d.name() == this.name() && level > 0) {
                var i = createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, "Recurrent type definition", this.attr("type") ? this.attr("type") : this)
                v.accept(i)
                throw new Error();
            }
            if (d instanceof defs.Array) {
                this.traverseDec(d.component, v, level + 1, visited);
                return;
            }
            if (d instanceof defs.Union) {
                this.traverseDec(d.left, v, level + 1, visited);
                this.traverseDec(d.right, v, level + 1, visited)

                return;
            }
            var mn = d.allSuperTypes();
            mn.forEach(yy=> {
                if (yy instanceof defs.AbstractType) {
                    var node = (<any>yy).getDeclaringNode();
                    if (node || yy instanceof defs.Array || yy instanceof defs.Union) {
                        this.traverseDec(yy, v, level + 1, visited);
                    }
                }
            })
        }
        finally{
            delete visited[d.name()]
        }

    }
    private _findNode(n:hl.IHighLevelNode,offset:number,end:number):hl.IHighLevelNode{
        if (n==null){
            return null;
        }
        if (n.lowLevel()) {
            //var node:ASTNode=<ASTNode>n;
            if (n.lowLevel().start() <= offset && n.lowLevel().end() >= end) {
                var res:hl.IHighLevelNode = n;
                //TODO INCLUDES
                n.elements().forEach(x=> {
                    if (x.lowLevel().unit()!=n.lowLevel().unit()){
                        return;
                    }
                    var m = this._findNode(x, offset, end);
                    if (m) {
                        res = <hl.IHighLevelNode>m;
                    }
                })
                return res;
            }
        }
        return null;
    }


    isStub(){
        return !this.lowLevel().unit()
    }

    private findInsertionPointLowLevel(llnode: ll.ILowLevelASTNode, property: hl.IProperty, attr: boolean):ll.ILowLevelASTNode{
        //always insert attributes at start
        var ch=this.children();
        var insertionPoint:ll.ILowLevelASTNode=null;
        var embed = property && property.isEmbedMap();
        if (embed&&_.find(this.lowLevel().children(),x=>x.key()==property.name())){
            embed=false;
        }
        if (attr||embed) {
            for (var i = 0; i < ch.length; i++) {
                if (!ch[i].isAttr()){
                    break;
                } else{
                    insertionPoint=ch[i].lowLevel();
                }
            }
            if (insertionPoint==null){
                insertionPoint=this.lowLevel();
            }
        }

        return insertionPoint;
    }

    private findInsertionPoint(node:hl.IHighLevelNode|hl.IAttribute):ll.ILowLevelASTNode{
        //always insert attributes at start
        var ch=this.children();
        var toRet:ll.ILowLevelASTNode=null;
        var embed=node.property()&&node.property().isEmbedMap();
        if (embed&&_.find(this.lowLevel().children(),x=>x.key()==node.property().name())){
            embed=false;
        }
        if (node.isAttr()||embed) {
            for (var i = 0; i < ch.length; i++) {
                if (!ch[i].isAttr()){
                    break;
                } else{
                    toRet=ch[i].lowLevel();
                }
            }
            if (toRet==null){
                toRet=this.lowLevel();
            }
        }

        return toRet;
    }
    add(node: hl.IHighLevelNode|hl.IAttribute){

        if (!this._children){
            this._children=[];
        }
        if (!node.property()){
            //now we should find correct property;
            var an=<ASTNodeImpl>node;
            var allProps=this.definition().allProperties()

            var cp:hl.IProperty=null;
            allProps.forEach(x=>{
                var r=x.range();
                if (r==an.definition()){
                    cp=x;
                }
                var isOk=_.find(an.definition().allSuperTypes(),x=>x==r);
                if (isOk){
                    cp=x;
                }
            });
            if (!cp){
                throw new Error("Unable to find correct child")
            }
            else{
                an._prop=cp;
            }
        }

        //console.log('high level op: ' + this.property() + '.add ' + node.property().name());

        var insertionPoint:ll.ILowLevelASTNode = this.findInsertionPoint(node);

        var newLowLevel:ll.ILowLevelASTNode=null;
        var command=new ll.CompositeCommand();
        //now we need to understand to which low level node it should go
        //command.commands.push(ll.insertNode(this.lowLevel(), node.lowLevel()))
        var insertionTarget = null;
        if (node.property().isMerged()||node.property().range().isValueType()){
            //console.log('CASE 1');
            newLowLevel = node.lowLevel();
            command.commands.push(ll.insertNode(this.lowLevel(), newLowLevel, insertionPoint));
            insertionTarget = this.lowLevel();
        } else{
            //console.log('CASE 2');
            var name = node.property().name();
            var target = this.lowLevel();
            var found = (<jsyaml.ASTNode>this.lowLevel()).find(name);
            insertionTarget = found;
            if (!found){
                var nn:jsyaml.ASTNode = null;
                //var nn: jsyaml.ASTNode = jsyaml.createSeqNode(name);
                //var mapping = <yaml.YAMLMapping>nn._actualNode();
                //var seq: yaml.YAMLSequence = <yaml.YAMLSequence>mapping.value;
                //if(!seq.items) seq.items = [];
                //seq.items.push((<jsyaml.ASTNode>node.lowLevel())._actualNode());
                if (node.property().isEmbedMap()){
                    nn=jsyaml.createSeqNode(name);
                    //console.log('NN: ' + yaml.Kind[nn._actualNode().kind]);
                    nn.addChild(node.lowLevel());
                }
                else{
                    nn=jsyaml.createNode(name);
                    nn.addChild(node.lowLevel());
                }
                newLowLevel=nn;
                //target.show('INSERT1: ');
                command.commands.push(ll.insertNode(target, nn,insertionPoint))
                insertionTarget = target;
            } else {
                //found.show('INSERT2: ');
                if (node.property().isEmbedMap()){
                    newLowLevel=node.lowLevel();
                    command.commands.push(ll.insertNode(found, node.lowLevel(),insertionPoint,true));
                } else {
                    newLowLevel=node.lowLevel();
                    command.commands.push(ll.insertNode(found, node.lowLevel(),insertionPoint,false));
                }
            }

        }
        if (this.isStub()){
            var insertionIndex = this.findLastAttributeIndex();
            if(insertionIndex < 0) {
                this._children.push(node);
            } else {
                //TODO behavior should be smarter we are ignoring insertion points now
                this._children.splice(insertionIndex, 0, node);
            }
            command.commands.forEach(x=>insertionTarget.addChild(<ll.ILowLevelASTNode>x.value));
            return;
        }
        this.lowLevel().execute(command)
        this._children.push(node);
        //now we need to add new child to our children;
        node.setParent(this);
    }

    remove(node:hl.IHighLevelNode|hl.IAttribute){
        if (this.isStub()){
            if (!this._children){
                return;
            }
            this._children=this._children.filter(x=>x!=node);
            return;
        }
        var command=new ll.CompositeCommand();
        if (node instanceof ASTNodeImpl){
            var aNode=<ASTNodeImpl>node;
            if (!aNode.property().isMerged()){
                if (this.elementsOfKind(aNode.property().name()).length==1){
                    command.commands.push(ll.removeNode(this.lowLevel(), aNode.lowLevel().parent().parent()))
                } else {
                    command.commands.push(ll.removeNode(this.lowLevel(), aNode.lowLevel()))
                }
            } else {
                command.commands.push(ll.removeNode(this.lowLevel(), aNode.lowLevel()))
            }
        } else {
            command.commands.push(ll.removeNode(this.lowLevel(), node.lowLevel()))
        }
        this.lowLevel().execute(command)
        //update high level
        this._children=this._children.filter(x=>x!=node);
    }

    dump(flavor:string):string{
        return this._node.dump()
    }

    patchType(d:hl.INodeDefinition){
        this._def=d;
        var ass=this._associatedDef;
        this._associatedDef=null;
        this._children=null;


    }

    children():hl.IParseResult[] {

        if (this._children){
            var extra=this.getExtractedChildren();
            var res=this._children.concat(extra);
            return res;
        }
        if (this._node) {
            this._children = nodeBuilder.process(this, this._node.children());
            this._children=this._children.filter(x=>x!=null);
            //FIXME
            var extra=this.getExtractedChildren();
            var res=this._children.concat(extra);
            return res;

        }
        return [];
    }
    directChildren():hl.IParseResult[] {

        if (this._children){
            return this._children;
        }
        if (this._node) {
            this._children = nodeBuilder.process(this, this._node.children());
            return this._children;

        }
        return [];
    }

    resetChildren(){
        this._children = null;
    }

    //createAttr(n:string,v:string){
    //    var mapping=jsyaml.createMapping(n,v);
    //    this._node.addChild(mapping);
    //    this._children=null;
    //}

    private findLastAttributeIndex(): number {
        var last = -1;
        var childs = this.lowLevel().children();
        for(var i=0; i<childs.length; i++) {
            var node = <jsyaml.ASTNode>childs[i];
            if(!node.isMapping()) continue;
            var name = node.asMapping().key.value;
            var property = this.definition().property(name);
            if(property && (property.isValue() || property.isMultiValue())) {
                last = i;
            }
        }
        return last;
    }

    private findLastAttribute(): jsyaml.ASTNode {
        var childs = this.lowLevel().children();
        var index = this.findLastAttributeIndex();
        return (index< 0)? null : <jsyaml.ASTNode>childs[index];
    }

    createAttr(n:string,v:string){
        var mapping=jsyaml.createMapping(n,v);
        //console.log('create attribute: ' + n);
        if(this.isStub()) {
            var insertionIndex = this.findLastAttributeIndex();
            this._node.addChild(mapping, insertionIndex);
            //console.log('insertion: ' + insertionPoint);
            //insertionPoint.show("INSERTION");
            //this.lowLevel().show("ADDED");
        } else {
            //this._node.addChild(mapping);
            this._children=null;
            var command=new ll.CompositeCommand();
            var insertionPoint = this.findInsertionPointLowLevel(mapping, this.definition().property(n), true);
            //command.commands.push(ll.insertNode(this.lowLevel(), mapping, null));
            command.commands.push(ll.insertNode(this.lowLevel(), mapping, insertionPoint));
            this.lowLevel().execute(command);
            //insertionPoint.show("INSERTION");
            //this.lowLevel().show("ADDED");
        }
        this.clearChildrenCache();
    }


    isAttr():boolean{
        return false;
    }
    isUnknown():boolean{
        return false;
    }
    value():any{
        return this._node.value();
    }

    valuesOf(propName:string):hl.IHighLevelNode[]{
        var pr= this._def.property(propName)
        if (pr!=null){
            return this.elements().filter(x=>x.property()==pr);
        }
        return [];
    }
    attr(n:string):hl.IAttribute{
        return _.find(this.attrs(),y=>y.name()==n);
    }

    attrOrCreate(name: string):hl.IAttribute{
        var a = this.attr(name);
        if(!a) this.createAttr(name, '');
        return this.attr(name);
    }

    attributes(n:string):hl.IAttribute[]{
        return _.filter(this.attrs(),y=>y.name()==n);
    }


    attrs():hl.IAttribute[]{


        return <hl.IAttribute[]>this.children().filter(x=>x.isAttr());
    }

    /*
    allAttrs():hl.IAttribute[]{
        var attrs = <hl.IAttribute[]>this.children().filter(x=>x.isAttr());
        var attributes = [];
        //console.log('Attributes(' + this.definition().name() + '): ');
        (<NodeClass>this.definition()).allProperties().forEach(x=>{
            if(x.range().isValueType()&&!x.isSystem()){
                var a = _.find(attrs,y=>y.name()==x.name());
                //var a = this.attr(x.name());
                if (a){
                    //console.log('  real   : ' + x.name() + ' = ' + a.value());
                    attributes.push(a);
                } else {
                    a = new VirtualAttribute(this, this.definition(), x, false);
                    //console.log('  virtual: ' + x.name());
                    attributes.push(a);
                }
            }
        })
        return attributes;
    }
    */

    elements():hl.IHighLevelNode[]{
        return <hl.IHighLevelNode[]>this.children()
            .filter(x=>!x.isAttr()&&!x.isUnknown())
    }
    element(n:string):hl.IHighLevelNode{
        var r= this.elementsOfKind(n)
        if (r.length>0){
            return r[0];
        }
        return null;
    }

    elementsOfKind(n:string):hl.IHighLevelNode[]{
        var r= this.elements().filter(x=>x.property().name()==n)
        return r;
    }

    definition():hl.INodeDefinition {
        return this._def;
    }

    property():hl.IProperty {
        return this._prop;
    }

    isExpanded():boolean {
        return this._expanded;
    }

    copy(): ASTNodeImpl {
        return new ASTNodeImpl(this.lowLevel().copy(), this.parent(), this.definition(), this.property());
    }

    clearChildrenCache() {
        this._children = null;
    }
}
export function typeFromNode(node:hl.IHighLevelNode):hl.ITypeDefinition{
    return typeBuilder.typeFromNode(node);
}

export function createStub(parent: hl.IHighLevelNode, property: string, key?: string) : hl.IHighLevelNode {
    var p = parent.definition().property(property);
    if (!p) return null;
    var nc: def.NodeClass = <def.NodeClass> p.range();
    
    return nc.createStubNode(p, key);
}

export function createResourceStub(parent: hl.IHighLevelNode, key?: string) : hl.IHighLevelNode {
    return createStub(parent, "resources", key);
}

export function createMethodStub(parent: hl.IHighLevelNode, key?: string) : hl.IHighLevelNode {
    return createStub(parent, 'methods', key);
}

export function createResponseStub(parent: hl.IHighLevelNode, key?: string) : hl.IHighLevelNode {
    return createStub(parent, 'responses', key);
}
export function createBodyStub(parent: hl.IHighLevelNode, key?: string) : hl.IHighLevelNode {
    return createStub(parent, 'body', key);
}
export function createUriParameterStub(parent: hl.IHighLevelNode, key?: string) : hl.IHighLevelNode {
    return createStub(parent, 'uriParameters', key);
}
export function createObjectFieldStub(parent: hl.IHighLevelNode, name: string) : hl.IHighLevelNode {
    var type = <def.NodeClass>parent.definition().universe().getType('ObjectField');
    //var property = def.prop('types', 'xxx', <def.NodeClass>parent.definition(), type);
    var property = parent.definition().property('types');
    property = (<def.Property>property).withRange(type);
    console.log('property: ' + property.name());
    var nc: def.NodeClass = <def.NodeClass> property.range();
    return nc.createStubNode(property, name);
}

var allowOwerride = { resources: 1, queryParameters: 1, headers: 1, body: 1, methods: 1 }
