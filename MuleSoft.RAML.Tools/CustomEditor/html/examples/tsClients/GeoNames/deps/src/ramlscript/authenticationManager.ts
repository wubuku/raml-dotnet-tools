/**
 * Created by Sviridov on 5/12/2015.
 */

import RamlWrapper = require('../Raml08Wrapper')
import RamlWrapper1 = require('../raml1/artifacts/raml003parser')
import Helper = require('../raml1/wrapperHelper')
import hlImpl = require("../raml1/highLevelImpl")

import Opt = require('../Opt')
import util = require('../util/index')

export var SCHEMA_TYPE_OAUTH2 = 'OAuth 2.0';
export var SCHEMA_TYPE_OAUTH1 = 'OAuth 1.0';
export var SCHEMA_TYPE_DIGEST = 'Digest Authentication'
export var SCHEMA_TYPE_BASIC = 'Basic Authentication'

var optionsMapping = {
    "clientId" : "clientId"
}


export interface SecurityParametersProvider{

    getValue(name:string,loc?:RamlWrapper.ParamLocation):string

    writeValue(name:string,value:string,loc?:RamlWrapper.ParamLocation)

    getSubProvider(name:string):SecurityParametersProvider
}

export class AuthManager{

    constructor(private paramsProvider:SecurityParametersProvider) {}

    apiMap:{[key:string]:ApiSecurityData} = {}

    public patchRequest(request:har.Request,method:RamlWrapper.Method|RamlWrapper1.Method):void{

        var api:RamlWrapper.Api|RamlWrapper1.Api = apiForMethod(method);
        var title:string = apiTitle(api);
        var apiData:ApiSecurityData = this.apiMap[title];
        apiData.patchRequest(request,method);
    }

    registerApi(api:RamlWrapper.Api|RamlWrapper1.Api):void{
        var title:string = apiTitle(api)
        var data:ApiSecurityData = this.apiMap[title];
        if(!data) {
            data = new ApiSecurityData(title, this.paramsProvider.getSubProvider(title))
            this.apiMap[title] = data
        }
        data.registerApiVersion(api);
    }

    registerSchemes(api:RamlWrapper.Api|RamlWrapper1.Api,schemes:IAuthSchema[]):void{
        var title:string = apiTitle(api);
        var data:ApiSecurityData = this.apiMap[title];
        if(data) {
            data.registerSchemes(api,schemes);
        }
    }

    store(apiName?:string,apiVersion?:string){
        if(apiName){
            var apiData:ApiSecurityData = this.apiMap[apiName]
            if(apiData){
                apiData.store(apiVersion);
            }
        }
        else{
            Object.keys(this.apiMap).forEach(x=>this.apiMap[x].store());
        }
    }

    updateSchema(api:RamlWrapper.Api|RamlWrapper1.Api,schemaName:string,options:any){
        var apiData:ApiSecurityData = this.apiMap[apiTitle(api)]
        if(!apiData) {
            return
        }
        apiData.updateSchema(api,schemaName,options)
    }

    isReady():boolean{
        var result:boolean = true
        Object.keys(this.apiMap).forEach(x=>result = result && this.apiMap[x].isReady())
        return result
    }
}

export interface IAuthSchema{

    apply(request:har.Request)

    store()

    update(options:any)

    isReady():boolean

    setSecurityProvider(paramsProvider:SecurityParametersProvider)

    name():string
}

class AuthSchema implements IAuthSchema{

    constructor(
        private ramlSchema:RamlWrapper.SecuritySchemaDef|RamlWrapper1.SecuritySchema,
        private provider:SecurityParametersProvider){
        this.init()
    }

    headers:Parameter[] = []

    queryParams:Parameter[] = []

    options:Object = {}
    
    schemaType:string

    setSecurityProvider(paramsProvider:SecurityParametersProvider){
        this.provider = paramsProvider
    }

    name(){
        return this.ramlSchema.name();
    }

