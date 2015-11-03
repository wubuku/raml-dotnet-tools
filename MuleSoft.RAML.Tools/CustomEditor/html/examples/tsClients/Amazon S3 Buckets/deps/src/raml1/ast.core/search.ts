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
import typeBuilder=require("./typeBuilder")
import path=require("path")
//FIXME CORRECTLY STRUCTURE IT
export function resolveRamlPointer(point:hl.IHighLevelNode,path:string):hl.IHighLevelNode{
    var components:string[]=path.split(".");

    var currentNode=point;
    if (currentNode.definition().isAnnotation()){
        currentNode=currentNode.parent();
    }
    components.forEach(x=>{
        if (currentNode==null){
            return;
        }
        if (x=='$parent'){
            currentNode=currentNode.parent();
            return;
        }
        if (x=='$root'){
            currentNode=currentNode.root();
            return;
        }
        if (x=='$top'){
            currentNode=declRoot(currentNode);
            return;
        }
        var newEl=_.find(currentNode.elements(),y=>y.name()==x);

        currentNode=newEl;
    });
    return currentNode;
}
export var declRoot = function (h:hl.IHighLevelNode):hl.IHighLevelNode {
    var declRoot = h;
    while (true) {
        if (declRoot.definition().name() == "Library"&&declRoot.definition() instanceof defs.NodeClass) {
            break;
        }
        var np = declRoot.parent();
        if (!np) {
            break;
        }
        declRoot=np;
    }
    return declRoot;

};
export function globalDeclarations(h:hl.IHighLevelNode):hl.IHighLevelNode[]{

    var decl = declRoot(h);
    return findDeclarations(decl);

}
export function findDeclarations(h:hl.IHighLevelNode):hl.IHighLevelNode[]{
    var rs:hl.IHighLevelNode[]=[];
    h.elements().forEach(x=>{
        if (x.definition().name()=="Library"){
            rs=rs.concat(findDeclarations(x));
        }
        rs.push(x);
    });
    return rs;
}
function getIndent2(offset:number,text:string):string{
    var spaces="";
    for (var i=offset-1;i>=0;i--){
        var c=text.charAt(i);
        if (c==' '||c=='\t'){
            if (spaces){
                spaces+=c;
            }
            else{
                spaces=c;
            }
        }
        else if (c=='\r'||c=='\n'){
            return spaces;
        }

    }
}
function deepFindNode(n:hl.IParseResult,offset:number,end:number):hl.IParseResult{
    if (n==null){
        return null;
    }
    if (n.lowLevel()) {
        //var node:ASTNode=<ASTNode>n;
        if (n.lowLevel().start() <= offset && n.lowLevel().end() >= end) {
            if (n instanceof  hlimpl.ASTNodeImpl){
                var hn=<hlimpl.ASTNodeImpl>n;
                var all=hn.children();
                for(var i=0;i<all.length;i++){
                    var node=deepFindNode(all[i],offset,end);
                    if (node){
                        return node;
                    }
                }
                return n;
            }
            if (n instanceof  hlimpl.ASTPropImpl){
                var attr=<hlimpl.ASTPropImpl>n;
                if (!attr.property().isKey()) {
                    var vl = attr.value();
                    if (vl instanceof hlimpl.StructuredValue) {
                        var st = <hlimpl.StructuredValue>vl;
                        var hl = st.toHighlevel();
                        var node = deepFindNode(hl, offset, end);
                        if (node) {
                            return node;
                        }

                    }
                    return attr;
                }
                return null;
            }
            return n;
        }
    }
    return null;
}


