package com.mulesoft.raml1.java.parser.registry;

import java.util.HashMap;

public class ModelRegistry {

    protected static ModelRegistry instance;

    public static ModelRegistry getInstance(){
        if(instance==null){
            instance = new ModelRegistry();
        }
        return instance;
    }


    protected ModelRegistry(){
        this.init();
    }

    public String rootPackage(){
        return "com.mulesoft.raml1.java.parser";
    }

    protected HashMap<String,String> packageMap;

    @SuppressWarnings("unchecked")
	public <S> Class<? extends S> getModelClass(String simpleName, Class<S> clazz){
        String pkg = this.packageMap.get(simpleName);
        if(pkg==null){
            return null;
        }
        String qualifiedName = pkg + "." + simpleName;
        try {
            Class<?> result = this.getClass().getClassLoader().loadClass(qualifiedName);
            if(result!=null && clazz.isAssignableFrom(result)){
            	return (Class<? extends S>) result;
            }
        }
        catch(Exception e){}

        return null;
    }

    protected void init(){

        this.packageMap = new HashMap<String,String>();

        this.packageMap.put( "RAMLLanguageElement", "com.mulesoft.raml1.java.parser.model.common" );

        this.packageMap.put( "RAMLLanguageElementImpl", "com.mulesoft.raml1.java.parser.impl.common" );

        this.packageMap.put( "ValueType", "com.mulesoft.raml1.java.parser.model.systemTypes" );

        this.packageMap.put( "ValueTypeImpl", "com.mulesoft.raml1.java.parser.impl.systemTypes" );

        this.packageMap.put( "NumberType", "com.mulesoft.raml1.java.parser.model.systemTypes" );

        this.packageMap.put( "NumberTypeImpl", "com.mulesoft.raml1.java.parser.impl.systemTypes" );

        this.packageMap.put( "BooleanType", "com.mulesoft.raml1.java.parser.model.systemTypes" );

        this.packageMap.put( "BooleanTypeImpl", "com.mulesoft.raml1.java.parser.impl.systemTypes" );

        this.packageMap.put( "Reference", "com.mulesoft.raml1.java.parser.model.systemTypes" );

        this.packageMap.put( "ReferenceImpl", "com.mulesoft.raml1.java.parser.impl.systemTypes" );

        this.packageMap.put( "ResourceTypeRef", "com.mulesoft.raml1.java.parser.model.methodsAndResources" );

        this.packageMap.put( "ResourceTypeRefImpl", "com.mulesoft.raml1.java.parser.impl.methodsAndResources" );

        this.packageMap.put( "TraitRef", "com.mulesoft.raml1.java.parser.model.methodsAndResources" );

        this.packageMap.put( "TraitRefImpl", "com.mulesoft.raml1.java.parser.impl.methodsAndResources" );

        this.packageMap.put( "SecuritySchemaRef", "com.mulesoft.raml1.java.parser.model.methodsAndResources" );

        this.packageMap.put( "SecuritySchemaRefImpl", "com.mulesoft.raml1.java.parser.impl.methodsAndResources" );

        this.packageMap.put( "StringType", "com.mulesoft.raml1.java.parser.model.systemTypes" );

        this.packageMap.put( "StringTypeImpl", "com.mulesoft.raml1.java.parser.impl.systemTypes" );

        this.packageMap.put( "UriTemplate", "com.mulesoft.raml1.java.parser.model.systemTypes" );

        this.packageMap.put( "UriTemplateImpl", "com.mulesoft.raml1.java.parser.impl.systemTypes" );

        this.packageMap.put( "RelativeUri", "com.mulesoft.raml1.java.parser.model.systemTypes" );

        this.packageMap.put( "RelativeUriImpl", "com.mulesoft.raml1.java.parser.impl.systemTypes" );

        this.packageMap.put( "FullUriTemplate", "com.mulesoft.raml1.java.parser.model.systemTypes" );

        this.packageMap.put( "FullUriTemplateImpl", "com.mulesoft.raml1.java.parser.impl.systemTypes" );

        this.packageMap.put( "FixedUri", "com.mulesoft.raml1.java.parser.model.systemTypes" );

        this.packageMap.put( "FixedUriImpl", "com.mulesoft.raml1.java.parser.impl.systemTypes" );

        this.packageMap.put( "SchemaString", "com.mulesoft.raml1.java.parser.model.systemTypes" );

        this.packageMap.put( "SchemaStringImpl", "com.mulesoft.raml1.java.parser.impl.systemTypes" );

        this.packageMap.put( "JSonSchemaString", "com.mulesoft.raml1.java.parser.model.systemTypes" );

        this.packageMap.put( "JSonSchemaStringImpl", "com.mulesoft.raml1.java.parser.impl.systemTypes" );

        this.packageMap.put( "XMLSchemaString", "com.mulesoft.raml1.java.parser.model.systemTypes" );

        this.packageMap.put( "XMLSchemaStringImpl", "com.mulesoft.raml1.java.parser.impl.systemTypes" );

        this.packageMap.put( "ExampleString", "com.mulesoft.raml1.java.parser.model.systemTypes" );

        this.packageMap.put( "ExampleStringImpl", "com.mulesoft.raml1.java.parser.impl.systemTypes" );

        this.packageMap.put( "JSONExample", "com.mulesoft.raml1.java.parser.model.systemTypes" );

        this.packageMap.put( "JSONExampleImpl", "com.mulesoft.raml1.java.parser.impl.systemTypes" );

        this.packageMap.put( "XMLExample", "com.mulesoft.raml1.java.parser.model.systemTypes" );

        this.packageMap.put( "XMLExampleImpl", "com.mulesoft.raml1.java.parser.impl.systemTypes" );

        this.packageMap.put( "StatusCode", "com.mulesoft.raml1.java.parser.model.systemTypes" );

        this.packageMap.put( "StatusCodeImpl", "com.mulesoft.raml1.java.parser.impl.systemTypes" );

        this.packageMap.put( "MimeType", "com.mulesoft.raml1.java.parser.model.bodies" );

        this.packageMap.put( "MimeTypeImpl", "com.mulesoft.raml1.java.parser.impl.bodies" );

        this.packageMap.put( "MarkdownString", "com.mulesoft.raml1.java.parser.model.systemTypes" );

        this.packageMap.put( "MarkdownStringImpl", "com.mulesoft.raml1.java.parser.impl.systemTypes" );

        this.packageMap.put( "SecuritySchema", "com.mulesoft.raml1.java.parser.model.methodsAndResources" );

        this.packageMap.put( "SecuritySchemaImpl", "com.mulesoft.raml1.java.parser.impl.methodsAndResources" );

        this.packageMap.put( "RAMLSimpleElement", "com.mulesoft.raml1.java.parser.model.common" );

        this.packageMap.put( "RAMLSimpleElementImpl", "com.mulesoft.raml1.java.parser.impl.common" );

        this.packageMap.put( "GlobalSchema", "com.mulesoft.raml1.java.parser.model.api" );

        this.packageMap.put( "GlobalSchemaImpl", "com.mulesoft.raml1.java.parser.impl.api" );

        this.packageMap.put( "DocumentationItem", "com.mulesoft.raml1.java.parser.model.api" );

        this.packageMap.put( "DocumentationItemImpl", "com.mulesoft.raml1.java.parser.impl.api" );

        this.packageMap.put( "SecuritySchemaSettings", "com.mulesoft.raml1.java.parser.model.methodsAndResources" );

        this.packageMap.put( "SecuritySchemaSettingsImpl", "com.mulesoft.raml1.java.parser.impl.methodsAndResources" );

        this.packageMap.put( "OAuth1SecuritySchemeSettings", "com.mulesoft.raml1.java.parser.model.methodsAndResources" );

        this.packageMap.put( "OAuth1SecuritySchemeSettingsImpl", "com.mulesoft.raml1.java.parser.impl.methodsAndResources" );

        this.packageMap.put( "OAuth2SecuritySchemeSettings", "com.mulesoft.raml1.java.parser.model.methodsAndResources" );

        this.packageMap.put( "OAuth2SecuritySchemeSettingsImpl", "com.mulesoft.raml1.java.parser.impl.methodsAndResources" );

        this.packageMap.put( "SecuritySchemaPart", "com.mulesoft.raml1.java.parser.model.methodsAndResources" );

        this.packageMap.put( "SecuritySchemaPartImpl", "com.mulesoft.raml1.java.parser.impl.methodsAndResources" );

        this.packageMap.put( "ResourceType", "com.mulesoft.raml1.java.parser.model.methodsAndResources" );

        this.packageMap.put( "ResourceTypeImpl", "com.mulesoft.raml1.java.parser.impl.methodsAndResources" );

        this.packageMap.put( "HasNormalParameters", "com.mulesoft.raml1.java.parser.model.parameters" );

        this.packageMap.put( "HasNormalParametersImpl", "com.mulesoft.raml1.java.parser.impl.parameters" );

        this.packageMap.put( "Parameter", "com.mulesoft.raml1.java.parser.model.parameters" );

        this.packageMap.put( "ParameterImpl", "com.mulesoft.raml1.java.parser.impl.parameters" );

        this.packageMap.put( "ParameterLocation", "com.mulesoft.raml1.java.parser.model.parameters" );

        this.packageMap.put( "StrElement", "com.mulesoft.raml1.java.parser.model.parameters" );

        this.packageMap.put( "StrElementImpl", "com.mulesoft.raml1.java.parser.impl.parameters" );

        this.packageMap.put( "BooleanElement", "com.mulesoft.raml1.java.parser.model.parameters" );

        this.packageMap.put( "BooleanElementImpl", "com.mulesoft.raml1.java.parser.impl.parameters" );

        this.packageMap.put( "NumberElement", "com.mulesoft.raml1.java.parser.model.parameters" );

        this.packageMap.put( "NumberElementImpl", "com.mulesoft.raml1.java.parser.impl.parameters" );

        this.packageMap.put( "IntegerElement", "com.mulesoft.raml1.java.parser.model.parameters" );

        this.packageMap.put( "IntegerElementImpl", "com.mulesoft.raml1.java.parser.impl.parameters" );

        this.packageMap.put( "DateElement", "com.mulesoft.raml1.java.parser.model.parameters" );

        this.packageMap.put( "DateElementImpl", "com.mulesoft.raml1.java.parser.impl.parameters" );

        this.packageMap.put( "FileElement", "com.mulesoft.raml1.java.parser.model.parameters" );

        this.packageMap.put( "FileElementImpl", "com.mulesoft.raml1.java.parser.impl.parameters" );

        this.packageMap.put( "MethodBase", "com.mulesoft.raml1.java.parser.model.methodsAndResources" );

        this.packageMap.put( "MethodBaseImpl", "com.mulesoft.raml1.java.parser.impl.methodsAndResources" );

        this.packageMap.put( "Response", "com.mulesoft.raml1.java.parser.model.bodies" );

        this.packageMap.put( "ResponseImpl", "com.mulesoft.raml1.java.parser.impl.bodies" );

        this.packageMap.put( "BodyLike", "com.mulesoft.raml1.java.parser.model.bodies" );

        this.packageMap.put( "BodyLikeImpl", "com.mulesoft.raml1.java.parser.impl.bodies" );

        this.packageMap.put( "XMLBody", "com.mulesoft.raml1.java.parser.model.bodies" );

        this.packageMap.put( "XMLBodyImpl", "com.mulesoft.raml1.java.parser.impl.bodies" );

        this.packageMap.put( "JSONBody", "com.mulesoft.raml1.java.parser.model.bodies" );

        this.packageMap.put( "JSONBodyImpl", "com.mulesoft.raml1.java.parser.impl.bodies" );

        this.packageMap.put( "Trait", "com.mulesoft.raml1.java.parser.model.methodsAndResources" );

        this.packageMap.put( "TraitImpl", "com.mulesoft.raml1.java.parser.impl.methodsAndResources" );

        this.packageMap.put( "Method", "com.mulesoft.raml1.java.parser.model.methodsAndResources" );

        this.packageMap.put( "MethodImpl", "com.mulesoft.raml1.java.parser.impl.methodsAndResources" );

        this.packageMap.put( "Resource", "com.mulesoft.raml1.java.parser.model.methodsAndResources" );

        this.packageMap.put( "ResourceImpl", "com.mulesoft.raml1.java.parser.impl.methodsAndResources" );

        this.packageMap.put( "Api", "com.mulesoft.raml1.java.parser.model.api" );

        this.packageMap.put( "ApiImpl", "com.mulesoft.raml1.java.parser.impl.api" );
    }

}