    private init(){
        this.schemaType
            = this.ramlSchema instanceof RamlWrapper.SecuritySchemaDef
            ? (<RamlWrapper.SecuritySchemaDef>this.ramlSchema).type().type().getOrThrow()
            : (<RamlWrapper1.SecuritySchema>this.ramlSchema).type();

        var headers:(RamlWrapper.Param|RamlWrapper1.DataElement)[] = [];
        var queryParameters:(RamlWrapper.Param|RamlWrapper1.DataElement)[] = [];

        var describedBy:RamlWrapper.SecuritySchemaType|RamlWrapper1.SecuritySchemaPart
            = this.ramlSchema instanceof RamlWrapper.SecuritySchemaDef
            ? (<RamlWrapper.SecuritySchemaDef>this.ramlSchema).type()
            : (<RamlWrapper1.SecuritySchema>this.ramlSchema).describedBy();

        if(describedBy) {
            headers = describedBy.headers();
            queryParameters = describedBy.queryParameters();
        }

        if(this.schemaType == SCHEMA_TYPE_OAUTH2){
            this.getOption('clientId')
            this.getOption('clientSecret')
            var accessToken = this.getOption('accessToken')
            if(accessToken){
                if(headers.filter(x=>x.name()=='Authorization').length>0) {
                    this.headers = [{name: 'Authorization', value: 'Bearer ' + accessToken}]
                }
                queryParameters.forEach(x=> {
                    if (x.name().toLowerCase().indexOf('token') >= 0) {
                        this.queryParams.push({
                            name: x.name(),
                            value: accessToken
                        })
                    }
                })
            }
        }
        else if(this.schemaType == SCHEMA_TYPE_OAUTH1){

        }
        else if(this.schemaType == SCHEMA_TYPE_DIGEST){

        }
        else if(this.schemaType == SCHEMA_TYPE_BASIC){
            var username:string = this.getOption('username')
            var password:string = this.getOption('password')
            if(username&&password) {
                var authHeaderValue = 'Basic ' + new Buffer(username + ':' + password).toString('base64');
                this.headers = [{name: 'Authorization', value: authHeaderValue}]
            }
        }
        else  if(this.schemaType.indexOf('x-')==0){
            headers.forEach( x=>
                this.headers.push( {
                    name: x.name(),
                    value: this.provider.getValue(x.name(),RamlWrapper.ParamLocation.header)
                } )
            )
            queryParameters.forEach(x=>{
                this.queryParams.push( {
                    name: x.name(),
                     value: this.provider.getValue(x.name(),RamlWrapper.ParamLocation.query)
                } )
            })
        }
    }

    store(){
        if(this.schemaType == SCHEMA_TYPE_OAUTH2){
            this.saveOption('clientId')
            this.saveOption('clientSecret')
            this.saveOption('accessToken')
        }
        else if(this.schemaType == SCHEMA_TYPE_OAUTH1){

        }
        else if(this.schemaType == SCHEMA_TYPE_DIGEST){

        }
        else if(this.schemaType == SCHEMA_TYPE_BASIC){
            this.saveOption('username')
            this.saveOption('password')
        }
        else  if(this.schemaType.indexOf('x-')==0){
            this.queryParams.forEach(x=>this.provider.writeValue(x.name,x.value,RamlWrapper.ParamLocation.query))
            this.headers.forEach(x=>this.provider.writeValue(x.name,x.value,RamlWrapper.ParamLocation.header))
        }
    }

    private getOption(key:string):string{
        var value:string = this.provider.getValue(key)
        if(value){
            this.options[key] = value
        }
        return value
    }

    private saveOption(key:string){
        var value:string = this.options[key]
        if(value){
            this.provider.writeValue(key,value)
        }
    }

    apply(request:har.Request):boolean{

        this.init();

        if(this.headers.length==0&&this.queryParams.length==0){
            return false
        }
        var applied:boolean = true;
        if(this.headers.length != 0) {
            request.headers = request.headers ? request.headers : []
            applied = applied && this.applyParams(this.headers, request.headers);
        }

        if(this.queryParams.length != 0) {
            request.queryString = request.queryString ? request.queryString : []
            applied = applied && this.applyParams(this.queryParams, request.queryString);
        }
        return applied
    }

    private applyParams(schemaParams:Parameter[], requestParams:har.Header[]|har.QueryParameter[]):boolean
    {
        var appliable:boolean = true;
        schemaParams.forEach(x=> {
            var selected = requestParams.filter(z=>z.name == x.name)
            if (selected && selected.length != 0) {
                var definedParams = selected.filter(z=>{
                    if(z.value==undefined||z.value==null){
                        return false;
                    }
                    if(z.toString().trim().length==0){
                        return false
                    }
                    return true;
                })
                appliable = appliable && ((x.value!=undefined)||definedParams.length>0);
            }
            else {
                appliable = appliable && (x.value!=undefined);
            }
        })
        if(appliable) {
            schemaParams.forEach(x=> {
                var selected = requestParams.filter(z=>z.name == x.name)
                if (selected && selected.length != 0) {
                    selected.forEach(z=>{
                        if(z.value==undefined || z.value == null || z.value.toString().trim().length==0) {
                            z.value = x.value
                        }
                    })
                }
                else {
                    requestParams.push(x)
                }
            })
        }
        return appliable
    }