function getValueAt(text:string,offset:number):string{
    var sp=-1;
    for (var i=offset-1;i>=0;i--){
        var c=text.charAt(i);
        if (c=='\r'||c=='\n'||c==' '||c=='\t'||c=='"'||c=='\''||c==':'){
            sp=i+1;
            break;
        }
    }
    var ep=-1;
    for (var i=offset;i<text.length;i++){
        var c=text.charAt(i);
        if (c=='\r'||c=='\n'||c==' '||c=='\t'||c=='"'||c=='\''||c==':'){
            ep=i;
            break;
        }
    }
    if (sp!=-1&&ep!=-1){
        return text.substring(sp,ep);
    }
    return "";
}
export function extractName(cleaned:string,offset:number):string{
    var txt="";
    for (var i=offset;i>=0;i--){
        var c=cleaned[i];
        if (c==' '||c=='\r'||c=='\n'||c=='|'||c=='['||c==']'||c==':'||c=='('||c==')'){
            break;
        }
        txt=c+txt;
    }
    for (var i=offset+1;i<cleaned.length;i++){
        var c=cleaned[i];
        if (c==' '||c=='\r'||c=='\n'||c=='|'||c=='['||c==']'||c==':'||c=='('||c==')'){
            break;
        }
        txt=txt+c;
    }
    return txt;
}
var searchInTheValue = function (offset:number,content: string,attr:hl.IAttribute, hlnode:hl.IHighLevelNode,p:hl.IProperty=attr.property()):hl.IHighLevelNode {
    var targets = p.referenceTargets(hlnode);
    var txt=extractName(content,offset);
    var t:hl.IHighLevelNode = _.find(targets, x=>hlimpl.qName(x, hlnode) == txt)
    if (t) {
        //TODO EXTRACT COMMON OPEN NODE FUNC
        return t;
        //ed.setSelectedBufferRange();
    }
    return null;
};
export interface FindUsagesResult{
    node:hl.IHighLevelNode
    results:hl.IParseResult[]
}
export function findUsages(unit:ll.ICompilationUnit, offset:number):FindUsagesResult{
    var decl=findDeclaration(unit,offset);
    if (decl){
        if (decl instanceof hlimpl.ASTNodeImpl){
            var hnode=<hlimpl.ASTNodeImpl>decl;
            return {node:hnode,results:hnode.findReferences()};
        }
        if (decl instanceof hlimpl.ASTPropImpl){
            //var prop=<hlimpl.ASTPropImpl>decl;
            //return {node:prop,results:prop.findReferences()};
        }
    }
    var node = deepFindNode(hl.fromUnit(unit), offset,offset);
    if (node instanceof hlimpl.ASTNodeImpl){
        return {node:<hlimpl.ASTNodeImpl>node,results:(<hlimpl.ASTNodeImpl>node).findReferences()};
    }
    if (node instanceof hlimpl.ASTPropImpl){
        var prop=<hlimpl.ASTPropImpl>node;
        if (prop.property().canBeValue()){
            return {node:<hlimpl.ASTNodeImpl>prop.parent(),results:(<hlimpl.ASTNodeImpl>prop.parent()).findReferences()};

        }
    }

    return {node: null,results:[]};
}
export function findDeclaration(unit:ll.ICompilationUnit, offset:number):ll.ICompilationUnit|hl.IHighLevelNode {
    var node = deepFindNode(hl.fromUnit(unit), offset,offset);
    var kind = determineCompletionKind(unit.contents(), offset);
    if (kind == LocationKind.VALUE_COMPLETION) {

        var hlnode = <hl.IHighLevelNode>node;
        if (node instanceof hlimpl.ASTPropImpl) {
            var attr = node;
            if (attr) {
                if (attr.value()) {
                    if (attr.value() instanceof hlimpl.StructuredValue) {
                        var sval = <hlimpl.StructuredValue>attr.value();
                        var hlvalue = sval.toHighlevel();

                        if (hlvalue) {

                            var newAttr = _.find(hlvalue.attrs(), x=>x.lowLevel().start() < offset && x.lowLevel().end() >= offset);
                            if (newAttr) {
                                return searchInTheValue(offset,unit.contents(),newAttr, hlvalue, attr.property());
                            }

                        }
                    } else {
                        return searchInTheValue(offset,unit.contents(),attr, hlnode);
                    }

                }
                //console.log(attr.value());
            }
        }
    }
    if (kind == LocationKind.KEY_COMPLETION||kind==LocationKind.SEQUENCE_KEY_COPLETION) {
        var hlnode = <hl.IHighLevelNode>node;
        var pp=node.property();
        if (pp instanceof defs.UserDefinedProp){
            var up=<defs.UserDefinedProp>pp;
            return up.node();
        }
        if (node instanceof hlimpl.ASTNodeImpl) {
            if (hlnode.definition() instanceof defs.UserDefinedClass) {
                var uc = <defs.UserDefinedClass>hlnode.definition();
                return uc.getDeclaringNode();
            }
        }
        if (node instanceof  hlimpl.ASTPropImpl){
            var pr=<hlimpl.ASTPropImpl>node;
            if (isExampleNodeContent(pr)) {
                var contentType = findExampleContentType(pr)
                if (contentType) {
                    var documentationRoot:hl.IHighLevelNode = parseDocumentationContent(pr,
                        <hl.INodeDefinition>contentType.toRuntime());
                    if (documentationRoot) {
                        var node = deepFindNode(documentationRoot, offset,offset);

                        var pp=node.property();
                        if (pp instanceof defs.UserDefinedProp){
                            var up=<defs.UserDefinedProp>pp;
                            return up.node();
                        }
                        if (node instanceof hlimpl.ASTNodeImpl) {
                            if (hlnode.definition() instanceof defs.UserDefinedClass) {
                                var uc = <defs.UserDefinedClass>hlnode.definition();
                                return uc.getDeclaringNode();
                            }
                        }
                        //return propertyCompletion(documentationRoot, offset, text, false, true)
                    }
                }
            }
        }
    }
    if (kind == LocationKind.PATH_COMPLETION) {
        var inclpath = getValueAt(unit.contents(), offset);
        if (inclpath) {
            var ap = unit.resolve(inclpath);
            return ap;
        }
    }
}
export function findExampleContentType(node : hl.IAttribute) : hl.INodeDefinition {
    var p=node.parent();
    if (node.property().name()=="content"){
        p=p.parent();
    }
    return <hl.INodeDefinition>hlimpl.typeFromNode(p);
}

