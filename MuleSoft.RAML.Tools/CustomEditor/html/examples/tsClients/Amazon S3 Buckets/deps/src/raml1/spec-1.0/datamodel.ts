import  MetaModel = require("../metamodel")
import  Sys = require("./systemTypes")
import  Bodies=require("./bodies")
import  Common=require("./common")
import  Declarations=require("./declarations")


export enum ModelLocation{
    QUERY,HEADERS,URI,FORM,BURI,ANNOTATION,MODEL,SECURITYSCHEMATYPE
}
export enum LocationKind{
    APISTRUCTURE,DECLARATIONS,MODELS
}

export class ExampleSpec extends Common.RAMLLanguageElement{
    content:string
    $content=[MetaModel.selfNode()]
    strict:boolean
    name:string
    $name=[MetaModel.key()];
}



export class DataElement extends Common.RAMLLanguageElement{
    name:string
    $name=[MetaModel.key(),MetaModel.description("name of the parameter"),MetaModel.extraMetaKey("headers")]


    facets:DataElement[];
    $facets=[MetaModel.declaringFields()]

    schema: string
    usage: string
    type:string;
    $type=[
        MetaModel.allowMultiple(),
        MetaModel.needsClarification("I suggest to remove multiple type feature from RAML 1.0 in favour of parameter overloading"),
        MetaModel.canBeValue(),
        MetaModel.defaultValue("string"),MetaModel.descriminatingProperty(),MetaModel.description("The type attribute specifies the primitive type of the parameter's resolved value. " +
        "API clients MUST return/throw an error if the parameter's resolved value does not match the specified type. If type is not specified, it defaults to string.")]
     location:ModelLocation
     $location=[MetaModel.system(),MetaModel.description("Location of the parameter (can not be edited by user)")]

//    formParameters:DataElement[]
//    $formParameters=[ MetaModel.requireValue("form","true"), MetaModel.setsContextValue("fieldOrParam",true),MetaModel.setsContextValue("location","models.ModelLocation.FORM"),
//        MetaModel.setsContextValue("locationKind","models.LocationKind.APISTRUCTURE"),MetaModel.description(`Web forms REQUIRE special encoding and custom declaration.
//If the API's media type is either application/x-www-form-urlencoded or multipart/form-data, the formParameters property MUST specify the name-value pairs that the API is expecting.
//The formParameters property is a map in which the key is the name of the web form parameter, and the value is itself a map the specifies the web form parameter's attributes`)]

    //$=[]

    locationKind:LocationKind;
    $locationKind=[MetaModel.system(),MetaModel.description("Kind of location")]

    default:string
    $default=[MetaModel.description("The default attribute specifies the default value to use for the property if the property is omitted or its value is not specified." +
    " This SHOULD NOT be interpreted as a requirement for the client to send the default attribute's value if there is no other value to send. " +
    "Instead, the default attribute's value is the value the server uses if the client does not send a value if send default by client is not set to true.")]

    //sendDefaultByClient:boolean;
    //$sendDefaultByClient=[MetaModel.requireValue("fieldOrParam",true),MetaModel.thisFeatureCovers("https://github.com/raml-org/raml-spec/issues/86"),MetaModel.requireValue("locationKind",LocationKind.APISTRUCTURE)]

    example:string
    $example=[MetaModel.selfNode(),MetaModel.description("(Optional) The example attribute shows an example value for the property." +
    " This can be used, e.g., by documentation generators to generate sample values for the property."),MetaModel.needsClarification("It sounds consistent to allow multiple examples for parameters, " +
    "but it will make hard to describe difference between 0.8 and 1.0 in terms of def system")]
    repeat:boolean
    examples: ExampleSpec[]

    $repeat=[MetaModel.requireValue("fieldOrParam",true),MetaModel.description("The repeat attribute specifies that the parameter can be repeated. " +
    "If the parameter can be used multiple times, the repeat parameter value MUST be set to 'true'. Otherwise, the default value is 'false' and the parameter may not be repeated."),
        MetaModel.issue("semantic of repeat " +
        "is not clearly specified and actually multiple possible reasonable options exists at the same time "),MetaModel.issue("https://github.com/raml-org/raml-spec/issues/152"),
        MetaModel.requireValue("locationKind",LocationKind.APISTRUCTURE)]

    //TODO remove repeated;

    //collectionFormat:string
    //$collectionFormat=[MetaModel.oneOf(["csv","ssv","tsv","pipes","multi"])];

    required: boolean
    $required=[MetaModel.requireValue("fieldOrParam",true),MetaModel.description("Set to true if parameter is required"),MetaModel.describesAnnotation("required")]

