import  MetaModel = require("../metamodel")
import  Sys = require("../spec-0.8/systemTypes")
import paths=require("./paths");
import defs=require("./definitions")
import core=require("./core")
export class InfoObject{

    title:string;
    $title=[MetaModel.required(),MetaModel.description("The title of the application.")]

    description:Sys.MarkdownString;
    $description=[MetaModel.description("A short description of the application. GFM syntax can be used for rich text representation.")]

    termsOfService:string;
    $termsOfService=[MetaModel.description("The Terms of Service for the API.")]

    version:string;
    $version=[MetaModel.description("Provides the version of the application API (not to be confused with the specification version)"),MetaModel.required()];

    contact:ContactObject;
    $contact=[MetaModel.description("Contact information for the exposed API")]

    license:LicenseObject;
    $license=[MetaModel.description("The license information for the exposed API.")]
}

export class ContactObject{
    name:string
    $name=[MetaModel.description("The identifying name of the contact person/organization.")]
    url:Sys.FixedUri
    $url=[MetaModel.description("The URL pointing to the contact information. MUST be in the format of a URL.")]
    email:string
    $email=[MetaModel.description("The URL pointing to the contact information. MUST be in the format of a URL.")];
}
export class LicenseObject{
    name:string
    $name=[MetaModel.description("The identifying name of license")]
    url:Sys.FixedUri
    $url=[MetaModel.description("A URL to the license used for the API. MUST be in the format of a URL.")]

}
export class SecurityRequirementObject{

}
export class TagsObject{

}
export class SwaggerObject{
    //handled
    swagger:string
    $swagger=[MetaModel.description("Required. Specifies the Swagger Specification version being used. It can be used by the Swagger UI and other clients to interpret the API listing. The value MUST be '2.0'"),MetaModel.required(),MetaModel.oneOf(["2.0"])]
    //handled
    info:InfoObject
    $info=[MetaModel.required()]
    //handled
    host:string
    $host=[MetaModel.description("The host (name or ip) serving the API. This MUST be the host only and does not include the scheme nor sub-paths. It MAY include a port. If the host is not included, the host serving the documentation is to be used (including the port). The host does not support path templating.")]

    //handled
    basePath:string
    $basePath=[MetaModel.description("The base path on which the API is served, which is relative to the host. If it is not included, the API is served directly under the host. The value MUST start with a leading slash (/). The basePath does not support path templating.")]
    //handled
    schemes:string[]
    $schemes=[MetaModel.oneOf(["http","https","ws","wss"]),MetaModel.description('The transfer protocol of the API. Values MUST be from the list: "http", "https", "ws", "wss". If the schemes is not included, the default scheme to be used is the one used to access the Swagger definition itself.')]
    //handled
    consumes:string[]
    $consumes=[MetaModel.description("A list of MIME types the APIs can consume. This is global to all APIs but can be overridden on specific API calls. Value MUST be as described under Mime Types.")]
    //handled
    produces:string[]
    $produces=[MetaModel.description("A list of MIME types the APIs can consume. This is global to all APIs but can be overridden on specific API calls. Value MUST be as described under Mime Types.")]
    //handled
    paths:paths.PathsObject
    $paths=[MetaModel.required(),MetaModel.description("The available paths and operations for the API.")]
    //
    definitions:defs.DefinitionsObject[]
    $definitions=[MetaModel.description("An object to hold data types produced and consumed by operations.")]
    //handled
    parameters:defs.ParametersDefinitionsObject[]
    $parameters=[MetaModel.description("An object to hold parameters that can be used across operations. This property does not define global parameters for all operations.")]
    //handled
    responses:defs.ResponsesDefinitionsObject[];
    $responses=[MetaModel.description("An object to hold responses that can be used across operations. This property does not define global responses for all operations.")]
    //handled
    securityDefinitions:defs.SecurityDefinitionsObject[];
    $securityDefinitions=[MetaModel.description("Security scheme definitions that can be used across the specification.")]

    security:SecurityRequirementObject;
    $security=[MetaModel.description("A declaration of which security schemes are applied for the API as a whole. The list of values describes alternative security schemes that can be used (that is, there is a logical OR between the security requirements). Individual operations can override this definition.")]

    tags:TagsObject
    $tags=[MetaModel.description("A list of tags used by the specification with additional metadata. The order of the tags can be used to reflect on their order by the parsing tools. Not all tags that are used by the Operation Object must be declared. The tags that are not declared may be organized randomly or based on the tools' logic. Each tag name in the list MUST be unique.")]

    externalDocs:core.ExternalDocumentationObject
    $externalDocs=[MetaModel.description("Additional external documentation.")]
}