export function parseDocumentationContent(attribute : hl.IAttribute, type : hl.INodeDefinition) : hl.IHighLevelNode {
    if (!(attribute.value() instanceof hlimpl.StructuredValue)){
        return null
    }
    return new hlimpl.ASTNodeImpl((<hlimpl.StructuredValue>attribute.value()).lowLevel(), attribute.parent(), type, attribute.property())
}

export function isExampleNodeContent(node : hl.IAttribute) : boolean {
    if (!(node instanceof hlimpl.ASTPropImpl)){
        return false
    }

    var property = <hlimpl.ASTPropImpl>node;

    if ("content" == property.name() && "StringType" == property.definition().name()) {

        if(property.parent() instanceof hlimpl.ASTNodeImpl &&
            ("examples" == (<hlimpl.ASTNodeImpl>property.parent()).property().name()
            || "example" == (<hlimpl.ASTNodeImpl>property.parent()).property().name())){

            if (property.parent().parent() instanceof hlimpl.ASTNodeImpl &&
                (<hlimpl.ASTNodeImpl>property.parent().parent()).definition().isAssignableFrom("ObjectField")) {

                return true;
            }
        }
    }
    else if ("example" == property.name() && "StringType" == property.definition().name()) {
        if (property.parent() instanceof hlimpl.ASTNodeImpl &&
            (<hlimpl.ASTNodeImpl>property.parent()).definition().isAssignableFrom("ObjectField")) {

            return true;
        }
    }

    return false;
}
export function determineCompletionKind(text:string,offset:number):LocationKind{
    var hasIn=false;
    var hasSeq=false;
    var canBeInComment=false;
    for (var i=offset-1;i>=0;i--){
        var c=text.charAt(i);
        if (c=='#'){
            if (i==0) {
                return LocationKind.VERSION_COMPLETION;
            }
            return LocationKind.INCOMMENT;
        }
        if (c==':'){
            if (hasIn){
                return LocationKind.DIRECTIVE_COMPLETION
            }
            return LocationKind.VALUE_COMPLETION
        }
        if (c=='\r'||c=='\n'){
            //check for multiline literal
            var insideOfMultiline=false;
            var ind=getIndent2(offset,text);
            for (var a=i;a>0;a--){
                c=text.charAt(a);
                //TODO this can be further improved
                if (c==':')
                {
                    if (insideOfMultiline){
                        var ll=getIndent2(a,text);
                        if (ll.length<ind.length) {
                            return LocationKind.VALUE_COMPLETION
                        }
                    }
                    break;
                }
                if (c=='|'){
                    insideOfMultiline=true;
                    continue;
                }
                if (c=='\r'||c=='\n'){
                    insideOfMultiline=false;
                }
                if (c!=' '&&c!='\t'){
                    insideOfMultiline=false;
                }
            }
            if (hasSeq){
                return LocationKind.SEQUENCE_KEY_COPLETION
            }
            return LocationKind.KEY_COMPLETION
        }
        if (c=='-') {
            hasSeq=true;
        }
        if (c=='!'){
            if (text.indexOf("!include",i)==i) {
                return LocationKind.PATH_COMPLETION;
            }
            if (text.indexOf("!i",i)==i) {
                hasIn=true;
            }
        }
    }
}