    update(options:any){

        if(this.schemaType==SCHEMA_TYPE_OAUTH2){
            if(options){
                if(options['options']){
                    this.options['clientId'] = options['options']['clientId']
                    this.options['clientSecret'] = options['options']['clientSecret']
                }
                if(options['user']){
                    this.options['accessToken'] = options['user']['accessToken']
                }
            }
        }
        else if(this.schemaType==SCHEMA_TYPE_BASIC){

        }        
        this.store()
        this.init()
    }

    isReady():boolean{
        var result:boolean = true
        if(this.schemaType == SCHEMA_TYPE_OAUTH2){
            result = result && util.isEssential(this.options['accessToken'])
        }
        else if(this.schemaType == SCHEMA_TYPE_OAUTH1){

        }
        else if(this.schemaType == SCHEMA_TYPE_DIGEST){

        }
        else if(this.schemaType == SCHEMA_TYPE_BASIC){
            result = util.isEssential(this.options['username']) && util.isEssential(this.options['password'])
        }
        else  if(this.schemaType.indexOf('x-')==0){
            this.queryParams.forEach(x => result = result && util.isEssential(x.value))
            this.headers.forEach(x => result = result && util.isEssential(x.value))
        }
        return result
    }

}

class Parameter{

    name:string

    value:string
}

class ApiSecurityData{

    constructor(private _title:string, private paramsProvider:SecurityParametersProvider){}

    title():string{ return this._title; }

    versionsMap:{[key:string]:ApiVersionSecurityData} = {}

    registerApiVersion(api:RamlWrapper.Api|RamlWrapper1.Api){
        var version:string = apiVersion(api);
        this.versionsMap[version] = new ApiVersionSecurityData(api,this.paramsProvider.getSubProvider(version));
    }

    patchRequest(request:har.Request,method:RamlWrapper.Method|RamlWrapper1.Method):void{

        var api:RamlWrapper.Api|RamlWrapper1.Api = apiForMethod(method);
        var version:string = apiVersion(api);
        var versionData:ApiVersionSecurityData = this.versionsMap[version];
        versionData.patchRequest(request,method);
    }

    registerSchemes(api:RamlWrapper.Api|RamlWrapper1.Api,schemes:IAuthSchema[]):void{
        var version:string = apiVersion(api);
        var data:ApiVersionSecurityData = this.versionsMap[version];
        if(data) {
            data.registerSchemes(schemes);
        }
    }

    store(apiVersion?:string){
        if(apiVersion){
            var versionData:ApiVersionSecurityData = this.versionsMap[apiVersion]
            if(versionData){
                versionData.store();
            }
        }
        else{
            Object.keys(this.versionsMap).forEach(x=>this.versionsMap[x].store());
        }
    }

    updateSchema(api:RamlWrapper.Api|RamlWrapper1.Api,schemaName:string,options:any){
        var versionData:ApiVersionSecurityData = this.versionsMap[apiVersion(api)]
        if(!versionData) {
            return
        }
        versionData.updateSchema(schemaName,options)
    }

    isReady():boolean{
        var result:boolean = true
        Object.keys(this.versionsMap).forEach(x=>result = result && this.versionsMap[x].isReady())
        return result
    }
}

class ApiVersionSecurityData{

    constructor(private api:RamlWrapper.Api|RamlWrapper1.Api, private paramsProvider:SecurityParametersProvider){
        this.init()
    }

    methodToSchemaMap:{[key:string]:string[]} = {}

    schemaMap:{[key:string]:IAuthSchema} = {}

    //TODO implement personal storage parts for each API and version
    //apiTitle:string
    //
    //apiVersion:string

    public patchRequest(request:har.Request,method:RamlWrapper.Method|RamlWrapper1.Method):void{

        var methodId:string
            = (method instanceof RamlWrapper.Method)
            ? method.id().value()
            : Helper.methodId(<RamlWrapper1.Method>method);

        var schemas:string[] = this.methodToSchemaMap[methodId]
        if(schemas){
            for(var i = 0 , applied = false; !applied && i < schemas.length ; i++){
                applied = this.schemaMap[schemas[i]].apply(request)
            }
        }
    }

