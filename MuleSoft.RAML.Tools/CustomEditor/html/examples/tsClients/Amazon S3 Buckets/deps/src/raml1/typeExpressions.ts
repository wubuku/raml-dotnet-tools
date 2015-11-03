/// <reference path="../../typings/tsd.d.ts" />

import defs=require("./definitionSystem")
import hl=require("./highLevelAST")
import ll=require("./lowLevelAST")
import _=require("underscore")
import typeExpression=require("./typeExpressionParser")
import hlImpl=require("./highLevelImpl")
import search=require("./ast.core/search")
import linter=require("./ast.core/linter")
import schema=require("./schemaUtil")
export interface BaseNode{
    type:string
}
export interface Union extends BaseNode{
    first: BaseNode
    rest: BaseNode
}
export interface CodeAndType{
  code: string
  expr: BaseNode
}
export interface Responses extends BaseNode{
    codes: CodeAndType[]
}
export interface Parens{
    expr:  BaseNode
    arr: number
}
export interface Literal{
    value: string
    arr?: number
    params?:BaseNode[];
}
export function validate(str:string, node:hl.IAttribute,cb:hl.ValidationAcceptor){
    var x=str.trim();
    if (x.length>0){
        try {
            if (x.charAt(0) == "{") {
                schema.getJSONSchema(str)
                //this is json schema
                return;
            }
            if (x.charAt(0) == "<") {
                schema.getXMLSchema(str)
                //this is xsd schema
                return;
            }
        } catch (e){
            cb.accept(linter.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,e.message,node));
        }
    }
    var result:BaseNode=typeExpression.parse(str);
    validateNode(result,node,cb);
}
export function getType(node:hl.IHighLevelNode,expression:string,defined:{[name:string]:hl.ITypeDefinition},toRuntime=false):hl.ITypeDefinition{
        //console.log(expression);
        //console.log(defined)
        if (Object.keys(defined).length==0){
            //console.log("A")
        }
        if (!expression) {
            return node.definition().universe().getType("StrElement");
        }
        if (toRuntime) {
            if (buildInsRuntime[expression]) {
                return node.definition().universe().getType(buildInsRuntime[expression]);
            }
        }
        if (buildIns[expression]) {
            return node.definition().universe().getType(buildIns[expression]);
        }
        try {
            var vl=expression;
            vl = vl.trim();
            if (vl.charAt(0) == '{') {
                return null;
            }
            if (vl.charAt(0) == '<') {
                return null;
            }
            var result:BaseNode = typeExpression.parse(expression);

        } catch (e) {
            return null;
        }
        return deriveType(node, result,toRuntime,defined);

}
var buildIns={

    string:"StrElement",
    number:"NumberElement",
    integer:"IntegerElement",
    date:"DateElement",
    object:"ObjectField",
    boolean:"BooleanElement",
    value:"ValueElement",
    file:"FileParameter"
}

var buildInsRuntime={

    string:"string",
    number:"number",
    integer:"integer",
    date:"date",
    object:"ObjectField",
    boolean:"boolean",
    value:"string",
    file:"file"
}

export function deriveType(node:hl.IHighLevelNode,r:BaseNode,toRuntime:boolean=false,defining:{[name:string]:hl.ITypeDefinition}={}){
    if (r.type=="union"){
        var u:Union=<Union>r;
        var left=deriveType(node,u.first,toRuntime,defining);
        var right=deriveType(node,u.rest,toRuntime,defining);
        var ut= node.definition().universe().getType("UnionField")
        var union=new defs.Union("Union"+node.lowLevel().start(),<any>node.definition().universe(),"");
        union._superTypes.push(ut);
        union.left=left;
        union.right=right;
        return union;
    }
    if (r.type=='responses'){
        var res:Responses=<any>r;
        var result=null;
        res.codes.forEach(t=>{
            var tp=deriveType(node,t.expr,toRuntime,defining);
            if (tp) {
                if (!result) {
                    result = tp;
                }
                else {
                    var union = new defs.Union("Union" + node.lowLevel().start(), <any>node.definition().universe(), "");
                    union.left = result;
                    union.right = tp;
                    result = tp;
                }
            }
        })
        return result;
    }
    if (r.type=='parens'){
        var ex:Parens=<any>r;
        return deriveType(node,ex.expr,toRuntime,defining);
    }
    if (r.type=='name'){
        var l:Literal=<any>r;
        var val=l.value;
        var ind=val.lastIndexOf("[]");
        if (ind!=-1&&ind==val.length-2){
            val=val.substr(0,val.length-2);//FIXME Should be in PEG
        }
        if (l.arr>0){
            var types=search.subTypesWithLocals(node.definition().universe().getType("DataElement"),node);
            var tp= _.find(types,x=>{

                var c=x.name()==val;
                if (!c){
                    if (x instanceof defs.AbstractType){
                        var at=<defs.AbstractType><any>x;
                        at.getAliases().forEach(y=>{
                            if (y==val){
                              c=true;
                            }
                        })
                    }
                }
                return c;
            });
            if (!tp) {
                //TOD make it simpler
                if (toRuntime||true) {
                    //it is always runtime model when we are here
                    if (buildInsRuntime[val]) {
                        tp = node.definition().universe().getType(buildInsRuntime[val]);
                    }
                }
                else if (buildIns[val]) {
                    tp = node.definition().universe().getType(buildIns[val]);
                }
            }
            if (!tp){
                tp=new defs.ValueType("String",<defs.Universe>node.definition().universe(),"");
            }
            var at=node.definition().universe().getType("ArrayField");
            var arr=new defs.Array(tp.name()+"[]",<any>node.definition().universe(),"");
            arr._superTypes.push(at);
            arr.component=tp;
            arr.dimensions=l.arr;
            return arr;
        }
        if (toRuntime){
            if (buildInsRuntime[val]){
                return node.definition().universe().getType(buildInsRuntime[val]);
            }
        }
        if (buildIns[val]){
            return node.definition().universe().getType(buildIns[val]);
        }
        var de=node.definition().universe().getType("DataElement");
        if (!de){
            de=node.definition().universe().getType("GlobalSchema")
        }
        //if (defining[val]){
        //    return defining[val];
        //}
        var qm=search.subTypesWithName(val,node,defining);
        if (qm){
            return qm;
        }
        //return null;
        de=node.definition().universe().getType("GlobalSchema")
        return search.schemasWithName(val,node,defining);
    }
    return null;
}


