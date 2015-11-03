import RamlWrapper = require('./artifacts/raml003parser')
import ramlPathMatch = require("../util/raml-path-match")
import hl = require('./highLevelAST');
import Opt = require('../Opt')
import util = require('../util/index');

export function completeRelativeUri(res:RamlWrapper.Resource):string{
    var uri = ''
    var parent:any = res;
    do{
        res = <RamlWrapper.Resource>parent;//(parent instanceof RamlWrapper.ResourceImpl) ? <RamlWrapper.Resource>parent : null;
        uri = res.relativeUri().value() + uri;
        parent = res.parent();

    }
    while (parent['relativeUri'] );
    return uri;
}

export function absoluteUri(res:RamlWrapper.Resource):string{
    var uri = ''
    var parent:any = res;
    do{
        res = <RamlWrapper.Resource>parent;//(parent instanceof RamlWrapper.ResourceImpl) ? <RamlWrapper.Resource>parent : null;
        uri = res.relativeUri().value() + uri;
        parent = res.parent();
    }
    while (parent['relativeUri'] );
    uri = uri.replace(/\/\//g,'/');
    var base = (<RamlWrapper.Api>parent).baseUri().value();
    if(util.stringEndsWith(base,'/')){
        uri = uri.substring(1);
    }
    uri = base + uri;
    return uri;
}

export function relativeUriSegments(res:RamlWrapper.Resource):string[]{
    var result:string[] = [];
    var parent:any = res;
    do{
        res = <RamlWrapper.Resource>parent;//(parent instanceof RamlWrapper.ResourceImpl) ? <RamlWrapper.Resource>parent : null;
        result.push(res.relativeUri().value());
        parent = res.parent();
    }
    while (parent['relativeUri'] );
    return result.reverse();
}

export function parentResource(method:RamlWrapper.Method):RamlWrapper.Resource{
    return <RamlWrapper.Resource>method.parent();
}

export function parent(resource:RamlWrapper.Resource):Opt<RamlWrapper.Resource>{
    var parent = resource.parent();
    if(isApi(parent)){
        return Opt.empty<RamlWrapper.Resource>();
    }
    return new Opt<RamlWrapper.Resource>(<RamlWrapper.Resource>parent);
}

export function getChildResource(container:RamlWrapper.Resource|RamlWrapper.Api, relPath:string):Opt<RamlWrapper.Resource>{

    if(container==null){
        return Opt.empty<RamlWrapper.Resource>();
    }
    var resources:RamlWrapper.Resource[] = container.resources();
    if(!resources){
        return Opt.empty<RamlWrapper.Resource>();
    }
    resources = resources.filter(x=>x.relativeUri().value()==relPath);
    if(resources.length==0){
        return Opt.empty<RamlWrapper.Resource>();
    }
    return new Opt<RamlWrapper.Resource>(resources[0]);
}

export function getResource(container:RamlWrapper.Api|RamlWrapper.Resource, path:string[]):Opt<RamlWrapper.Resource>{

    if(!container){
        return null;
    }
    var opt:Opt<RamlWrapper.Resource> = Opt.empty<RamlWrapper.Resource>();
    for (var i = 0; i < path.length; i++) {
        opt = getChildResource(container, path[i]);
        if(!opt.isDefined()){
            return opt;
        }
        container = opt.getOrThrow();
    }
    return opt;
}

export function getChildMethod(resource:RamlWrapper.Resource, method:string):RamlWrapper.Method[]{

    if(!resource){
        return null;
    }
    return resource.methods().filter(x=>x.method()==method);
}

export function getMethod(container:RamlWrapper.Resource|RamlWrapper.Api, path:string[],method:string):RamlWrapper.Method[]{

    var resource = getResource(container,path);
    return getChildMethod(resource.getOrElse(null),method);
}

function isApi(obj) {
    return (obj['title'] && obj['version'] && obj['baseUri']);
};

export function ownerApi(method:RamlWrapper.Method|RamlWrapper.Resource):RamlWrapper.Api{
    var obj:RamlWrapper.BasicNode = method;
    while(!isApi(obj)){
        obj = obj.parent();
    }
    return <RamlWrapper.Api>obj;
}

export function methodId(method:RamlWrapper.Method){
    return completeRelativeUri(parentResource(method)) + ' ' + method.method().toLowerCase();
}


export function isOkRange(response:RamlWrapper.Response):boolean{
    return parseInt(response.code().value()) < 400;
}

export function allResources(api:RamlWrapper.Api):RamlWrapper.Resource[]{

    var resources:RamlWrapper.Resource[] = []
    var visitor = (res:RamlWrapper.Resource) => {
        resources.push(res);
        res.resources().forEach(x=>visitor(x));
    }
    api.resources().forEach(x=>visitor(x));
    return resources;
}

export function matchUri(apiRootRelativeUri:string, resource:RamlWrapper.Resource):Opt<ParamValue[]>{

    var allParameters:Raml08Parser.NamedParameterMap = {}
    var opt:Opt<RamlWrapper.Resource> = new Opt<RamlWrapper.Resource>(resource);
    while(opt.isDefined()){
        var res:RamlWrapper.Resource = opt.getOrThrow();
        uriParameters(res).forEach(x=>allParameters[x.name()]=new ParamWrapper(x));
        opt = parent(res);
    }
    var result = ramlPathMatch(completeRelativeUri(resource), allParameters, {})(apiRootRelativeUri);
    if (result) {
        return new Opt<ParamValue[]>(Object.keys((<any>result).params)
            .map(x=>new ParamValue(x, result['params'][x])));
    }
    return Opt.empty<ParamValue[]>();
}

var schemaContentChars:string[] = [ '{', '<' ]

export function schema(body:RamlWrapper.DataElement, api:RamlWrapper.Api):Opt<SchemaDef>{

    var schemaNode = body.schema();
    if(!schemaNode){
        return Opt.empty<SchemaDef>();
    }
    var schemaString = schemaNode;
    var isContent:boolean = false;
    schemaContentChars.forEach(x=>isContent = isContent||schemaString.indexOf(x)>=0);
    var schDef:SchemaDef;
    if(isContent) {
        schDef = new SchemaDef(schemaString);
    }
    else{
        var globalSchemes = api.schemas().filter(x=>x.key()==schemaString);
        if(globalSchemes.length>0){
            schDef = new SchemaDef(globalSchemes[0].value().value(),globalSchemes[0].key());
        }
        else{
            return Opt.empty<SchemaDef>();
        }
    }
    return new Opt<SchemaDef>(schDef);
}

export function uriParameters(resource:RamlWrapper.Resource):RamlWrapper.DataElement[]{

    var uri = resource.relativeUri().value();
    var params = resource.uriParameters();

    return extractParams(params, uri, resource);
}

export function baseUriParameters(api:RamlWrapper.Api):RamlWrapper.DataElement[]{

    var uri = api.baseUri().value();
    var params = api.baseUriParameters();

    return extractParams(params, uri, api);
}

export function absoluteUriParameters(res:RamlWrapper.Resource):RamlWrapper.DataElement[]{
    var params:RamlWrapper.DataElement[] = []
    var parent:any = res;
    do{
        res = <RamlWrapper.Resource>parent;
        var uri = res.relativeUri().value();
        var uriParams = res.uriParameters();
        params = extractParams(uriParams, uri, res).concat(params);
        parent = res.parent();

    }
    while (parent['relativeUri'] );

    var api = <RamlWrapper.Api>parent;
    var baseUri = api.baseUri().value();
    var baseUriParams = api.baseUriParameters();
    params = extractParams(baseUriParams, baseUri, api).concat(params);
    return params;
}

function extractParams(
    params:RamlWrapper.DataElement[],
    uri:string,
    resource:RamlWrapper.BasicNode):RamlWrapper.DataElement[] {

    var describedParams = {};
    params.forEach(x=>describedParams[x.name()] = x);

    var allParams:RamlWrapper.DataElement[] = []
    var prev = 0;
    for (var i = uri.indexOf('{'); i >= 0; i = uri.indexOf('{', prev)) {
        prev = uri.indexOf('}', ++i);
        var paramName = uri.substring(i, prev);
        if (describedParams[paramName]) {
            allParams.push(describedParams[paramName]);
        }
    else {
            allParams.push(new HelperUriParam(paramName, resource));
        }
    }
    return allParams;
};

export class HelperUriParam implements RamlWrapper.DataElement{

    constructor(private _name:string,private _parent:RamlWrapper.BasicNode ){}

    wrapperClassName():string{ return "HelperUriParam"; }
    name():string{ return this._name; }
    "type"(  ):string[]{ return [ "string" ] }
    location(  ):RamlWrapper.ModelLocation{ return { wrapperClassName: ()=>"HelperModelLocation" }; }
    locationKind(  ):RamlWrapper.LocationKind{ return { wrapperClassName: ()=>"HelperLocationKind" }; }
    "default"(  ):string{return null;}
    sendDefaultByClient(  ):boolean{ return false;}
    example(  ):string{ return ''}
    schema():string{return null}
    formParameters():RamlWrapper.DataElement[]{return []}
    examples(  ):RamlWrapper.ExampleSpec[]{ return []}
    repeat(  ):boolean{ return false; }
    enum(  ):string[] { return [] }
    collectionFormat(  ):string { return 'multi'; }
    required(  ):boolean{ return true; }
    readOnly(  ):boolean{ return false }
    facets(){return []}
    scope(  ):string[] { return []; }
    //xml(  ):RamlWrapper.XMLInfo{ return null; }
    validWhen(  ):RamlWrapper.ramlexpression{ return null; }
    requiredWhen(  ):RamlWrapper.ramlexpression{ return null; }

    displayName(  ):string{ return this._name;}
    description(  ):RamlWrapper.MarkdownString{ return null; }
    annotations(  ):RamlWrapper.AnnotationRef[]{ return []; }

    usage(){return null;}
    parent():RamlWrapper.BasicNode{ return this._parent;}
    highLevel():hl.IHighLevelNode{
        return null;
    }
}


export class SchemaDef{

    constructor(private _content:string, private _name?:string){}

    name():string{return this._name}

    content(): string{return this._content}
}


export class ParamValue{
    key:string
    value:any

    constructor(key:string, value:any) {
        this.key = key;
        this.value = value;
    }
}


class ParamWrapper implements Raml08Parser.BasicNamedParameter{

    constructor(private _param:RamlWrapper.DataElement){

        this.description = _param.description() ? _param.description().value() : this.description;

        this.displayName = _param.displayName();

//        this.enum = _param.enum();

        this.type = _param.type().length > 0 ? _param.type()[0] : "string";

        this.example = _param.example();

        this.repeat = _param.repeat();

        this.required = _param.required();

        this.default = _param.default();
    }

    description: Raml08Parser.MarkdownString

    displayName: string

    'enum': any[]

    type: string

    example: any

    repeat: boolean

    required: boolean

    'default': any

}
