import RamlWrapper = require("./raml003parser");
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

    "APIKey": (x)=>{return new RamlWrapper.APIKeyImpl(x)},

    "APIKeySettings": (x)=>{return new RamlWrapper.APIKeySettingsImpl(x)},

    "AnnotationRef": (x)=>{return new RamlWrapper.AnnotationRefImpl(x)},

    "AnnotationTarget": (x)=>{return new RamlWrapper.AnnotationTargetImpl(x)},

    "AnnotationType": (x)=>{return new RamlWrapper.AnnotationTypeImpl(x)},

    "Api": (x)=>{return new RamlWrapper.ApiImpl(x)},

    "ApiDescription": (x)=>{return new RamlWrapper.ApiDescriptionImpl(x)},

    "ArrayField": (x)=>{return new RamlWrapper.ArrayFieldImpl(x)},

    "Basic": (x)=>{return new RamlWrapper.BasicImpl(x)},

    "BooleanElement": (x)=>{return new RamlWrapper.BooleanElementImpl(x)},

    "BooleanType": (x)=>{return new RamlWrapper.BooleanTypeImpl(x)},

    "CallbackAPIDescription": (x)=>{return new RamlWrapper.CallbackAPIDescriptionImpl(x)},

    "ContentType": (x)=>{return new RamlWrapper.ContentTypeImpl(x)},

    "Custom": (x)=>{return new RamlWrapper.CustomImpl(x)},

    "DataElement": (x)=>{return new RamlWrapper.DataElementImpl(x)},

    "DataElementRef": (x)=>{return new RamlWrapper.DataElementRefImpl(x)},

    "DateElement": (x)=>{return new RamlWrapper.DateElementImpl(x)},

    "DateFormatSpec": (x)=>{return new RamlWrapper.DateFormatSpecImpl(x)},

    "Digest": (x)=>{return new RamlWrapper.DigestImpl(x)},

    "DocumentationItem": (x)=>{return new RamlWrapper.DocumentationItemImpl(x)},

    "ExampleSpec": (x)=>{return new RamlWrapper.ExampleSpecImpl(x)},

    "ExampleString": (x)=>{return new RamlWrapper.ExampleStringImpl(x)},

    "Extension": (x)=>{return new RamlWrapper.ExtensionImpl(x)},

    "FileParameter": (x)=>{return new RamlWrapper.FileParameterImpl(x)},

    "FixedUri": (x)=>{return new RamlWrapper.FixedUriImpl(x)},

    "FullUriTemplate": (x)=>{return new RamlWrapper.FullUriTemplateImpl(x)},

    "FunctionalInterface": (x)=>{return new RamlWrapper.FunctionalInterfaceImpl(x)},

    "GlobalSchema": (x)=>{return new RamlWrapper.GlobalSchemaImpl(x)},

    "HasNormalParameters": (x)=>{return new RamlWrapper.HasNormalParametersImpl(x)},

    "ImportDeclaration": (x)=>{return new RamlWrapper.ImportDeclarationImpl(x)},

    "IntegerElement": (x)=>{return new RamlWrapper.IntegerElementImpl(x)},

    "JSonSchemaString": (x)=>{return new RamlWrapper.JSonSchemaStringImpl(x)},

    "Library": (x)=>{return new RamlWrapper.LibraryImpl(x)},

    "LocationKind": (x)=>{return new RamlWrapper.LocationKindImpl(x)},

    "MarkdownString": (x)=>{return new RamlWrapper.MarkdownStringImpl(x)},

    "Method": (x)=>{return new RamlWrapper.MethodImpl(x)},

    "MethodBase": (x)=>{return new RamlWrapper.MethodBaseImpl(x)},

    "MimeType": (x)=>{return new RamlWrapper.MimeTypeImpl(x)},

    "ModelLocation": (x)=>{return new RamlWrapper.ModelLocationImpl(x)},

    "NumberElement": (x)=>{return new RamlWrapper.NumberElementImpl(x)},

    "NumberType": (x)=>{return new RamlWrapper.NumberTypeImpl(x)},

    "Oath1": (x)=>{return new RamlWrapper.Oath1Impl(x)},

    "Oath1SecuritySchemaSettings": (x)=>{return new RamlWrapper.Oath1SecuritySchemaSettingsImpl(x)},

    "Oath2": (x)=>{return new RamlWrapper.Oath2Impl(x)},

    "Oath2SecurySchemaSettings": (x)=>{return new RamlWrapper.Oath2SecurySchemaSettingsImpl(x)},

    "ObjectField": (x)=>{return new RamlWrapper.ObjectFieldImpl(x)},

    "Overlay": (x)=>{return new RamlWrapper.OverlayImpl(x)},

    "RAMLExpression": (x)=>{return new RamlWrapper.RAMLExpressionImpl(x)},

    "RAMLLanguageElement": (x)=>{return new RamlWrapper.RAMLLanguageElementImpl(x)},

    "RAMLPointer": (x)=>{return new RamlWrapper.RAMLPointerImpl(x)},

    "RAMLPointerElement": (x)=>{return new RamlWrapper.RAMLPointerElementImpl(x)},

    "RAMLProject": (x)=>{return new RamlWrapper.RAMLProjectImpl(x)},

    "RAMLSelector": (x)=>{return new RamlWrapper.RAMLSelectorImpl(x)},

    "RAMLSimpleElement": (x)=>{return new RamlWrapper.RAMLSimpleElementImpl(x)},

    "Reference": (x)=>{return new RamlWrapper.ReferenceImpl(x)},

    "RelativeUri": (x)=>{return new RamlWrapper.RelativeUriImpl(x)},

    "Resource": (x)=>{return new RamlWrapper.ResourceImpl(x)},

    "ResourceBase": (x)=>{return new RamlWrapper.ResourceBaseImpl(x)},

    "ResourceType": (x)=>{return new RamlWrapper.ResourceTypeImpl(x)},

    "ResourceTypeRef": (x)=>{return new RamlWrapper.ResourceTypeRefImpl(x)},

    "Response": (x)=>{return new RamlWrapper.ResponseImpl(x)},

    "SchemaElement": (x)=>{return new RamlWrapper.SchemaElementImpl(x)},

    "SchemaString": (x)=>{return new RamlWrapper.SchemaStringImpl(x)},

    "ScriptHookElement": (x)=>{return new RamlWrapper.ScriptHookElementImpl(x)},

    "ScriptSpec": (x)=>{return new RamlWrapper.ScriptSpecImpl(x)},

    "ScriptingHook": (x)=>{return new RamlWrapper.ScriptingHookImpl(x)},

    "SecuritySchema": (x)=>{return new RamlWrapper.SecuritySchemaImpl(x)},

    "SecuritySchemaHook": (x)=>{return new RamlWrapper.SecuritySchemaHookImpl(x)},

    "SecuritySchemaHookScript": (x)=>{return new RamlWrapper.SecuritySchemaHookScriptImpl(x)},

    "SecuritySchemaPart": (x)=>{return new RamlWrapper.SecuritySchemaPartImpl(x)},

    "SecuritySchemaRef": (x)=>{return new RamlWrapper.SecuritySchemaRefImpl(x)},

    "SecuritySchemaSettings": (x)=>{return new RamlWrapper.SecuritySchemaSettingsImpl(x)},

    "SecuritySchemaType": (x)=>{return new RamlWrapper.SecuritySchemaTypeImpl(x)},

    "StatusCode": (x)=>{return new RamlWrapper.StatusCodeImpl(x)},

    "StrElement": (x)=>{return new RamlWrapper.StrElementImpl(x)},

    "StringType": (x)=>{return new RamlWrapper.StringTypeImpl(x)},

    "Trait": (x)=>{return new RamlWrapper.TraitImpl(x)},

    "TraitRef": (x)=>{return new RamlWrapper.TraitRefImpl(x)},

    "UnionField": (x)=>{return new RamlWrapper.UnionFieldImpl(x)},

    "UriTemplate": (x)=>{return new RamlWrapper.UriTemplateImpl(x)},

    "ValidityExpression": (x)=>{return new RamlWrapper.ValidityExpressionImpl(x)},

    "ValueElement": (x)=>{return new RamlWrapper.ValueElementImpl(x)},

    "ValueType": (x)=>{return new RamlWrapper.ValueTypeImpl(x)},

    "XMLSchemaString": (x)=>{return new RamlWrapper.XMLSchemaStringImpl(x)},

    "pointer": (x)=>{return new RamlWrapper.pointerImpl(x)},

    "ramlexpression": (x)=>{return new RamlWrapper.ramlexpressionImpl(x)}

};