    //scope: string[];
    //$scope=[MetaModel.requireValue("fieldOrParam",true),MetaModel.requireValue("locationKind",LocationKind.MODELS)]

    //xml:XMLInfo
    //$xml=[MetaModel.requireValue("locationKind",LocationKind.MODELS)]

    //validWhen:Sys.ramlexpression;//another alternative conflicts
    //$validWhen=[MetaModel.requireValue("fieldOrParam",true),MetaModel.version(MetaModel.RAMLVersion.RAML10),MetaModel.description("allows to specify expression to compute parameter validity"),MetaModel.thisFeatureCovers("https://github.com/raml-org/raml-spec/issues/53"),MetaModel.thisFeatureCovers("https://github.com/raml-org/raml-spec/issues/46")]
    //
    //requiredWhen:Sys.ramlexpression;//another alternative requires
    //$requiredWhen=[MetaModel.requireValue("fieldOrParam",true),MetaModel.version(MetaModel.RAMLVersion.RAML10),MetaModel.description("allows to specify expression to compute parameter requirement"),MetaModel.thisFeatureCovers("https://github.com/raml-org/raml-spec/issues/53"),MetaModel.thisFeatureCovers("https://github.com/raml-org/raml-spec/issues/46")]



    $=[MetaModel.convertsToGlobalOfType("SchemaString"),MetaModel.canInherit("mediaType")]
}

export class ArrayField extends DataElement{
    //items:DataElementRef[];
    type="array"
    uniqueItems:boolean
    $uniqueItems=[MetaModel.facetId("uniqueItems")]
    minItems: number
    $minItems=[MetaModel.facetId("minItems")]

    maxItems: number
    $maxItems=[MetaModel.facetId("maxItems")]

    $=[MetaModel.convertsToGlobalOfType("SchemaString"),MetaModel.requireValue("locationKind",LocationKind.MODELS),MetaModel.alias("array"),MetaModel.declaresSubTypeOf("DataElement")]
}
export class UnionField extends DataElement{
    discriminator:string;//FIXME should be pointer at some moment
    $discriminator=[MetaModel.selector("*.DataElement")];

    //oneOf:pointer[]
    //$oneOf=[MetaModel.selector("$$.**.DataElement"),MetaModel.required()]
    type="union";
    $=[MetaModel.convertsToGlobalOfType("SchemaString"),MetaModel.requireValue("locationKind",LocationKind.MODELS),MetaModel.declaresSubTypeOf("DataElement")]
}
export class DataElementRef extends Sys.Reference<DataElement>{

}
export class ObjectField extends DataElement{

    properties:DataElement[]
    $properties=[MetaModel.setsContextValue("fieldOrParam",true)]


    minProperties:number
    $minProperties=[MetaModel.facetId("minProperties")]
    maxProperties:number
    $maxProperties=[MetaModel.facetId("maxProperties")]

    additionalProperties:DataElement;
    patternProperties:DataElement[];
    discriminator:pointer
    $discriminator=[MetaModel.selector("*.DataElement")];

    discriminatorValue:string

    type="object";
    $=[ MetaModel.definingPropertyIsEnough("properties"),MetaModel.setsContextValue("field","true"),MetaModel.convertsToGlobalOfType("SchemaString")
        ,MetaModel.declaresSubTypeOf("DataElement")]

}
//additionalProperties
//required should be handled with raml pointer in swagger for us required is placed inside at this moment (is this ok?)
//consider renaming fields to properties
//allOf (I prefer using extends)
//descriminator (it is pretty primitive in swagger,we can do it better)
//format (it is nice place to plug scripting in as well as scripting in general)