export enum LocationKind{
    VALUE_COMPLETION,
    KEY_COMPLETION,
    PATH_COMPLETION,
    DIRECTIVE_COMPLETION,
    VERSION_COMPLETION,
    SEQUENCE_KEY_COPLETION,
    INCOMMENT
}
export function resolveReference(point:ll.ILowLevelASTNode,path:string):ll.ILowLevelASTNode{
    if (!path){
        return null;
    }
    var sp=path.split("/");
    var result=point;
    for(var i=0;i<sp.length;i++){
        if (sp[i]=='#'){
            result=point.unit().ast();
            continue;
        }
        result=_.find(result.children(),x=>x.key()==sp[i]);
        if (!result){
            return null;
        }
    }
    return result;
}

/**
 * return all sub types of given type visible from parent node
 * @param range
 * @param parentNode
 * @returns ITypeDefinition[]
 */
export var subTypesWithLocals = function (range:hl.ITypeDefinition, parentNode:hl.IHighLevelNode):hl.ITypeDefinition[] {
    if (range==null){
        return [];
    }
    var name=range.name();

    parentNode=declRoot(parentNode);
    var actual=<hlimpl.ASTNodeImpl>parentNode;
    if (actual._subTypesCache){
        var cached=actual._subTypesCache[name];
        if (cached){
            return cached;
        }
    }
    else{
        actual._subTypesCache={};
    }
    var result = range.allSubTypes();
    var decls=globalDeclarations(parentNode);


    if (range.getRuntimeExtenders().length > 0&&parentNode) {
        var extenders = range.getRuntimeExtenders();
        var root = parentNode.root();
        extenders.forEach(x=> {
            var definitionNodes =decls.filter(z=>
            {
                var def=  z.definition().allSuperTypes();
                def.push(z.definition())
                var rr= (z.definition() == x)||(_.find(def,d=>d==x)!=null)||(_.find(def,d=>d==range)!=null);
                return rr;
            });
            result = result.concat(definitionNodes.map(x=>typeBuilder.typeFromNode(x)))
        })
    }
    result=_.unique(result);
    actual._subTypesCache[name]=result;
    return result;
};
export var subTypesWithName = function (tname: string, parentNode:hl.IHighLevelNode,backup:{[name:string]:hl.ITypeDefinition}):hl.ITypeDefinition {
    parentNode=declRoot(parentNode);
    var decls=globalDeclarations(parentNode);
    var declNode=_.find(decls,x=>hlimpl.qName(x,parentNode)==tname&&x.property()&&
    (x.property().name()=='types'));
    var result=typeBuilder.typeFromNode(declNode);
    return result;
};
export var schemasWithName = function (tname: string, parentNode:hl.IHighLevelNode,backup:{[name:string]:hl.ITypeDefinition}):hl.ITypeDefinition {
    parentNode=declRoot(parentNode);
    var decls=globalDeclarations(parentNode);
    var declNode=_.find(decls,x=>hlimpl.qName(x,parentNode)==tname&&x.property()&&
    (x.property().name()=='schemas'));
    var result=typeBuilder.typeFromNode(declNode);
    return result;
};


export var nodesDeclaringType = function (range:hl.ITypeDefinition, n:hl.IHighLevelNode):hl.IHighLevelNode[] {
    var result:hl.IHighLevelNode[] = [];
    if (range.getRuntimeExtenders().length > 0&&n) {
        var extenders = range.getRuntimeExtenders();
        var root = n;
        extenders.forEach(x=> {
            var definitionNodes = globalDeclarations(root).filter(z=>z.definition() == x);
            result = result.concat(definitionNodes)
        })
    }
    var isElementType=!range.isValueType();
    if (isElementType&&(<hl.INodeDefinition>range).isInlinedTemplates() && n){
        var root = n;
        //TODO I did not like it it might be written much better
        var definitionNodes = globalDeclarations(root).filter(z=>z.definition() == range);
        result=result.concat(definitionNodes);
    }
    else{
        var root = n;
        var q={};
        range.allSubTypes().forEach(x=>q[x.name()]=true)
        q[range.name()]=true;
        var definitionNodes = globalDeclarations(root).filter(z=>q[z.definition().name()]);
        result=result.concat(definitionNodes);
    }
    return result;
};
export function findAllSubTypes(p:hl.IProperty,n:hl.IHighLevelNode) {
    var range=p.range();
    return subTypesWithLocals(range, n);
};