export function nodeToString(r:BaseNode): string{
    if (r.type=="union"){
        var u:Union=<Union>r;
        return nodeToString(u.first)+"|"+nodeToString(u.rest)
    }
    if (r.type=="responses"){
        var res:Responses=<Responses>r;
        var rs="{"
        for (var i=0;i<res.codes.length;i++){
            rs+=res.codes[i].code
            rs+=":"
            rs+=nodeToString(res.codes[i].expr);
            if (i!=res.codes.length-1){
                rs+=","
            }
        }
        rs+='}'
        return rs;
        //validateNode(u.first,node,cb);
        //validateNode(u.rest,node,cb);
    }
    if (r.type=='parens'){
        var ex:Parens=<any>r;
        var pr="("+nodeToString(ex.expr)+")"
        if (ex.arr){
            pr+="[]"
        }
        return pr;
    }
    if (r.type=='name'){
        var l:Literal=<any>r;
        var val=l.value;
        var pr=val;
        if (l.arr){
            pr+="[]"
        }
        return pr
    }
}
export function validateNode(r:BaseNode,node:hl.IAttribute,cb:hl.ValidationAcceptor){
    if (r.type=="union"){
        var u:Union=<Union>r;
        validateNode(u.first,node,cb);
        validateNode(u.rest,node,cb);
    }
    if (r.type=="responses"){
        var res:Responses=<Responses>r;
        res.codes.forEach(x=>{
              var v=x.code;
              for (var i=0;i<v.length;i++){
                  var c=v[i];
                  if (!_.find(['0','1','2','3','4','5','6','7','8','9','x','X'],x=>x==c)){
                      cb.accept(linter.createIssue(hl.IssueCode.INVALID_VALUE_SCHEMA,"Status code should be 3 digits number with optional 'x' as wildcards",node));
                      return;
                  }
              }

          validateNode(x.expr,node,cb);
        })
        //validateNode(u.first,node,cb);
        //validateNode(u.rest,node,cb);
    }
    if (r.type=='parens'){
        var ex:Parens=<any>r;
        validateNode(ex.expr,node,cb);

    }
    if (r.type=='name'){
        var l:Literal=<any>r;
        var val=l.value;
        if (val.lastIndexOf("[]")!=-1){
            val=val.substr(0,val.length-2);//FIXME Should be in PEG
        }

        var pr=<defs.Property>node.property()
        if (pr.isValidValue(val,node.parent())){
            return;
        }
        var values = pr.enumValues(node.parent());
        values=values.map(x=>{
            var tp=x.indexOf("<");
            if (tp!=-1){
                return x.substring(0,tp);
            }
            return x;
        })
        if (l.params){
            l.params.forEach(x=>{
                validateNode(x,node,cb);
            })
        }
        values.push("number");
        values.push("integer")
        values.push("file");
        values.push("boolean");
        values.push("any")
        values.push("date");
        values.push("void");
        values.push("string");
        values.push("value")
        if (!_.find(values, x=>x == val)) {

            cb.accept(linter.createIssue(hl.IssueCode.UNRESOLVED_REFERENCE,"Unresolved reference:"+val,node));
            return true;
        }
    }

}