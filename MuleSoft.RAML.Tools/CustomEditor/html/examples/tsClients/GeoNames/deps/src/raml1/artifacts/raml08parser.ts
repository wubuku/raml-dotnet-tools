import hl=require("../highLevelAST");
import hlImpl=require("../highLevelImpl");
import jsyaml=require("../jsyaml/jsyaml2lowLevel");
import def=require("../definitionSystem");
import core=require("../parserCore");


export interface BasicNode extends core.BasicSuperNode{

    parent():BasicNode

    highLevel():hl.IHighLevelNode
}

export class BasicNodeImpl extends core.BasicSuperNodeImpl implements BasicNode{

    constructor(node:hl.IHighLevelNode){
        super(node);
    }

    wrapperClassName():string{
        return 'BasicNodeImpl';
    }

    parent():BasicNode{
        return <BasicNode>super.parent();
    }
}

        export interface RAMLLanguageElement extends BasicNode{

        /**
         *
         **/
         //description
         description(  ):MarkdownString
}

export class RAMLLanguageElementImpl extends BasicNodeImpl implements RAMLLanguageElement{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createRAMLLanguageElement(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "RAMLLanguageElementImpl";}


        /**
         *
         **/
         //description
         description(  ):MarkdownString{
             return <MarkdownString>super.attribute('description', (attr:hl.IAttribute)=>new MarkdownStringImpl(attr));
         }
}

export interface ValueType extends core.AbstractWrapperNode{

        /**
         *
         **/
         //value
         value(  ):string


        /**
         *
         **/
         //highLevel
         highLevel(  ):hl.IAttribute
}

export class ValueTypeImpl implements ValueType{

        /**
         *
         **/
         //constructor
         constructor( protected attr:hl.IAttribute ){}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ValueTypeImpl";}


        /**
         *
         **/
         //value
         value(  ):string{return this.attr.value();}


        /**
         *
         **/
         //highLevel
         highLevel(  ):hl.IAttribute{return this.attr;}
}

export interface NumberType extends ValueType{}

export class NumberTypeImpl extends ValueTypeImpl implements NumberType{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "NumberTypeImpl";}
}

export interface BooleanType extends ValueType{}

export class BooleanTypeImpl extends ValueTypeImpl implements BooleanType{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "BooleanTypeImpl";}
}

export interface Reference extends ValueType{}

export class ReferenceImpl extends ValueTypeImpl implements Reference{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ReferenceImpl";}


        /**
         *
         **/
         //value
         value(  ):string{return <any>core.toStructuredValue(this.attr);}
}

export interface ResourceTypeRef extends Reference{}

export class ResourceTypeRefImpl extends ReferenceImpl implements ResourceTypeRef{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ResourceTypeRefImpl";}
}

export interface TraitRef extends Reference{}

export class TraitRefImpl extends ReferenceImpl implements TraitRef{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "TraitRefImpl";}
}

export interface SecuritySchemaRef extends Reference{}

export class SecuritySchemaRefImpl extends ReferenceImpl implements SecuritySchemaRef{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "SecuritySchemaRefImpl";}
}

export interface StringType extends ValueType{}

export class StringTypeImpl extends ValueTypeImpl implements StringType{

        /**
         *
         **/
         //constructor
         constructor( protected attr:hl.IAttribute ){super(attr);}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "StringTypeImpl";}
}

export interface UriTemplate extends StringType{}

export class UriTemplateImpl extends StringTypeImpl implements UriTemplate{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "UriTemplateImpl";}
}

export interface RelativeUri extends UriTemplate{}

export class RelativeUriImpl extends UriTemplateImpl implements RelativeUri{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "RelativeUriImpl";}
}

export interface FullUriTemplate extends UriTemplate{}

export class FullUriTemplateImpl extends UriTemplateImpl implements FullUriTemplate{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "FullUriTemplateImpl";}
}

export interface FixedUri extends StringType{}

export class FixedUriImpl extends StringTypeImpl implements FixedUri{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "FixedUriImpl";}
}

export interface SchemaString extends StringType{}

export class SchemaStringImpl extends StringTypeImpl implements SchemaString{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "SchemaStringImpl";}
}

export interface JSonSchemaString extends SchemaString{}

export class JSonSchemaStringImpl extends SchemaStringImpl implements JSonSchemaString{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "JSonSchemaStringImpl";}
}

export interface XMLSchemaString extends SchemaString{}

export class XMLSchemaStringImpl extends SchemaStringImpl implements XMLSchemaString{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "XMLSchemaStringImpl";}
}

export interface ExampleString extends StringType{}

export class ExampleStringImpl extends StringTypeImpl implements ExampleString{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ExampleStringImpl";}
}