export class StrElement extends DataElement{
    pattern:string;
    $pattern=[
        MetaModel.description("(Optional, applicable only for parameters of type string) The pattern attribute is a regular expression that a parameter of type string MUST match. Regular expressions MUST follow the regular expression specification from ECMA 262/Perl 5. The pattern MAY be enclosed in double quotes for readability and clarity.")]
    minLength:number
    $minLength=[
        MetaModel.thisFeatureCovers("https://github.com/raml-org/raml-spec/issues/141"),MetaModel.description("(Optional, applicable only for parameters of type string) The minLength attribute specifies the parameter value's minimum number of characters.The length is equal to the number of 16-bit Unicode characters in the string")]
    maxLength:number
    $maxLength=[MetaModel.description("(Optional, applicable only for parameters of type string) The maxLength attribute specifies the parameter value's maximum number of characters.The length is equal to the number of 16-bit Unicode characters in the string")]
    type="string"
    $=[MetaModel.description("Value must be a string"),MetaModel.declaresSubTypeOf("DataElement")]
    enum:string[]
    $enum=[
        MetaModel.thisFeatureCovers("https://github.com/raml-org/raml-spec/issues/82"),
        MetaModel.describesAnnotation("oneOf"),
        MetaModel.description("(Optional, applicable only for parameters of type string) The enum attribute provides an enumeration of the parameter's valid values. This MUST be an array. If the enum attribute is defined, API clients and servers MUST verify that a parameter's value matches a value in the enum array. If there is no matching value, the clients and servers MUST treat this as an error.")]



}
//export class WrappedJSON extends DataElement{
//    $=[MetaModel.declaresSubTypeOf("DataElement")]
//    type="jsonstring"
//    schema:Sys.SchemaString;
//    $schema=[MetaModel.description("Allows to provide schema of the content in case if it is json or xml"),MetaModel.version(MetaModel.RAMLVersion.RAML10)]
//
//}
//export class WrappedXML extends DataElement{
//    $=[MetaModel.declaresSubTypeOf("DataElement")]
//
//    type="xmlstring"
//    schema:Sys.SchemaString;
//    $schema=[MetaModel.description("Allows to provide schema of the content in case if it is json or xml"),MetaModel.version(MetaModel.RAMLVersion.RAML10)]
//
//}
export class BooleanElement extends DataElement{
    type="boolean"
    $=[MetaModel.description("Value must be a boolean"),MetaModel.declaresSubTypeOf("DataElement")]



}
export class ValueElement extends DataElement{
    type="value"
    $=[MetaModel.description("Value must be a boolean"),MetaModel.declaresSubTypeOf("DataElement")]



}
export class NumberElement extends DataElement{
    type="number"
    minimum:number
    $minimum=[MetaModel.description("(Optional, applicable only for parameters of type number or integer) The minimum attribute specifies the parameter's minimum value.")]
    maximum:number
    $maximum=[MetaModel.description("(Optional, applicable only for parameters of type number or integer) The maximum attribute specifies the parameter's maximum value.")]

    $=[MetaModel.description("Value MUST be a number. Indicate floating point numbers as defined by YAML."),MetaModel.declaresSubTypeOf("DataElement")]
    enum:string[]
    $enum=[
        MetaModel.thisFeatureCovers("https://github.com/raml-org/raml-spec/issues/82"),
        MetaModel.describesAnnotation("oneOf"),
        MetaModel.description("(Optional, applicable only for parameters of type string) The enum attribute provides an enumeration of the parameter's valid values. This MUST be an array. If the enum attribute is defined, API clients and servers MUST verify that a parameter's value matches a value in the enum array. If there is no matching value, the clients and servers MUST treat this as an error.")]

    format:string
    $format=[MetaModel.oneOf(["int32","int64","int","long","float","double","int16","int8"])];


}
export class IntegerElement extends NumberElement{
    type="integer"
    $=[MetaModel.description("Value MUST be a integer."),MetaModel.declaresSubTypeOf("DataElement")]
    format:string
    $format=[MetaModel.oneOf(["int32","int64","int","long","int16","int8"])];


}
export class RAMLPointerElement extends DataElement{
    type="pointer"
    target:Sys.RAMLSelector
    $=[MetaModel.requireValue("locationKind",LocationKind.APISTRUCTURE)]
}
export class pointer extends Sys.ValueType{}

export class RAMLExpression extends DataElement{
    type="ramlexpression"
    $=[MetaModel.requireValue("locationKind",LocationKind.APISTRUCTURE),MetaModel.requireValue("location",ModelLocation.ANNOTATION)]
}


export class ScriptHookElement extends DataElement{
    $=[MetaModel.requireValue("locationKind",LocationKind.APISTRUCTURE),MetaModel.requireValue("location",ModelLocation.ANNOTATION)]

    type="script"
    declararedIn: string
    $declaredIn=[MetaModel.description("Typescript file defining interface which this scrip should comply to")]
    interfaceName: string
    $interfaceName=[MetaModel.description("Name of the interface which scripts should comply to")]
}

export class SchemaElement extends DataElement{
    $=[MetaModel.requireValue("locationKind",LocationKind.APISTRUCTURE),MetaModel.nameAtRuntime("SchemaString")]
    type="schema"

}

export class DateElement extends DataElement{
    type="date"
    $=[MetaModel.description("Value MUST be a string representation of a date as defined in RFC2616 Section 3.3 [RFC2616]. or according to specified date format"),MetaModel.declaresSubTypeOf("DataElement")]
    dateFormat:Sys.DateFormatSpec;
    $dateFormat=[MetaModel.thisFeatureCovers("https://github.com/raml-org/raml-spec/issues/105")]
}