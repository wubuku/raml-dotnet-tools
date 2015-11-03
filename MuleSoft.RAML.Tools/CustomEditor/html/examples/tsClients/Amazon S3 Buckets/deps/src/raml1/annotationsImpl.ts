/// <reference path="../../typings/tsd.d.ts" />
import RamlWrapper = require('./artifacts/raml003parser')
import TS = require("../ramlscript/TSDeclModel")
import raml2ts1 = require('../ramlscript/raml2ts1')
import HL = require('./highLevelImpl');
import json = require("./jsyaml/json2lowLevel");

export class PagingImpl<COMPONENT,CONTAINER,OPTIONS>{

    constructor(
        private method:(options:any)=>any,
        private pageSizeParam:string,
        private offsetParam:string,
        private collectionField?:string
    ){}

    forEach( f:(x:COMPONENT)=>any,options:OPTIONS = <OPTIONS>{} ):void{

        var defaultPageSize = 5;
        options[this.pageSizeParam] = defaultPageSize;
        for(var i = 0 ; true ; i++){
            options[this.offsetParam] = i*defaultPageSize;
            var response:CONTAINER = this.method(options);
            var collection:COMPONENT[] = this.getMainCollection(response);
            if(!collection){
                break;
            }
            collection.forEach(f);
        }
    }

    itemAt( i:number, options:any = {} ):COMPONENT{

        options.form_limit = 1;
        options.form_skip = i;
        var response:CONTAINER = this.method(options);
        var collection:any[] = this.getMainCollection(response);
        if(!collection){
            return null;
        }
        return collection[0];
    }

    private getMainCollection(response:CONTAINER):COMPONENT[]{

        if(this.collectionField){
            var f = response[this.collectionField];
            return Array.isArray(f) && f.length > 0 ? f : null;
        }

        var keys:string[] = Object.keys(response);
        for( var i = 0 ; i < keys.length ; i++ ){
            var f = response[keys[i]];
            if(Array.isArray(f)){
                return f.length > 0 ? f : null;
            }
        }
        null;
    }
}

export class PagingImplAsync<COMPONENT,CONTAINER,OPTIONS>{

    constructor(
        private method:(options:any)=>Promise<any>,
        private pageSizeParam:string,
        private offsetParam:string,
        private collectionField?:string
    ){}

    forEach( f:(x:COMPONENT)=>any,options:OPTIONS = <OPTIONS>{} ):void{

        var defaultPageSize = 5;
        options[this.pageSizeParam] = defaultPageSize;
        var exec = function(i:number){

            options[this.offsetParam] = i*defaultPageSize;
            var response:Promise<CONTAINER> = this.method(options);
            response.then(cont=>{
                var collection:COMPONENT[] = this.getMainCollection(cont);
                if(collection) {
                    collection.forEach(f);
                    exec(i+1);
                }
            });
        };
        exec(0);
    }

    itemAt( i:number, options:any = {} ):Promise<COMPONENT>{

        options.form_limit = 1;
        options.form_skip = i;
        var response:Promise<CONTAINER> = this.method(options);
        return response.then(cont=> {
            var collection:any[] = this.getMainCollection(cont);
            if (!collection) {
                return null;
            }
            return collection[0];
        });
    }

    private getMainCollection(response:CONTAINER):COMPONENT[]{

        if(this.collectionField){
            var f = response[this.collectionField];
            return Array.isArray(f) && f.length > 0 ? f : null;
        }

        var keys:string[] = Object.keys(response);
        for( var i = 0 ; i < keys.length ; i++ ){
            var f = response[keys[i]];
            if(Array.isArray(f)){
                return f.length > 0 ? f : null;
            }
        }
        null;
    }
}


export class AnnotationProcessor {

    constructor(
        protected method:raml2ts1.TSFullyMappedApiElement,
        protected parent:TS.TSTypeDeclaration ){}

    processAnnotations() {
        this.method.originalMethod.annotations().map(x=>x.value()).forEach(x=> {

            if (<any>x instanceof HL.StructuredValue) {
                var annotation:HL.StructuredValue = <HL.StructuredValue><any>x;
                if (annotation.valueName().indexOf('.paging') > 0) {
                    this.processPagingAnnotation(annotation);
                }
            }
        });
    }

     private processPagingAnnotation(ann:HL.StructuredValue){
         new PagingImplField(this.parent,this.method);
         new InitPagingMethod(this.parent,this.method,ann);
         new ForEachMethod(this.parent,this.method);
         new ItemAtMethod(this.parent,this.method);
     }

}

class InitPagingMethod extends TS.TSAPIElementDeclaration {

    originalResource:RamlWrapper.Resource;

    constructor(
        p:TS.TSModelElement<any>,
        private method:raml2ts1.TSFullyMappedApiElement,
        private annotation:HL.StructuredValue) {

        super(p, 'initPaging');

        var aParams:HL.StructuredValue[] = annotation.children();

        var map={}
        aParams.forEach(x=>{
            var paramPath = x.lowLevel().value();
            var prefix:string =
                paramPath.indexOf('application/x-www-form-urlencoded')>=0 || paramPath.indexOf('application/form-data')>=0
                ? 'form_': '';

            var paramName = prefix + paramPath.substring(paramPath.lastIndexOf('.')+1);
            map[x.valueName()] = paramName;
        })

        var collectionField = extractCollectionField(this.method.rangeType);
        var fName = collectionField.name;
        var implName = isAsync(method.rangeType) ? 'PagingImplAsync' : 'PagingImpl';

        var typeParamStr = [
            extractComponentType(this.method.rangeType, this),
            this.method.rangeType
        ].concat(this.method.parameters.map(p=>p.ptype)).map(p => p.serializeToString()).join(',');

        this._body = `
            if(!this.pagingImpl){
                this.pagingImpl = new AnnotationsImpl.${implName}<${typeParamStr}>(this.${this.method.name},'${map['pageSize']}','${map['offset']}','${fName}');
            }
        `;
        this.rangeType = new TS.TSSimpleTypeReference(this,'void');
        this.isPrivate = true;
        this.isFunc = true;
    }
    isInterfaceMethodWithBody():boolean{ return true; }

