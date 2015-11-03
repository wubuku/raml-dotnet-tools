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
import search=require("./search")
type ASTNodeImpl=hlimpl.ASTNodeImpl;
type ASTPropImpl=hlimpl.ASTPropImpl;
class KeyMatcher{

    parentValue:hl.IProperty
    parentKey:hl.IProperty
    canBeValue:hl.IProperty

    constructor(private _props:hl.IProperty[]){
        this.parentValue=_.find(_props,x=>x.isFromParentValue());
        this.parentKey=_.find(_props,x=>x.isFromParentKey());
        this.canBeValue=_.find(_props,x=>(<defs.Property>x).canBeValue());
    }

    match(key:string):hl.IProperty{
        var _res:hl.IProperty=null;
        var lastPref=""

        this._props.forEach(p=>{
            if (p.isSystem()){
                return;
            }
            if (p!=this.parentValue&&p!=this.parentKey&&p.matchKey(key)){
                if (p.keyPrefix()!=null) {
                    if (p.keyPrefix().length >= lastPref.length) {
                        lastPref=p.keyPrefix();
                        _res = p;
                    }
                }
                else{
                    _res=p;
                    lastPref=p.name();
                }
            }
        })
        return _res;
    }
}
var level=0;
export class BasicNodeBuilder implements hl.INodeBuilder{


