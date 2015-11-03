/// <reference path="../../../typings/tsd.d.ts" />

/**
 * Created by kor on 05/05/15.
 */
import Error=require("./js-yaml/exception")
import lowlevel=require("../lowLevelAST")
import highlevel=require("../highLevelAST")
import yaml=require("./yamlAST")
import util=require("../../util/index")
import llImpl=require("./jsyaml2lowLevel")

export class CompilationUnit implements lowlevel.ICompilationUnit{

    constructor(
        protected _absolutePath:string,
        protected _path:string,
        protected _content:string,
        protected _project:lowlevel.IProject,
        protected _isTopoLevel:boolean){

        this._node = new AstNode(this,JSON.parse(this._content));
    }

    protected _node:AstNode;

    absolutePath(){
        return this._absolutePath;
    }

    contents(){
        return this._content;
    }

    path(){
        return this._content;
    }

    isTopLevel(){
        return this._isTopoLevel;
    }

    ast(){
        return this._node;
    }

    isDirty(){
        return true;
    }

    isRAMLUnit(){
        return true;
    }

    project(){
        return this._project;
    }

    updateContent(newContent:string){}

    ramlVersion():string{

        throw 'not implemented';
    }
    
    resolve(p: string) : lowlevel.ICompilationUnit { return null; } // TODO FIXME 
}

//export interface IProject{
//    units():ICompilationUnit[];//returns units with apis in this folder
//
//    execute(cmd:CompositeCommand)
//
//    executeTextChange(textCommand:TextChangeCommand);//this may result in broken nodes?
//
//    addListener(listener:IASTListener);
//
//    removeListener(listener:IASTListener)
//
//    addTextChangeListener(listener:ITextChangeCommandListener);
//    removeTextChangeListener(listener:ITextChangeCommandListener);
//}

//export interface IASTListener{
//    (delta:ASTDelta)
//}
//
//export interface ITextChangeCommandListener{
//    (delta:TextChangeCommand)
//}
//export class ASTDelta{
//    commands:ASTChangeCommand[]
//}
//export interface ASTVisitor{
//    (node:ILowLevelASTNode):boolean
//}

export class AstNode implements lowlevel.ILowLevelASTNode{



    constructor(
        private _unit:CompilationUnit,
        protected _object:any,
        protected _parent ?:AstNode,
        protected _key?:string){

        if(this._object instanceof Object) {
            Object.keys(this._object).forEach(x=> {
                var u = unescapeKey(x);
                if (u != x) {
                    var val = this._object[x];
                    delete this._object[x];
                    this._object[u] = val;
                }
            });
        }
    }


    private _highLevelNode:highlevel.IHighLevelNode

    private _highLevelParseResult:highlevel.IParseResult


    start(){ return -1; }

    end(){ return -1; }

    value(){
        return this._object;
    }

    includeErrors(){return [];}

    includePath(){return null;}

    key(){ return this._key; }

    children(){

        if(!this._object){
            return [];
        }

        if(Array.isArray(this._object)){
            return this._object.map(x=>new AstNode(this._unit,x,this));
        }
        else if(this._object instanceof Object){
            return Object.keys(this._object).map(x=>new AstNode(this._unit,this._object[x],this,x));
        }
        else{
            return [];
        }
    }

    parent(){ return this._parent; }

    unit(){ return this._unit; }

    anchorId(){ return null; }

    errors(){ return []; }

    anchoredFrom(){ return this; }

    includedFrom(){ return this; }

    visit(v:lowlevel.ASTVisitor){
        if(v(this)){
            this.children().forEach(x=>x.visit(v));
        }
    }
    dumpToObject(){
        return this._object;
    }

    addChild(n:lowlevel.ILowLevelASTNode){}

    execute(cmd:lowlevel.CompositeCommand){}

    dump(){ return JSON.stringify(this._object)}

    keyStart(){ return -1; }

    keyEnd(){ return -1; }

    valueStart(){ return -1; }

    valueEnd(){ return -1; }

    isValueLocal(){ return true; }

    kind(){
        if(Array.isArray(this._object)){
            return yaml.Kind.SEQ;
        }
        else if(this._object instanceof Object){
            return yaml.Kind.MAP;
        }
        else{
            return yaml.Kind.SCALAR;
        }
    }

    valueKind(){ return null;}

    show(msg: string){}

    setHighLevelParseResult(highLevelParseResult:highlevel.IParseResult){
        this._highLevelParseResult = highLevelParseResult;
    }

    highLevelParseResult():highlevel.IParseResult{
        return this._highLevelParseResult;
    }

    setHighLevelNode(highLevel:highlevel.IHighLevelNode){
        this._highLevelNode = highLevel;
    }

    highLevelNode():highlevel.IHighLevelNode{
        return this._highLevelNode;
    }

    text(unitText:string):string {
        throw "not implemented";
    }

    copy():AstNode{
        throw "not implemented";
    }

    markup(json?: boolean): string {
        throw "not implemented";
    }

    nodeDefinition(): highlevel.INodeDefinition{
        return llImpl.getDefinitionForLowLevelNode(this);
    }
}

export function serialize(node:lowlevel.ILowLevelASTNode):any{

    if(node.children().length==0){
        if(node.value()){
            return node.value();
        }
        return '';
    }

    if(!node.children()[0].key()){
        var arr = []
        node.children().forEach(x=> {
            arr.push(serialize(x));
        });
        return arr;
    }
    else {
        var obj = {};
        node.children().forEach(x=> {
            obj[escapeKey(x.key())] = serialize(x);
        });
        return obj;
    }

}

function escapeKey(key:string):string{
    if(!key){
        return key;
    }
    if(key.replace(/\d/g,'').trim().length==0){
        return '__$EscapedKey$__' +key;
    }
    return key;

}

function unescapeKey(key:string):string{
    if(!key){
        return key;
    }
    if(util.stringStartsWith(key,'__$EscapedKey$__')){
        return key.substring('__$EscapedKey$__'.length);
    }
    return key;
}