    body():string{
        return this._body;
    }
}


class ForEachMethod extends TS.TSAPIElementDeclaration {

    originalResource:RamlWrapper.Resource;

    componentType:TS.TSTypeReference<any>;

    constructor(p:TS.TSModelElement<any>,private method:raml2ts1.TSFullyMappedApiElement) {
        super(p, 'forEach');
        this.initParams();
        this._body = `
            this.initPaging();
            return this.pagingImpl.forEach(${this.parameters.map(x=>x.name).join(',')});
        `;
        this.rangeType = new TS.TSSimpleTypeReference(this,'void');
        this.componentType = extractComponentType(this.method.rangeType, this);
    }

    commentCode():string{

        return `
        /**
         * Apply function to all the elements of the collection
         **/
            `
    }

    private initParams(){

        var functionType:TS.TSFunctionReference = new TS.TSFunctionReference(this);
        functionType.parameters.push(
        new TS.Param(this,'x',TS.ParamLocation.OTHER,extractComponentType(this.method.rangeType,this),null) );

        this.parameters.push(
        new TS.Param(this, 'f', TS.ParamLocation.OTHER, functionType, null) );

        this.method.parameters.forEach((x,i)=> {
            var optionsParam:TS.Param = new TS.Param(this, 'options' + (i > 0 ? i : ''), TS.ParamLocation.OTHER, x.ptype, {})
            this.parameters.push(optionsParam);
        });
    }

    isInterfaceMethodWithBody():boolean{ return true; }

    body():string{
        return this._body;
    }
}


class ItemAtMethod extends TS.TSAPIElementDeclaration {

    originalResource:RamlWrapper.Resource;

    componentType:TS.TSTypeReference<any>;

    constructor(p:TS.TSModelElement<any>,private method:raml2ts1.TSFullyMappedApiElement) {
        super(p, 'itemAt');
        this.initParams();
        this._body = `
            this.initPaging();
            return this.pagingImpl.itemAt(${this.parameters.map(x=>x.name).join(',')});
        `;
        this.componentType = extractComponentType(this.method.rangeType, this);
        if(isAsync(method.rangeType)){
            this.rangeType = new TS.TSSimpleTypeReference(this,'Promise');
            (<TS.TSSimpleTypeReference>this.rangeType).typeParameters = [ this.componentType ];
        }
        else{
            this.rangeType = this.componentType;
        }
    }

    commentCode():string{

        return `
        /**
         * Retrieve certain element of the collection
         * @param i element index
         **/
            `
    }

    private initParams(){

        this.parameters.push(
            new TS.Param(this, 'i', TS.ParamLocation.OTHER, new TS.TSSimpleTypeReference(this,'number')));

        this.method.parameters.forEach((x,i)=> {
            var optionsParam:TS.Param = new TS.Param(this, 'options' + (i > 0 ? i : ''), TS.ParamLocation.OTHER, x.ptype, {})
            this.parameters.push(optionsParam);
        });
    }

    isInterfaceMethodWithBody():boolean{ return true; }

    body():string{
        return this._body;
    }
}

class PagingImplField extends TS.TSAPIElementDeclaration {

    originalResource:RamlWrapper.Resource;

    constructor(p:TS.TSModelElement<any>,private method:raml2ts1.TSFullyMappedApiElement) {
        super(p, 'pagingImpl');
        var implName = isAsync(method.rangeType) ? 'PagingImplAsync' : 'PagingImpl';
        var ref:TS.TSSimpleTypeReference = new TS.TSSimpleTypeReference(this,`AnnotationsImpl.${implName}`);
        this.rangeType = ref;
        ref.typeParameters = [
            extractComponentType(this.method.rangeType,this),
            this.method.rangeType
        ].concat(this.method.parameters.map(p=>p.ptype));
        this.isPrivate = true;
    }
}

function extractComponentType(rangeType:TS.TSTypeReference<any>,parent:TS.TSModelElement<any>):TS.TSTypeReference<any>{

    var collectionField:TS.TSAPIElementDeclaration = extractCollectionField(rangeType);
    if(!collectionField){
        return new TS.AnyType();
    }
    var componentType = (<TS.TSArrayReference>collectionField.rangeType).componentType;
    return componentType;
}

function extractCollectionField(rangeType:TS.TSTypeReference<any>):TS.TSAPIElementDeclaration{

    if(isAsync(rangeType)){
        var params = (<TS.TSSimpleTypeReference>rangeType).typeParameters;
        if(params && params.length>0){
            return extractCollectionField(params[0]);
        }
    }
    if(rangeType instanceof TS.TSDeclaredInterfaceReference){
        var original = (<TS.TSDeclaredInterfaceReference>rangeType).getOriginal();
        var children:any[] = original.children();
        for(var i = 0 ; i < children.length ; i++){
            var ch = children[i];
            if(ch.rangeType.array()){
                return ch;
            }
        }
    }
    return null;
}

function isAsync(rangeType:TS.TSTypeReference<any>):boolean{

    if(rangeType instanceof TS.TSSimpleTypeReference){
        var name = (<TS.TSSimpleTypeReference>rangeType).name;
        if(name=='Promise'){
            return true;
        }
    }
    return false;
}

