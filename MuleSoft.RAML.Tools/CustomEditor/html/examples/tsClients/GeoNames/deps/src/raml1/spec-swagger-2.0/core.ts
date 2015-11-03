import  MetaModel = require("../metamodel")
import  Sys = require("../spec-0.8/systemTypes")

export class ExternalDocumentationObject{
    description:string
    $description=[MetaModel.description("A short description of the target documentation. GFM syntax can be used for rich text representation.")]


    url:string
    $url=[MetaModel.required(),MetaModel.description("The URL for the target documentation. Value MUST be in the format of a URL.")];
}