export interface JSONExample extends ExampleString{}

export class JSONExampleImpl extends ExampleStringImpl implements JSONExample{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "JSONExampleImpl";}
}

export interface XMLExample extends ExampleString{}

export class XMLExampleImpl extends ExampleStringImpl implements XMLExample{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "XMLExampleImpl";}
}

export interface StatusCode extends StringType{}

export class StatusCodeImpl extends StringTypeImpl implements StatusCode{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "StatusCodeImpl";}
}

export interface MimeType extends StringType{}

export class MimeTypeImpl extends StringTypeImpl implements MimeType{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "MimeTypeImpl";}
}

export interface MarkdownString extends StringType{}

export class MarkdownStringImpl extends StringTypeImpl implements MarkdownString{

        /**
         *
         **/
         //constructor
         constructor( protected attr:hl.IAttribute ){super(attr);}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "MarkdownStringImpl";}
}

export interface SecuritySchema extends RAMLLanguageElement{

        /**
         *
         **/
         //name
         name(  ):string


        /**
         *
         **/
         //type
         "type"(  ):string


        /**
         *
         **/
         //description
         description(  ):MarkdownString


        /**
         *
         **/
         //describedBy
         describedBy(  ):SecuritySchemaPart


        /**
         *
         **/
         //settings
         settings(  ):SecuritySchemaSettings
}

export class SecuritySchemaImpl extends RAMLLanguageElementImpl implements SecuritySchema{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createSecuritySchema(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "SecuritySchemaImpl";}


        /**
         *
         **/
         //name
         name(  ):string{
             return <string>super.attribute('name');
         }