    registerSchemes(schemes:IAuthSchema[]){
        schemes.forEach(x=>{
            this.schemaMap[x.name()] = x;
            x.setSecurityProvider(this.paramsProvider.getSubProvider(x.name()));
        })
    }


    private init():void{
        var securitySchemes:(RamlWrapper.SecuritySchemaDef|RamlWrapper1.SecuritySchema)[]
            = this.api instanceof RamlWrapper.Api
            ? (<RamlWrapper.Api>this.api).securitySchemas()
            : (<RamlWrapper1.Api>this.api).securitySchemes();

        securitySchemes.forEach(x=>this.schemaMap[x.name()]
            = new AuthSchema(x, this.paramsProvider.getSubProvider(x.name())));

        var resources:(RamlWrapper.Resource|RamlWrapper1.Resource)[] = this.api.resources();
        resources.forEach(x=>this.inspectResource(x));
    }

    private inspectResource(resource:RamlWrapper.Resource|RamlWrapper1.Resource):void{
        var globalSecuredBy:string[]
            = this.api instanceof RamlWrapper.Api
            ? (<RamlWrapper.Api>this.api).securedBy().filter(x=>x.isDefined()).map(x=>x.getOrThrow())
            : (<RamlWrapper1.Api>this.api).securedBy().map(x=>x.value());

        var methods:(RamlWrapper.Method|RamlWrapper1.Method)[] = resource.methods();
        methods.forEach(method=>{

            var id:string
                = method instanceof RamlWrapper.Method
                ? (<RamlWrapper.Method>method).id().value()
                : Helper.methodId(<RamlWrapper1.Method>method);

            var localSecuredBy:string[]
                = method instanceof RamlWrapper.Method
                ? (<RamlWrapper.Method>method).securedBy().filter(x=>x.isDefined()).map(x=>x.getOrThrow())
                : (<RamlWrapper1.Method>method).securedBy().map(x=>(<hlImpl.StructuredValue><any>x.value()).valueName());

            this.registerSchemasForMethod(localSecuredBy, id);
            if(!this.methodToSchemaMap[id]){
                this.registerSchemasForMethod(globalSecuredBy, id);
            }
        })
        var resources:(RamlWrapper.Resource|RamlWrapper1.Resource)[] = resource.resources();
        resources.forEach(x=>this.inspectResource(x));
    }

    private registerSchemasForMethod(localSecuredBy:string[], id:string) {
        localSecuredBy.forEach(x=> {
            var schema = this.schemaMap[x];
            if (!schema) {
                return
            }
            var methodSchemas:string[] = this.methodToSchemaMap[id]
            if (!methodSchemas) {
                methodSchemas = []
                this.methodToSchemaMap[id] = methodSchemas
            }
            methodSchemas.push(x)
        })
    }
    
    store(){
        Object.keys(this.schemaMap).forEach(x=>this.schemaMap[x].store())
    }

    updateSchema(schemaName:string,options:any){
        var schema:IAuthSchema = this.schemaMap[schemaName]
        if(schema){
            schema.update(options)
        }
    }

    isReady():boolean{
        var result:boolean = true
        Object.keys(this.schemaMap).forEach(x=> result = result && this.schemaMap[x].isReady())
        return result
    }
            
}

function apiVersion(api:RamlWrapper.Api|RamlWrapper1.Api):string{
    if(api instanceof RamlWrapper.Api) {
        var api08 = <RamlWrapper.Api>api
        return api08.version().getOrElse("V")
    }
    else{
        var api10 = <RamlWrapper1.Api>api
        return api10.version();
    }

}


function apiTitle(api:RamlWrapper.Api|RamlWrapper1.Api):string{
    if(api instanceof RamlWrapper.Api) {
        var api08 = <RamlWrapper.Api>api
        return api08.title().getOrElse("T0")
    }
    else{
        var api10 = <RamlWrapper1.Api>api
        return api10.title();
    }

}

function apiForMethod(method:RamlWrapper.Method|RamlWrapper1.Method):RamlWrapper.Api|RamlWrapper1.Api {
    return (method instanceof RamlWrapper.Method)
        ? (<RamlWrapper.Method>method).api()
        : Helper.ownerApi(<RamlWrapper1.Method>method);
}