function possibleNodes(p:defs.Property,c:hl.IHighLevelNode):hl.IHighLevelNode[]{
    if (c) {
        if (p.isDescriminating()) {
            var range=p.range();
            if (range.getRuntimeExtenders().length > 0&&c) {
                var extenders = range.getRuntimeExtenders();
                var result:hl.IHighLevelNode[]=[]
                extenders.forEach(x=> {
                    var definitionNodes = globalDeclarations(c).filter(z=>z.definition() == x);
                    result = result.concat(definitionNodes);
                });
                return result;
            }
            return []
        }
        if (p.isReference()) {
            return nodesDeclaringType(p.referencesTo(), c);
        }
        if (p.range().isValueType()) {
            var vt = <defs.ValueType>p.range();
            if (vt.globallyDeclaredBy&&vt.globallyDeclaredBy().length>0) {
                var definitionNodes = globalDeclarations(c).filter(z=>_.find( vt.globallyDeclaredBy(),x=>x==z.definition())!=null);
                return definitionNodes;
            }
        }
    }
    return this._enumOptions;
}
export function allChildren(node:hl.IHighLevelNode):hl.IParseResult[]{
    var res=[];
    gather(node,res)
    return res;
}
function gather(node:hl.IParseResult,result:hl.IParseResult[]){
    node.children().forEach(x=>{result.push(x);gather(x,result);});
}

var testUsage = function (ck:def.NodeClass, x:hl.IParseResult, node:hl.IHighLevelNode, result:hl.IParseResult[]) {
    if (ck instanceof defs.UserDefinedClass) {
        var ud = <defs.UserDefinedClass>ck;
        if (ud.getDeclaringNode() == node) {
            result.push(x);
        }
    }
    if (ck instanceof defs.Array) {
        var cmp = <defs.Array>ck;
        testUsage(<defs.NodeClass>cmp.component, x, node, result);
    }
    if (ck instanceof defs.Union) {
        var uni = <defs.Union>ck;
        testUsage(<defs.NodeClass>uni.left, x, node, result);
        testUsage(<defs.NodeClass>uni.right, x, node, result);
    }
};
export function refFinder(root:hl.IHighLevelNode,node:hl.IHighLevelNode,result:hl.IParseResult[]):void{
    root.elements().forEach(x=>{
        refFinder(x,node,result);
        //console.log(x.name())
        var ck=<def.NodeClass>x.definition();
        //testUsage(ck, x, node, result);

    })
    root.attrs().forEach(a=>{
        var pr=a.property();
        var vl=a.value();
        //if (pr.isTypeExpr()){
        //    typeExpression.
        //}
        if (pr instanceof defs.UserDefinedProp){
            var up=(<defs.UserDefinedProp>pr).node();
            if (up==node){
                result.push(a);
            }
            //Runtime properties
            else if (up.lowLevel().start()==node.lowLevel().start()){
                if (up.lowLevel().unit()==node.lowLevel().unit()) {
                    result.push(a);
                }
            }
        }
        if (isExampleNodeContent(a)){
            var contentType = findExampleContentType(a)
            if (contentType) {
                var documentationRoot:hl.IHighLevelNode = parseDocumentationContent(a,
                    <hl.INodeDefinition>contentType.toRuntime());
                if (documentationRoot) {
                    refFinder(documentationRoot, node, result);
                    //return propertyCompletion(documentationRoot, offset, text, false, true)
                }
            }
        }
        else if (pr.isTypeExpr()&&typeof vl=="string"){
            if (pr.name()=="signature"){
                var sig=ramlSignature.parse(a);
                if (sig){
                    sig.args.forEach(x=>{
                        var tp=typeExpression.deriveType(root, x.type)
                        if (tp) {
                            testUsage(<def.NodeClass>tp, a, node, result);
                        }
                    })
                    if (sig.returnType){
                        var tp=typeExpression.deriveType(root, sig.returnType)
                        if (tp) {
                            testUsage(<def.NodeClass>tp, a, node, result);
                        }
                    }
                }

            }
            else {
                var tpa = typeExpression.getType(root, "" + vl, {});
                testUsage(<def.NodeClass>tpa, a, node, result);
            }
        }
        if (pr.isReference()||pr.isDescriminator()){
            if (typeof vl=='string'){
                var pn=possibleNodes(<any>pr, root);
                if (_.find(pn,x=>x.name()==vl&&x==node)){
                    result.push(a);
                }
            }
            else{
                var st=<hlimpl.StructuredValue>vl;
                if (st) {
                    var vn = st.valueName();
                    var pn=possibleNodes(<any>pr, root);
                    if (_.find(pn,x=>x.name()==vn&&x==node)){
                        result.push(a);
                    }
                    var hnode = st.toHighlevel()
                    if (hnode) {
                        refFinder(hnode, node, result);
                    }
                }
            }
        }
        else{
            var pn=possibleNodes(<any>pr, root);
            if (_.find(pn,x=>x.name()==vl&&x==node)){
                result.push(a);
            }
        }
    });
}