    process(node:hl.IHighLevelNode, childrenToAdopt:ll.ILowLevelASTNode[]):hl.IParseResult[] {
        try {
            if (level>200){
                return [new hlimpl.BasicASTNode(node.lowLevel(), node)];
            }
            level++;
            if (!node.definition()) {
                return;
            }
            if (node.lowLevel()['c']){
                return;
            }
            var km = new KeyMatcher(node.definition().allProperties());
            var aNode = <ASTNodeImpl>node;

            var allowsQuestion = aNode._allowQuestion || node.definition().getAllowQuestion();
            var res:hl.IParseResult[] = []
            if (km.parentKey) {
                if (node.lowLevel().key()) {
                    res.push(new hlimpl.ASTPropImpl(node.lowLevel(), node, km.parentKey.range(), km.parentKey, true));
                }
            }
            if (node.lowLevel().value()) {
                if (km.parentValue) {
                    res.push(new hlimpl.ASTPropImpl(node.lowLevel(), node, km.parentValue.range(), km.parentValue));
                }
                else if (km.canBeValue) {
                    var s = node.lowLevel().value();
                    if (typeof s == 'string' && (<string>s).trim().length > 0) {
                        res.push(new hlimpl.ASTPropImpl(node.lowLevel(), node, km.canBeValue.range(), km.canBeValue));
                    }
                }
            }
            else {
                if (km.canBeValue && km.canBeValue.range() instanceof def.NodeClass) {
                    var ch = new hlimpl.ASTNodeImpl(node.lowLevel(), aNode, <hl.INodeDefinition>km.canBeValue.range(), km.canBeValue);
                    return [ch];
                }
            }

            aNode._children = res;
            childrenToAdopt.forEach(x=> {
                var key:string = x.key();
                if (key == '$ref' && aNode.universe().version() == "Swagger") {
                    var resolved = search.resolveReference(x, x.value());
                    if (!resolved) {
                        var bnode = new hlimpl.BasicASTNode(x, aNode);
                        bnode.unresolvedRef = "ref";
                        res.push(bnode);
                    }
                    else {
                        var mm = this.process(aNode, resolved.children());
                        mm.forEach(x=> {
                            if (x.property() && x.property().isKey()) {
                                return;
                            }
                            res.push(x);
                        })

                    }
                }
                if (allowsQuestion) {
                    if (key != null && key.charAt(key.length - 1) == '?') {
                        key = key.substr(0, key.length - 1);
                    }
                }
                var p = km.match(key);

                if (p != null) {
                    var range = p.range();
                    var um=false;
                    var multyValue = p.isMultiValue();
                    if (range instanceof def.Array) {
                        var at = <def.Array>range;
                        multyValue = true;
                        range = at.component;
                        um=true;
                    }
                    else if (range.isArray()){
                        multyValue=true;
                        um=true;
                    }
                    //TODO DESCRIMINATORS
                    if (range.isValueType()) {


                        var ch = x.children();
                        var seq = (x.valueKind() == yaml.Kind.SEQ);
                        if ((seq && ch.length > 0 || ch.length > 1) && multyValue) {

                            ch.forEach(y=>{
                                var pi=new hlimpl.ASTPropImpl(y, aNode, range, p)
                                res.push(pi)
                            });

                        }
                        else {

                            if (p.isInherited()) {
                                aNode.setComputed(p.name(), x.value());
                            }
                            res.push(new hlimpl.ASTPropImpl(x, aNode, range, p));
                        }

                        //}
                        return;
                    }
                    else {
                        var rs:ASTNodeImpl[] = [];
                        //now we need determine actual type
                        aNode._children = res;


                        if (!p.isMerged()) {
                            if (multyValue) {
                                if (p.isEmbedMap()) {

                                    var chld = x.children();

                                    if (chld.length == 0) {
                                        if (x.value()) {
                                            var bnode = new hlimpl.BasicASTNode(x, aNode);
                                            bnode.knownProperty = p;
                                            res.push(bnode);
                                        }
                                    }
                                    chld.forEach(y=> {
                                        //TODO TRACK GROUP KEY
                                        var cld = y.children()
                                        if (!y.key() && cld.length == 1) {
                                            var node = new hlimpl.ASTNodeImpl(cld[0], aNode, <any> range, p);
                                            node._allowQuestion = allowsQuestion;
                                            rs.push(node);
                                        }
                                        else {
                                            if (aNode.universe().version() == "RAML10") {
                                                var node = new hlimpl.ASTNodeImpl(y, aNode, <any> range, p);
                                                node._allowQuestion = allowsQuestion;
                                                rs.push(node);
                                            }
                                            else {
                                                var bnode = new hlimpl.BasicASTNode(y, aNode);
                                                res.push(bnode);
                                                if (y.key()) {
                                                    bnode.needSequence = true;
                                                }
                                            }
                                        }
                                    })

                                }
                                else {
                                    var filter:any = {}
                                    var inherited:hlimpl.ASTNodeImpl[] = []
                                    if (range instanceof defs.NodeClass) {
                                        var nc = <defs.NodeClass>range;

                                        if (nc.getCanInherit().length > 0) {
                                            nc.getCanInherit().forEach(v=> {
                                                var vl = aNode.computedValue(v);
                                                if (vl && p.name() == "body") {
                                                    if (!_.find(x.children(), x=>x.key() == vl)) {
                                                        //we can create inherited node;
                                                        var node = new hlimpl.ASTNodeImpl(x, aNode, <any> range, p);
                                                        if (aNode.parent().definition().name() == "MethodBase") {
                                                            node.setComputed("form", "true")//FIXME
                                                        }
                                                        var t = descriminate(p, aNode, node);
                                                        if (t) {
                                                            node.patchType(<any>t)
                                                        }
                                                        var ch = node.children();
                                                        //this are false unknowns actual unknowns will be reported by parent node
                                                        node._children = ch.filter(x=>!x.isUnknown())

                                                        node._allowQuestion = allowsQuestion;
                                                        inherited.push(node);
                                                        node.children().forEach(x=> {
                                                            if (x.property().isKey()) {
                                                                var atr = <ASTPropImpl>x;
                                                                atr._computed = true;
                                                                return;
                                                            }
                                                            if (x.isElement()) {
                                                                if (!x.property().isMerged()) {
                                                                    filter[x.property().name()] = true;
                                                                }
                                                            }
                                                            filter[x.name()] = true;
                                                        })
                                                        node._computed = true;
                                                    }
                                                }
                                            })
                                        }
                                    }
                                    var parsed:hlimpl.ASTNodeImpl[] = []
                                    x.children().forEach(y=> {
                                        if (filter[y.key()]) {
                                            return;
                                        }
                                        var node = new hlimpl.ASTNodeImpl(y, aNode, <any> range, p);
                                        if (p.name() == "body" && p.domain().name() == "MethodBase") {
                                            node.setComputed("form", "true")//FIXME

                                        }
                                        node._allowQuestion = allowsQuestion;
                                        parsed.push(node);
                                    })

                                    if (parsed.length > 0) {
                                        parsed.forEach(x=>rs.push(x));
                                    }
                                    else {
                                        inherited.forEach(x=>rs.push(x))
                                    }
                                }
                            }
                            else {
                                //var y=x.children()[0];
                                rs.push(new hlimpl.ASTNodeImpl(x, aNode, <any> range, p));
                            }
                        }
                        else {
                            var node = new hlimpl.ASTNodeImpl(x, aNode, <any>range, p);
                            node._allowQuestion = allowsQuestion;
                            rs.push(node);
                        }
                        aNode._children = aNode._children.concat(rs);
                        res = res.concat(rs);
                        rs.forEach(x=> {
                            var rt = descriminate(p, aNode, x);
                            if (rt && rt != x.definition()) {
                                x.patchType(<hl.INodeDefinition>rt);
                            }
                            x._associatedDef = null;
                            p.childRestrictions().forEach(y=> {
                                x.setComputed(y.name, y.value)
                            })
                            var def = <hl.INodeDefinition>x.definition();
                        });
                    }
                }
                else {
                    res.push(new hlimpl.BasicASTNode(x, aNode));
                    //error
                }
            })
            aNode._children = res;
            //Functional descriminators are not needed any more
            //aNode._children.forEach(x=> {
            //    if (x instanceof hlimpl.ASTPropImpl) {
            //        var attr = <ASTPropImpl>x;
            //        var p = <defs.Property>attr.property();
            //        var tpes = p.range().name() == "StringType" ? [] : p.range().allSubTypes();
            //        var actualType = p.range();
            //        if (tpes.length > 0) {
            //            var rm = aNode.toRuntimeModel();
            //            tpes.forEach(t=> {
            //                var ds = (<defs.AbstractType><any>t).getFunctionalDescriminator();
            //                if (ds) {
            //                    try {
            //                        var q = hlimpl.evalInSandbox("return " + ds, rm, []);
            //                        if (q) {
            //                            attr.patchType(<any>t)
            //
            //                        }
            //                    } catch (e) {
            //                        //silently ignore
            //                    }
            //                }
            //            });
            //        }
            //    }
            //})
            return res;
        }finally{
           level--;
        }
    }
}
function desc1(p:hl.IProperty, parent:hl.IHighLevelNode, x:hl.IHighLevelNode){
    var tp=x.attr("type");
    var value="";
    if (tp){
        var mn:{ [name:string]:hl.ITypeDefinition}={};
        var c=new def.NodeClass(x.name(),<def.Universe>parent.definition().universe(),"")
        c.setDeclaringNode(x);
        c._superTypes.push(parent.definition().universe().getType("DataElement"));
        mn[tp.value()]=c;
        var newType= typeExpression.getType(parent,tp.value(),mn);
        if (newType instanceof def.Array){
            newType.setDeclaringNode(x);
        }
        return newType;
    }
    else{
        if (p.name()=="body"||_.find(x.lowLevel().children(),x=>x.key()=="properties")){
            return parent.definition().universe().getType("ObjectField");
        }

        return parent.definition().universe().getType("StrElement");
    }
    return null;
}
function descriminate (p:hl.IProperty, parent:hl.IHighLevelNode, x:hl.IHighLevelNode):hl.ITypeDefinition {
    if (p.range().name()=="DataElement"){
        var res=desc1(p,parent,x);
        //FIXME (think about it later)
        if (res!=null&&((p.name()=="body"||p.name()=="headers")||p.name()=="queryParameters")) {
            var ares = new defs.UserDefinedClass(x.lowLevel().key(), <defs.Universe>res.universe(), x,x.lowLevel().unit()?x.lowLevel().unit().path():"", "");
            ares._superTypes.push(res);
            return ares;
        }
        if (res) {
            return res;
        }
    }
    //generic case;
    var rt:hl.ITypeDefinition = null;
    var types = search.findAllSubTypes(p, parent);
    if (types.length > 0) {
        types.forEach(y=> {
            if (!rt) {
                if (y.match(x, rt)) {
                    rt = y;
                }
            }
        })
    }
    return rt;
};