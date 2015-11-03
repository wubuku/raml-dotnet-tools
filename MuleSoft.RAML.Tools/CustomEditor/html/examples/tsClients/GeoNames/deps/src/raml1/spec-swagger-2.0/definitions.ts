import  MetaModel = require("../metamodel")
import  Sys = require("../spec-0.8/systemTypes")
import pars=require('./paths')
export class DefinitionsObject extends pars.SchemaObject{

    name:string
    $name=[MetaModel.key()]

}
export class ParametersDefinitionsObject extends pars.ParameterObject{

    key:string
    $key=[MetaModel.key()]

}

export class ResponsesDefinitionsObject extends pars.ResponseObject{
    key:string
    $key=[MetaModel.key()]

}

export class SecurityDefinitionsObject{

    key:string
    $key=[MetaModel.key()]

    type:string
    $type=[MetaModel.oneOf(["basic","apiKey","oauth2"]),MetaModel.descriminatingProperty()]

    description:string

}

export class ApiKey extends SecurityDefinitionsObject{
    name:string
    $name=[MetaModel.required()]

    'in':string
    $in=[MetaModel.oneOf(['query',"header"]),MetaModel.required()]

    type='apiKey'
}
export class OAuth2 extends  SecurityDefinitionsObject{
    flow:string
    $flow=[MetaModel.required(),MetaModel.oneOf(["implicit","password","application","accessCode"])]

    authorizationUrl:string//TODO GUARDS
    tokenUrl:string
    scopes:ScopeObject[]

    type="oauth2"
}

export class Basic extends SecurityDefinitionsObject{
    type="basic"
}
export class ScopeObject{
    name:string
    $name=[MetaModel.key()]
    value:string
    $value=[MetaModel.value()]
}