import RamlWrapper = require("./raml08parser");
import hl = require("../highLevelAST")

export function buildWrapperNode(node:hl.IHighLevelNode){

    var nodeClassName = node.definition().name();

    var wrapperConstructor = classMap[nodeClassName];

    if(!wrapperConstructor){
        var m=node.definition().allSuperTypes();
        var wr=null;
        for (var i=0;i<m.length;i++){
            var nm=m[i].name();
            wrapperConstructor = classMap[nm];
            if (nm=="DataElement"){
                wr=nm;
                //This is only case of nested hierarchy
                continue;
            }
            if (nm=="RAMLLanguageElement"){
                //depth first
                continue;
            }
            if (wrapperConstructor){
                break;
            }
        }
        if (!wrapperConstructor){
            wr=nm;
        }
    }
    if (!wrapperConstructor){
        wrapperConstructor = classMap["RAMLLanguageElement"]

    }
    return wrapperConstructor(node);
}

var classMap = {

    "Api": (x)=>{return new RamlWrapper.ApiImpl(x)},

    "BodyLike": (x)=>{return new RamlWrapper.BodyLikeImpl(x)},

    "BooleanElement": (x)=>{return new RamlWrapper.BooleanElementImpl(x)},

    "BooleanType": (x)=>{return new RamlWrapper.BooleanTypeImpl(x)},

    "DateElement": (x)=>{return new RamlWrapper.DateElementImpl(x)},

    "DocumentationItem": (x)=>{return new RamlWrapper.DocumentationItemImpl(x)},

    "ExampleString": (x)=>{return new RamlWrapper.ExampleStringImpl(x)},

    "FileElement": (x)=>{return new RamlWrapper.FileElementImpl(x)},

    "FixedUri": (x)=>{return new RamlWrapper.FixedUriImpl(x)},

    "FullUriTemplate": (x)=>{return new RamlWrapper.FullUriTemplateImpl(x)},

    "GlobalSchema": (x)=>{return new RamlWrapper.GlobalSchemaImpl(x)},

    "HasNormalParameters": (x)=>{return new RamlWrapper.HasNormalParametersImpl(x)},

    "IntegerElement": (x)=>{return new RamlWrapper.IntegerElementImpl(x)},

    "JSONBody": (x)=>{return new RamlWrapper.JSONBodyImpl(x)},

    "JSONExample": (x)=>{return new RamlWrapper.JSONExampleImpl(x)},

    "JSonSchemaString": (x)=>{return new RamlWrapper.JSonSchemaStringImpl(x)},

    "MarkdownString": (x)=>{return new RamlWrapper.MarkdownStringImpl(x)},

    "Method": (x)=>{return new RamlWrapper.MethodImpl(x)},

    "MethodBase": (x)=>{return new RamlWrapper.MethodBaseImpl(x)},

    "MimeType": (x)=>{return new RamlWrapper.MimeTypeImpl(x)},

    "NumberElement": (x)=>{return new RamlWrapper.NumberElementImpl(x)},

    "NumberType": (x)=>{return new RamlWrapper.NumberTypeImpl(x)},

    "Oath1SecurySchemaSettings": (x)=>{return new RamlWrapper.Oath1SecurySchemaSettingsImpl(x)},

    "Oath2SecurySchemaSettings": (x)=>{return new RamlWrapper.Oath2SecurySchemaSettingsImpl(x)},

    "Parameter": (x)=>{return new RamlWrapper.ParameterImpl(x)},

    "ParameterLocation": (x)=>{return new RamlWrapper.ParameterLocationImpl(x)},

    "RAMLLanguageElement": (x)=>{return new RamlWrapper.RAMLLanguageElementImpl(x)},

    "RAMLSimpleElement": (x)=>{return new RamlWrapper.RAMLSimpleElementImpl(x)},

    "Reference": (x)=>{return new RamlWrapper.ReferenceImpl(x)},

    "RelativeUri": (x)=>{return new RamlWrapper.RelativeUriImpl(x)},

    "Resource": (x)=>{return new RamlWrapper.ResourceImpl(x)},

    "ResourceType": (x)=>{return new RamlWrapper.ResourceTypeImpl(x)},

    "ResourceTypeRef": (x)=>{return new RamlWrapper.ResourceTypeRefImpl(x)},

    "Response": (x)=>{return new RamlWrapper.ResponseImpl(x)},

    "SchemaString": (x)=>{return new RamlWrapper.SchemaStringImpl(x)},

    "SecuritySchema": (x)=>{return new RamlWrapper.SecuritySchemaImpl(x)},

    "SecuritySchemaPart": (x)=>{return new RamlWrapper.SecuritySchemaPartImpl(x)},

    "SecuritySchemaRef": (x)=>{return new RamlWrapper.SecuritySchemaRefImpl(x)},

    "SecuritySchemaSettings": (x)=>{return new RamlWrapper.SecuritySchemaSettingsImpl(x)},

    "StatusCode": (x)=>{return new RamlWrapper.StatusCodeImpl(x)},

    "StrElement": (x)=>{return new RamlWrapper.StrElementImpl(x)},

    "StringType": (x)=>{return new RamlWrapper.StringTypeImpl(x)},

    "Trait": (x)=>{return new RamlWrapper.TraitImpl(x)},

    "TraitRef": (x)=>{return new RamlWrapper.TraitRefImpl(x)},

    "UriTemplate": (x)=>{return new RamlWrapper.UriTemplateImpl(x)},

    "ValueType": (x)=>{return new RamlWrapper.ValueTypeImpl(x)},

    "XMLBody": (x)=>{return new RamlWrapper.XMLBodyImpl(x)},

    "XMLExample": (x)=>{return new RamlWrapper.XMLExampleImpl(x)},

    "XMLSchemaString": (x)=>{return new RamlWrapper.XMLSchemaStringImpl(x)}

};