        /**
         *
         **/
         //setName
         setName( param:string ){
        {
        this.highLevel().attrOrCreate("name").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //type
         "type"(  ):string{
             return <string>super.attribute('type');
         }


        /**
         *
         **/
         //setType
         setType( param:string ){
        {
        this.highLevel().attrOrCreate("type").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //description
         description(  ):MarkdownString{
             return <MarkdownString>super.attribute('description', (attr:hl.IAttribute)=>new MarkdownStringImpl(attr));
         }


        /**
         *
         **/
         //describedBy
         describedBy(  ):SecuritySchemaPart{
             return <SecuritySchemaPart>super.element('describedBy');
         }


        /**
         *
         **/
         //settings
         settings(  ):SecuritySchemaSettings{
             return <SecuritySchemaSettings>super.element('settings');
         }
}

export interface RAMLSimpleElement extends BasicNode{}

export class RAMLSimpleElementImpl extends BasicNodeImpl implements RAMLSimpleElement{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createRAMLSimpleElement(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "RAMLSimpleElementImpl";}
}

export interface GlobalSchema extends RAMLSimpleElement{

        /**
         *
         **/
         //key
         key(  ):string


        /**
         *
         **/
         //value
         value(  ):SchemaString
}

export class GlobalSchemaImpl extends RAMLSimpleElementImpl implements GlobalSchema{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createGlobalSchema(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "GlobalSchemaImpl";}


        /**
         *
         **/
         //key
         key(  ):string{
             return <string>super.attribute('key');
         }


        /**
         *
         **/
         //setKey
         setKey( param:string ){
        {
        this.highLevel().attrOrCreate("key").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //value
         value(  ):SchemaString{
             return <SchemaString>super.attribute('value', (attr:hl.IAttribute)=>new SchemaStringImpl(attr));
         }
}

export interface DocumentationItem extends RAMLSimpleElement{

        /**
         *
         **/
         //title
         title(  ):string


        /**
         *
         **/
         //content
         content(  ):MarkdownString
}

export class DocumentationItemImpl extends RAMLSimpleElementImpl implements DocumentationItem{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createDocumentationItem(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "DocumentationItemImpl";}


        /**
         *
         **/
         //title
         title(  ):string{
             return <string>super.attribute('title');
         }


        /**
         *
         **/
         //setTitle
         setTitle( param:string ){
        {
        this.highLevel().attrOrCreate("title").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //content
         content(  ):MarkdownString{
             return <MarkdownString>super.attribute('content', (attr:hl.IAttribute)=>new MarkdownStringImpl(attr));
         }
}

export interface SecuritySchemaSettings extends RAMLSimpleElement{}

export class SecuritySchemaSettingsImpl extends RAMLSimpleElementImpl implements SecuritySchemaSettings{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createSecuritySchemaSettings(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "SecuritySchemaSettingsImpl";}
}

export interface Oath1SecurySchemaSettings extends SecuritySchemaSettings{

        /**
         *
         **/
         //requestTokenUri
         requestTokenUri(  ):FixedUri


        /**
         *
         **/
         //authorizationUri
         authorizationUri(  ):FixedUri


        /**
         *
         **/
         //tokenCredentialsUri
         tokenCredentialsUri(  ):FixedUri
}

export class Oath1SecurySchemaSettingsImpl extends SecuritySchemaSettingsImpl implements Oath1SecurySchemaSettings{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createOath1SecurySchemaSettings(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "Oath1SecurySchemaSettingsImpl";}


        /**
         *
         **/
         //requestTokenUri
         requestTokenUri(  ):FixedUri{
             return <FixedUri>super.attribute('requestTokenUri', (attr:hl.IAttribute)=>new FixedUriImpl(attr));
         }


        /**
         *
         **/
         //authorizationUri
         authorizationUri(  ):FixedUri{
             return <FixedUri>super.attribute('authorizationUri', (attr:hl.IAttribute)=>new FixedUriImpl(attr));
         }


        /**
         *
         **/
         //tokenCredentialsUri
         tokenCredentialsUri(  ):FixedUri{
             return <FixedUri>super.attribute('tokenCredentialsUri', (attr:hl.IAttribute)=>new FixedUriImpl(attr));
         }
}

export interface Oath2SecurySchemaSettings extends SecuritySchemaSettings{

        /**
         *
         **/
         //accessTokenUri
         accessTokenUri(  ):FixedUri


        /**
         *
         **/
         //authorizationUri
         authorizationUri(  ):FixedUri


        /**
         *
         **/
         //authorizationGrants
         authorizationGrants(  ):string[]


        /**
         *
         **/
         //scopes
         scopes(  ):string[]
}

export class Oath2SecurySchemaSettingsImpl extends SecuritySchemaSettingsImpl implements Oath2SecurySchemaSettings{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createOath2SecurySchemaSettings(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "Oath2SecurySchemaSettingsImpl";}


        /**
         *
         **/
         //accessTokenUri
         accessTokenUri(  ):FixedUri{
             return <FixedUri>super.attribute('accessTokenUri', (attr:hl.IAttribute)=>new FixedUriImpl(attr));
         }


        /**
         *
         **/
         //authorizationUri
         authorizationUri(  ):FixedUri{
             return <FixedUri>super.attribute('authorizationUri', (attr:hl.IAttribute)=>new FixedUriImpl(attr));
         }


        /**
         *
         **/
         //authorizationGrants
         authorizationGrants(  ):string[]{
             return <string[]>super.attributes('authorizationGrants');
         }


        /**
         *
         **/
         //setAuthorizationGrants
         setAuthorizationGrants( param:string ){
        {
        this.highLevel().attrOrCreate("authorizationGrants").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //scopes
         scopes(  ):string[]{
             return <string[]>super.attributes('scopes');
         }


        /**
         *
         **/
         //setScopes
         setScopes( param:string ){
        {
        this.highLevel().attrOrCreate("scopes").setValue(""+param);
        return this;
        }
        }
}

export interface SecuritySchemaPart extends RAMLSimpleElement{}

export class SecuritySchemaPartImpl extends RAMLSimpleElementImpl implements SecuritySchemaPart{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createSecuritySchemaPart(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "SecuritySchemaPartImpl";}
}

export interface ResourceType extends RAMLLanguageElement{

        /**
         *
         **/
         //name
         name(  ):string


        /**
         *
         **/
         //usage
         usage(  ):string


        /**
         *
         **/
         //methods
         methods(  ):Method[]


        /**
         *
         **/
         //is
         is(  ):TraitRef[]


        /**
         *
         **/
         //type
         "type"(  ):ResourceTypeRef


        /**
         *
         **/
         //securedBy
         securedBy(  ):SecuritySchemaRef[]


        /**
         *
         **/
         //uriParameters
         uriParameters(  ):Parameter[]
}

export class ResourceTypeImpl extends RAMLLanguageElementImpl implements ResourceType{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createResourceType(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ResourceTypeImpl";}


        /**
         *
         **/
         //name
         name(  ):string{
             return <string>super.attribute('name');
         }


        /**
         *
         **/
         //setName
         setName( param:string ){
        {
        this.highLevel().attrOrCreate("name").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //usage
         usage(  ):string{
             return <string>super.attribute('usage');
         }


        /**
         *
         **/
         //setUsage
         setUsage( param:string ){
        {
        this.highLevel().attrOrCreate("usage").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //methods
         methods(  ):Method[]{
             return <Method[]>super.elements('methods');
         }


        /**
         *
         **/
         //is
         is(  ):TraitRef[]{
             return <TraitRef[]>super.attributes('is', (attr:hl.IAttribute)=>new TraitRefImpl(attr));
         }


        /**
         *
         **/
         //type
         "type"(  ):ResourceTypeRef{
             return <ResourceTypeRef>super.attribute('type', (attr:hl.IAttribute)=>new ResourceTypeRefImpl(attr));
         }


        /**
         *
         **/
         //securedBy
         securedBy(  ):SecuritySchemaRef[]{
             return <SecuritySchemaRef[]>super.attributes('securedBy', (attr:hl.IAttribute)=>new SecuritySchemaRefImpl(attr));
         }


        /**
         *
         **/
         //uriParameters
         uriParameters(  ):Parameter[]{
             return <Parameter[]>super.elements('uriParameters');
         }
}

export interface HasNormalParameters extends RAMLLanguageElement{

        /**
         *
         **/
         //queryParameters
         queryParameters(  ):Parameter[]


        /**
         *
         **/
         //displayName
         displayName(  ):string


        /**
         *
         **/
         //headers
         headers(  ):Parameter[]
}

export class HasNormalParametersImpl extends RAMLLanguageElementImpl implements HasNormalParameters{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createHasNormalParameters(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "HasNormalParametersImpl";}


        /**
         *
         **/
         //queryParameters
         queryParameters(  ):Parameter[]{
             return <Parameter[]>super.elements('queryParameters');
         }


        /**
         *
         **/
         //displayName
         displayName(  ):string{
             return <string>super.attribute('displayName');
         }


        /**
         *
         **/
         //setDisplayName
         setDisplayName( param:string ){
        {
        this.highLevel().attrOrCreate("displayName").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //headers
         headers(  ):Parameter[]{
             return <Parameter[]>super.elements('headers');
         }
}

export interface Parameter extends RAMLLanguageElement{

        /**
         *
         **/
         //name
         name(  ):string


        /**
         *
         **/
         //displayName
         displayName(  ):string


        /**
         *
         **/
         //type
         "type"(  ):string


        /**
         *
         **/
         //location
         location(  ):ParameterLocation


        /**
         *
         **/
         //required
         required(  ):boolean


        /**
         *
         **/
         //default
         "default"(  ):string


        /**
         *
         **/
         //example
         example(  ):string


        /**
         *
         **/
         //repeat
         repeat(  ):boolean
}

export class ParameterImpl extends RAMLLanguageElementImpl implements Parameter{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createParameter(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ParameterImpl";}


        /**
         *
         **/
         //name
         name(  ):string{
             return <string>super.attribute('name');
         }


        /**
         *
         **/
         //setName
         setName( param:string ){
        {
        this.highLevel().attrOrCreate("name").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //displayName
         displayName(  ):string{
             return <string>super.attribute('displayName');
         }


        /**
         *
         **/
         //setDisplayName
         setDisplayName( param:string ){
        {
        this.highLevel().attrOrCreate("displayName").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //type
         "type"(  ):string{
             return <string>super.attribute('type');
         }


        /**
         *
         **/
         //setType
         setType( param:string ){
        {
        this.highLevel().attrOrCreate("type").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //location
         location(  ):ParameterLocation{
             return <ParameterLocation>super.attribute('location', (attr:hl.IAttribute)=>new ParameterLocationImpl(attr));
         }


        /**
         *
         **/
         //required
         required(  ):boolean{
             return <boolean>super.attribute('required');
         }


        /**
         *
         **/
         //setRequired
         setRequired( param:boolean ){
        {
        this.highLevel().attrOrCreate("required").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //default
         "default"(  ):string{
             return <string>super.attribute('default');
         }


        /**
         *
         **/
         //setDefault
         setDefault( param:string ){
        {
        this.highLevel().attrOrCreate("default").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //example
         example(  ):string{
             return <string>super.attribute('example');
         }


        /**
         *
         **/
         //setExample
         setExample( param:string ){
        {
        this.highLevel().attrOrCreate("example").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //repeat
         repeat(  ):boolean{
             return <boolean>super.attribute('repeat');
         }


        /**
         *
         **/
         //setRepeat
         setRepeat( param:boolean ){
        {
        this.highLevel().attrOrCreate("repeat").setValue(""+param);
        return this;
        }
        }
}

export interface ParameterLocation extends core.AbstractWrapperNode{}

export class ParameterLocationImpl implements ParameterLocation{

        /**
         *
         **/
         //constructor
         constructor( protected attr:hl.IAttribute ){}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ParameterLocationImpl";}
}

export interface StrElement extends Parameter{

        /**
         *
         **/
         //pattern
         pattern(  ):string


        /**
         *
         **/
         //enum
         enum(  ):string[]


        /**
         *
         **/
         //minLength
         minLength(  ):number


        /**
         *
         **/
         //maxLength
         maxLength(  ):number
}

export class StrElementImpl extends ParameterImpl implements StrElement{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createStrElement(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "StrElementImpl";}


        /**
         *
         **/
         //pattern
         pattern(  ):string{
             return <string>super.attribute('pattern');
         }


        /**
         *
         **/
         //setPattern
         setPattern( param:string ){
        {
        this.highLevel().attrOrCreate("pattern").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //enum
         enum(  ):string[]{
             return <string[]>super.attributes('enum');
         }


        /**
         *
         **/
         //setEnum
         setEnum( param:string ){
        {
        this.highLevel().attrOrCreate("enum").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //minLength
         minLength(  ):number{
             return <number>super.attribute('minLength');
         }


        /**
         *
         **/
         //setMinLength
         setMinLength( param:number ){
        {
        this.highLevel().attrOrCreate("minLength").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //maxLength
         maxLength(  ):number{
             return <number>super.attribute('maxLength');
         }


        /**
         *
         **/
         //setMaxLength
         setMaxLength( param:number ){
        {
        this.highLevel().attrOrCreate("maxLength").setValue(""+param);
        return this;
        }
        }
}

export interface BooleanElement extends Parameter{}

export class BooleanElementImpl extends ParameterImpl implements BooleanElement{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createBooleanElement(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "BooleanElementImpl";}
}

export interface NumberElement extends Parameter{

        /**
         *
         **/
         //minimum
         minimum(  ):number


        /**
         *
         **/
         //maximum
         maximum(  ):number
}

export class NumberElementImpl extends ParameterImpl implements NumberElement{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createNumberElement(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "NumberElementImpl";}


        /**
         *
         **/
         //minimum
         minimum(  ):number{
             return <number>super.attribute('minimum');
         }


        /**
         *
         **/
         //setMinimum
         setMinimum( param:number ){
        {
        this.highLevel().attrOrCreate("minimum").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //maximum
         maximum(  ):number{
             return <number>super.attribute('maximum');
         }


        /**
         *
         **/
         //setMaximum
         setMaximum( param:number ){
        {
        this.highLevel().attrOrCreate("maximum").setValue(""+param);
        return this;
        }
        }
}

export interface IntegerElement extends NumberElement{}

export class IntegerElementImpl extends NumberElementImpl implements IntegerElement{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createIntegerElement(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "IntegerElementImpl";}
}

export interface DateElement extends Parameter{}

export class DateElementImpl extends ParameterImpl implements DateElement{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createDateElement(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "DateElementImpl";}
}

export interface FileElement extends Parameter{}

export class FileElementImpl extends ParameterImpl implements FileElement{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createFileElement(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "FileElementImpl";}
}

export interface MethodBase extends HasNormalParameters{

        /**
         *
         **/
         //responses
         responses(  ):Response[]


        /**
         *
         **/
         //body
         body(  ):BodyLike[]


        /**
         *
         **/
         //is
         is(  ):TraitRef[]


        /**
         *
         **/
         //securedBy
         securedBy(  ):SecuritySchemaRef[]
}

export class MethodBaseImpl extends HasNormalParametersImpl implements MethodBase{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createMethodBase(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "MethodBaseImpl";}


        /**
         *
         **/
         //responses
         responses(  ):Response[]{
             return <Response[]>super.elements('responses');
         }


        /**
         *
         **/
         //body
         body(  ):BodyLike[]{
             return <BodyLike[]>super.elements('body');
         }


        /**
         *
         **/
         //is
         is(  ):TraitRef[]{
             return <TraitRef[]>super.attributes('is', (attr:hl.IAttribute)=>new TraitRefImpl(attr));
         }


        /**
         *
         **/
         //securedBy
         securedBy(  ):SecuritySchemaRef[]{
             return <SecuritySchemaRef[]>super.attributes('securedBy', (attr:hl.IAttribute)=>new SecuritySchemaRefImpl(attr));
         }
}

export interface Response extends RAMLLanguageElement{

        /**
         *
         **/
         //code
         code(  ):StatusCode


        /**
         *
         **/
         //headers
         headers(  ):Parameter[]


        /**
         *
         **/
         //body
         body(  ):BodyLike[]
}

export class ResponseImpl extends RAMLLanguageElementImpl implements Response{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createResponse(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ResponseImpl";}


        /**
         *
         **/
         //code
         code(  ):StatusCode{
             return <StatusCode>super.attribute('code', (attr:hl.IAttribute)=>new StatusCodeImpl(attr));
         }


        /**
         *
         **/
         //headers
         headers(  ):Parameter[]{
             return <Parameter[]>super.elements('headers');
         }


        /**
         *
         **/
         //body
         body(  ):BodyLike[]{
             return <BodyLike[]>super.elements('body');
         }
}

export interface BodyLike extends RAMLLanguageElement{

        /**
         *
         **/
         //name
         name(  ):string


        /**
         *
         **/
         //schema
         schema(  ):SchemaString


        /**
         *
         **/
         //example
         example(  ):ExampleString


        /**
         *
         **/
         //formParameters
         formParameters(  ):Parameter[]
}

export class BodyLikeImpl extends RAMLLanguageElementImpl implements BodyLike{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createBodyLike(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "BodyLikeImpl";}


        /**
         *
         **/
         //name
         name(  ):string{
             return <string>super.attribute('name');
         }


        /**
         *
         **/
         //setName
         setName( param:string ){
        {
        this.highLevel().attrOrCreate("name").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //schema
         schema(  ):SchemaString{
             return <SchemaString>super.attribute('schema', (attr:hl.IAttribute)=>new SchemaStringImpl(attr));
         }


        /**
         *
         **/
         //example
         example(  ):ExampleString{
             return <ExampleString>super.attribute('example', (attr:hl.IAttribute)=>new ExampleStringImpl(attr));
         }


        /**
         *
         **/
         //formParameters
         formParameters(  ):Parameter[]{
             return <Parameter[]>super.elements('formParameters');
         }
}

export interface XMLBody extends BodyLike{

        /**
         *
         **/
         //schema
         schema(  ):XMLSchemaString
}

export class XMLBodyImpl extends BodyLikeImpl implements XMLBody{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createXMLBody(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "XMLBodyImpl";}


        /**
         *
         **/
         //schema
         schema(  ):XMLSchemaString{
             return <XMLSchemaString>super.attribute('schema', (attr:hl.IAttribute)=>new XMLSchemaStringImpl(attr));
         }
}

export interface JSONBody extends BodyLike{

        /**
         *
         **/
         //schema
         schema(  ):JSonSchemaString
}

export class JSONBodyImpl extends BodyLikeImpl implements JSONBody{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createJSONBody(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "JSONBodyImpl";}


        /**
         *
         **/
         //schema
         schema(  ):JSonSchemaString{
             return <JSonSchemaString>super.attribute('schema', (attr:hl.IAttribute)=>new JSonSchemaStringImpl(attr));
         }
}

export interface Trait extends MethodBase{

        /**
         *
         **/
         //name
         name(  ):string


        /**
         *
         **/
         //usage
         usage(  ):string
}

export class TraitImpl extends MethodBaseImpl implements Trait{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createTrait(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "TraitImpl";}


        /**
         *
         **/
         //name
         name(  ):string{
             return <string>super.attribute('name');
         }


        /**
         *
         **/
         //setName
         setName( param:string ){
        {
        this.highLevel().attrOrCreate("name").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //usage
         usage(  ):string{
             return <string>super.attribute('usage');
         }


        /**
         *
         **/
         //setUsage
         setUsage( param:string ){
        {
        this.highLevel().attrOrCreate("usage").setValue(""+param);
        return this;
        }
        }
}

export interface Method extends MethodBase{

        /**
         *
         **/
         //method
         method(  ):string


        /**
         *
         **/
         //protocols
         protocols(  ):string[]


        /**
         *
         **/
         //securedBy
         securedBy(  ):SecuritySchemaRef[]
}

export class MethodImpl extends MethodBaseImpl implements Method{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createMethod(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "MethodImpl";}


        /**
         *
         **/
         //method
         method(  ):string{
             return <string>super.attribute('method');
         }


        /**
         *
         **/
         //setMethod
         setMethod( param:string ){
        {
        this.highLevel().attrOrCreate("method").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //protocols
         protocols(  ):string[]{
             return <string[]>super.attributes('protocols');
         }


        /**
         *
         **/
         //setProtocols
         setProtocols( param:string ){
        {
        this.highLevel().attrOrCreate("protocols").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //securedBy
         securedBy(  ):SecuritySchemaRef[]{
             return <SecuritySchemaRef[]>super.attributes('securedBy', (attr:hl.IAttribute)=>new SecuritySchemaRefImpl(attr));
         }
}

export interface Resource extends RAMLLanguageElement{

        /**
         *
         **/
         //relativeUri
         relativeUri(  ):RelativeUri


        /**
         *
         **/
         //type
         "type"(  ):ResourceTypeRef


        /**
         *
         **/
         //is
         is(  ):TraitRef[]


        /**
         *
         **/
         //securedBy
         securedBy(  ):SecuritySchemaRef[]


        /**
         *
         **/
         //uriParameters
         uriParameters(  ):Parameter[]


        /**
         *
         **/
         //methods
         methods(  ):Method[]


        /**
         *
         **/
         //resources
         resources(  ):Resource[]


        /**
         *
         **/
         //displayName
         displayName(  ):string


        /**
         *
         **/
         //baseUriParameters
         baseUriParameters(  ):Parameter[]
}

export class ResourceImpl extends RAMLLanguageElementImpl implements Resource{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createResource(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ResourceImpl";}


        /**
         *
         **/
         //relativeUri
         relativeUri(  ):RelativeUri{
             return <RelativeUri>super.attribute('relativeUri', (attr:hl.IAttribute)=>new RelativeUriImpl(attr));
         }


        /**
         *
         **/
         //type
         "type"(  ):ResourceTypeRef{
             return <ResourceTypeRef>super.attribute('type', (attr:hl.IAttribute)=>new ResourceTypeRefImpl(attr));
         }


        /**
         *
         **/
         //is
         is(  ):TraitRef[]{
             return <TraitRef[]>super.attributes('is', (attr:hl.IAttribute)=>new TraitRefImpl(attr));
         }


        /**
         *
         **/
         //securedBy
         securedBy(  ):SecuritySchemaRef[]{
             return <SecuritySchemaRef[]>super.attributes('securedBy', (attr:hl.IAttribute)=>new SecuritySchemaRefImpl(attr));
         }


        /**
         *
         **/
         //uriParameters
         uriParameters(  ):Parameter[]{
             return <Parameter[]>super.elements('uriParameters');
         }


        /**
         *
         **/
         //methods
         methods(  ):Method[]{
             return <Method[]>super.elements('methods');
         }


        /**
         *
         **/
         //resources
         resources(  ):Resource[]{
             return <Resource[]>super.elements('resources');
         }


        /**
         *
         **/
         //displayName
         displayName(  ):string{
             return <string>super.attribute('displayName');
         }


        /**
         *
         **/
         //setDisplayName
         setDisplayName( param:string ){
        {
        this.highLevel().attrOrCreate("displayName").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //baseUriParameters
         baseUriParameters(  ):Parameter[]{
             return <Parameter[]>super.elements('baseUriParameters');
         }
}

export interface Api extends RAMLLanguageElement{

        /**
         *
         **/
         //title
         title(  ):string


        /**
         *
         **/
         //version
         version(  ):string


        /**
         *
         **/
         //baseUri
         baseUri(  ):FullUriTemplate


        /**
         *
         **/
         //baseUriParameters
         baseUriParameters(  ):Parameter[]


        /**
         *
         **/
         //uriParameters
         uriParameters(  ):Parameter[]


        /**
         *
         **/
         //protocols
         protocols(  ):string[]


        /**
         *
         **/
         //mediaType
         mediaType(  ):MimeType


        /**
         *
         **/
         //schemas
         schemas(  ):GlobalSchema[]


        /**
         *
         **/
         //traits
         traits(  ):Trait[]


        /**
         *
         **/
         //securedBy
         securedBy(  ):SecuritySchemaRef[]


        /**
         *
         **/
         //securitySchemes
         securitySchemes(  ):SecuritySchema[]


        /**
         *
         **/
         //resourceTypes
         resourceTypes(  ):ResourceType[]


        /**
         *
         **/
         //resources
         resources(  ):Resource[]


        /**
         *
         **/
         //documentation
         documentation(  ):DocumentationItem[]
}

export class ApiImpl extends RAMLLanguageElementImpl implements Api{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createApi(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ApiImpl";}


        /**
         *
         **/
         //title
         title(  ):string{
             return <string>super.attribute('title');
         }


        /**
         *
         **/
         //setTitle
         setTitle( param:string ){
        {
        this.highLevel().attrOrCreate("title").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //version
         version(  ):string{
             return <string>super.attribute('version');
         }


        /**
         *
         **/
         //setVersion
         setVersion( param:string ){
        {
        this.highLevel().attrOrCreate("version").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //baseUri
         baseUri(  ):FullUriTemplate{
             return <FullUriTemplate>super.attribute('baseUri', (attr:hl.IAttribute)=>new FullUriTemplateImpl(attr));
         }


        /**
         *
         **/
         //baseUriParameters
         baseUriParameters(  ):Parameter[]{
             return <Parameter[]>super.elements('baseUriParameters');
         }


        /**
         *
         **/
         //uriParameters
         uriParameters(  ):Parameter[]{
             return <Parameter[]>super.elements('uriParameters');
         }


        /**
         *
         **/
         //protocols
         protocols(  ):string[]{
             return <string[]>super.attributes('protocols');
         }


        /**
         *
         **/
         //setProtocols
         setProtocols( param:string ){
        {
        this.highLevel().attrOrCreate("protocols").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //mediaType
         mediaType(  ):MimeType{
             return <MimeType>super.attribute('mediaType', (attr:hl.IAttribute)=>new MimeTypeImpl(attr));
         }


        /**
         *
         **/
         //schemas
         schemas(  ):GlobalSchema[]{
             return <GlobalSchema[]>super.elements('schemas');
         }


        /**
         *
         **/
         //traits
         traits(  ):Trait[]{
             return <Trait[]>super.elements('traits');
         }


        /**
         *
         **/
         //securedBy
         securedBy(  ):SecuritySchemaRef[]{
             return <SecuritySchemaRef[]>super.attributes('securedBy', (attr:hl.IAttribute)=>new SecuritySchemaRefImpl(attr));
         }


        /**
         *
         **/
         //securitySchemes
         securitySchemes(  ):SecuritySchema[]{
             return <SecuritySchema[]>super.elements('securitySchemes');
         }


        /**
         *
         **/
         //resourceTypes
         resourceTypes(  ):ResourceType[]{
             return <ResourceType[]>super.elements('resourceTypes');
         }


        /**
         *
         **/
         //resources
         resources(  ):Resource[]{
             return <Resource[]>super.elements('resources');
         }


        /**
         *
         **/
         //documentation
         documentation(  ):DocumentationItem[]{
             return <DocumentationItem[]>super.elements('documentation');
         }
}

function createApi(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("Api");
    var node=nc.createStubNode(null,key);
    return node;
}

function createRAMLLanguageElement(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("RAMLLanguageElement");
    var node=nc.createStubNode(null,key);
    return node;
}

function createSecuritySchema(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("SecuritySchema");
    var node=nc.createStubNode(null,key);
    return node;
}

function createSecuritySchemaPart(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("SecuritySchemaPart");
    var node=nc.createStubNode(null,key);
    return node;
}

function createRAMLSimpleElement(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("RAMLSimpleElement");
    var node=nc.createStubNode(null,key);
    return node;
}

function createGlobalSchema(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("GlobalSchema");
    var node=nc.createStubNode(null,key);
    return node;
}

function createDocumentationItem(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("DocumentationItem");
    var node=nc.createStubNode(null,key);
    return node;
}

function createSecuritySchemaSettings(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("SecuritySchemaSettings");
    var node=nc.createStubNode(null,key);
    return node;
}

function createOath1SecurySchemaSettings(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("Oath1SecurySchemaSettings");
    var node=nc.createStubNode(null,key);
    return node;
}

function createOath2SecurySchemaSettings(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("Oath2SecurySchemaSettings");
    var node=nc.createStubNode(null,key);
    return node;
}

function createResourceType(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("ResourceType");
    var node=nc.createStubNode(null,key);
    return node;
}

function createMethod(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("Method");
    var node=nc.createStubNode(null,key);
    return node;
}

function createMethodBase(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("MethodBase");
    var node=nc.createStubNode(null,key);
    return node;
}

function createHasNormalParameters(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("HasNormalParameters");
    var node=nc.createStubNode(null,key);
    return node;
}

function createParameter(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("Parameter");
    var node=nc.createStubNode(null,key);
    return node;
}

function createStrElement(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("StrElement");
    var node=nc.createStubNode(null,key);
    return node;
}

function createBooleanElement(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("BooleanElement");
    var node=nc.createStubNode(null,key);
    return node;
}

function createNumberElement(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("NumberElement");
    var node=nc.createStubNode(null,key);
    return node;
}

function createIntegerElement(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("IntegerElement");
    var node=nc.createStubNode(null,key);
    return node;
}

function createDateElement(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("DateElement");
    var node=nc.createStubNode(null,key);
    return node;
}

function createFileElement(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("FileElement");
    var node=nc.createStubNode(null,key);
    return node;
}

function createResponse(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("Response");
    var node=nc.createStubNode(null,key);
    return node;
}

function createBodyLike(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("BodyLike");
    var node=nc.createStubNode(null,key);
    return node;
}

function createXMLBody(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("XMLBody");
    var node=nc.createStubNode(null,key);
    return node;
}

function createJSONBody(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("JSONBody");
    var node=nc.createStubNode(null,key);
    return node;
}

function createTrait(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("Trait");
    var node=nc.createStubNode(null,key);
    return node;
}

function createResource(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("Resource");
    var node=nc.createStubNode(null,key);
    return node;
}
