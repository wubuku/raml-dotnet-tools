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
import hlImpl=require("./highLevelImpl")
import typeExpr=require("./typeExpressions")
import ramlSignatureParser=require("./ramlSignatureParser");
import wrapper=require("./artifacts/raml003parser")
export interface Signature{

    args: Argument[]
    returnType?:typeExpr.BaseNode
}

interface Argument{
    name: string
    type: typeExpr.BaseNode;
    opt: boolean
}

export function validate(s: string,node:hl.IAttribute,cb:hl.ValidationAcceptor){
    var result=ramlSignatureParser.parse(s);
    if (result.args){
        result.args.forEach(x=>{
            var ind=x.name.indexOf(".");
            if (ind==-1){
                if (x.name!="body") {
                    cb.accept(hlImpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, "Only body parameter may be not qualified", node, false))
                }
            }
            else{
                var qualifier=x.name.substring(0,ind);
                if (qualifier!="uri"&&qualifier!='header'&&qualifier!='query'){
                    cb.accept(hlImpl.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA, "qualifer should be one of 'query', 'header' or 'uri'", node, false))
                }
            }
            typeExpr.validateNode(x.type,node,cb)
        });
    }
    if (result.returnType){
        typeExpr.validateNode(result.returnType,node,cb);
    }
}
export function convertToTrait(s:Signature,defaultCode:string="200"){
    var trait=new wrapper.TraitImpl("tr");
    s.args.forEach(x=>{
        convertArgument(trait,x);

    })
    if (s.returnType){
        if (s.returnType.type=="responses"){
            var rsc=<typeExpr.Responses>s.returnType;
            rsc.codes.forEach(x=>{
                var rs=new wrapper.ResponseImpl(x.code);
                var da=new wrapper.DataElementImpl("application/json")
                da.setType(typeExpr.nodeToString(x.expr));
                rs.add(da);
                trait.add(rs);
            })
        } else{
            var rs=new wrapper.ResponseImpl(defaultCode);
            var da=new wrapper.DataElementImpl("application/json")
            da.setType(typeExpr.nodeToString(s.returnType));
            rs.add(da);
            trait.add(rs);
        }
    }
    return trait
}
function convertArgument(tr:wrapper.TraitImpl, arg: Argument){
    //hlimpl.createMethodStub()
    var dot=arg.name.indexOf(".");
    var type:string=null;
    var aName=arg.name;
    if (dot!=-1) {
        type = arg.name.substr(0, dot);
        aName = arg.name.substr(dot + 1)
    }

    var c=new wrapper.DataElementImpl(aName);

    if (aName=="body"){
        c=new wrapper.DataElementImpl("application/json")
        //aName="application/json"
    }
    c.setType(typeExpr.nodeToString(arg.type));
    if (type=="query") {
        tr.addToProp(c, "queryParameters");
    }
    else if (type=="header") {
        tr.addToProp(c, "headers");
    }
    else if (type=="uri") {
        tr.addToProp(c, "uriParameters");
    }
    else if (type=="body") {
        tr.addToProp(c, "body");
    }
    else{
        if (aName=="body"){
            tr.addToProp(c,"body")
        }
    }
    return c;
}

export function parse(node:hl.IAttribute):Signature{
    try {
        if (typeof node.value()=="string") {
            var result = ramlSignatureParser.parse(node.value());
            return result;
        }

    } catch (e){
        return null;
    }
}