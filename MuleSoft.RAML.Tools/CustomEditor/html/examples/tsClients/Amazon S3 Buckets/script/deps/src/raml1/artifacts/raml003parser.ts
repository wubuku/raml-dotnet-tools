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
         //displayName
         displayName(  ):string


        /**
         *
         **/
         //description
         description(  ):MarkdownString


        /**
         *
         **/
         //annotations
         annotations(  ):AnnotationRef[]
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
         //description
         description(  ):MarkdownString{
             return <MarkdownString>super.attribute('description', (attr:hl.IAttribute)=>new MarkdownStringImpl(attr));
         }


        /**
         *
         **/
         //annotations
         annotations(  ):AnnotationRef[]{
             return <AnnotationRef[]>super.attributes('annotations', (attr:hl.IAttribute)=>new AnnotationRefImpl(attr));
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

export interface AnnotationRef extends Reference{}

export class AnnotationRefImpl extends ReferenceImpl implements AnnotationRef{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "AnnotationRefImpl";}
}

export interface DataElementRef extends Reference{}

export class DataElementRefImpl extends ReferenceImpl implements DataElementRef{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "DataElementRefImpl";}
}

export interface ramlexpression extends ValueType{}

export class ramlexpressionImpl extends ValueTypeImpl implements ramlexpression{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ramlexpressionImpl";}
}

export interface AnnotationTarget extends ValueType{}

export class AnnotationTargetImpl extends ValueTypeImpl implements AnnotationTarget{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "AnnotationTargetImpl";}
}

export interface pointer extends ValueType{}

export class pointerImpl extends ValueTypeImpl implements pointer{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "pointerImpl";}
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

export interface StatusCode extends StringType{}

export class StatusCodeImpl extends StringTypeImpl implements StatusCode{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "StatusCodeImpl";}
}

export interface FixedUri extends StringType{}

export class FixedUriImpl extends StringTypeImpl implements FixedUri{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "FixedUriImpl";}
}

export interface ContentType extends StringType{}

export class ContentTypeImpl extends StringTypeImpl implements ContentType{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ContentTypeImpl";}
}

export interface ValidityExpression extends StringType{}

export class ValidityExpressionImpl extends StringTypeImpl implements ValidityExpression{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ValidityExpressionImpl";}
}

export interface DateFormatSpec extends StringType{}

export class DateFormatSpecImpl extends StringTypeImpl implements DateFormatSpec{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "DateFormatSpecImpl";}
}

export interface FunctionalInterface extends StringType{}

export class FunctionalInterfaceImpl extends StringTypeImpl implements FunctionalInterface{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "FunctionalInterfaceImpl";}
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

export interface ScriptingHook extends StringType{}

export class ScriptingHookImpl extends StringTypeImpl implements ScriptingHook{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ScriptingHookImpl";}
}

export interface SecuritySchemaHookScript extends ScriptingHook{}

export class SecuritySchemaHookScriptImpl extends ScriptingHookImpl implements SecuritySchemaHookScript{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "SecuritySchemaHookScriptImpl";}
}

export interface RAMLPointer extends StringType{}

export class RAMLPointerImpl extends StringTypeImpl implements RAMLPointer{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "RAMLPointerImpl";}
}

export interface RAMLSelector extends StringType{}

export class RAMLSelectorImpl extends StringTypeImpl implements RAMLSelector{

        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "RAMLSelectorImpl";}
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

export interface DocumentationItem extends RAMLLanguageElement{

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

export class DocumentationItemImpl extends RAMLLanguageElementImpl implements DocumentationItem{

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

export interface ScriptSpec extends RAMLLanguageElement{

        /**
         *
         **/
         //language
         language(  ):string


        /**
         *
         **/
         //content
         content(  ):string
}

export class ScriptSpecImpl extends RAMLLanguageElementImpl implements ScriptSpec{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createScriptSpec(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ScriptSpecImpl";}


        /**
         *
         **/
         //language
         language(  ):string{
             return <string>super.attribute('language');
         }


        /**
         *
         **/
         //setLanguage
         setLanguage( param:string ){
        {
        this.highLevel().attrOrCreate("language").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //content
         content(  ):string{
             return <string>super.attribute('content');
         }


        /**
         *
         **/
         //setContent
         setContent( param:string ){
        {
        this.highLevel().attrOrCreate("content").setValue(""+param);
        return this;
        }
        }
}

export interface ApiDescription extends RAMLLanguageElement{

        /**
         *
         **/
         //apiFiles
         apiFiles(  ):Api[]


        /**
         *
         **/
         //script
         script(  ):ScriptSpec[]


        /**
         *
         **/
         //type
         "type"(  ):string
}

export class ApiDescriptionImpl extends RAMLLanguageElementImpl implements ApiDescription{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createApiDescription(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ApiDescriptionImpl";}


        /**
         *
         **/
         //apiFiles
         apiFiles(  ):Api[]{
             return <Api[]>super.elements('apiFiles');
         }


        /**
         *
         **/
         //script
         script(  ):ScriptSpec[]{
             return <ScriptSpec[]>super.elements('script');
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
}

export interface CallbackAPIDescription extends ApiDescription{

        /**
         *
         **/
         //callbackFor
         callbackFor(  ):Api
}

export class CallbackAPIDescriptionImpl extends ApiDescriptionImpl implements CallbackAPIDescription{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createCallbackAPIDescription(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "CallbackAPIDescriptionImpl";}


        /**
         *
         **/
         //callbackFor
         callbackFor(  ):Api{
             return <Api>super.element('callbackFor');
         }
}

export interface RAMLProject extends RAMLLanguageElement{

        /**
         *
         **/
         //relatedProjects
         relatedProjects(  ):RAMLProject[]


        /**
         *
         **/
         //declaredApis
         declaredApis(  ):ApiDescription[]


        /**
         *
         **/
         //license
         license(  ):string


        /**
         *
         **/
         //overview
         overview(  ):string


        /**
         *
         **/
         //url
         url(  ):string
}

export class RAMLProjectImpl extends RAMLLanguageElementImpl implements RAMLProject{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createRAMLProject(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "RAMLProjectImpl";}


        /**
         *
         **/
         //relatedProjects
         relatedProjects(  ):RAMLProject[]{
             return <RAMLProject[]>super.elements('relatedProjects');
         }


        /**
         *
         **/
         //declaredApis
         declaredApis(  ):ApiDescription[]{
             return <ApiDescription[]>super.elements('declaredApis');
         }


        /**
         *
         **/
         //license
         license(  ):string{
             return <string>super.attribute('license');
         }


        /**
         *
         **/
         //setLicense
         setLicense( param:string ){
        {
        this.highLevel().attrOrCreate("license").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //overview
         overview(  ):string{
             return <string>super.attribute('overview');
         }


        /**
         *
         **/
         //setOverview
         setOverview( param:string ){
        {
        this.highLevel().attrOrCreate("overview").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //url
         url(  ):string{
             return <string>super.attribute('url');
         }


        /**
         *
         **/
         //setUrl
         setUrl( param:string ){
        {
        this.highLevel().attrOrCreate("url").setValue(""+param);
        return this;
        }
        }
}

export interface SecuritySchemaType extends RAMLLanguageElement{

        /**
         *
         **/
         //requiredSettings
         requiredSettings(  ):DataElement[]


        /**
         *
         **/
         //describedBy
         describedBy(  ):SecuritySchemaPart
}

export class SecuritySchemaTypeImpl extends RAMLLanguageElementImpl implements SecuritySchemaType{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createSecuritySchemaType(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "SecuritySchemaTypeImpl";}


        /**
         *
         **/
         //requiredSettings
         requiredSettings(  ):DataElement[]{
             return <DataElement[]>super.elements('requiredSettings');
         }


        /**
         *
         **/
         //describedBy
         describedBy(  ):SecuritySchemaPart{
             return <SecuritySchemaPart>super.element('describedBy');
         }
}

export interface DataElement extends RAMLLanguageElement{

        /**
         *
         **/
         //name
         name(  ):string


        /**
         *
         **/
         //facets
         facets(  ):DataElement[]


        /**
         *
         **/
         //schema
         schema(  ):string


        /**
         *
         **/
         //usage
         usage(  ):string


        /**
         *
         **/
         //type
         "type"(  ):string[]


        /**
         *
         **/
         //location
         location(  ):ModelLocation


        /**
         *
         **/
         //locationKind
         locationKind(  ):LocationKind


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


        /**
         *
         **/
         //examples
         examples(  ):ExampleSpec[]


        /**
         *
         **/
         //required
         required(  ):boolean
}

export class DataElementImpl extends RAMLLanguageElementImpl implements DataElement{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createDataElement(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "DataElementImpl";}


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
         //facets
         facets(  ):DataElement[]{
             return <DataElement[]>super.elements('facets');
         }


        /**
         *
         **/
         //schema
         schema(  ):string{
             return <string>super.attribute('schema');
         }


        /**
         *
         **/
         //setSchema
         setSchema( param:string ){
        {
        this.highLevel().attrOrCreate("schema").setValue(""+param);
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
         //type
         "type"(  ):string[]{
             return <string[]>super.attributes('type');
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
         location(  ):ModelLocation{
             return <ModelLocation>super.attribute('location', (attr:hl.IAttribute)=>new ModelLocationImpl(attr));
         }


        /**
         *
         **/
         //locationKind
         locationKind(  ):LocationKind{
             return <LocationKind>super.attribute('locationKind', (attr:hl.IAttribute)=>new LocationKindImpl(attr));
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


        /**
         *
         **/
         //examples
         examples(  ):ExampleSpec[]{
             return <ExampleSpec[]>super.elements('examples');
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
}

export interface ModelLocation extends core.AbstractWrapperNode{}

export class ModelLocationImpl implements ModelLocation{

        /**
         *
         **/
         //constructor
         constructor( protected attr:hl.IAttribute ){}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ModelLocationImpl";}
}

export interface LocationKind extends core.AbstractWrapperNode{}

export class LocationKindImpl implements LocationKind{

        /**
         *
         **/
         //constructor
         constructor( protected attr:hl.IAttribute ){}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "LocationKindImpl";}
}

export interface ExampleSpec extends RAMLLanguageElement{

        /**
         *
         **/
         //content
         content(  ):string


        /**
         *
         **/
         //strict
         strict(  ):boolean


        /**
         *
         **/
         //name
         name(  ):string
}

export class ExampleSpecImpl extends RAMLLanguageElementImpl implements ExampleSpec{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createExampleSpec(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ExampleSpecImpl";}


        /**
         *
         **/
         //content
         content(  ):string{
             return <string>super.attribute('content');
         }


        /**
         *
         **/
         //setContent
         setContent( param:string ){
        {
        this.highLevel().attrOrCreate("content").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //strict
         strict(  ):boolean{
             return <boolean>super.attribute('strict');
         }


        /**
         *
         **/
         //setStrict
         setStrict( param:boolean ){
        {
        this.highLevel().attrOrCreate("strict").setValue(""+param);
        return this;
        }
        }


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
}

export interface FileParameter extends DataElement{

        /**
         *
         **/
         //fileTypes
         fileTypes(  ):ContentType[]


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

export class FileParameterImpl extends DataElementImpl implements FileParameter{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createFileParameter(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "FileParameterImpl";}


        /**
         *
         **/
         //fileTypes
         fileTypes(  ):ContentType[]{
             return <ContentType[]>super.attributes('fileTypes', (attr:hl.IAttribute)=>new ContentTypeImpl(attr));
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

export interface ArrayField extends DataElement{

        /**
         *
         **/
         //uniqueItems
         uniqueItems(  ):boolean


        /**
         *
         **/
         //minItems
         minItems(  ):number


        /**
         *
         **/
         //maxItems
         maxItems(  ):number
}

export class ArrayFieldImpl extends DataElementImpl implements ArrayField{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createArrayField(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ArrayFieldImpl";}


        /**
         *
         **/
         //uniqueItems
         uniqueItems(  ):boolean{
             return <boolean>super.attribute('uniqueItems');
         }


        /**
         *
         **/
         //setUniqueItems
         setUniqueItems( param:boolean ){
        {
        this.highLevel().attrOrCreate("uniqueItems").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //minItems
         minItems(  ):number{
             return <number>super.attribute('minItems');
         }


        /**
         *
         **/
         //setMinItems
         setMinItems( param:number ){
        {
        this.highLevel().attrOrCreate("minItems").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //maxItems
         maxItems(  ):number{
             return <number>super.attribute('maxItems');
         }


        /**
         *
         **/
         //setMaxItems
         setMaxItems( param:number ){
        {
        this.highLevel().attrOrCreate("maxItems").setValue(""+param);
        return this;
        }
        }
}

export interface UnionField extends DataElement{

        /**
         *
         **/
         //discriminator
         discriminator(  ):string
}

export class UnionFieldImpl extends DataElementImpl implements UnionField{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createUnionField(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "UnionFieldImpl";}


        /**
         *
         **/
         //discriminator
         discriminator(  ):string{
             return <string>super.attribute('discriminator');
         }


        /**
         *
         **/
         //setDiscriminator
         setDiscriminator( param:string ){
        {
        this.highLevel().attrOrCreate("discriminator").setValue(""+param);
        return this;
        }
        }
}

export interface ObjectField extends DataElement{

        /**
         *
         **/
         //properties
         properties(  ):DataElement[]


        /**
         *
         **/
         //minProperties
         minProperties(  ):number


        /**
         *
         **/
         //maxProperties
         maxProperties(  ):number


        /**
         *
         **/
         //additionalProperties
         additionalProperties(  ):DataElement


        /**
         *
         **/
         //patternProperties
         patternProperties(  ):DataElement[]


        /**
         *
         **/
         //discriminator
         discriminator(  ):pointer


        /**
         *
         **/
         //discriminatorValue
         discriminatorValue(  ):string
}

export class ObjectFieldImpl extends DataElementImpl implements ObjectField{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createObjectField(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ObjectFieldImpl";}


        /**
         *
         **/
         //properties
         properties(  ):DataElement[]{
             return <DataElement[]>super.elements('properties');
         }


        /**
         *
         **/
         //minProperties
         minProperties(  ):number{
             return <number>super.attribute('minProperties');
         }


        /**
         *
         **/
         //setMinProperties
         setMinProperties( param:number ){
        {
        this.highLevel().attrOrCreate("minProperties").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //maxProperties
         maxProperties(  ):number{
             return <number>super.attribute('maxProperties');
         }


        /**
         *
         **/
         //setMaxProperties
         setMaxProperties( param:number ){
        {
        this.highLevel().attrOrCreate("maxProperties").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //additionalProperties
         additionalProperties(  ):DataElement{
             return <DataElement>super.element('additionalProperties');
         }


        /**
         *
         **/
         //patternProperties
         patternProperties(  ):DataElement[]{
             return <DataElement[]>super.elements('patternProperties');
         }


        /**
         *
         **/
         //discriminator
         discriminator(  ):pointer{
             return <pointer>super.attribute('discriminator', (attr:hl.IAttribute)=>new pointerImpl(attr));
         }


        /**
         *
         **/
         //discriminatorValue
         discriminatorValue(  ):string{
             return <string>super.attribute('discriminatorValue');
         }


        /**
         *
         **/
         //setDiscriminatorValue
         setDiscriminatorValue( param:string ){
        {
        this.highLevel().attrOrCreate("discriminatorValue").setValue(""+param);
        return this;
        }
        }
}

export interface StrElement extends DataElement{

        /**
         *
         **/
         //pattern
         pattern(  ):string


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


        /**
         *
         **/
         //enum
         enum(  ):string[]
}

export class StrElementImpl extends DataElementImpl implements StrElement{

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
}

export interface BooleanElement extends DataElement{}

export class BooleanElementImpl extends DataElementImpl implements BooleanElement{

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

export interface ValueElement extends DataElement{}

export class ValueElementImpl extends DataElementImpl implements ValueElement{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createValueElement(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ValueElementImpl";}
}

export interface NumberElement extends DataElement{

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


        /**
         *
         **/
         //enum
         enum(  ):string[]


        /**
         *
         **/
         //format
         format(  ):string
}

export class NumberElementImpl extends DataElementImpl implements NumberElement{

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
         //format
         format(  ):string{
             return <string>super.attribute('format');
         }


        /**
         *
         **/
         //setFormat
         setFormat( param:string ){
        {
        this.highLevel().attrOrCreate("format").setValue(""+param);
        return this;
        }
        }
}

export interface IntegerElement extends NumberElement{

        /**
         *
         **/
         //format
         format(  ):string
}

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


        /**
         *
         **/
         //format
         format(  ):string{
             return <string>super.attribute('format');
         }


        /**
         *
         **/
         //setFormat
         setFormat( param:string ){
        {
        this.highLevel().attrOrCreate("format").setValue(""+param);
        return this;
        }
        }
}

export interface RAMLPointerElement extends DataElement{

        /**
         *
         **/
         //target
         target(  ):RAMLSelector
}

export class RAMLPointerElementImpl extends DataElementImpl implements RAMLPointerElement{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createRAMLPointerElement(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "RAMLPointerElementImpl";}


        /**
         *
         **/
         //target
         target(  ):RAMLSelector{
             return <RAMLSelector>super.attribute('target', (attr:hl.IAttribute)=>new RAMLSelectorImpl(attr));
         }
}

export interface RAMLExpression extends DataElement{}

export class RAMLExpressionImpl extends DataElementImpl implements RAMLExpression{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createRAMLExpression(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "RAMLExpressionImpl";}
}

export interface ScriptHookElement extends DataElement{

        /**
         *
         **/
         //declararedIn
         declararedIn(  ):string


        /**
         *
         **/
         //interfaceName
         interfaceName(  ):string
}

export class ScriptHookElementImpl extends DataElementImpl implements ScriptHookElement{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createScriptHookElement(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ScriptHookElementImpl";}


        /**
         *
         **/
         //declararedIn
         declararedIn(  ):string{
             return <string>super.attribute('declararedIn');
         }


        /**
         *
         **/
         //setDeclararedIn
         setDeclararedIn( param:string ){
        {
        this.highLevel().attrOrCreate("declararedIn").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //interfaceName
         interfaceName(  ):string{
             return <string>super.attribute('interfaceName');
         }


        /**
         *
         **/
         //setInterfaceName
         setInterfaceName( param:string ){
        {
        this.highLevel().attrOrCreate("interfaceName").setValue(""+param);
        return this;
        }
        }
}

export interface SchemaElement extends DataElement{}

export class SchemaElementImpl extends DataElementImpl implements SchemaElement{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createSchemaElement(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "SchemaElementImpl";}
}

export interface DateElement extends DataElement{

        /**
         *
         **/
         //dateFormat
         dateFormat(  ):DateFormatSpec
}

export class DateElementImpl extends DataElementImpl implements DateElement{

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


        /**
         *
         **/
         //dateFormat
         dateFormat(  ):DateFormatSpec{
             return <DateFormatSpec>super.attribute('dateFormat', (attr:hl.IAttribute)=>new DateFormatSpecImpl(attr));
         }
}

export interface HasNormalParameters extends RAMLLanguageElement{

        /**
         *
         **/
         //queryParameters
         queryParameters(  ):DataElement[]


        /**
         *
         **/
         //headers
         headers(  ):DataElement[]
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
         queryParameters(  ):DataElement[]{
             return <DataElement[]>super.elements('queryParameters');
         }


        /**
         *
         **/
         //headers
         headers(  ):DataElement[]{
             return <DataElement[]>super.elements('headers');
         }
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
         body(  ):DataElement[]


        /**
         *
         **/
         //protocols
         protocols(  ):string[]


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
         body(  ):DataElement[]{
             return <DataElement[]>super.elements('body');
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
         headers(  ):DataElement[]


        /**
         *
         **/
         //body
         body(  ):DataElement[]
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
         headers(  ):DataElement[]{
             return <DataElement[]>super.elements('headers');
         }


        /**
         *
         **/
         //body
         body(  ):DataElement[]{
             return <DataElement[]>super.elements('body');
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
         //signature
         signature(  ):SchemaString


        /**
         *
         **/
         //method
         method(  ):string
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
         //signature
         signature(  ):SchemaString{
             return <SchemaString>super.attribute('signature', (attr:hl.IAttribute)=>new SchemaStringImpl(attr));
         }


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
}

export interface SecuritySchemaPart extends MethodBase{}

export class SecuritySchemaPartImpl extends MethodBaseImpl implements SecuritySchemaPart{

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

export interface SecuritySchemaSettings extends RAMLLanguageElement{

        /**
         *
         **/
         //authentificationConfigurator
         authentificationConfigurator(  ):SecuritySchemaHook
}

export class SecuritySchemaSettingsImpl extends RAMLLanguageElementImpl implements SecuritySchemaSettings{

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


        /**
         *
         **/
         //authentificationConfigurator
         authentificationConfigurator(  ):SecuritySchemaHook{
             return <SecuritySchemaHook>super.element('authentificationConfigurator');
         }
}

export interface SecuritySchemaHook extends BasicNode{

        /**
         *
         **/
         //parameters
         parameters(  ):DataElement[]


        /**
         *
         **/
         //script
         script(  ):SecuritySchemaHookScript
}

export class SecuritySchemaHookImpl extends BasicNodeImpl implements SecuritySchemaHook{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createSecuritySchemaHook(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "SecuritySchemaHookImpl";}


        /**
         *
         **/
         //parameters
         parameters(  ):DataElement[]{
             return <DataElement[]>super.elements('parameters');
         }


        /**
         *
         **/
         //script
         script(  ):SecuritySchemaHookScript{
             return <SecuritySchemaHookScript>super.attribute('script', (attr:hl.IAttribute)=>new SecuritySchemaHookScriptImpl(attr));
         }
}

export interface Oath1SecuritySchemaSettings extends SecuritySchemaSettings{

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


        /**
         *
         **/
         //signatures
         signatures(  ):string[]
}

export class Oath1SecuritySchemaSettingsImpl extends SecuritySchemaSettingsImpl implements Oath1SecuritySchemaSettings{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createOath1SecuritySchemaSettings(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "Oath1SecuritySchemaSettingsImpl";}


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


        /**
         *
         **/
         //signatures
         signatures(  ):string[]{
             return <string[]>super.attributes('signatures');
         }


        /**
         *
         **/
         //setSignatures
         setSignatures( param:string ){
        {
        this.highLevel().attrOrCreate("signatures").setValue(""+param);
        return this;
        }
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

export interface APIKeySettings extends SecuritySchemaSettings{

        /**
         *
         **/
         //queryParameterName
         queryParameterName(  ):string


        /**
         *
         **/
         //headerName
         headerName(  ):string
}

export class APIKeySettingsImpl extends SecuritySchemaSettingsImpl implements APIKeySettings{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createAPIKeySettings(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "APIKeySettingsImpl";}


        /**
         *
         **/
         //queryParameterName
         queryParameterName(  ):string{
             return <string>super.attribute('queryParameterName');
         }


        /**
         *
         **/
         //setQueryParameterName
         setQueryParameterName( param:string ){
        {
        this.highLevel().attrOrCreate("queryParameterName").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //headerName
         headerName(  ):string{
             return <string>super.attribute('headerName');
         }


        /**
         *
         **/
         //setHeaderName
         setHeaderName( param:string ){
        {
        this.highLevel().attrOrCreate("headerName").setValue(""+param);
        return this;
        }
        }
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

export interface Oath2 extends SecuritySchema{

        /**
         *
         **/
         //settings
         settings(  ):Oath2SecurySchemaSettings
}

export class Oath2Impl extends SecuritySchemaImpl implements Oath2{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createOath2(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "Oath2Impl";}


        /**
         *
         **/
         //settings
         settings(  ):Oath2SecurySchemaSettings{
             return <Oath2SecurySchemaSettings>super.element('settings');
         }
}

export interface Oath1 extends SecuritySchema{

        /**
         *
         **/
         //settings
         settings(  ):Oath1SecuritySchemaSettings
}

export class Oath1Impl extends SecuritySchemaImpl implements Oath1{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createOath1(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "Oath1Impl";}


        /**
         *
         **/
         //settings
         settings(  ):Oath1SecuritySchemaSettings{
             return <Oath1SecuritySchemaSettings>super.element('settings');
         }
}

export interface APIKey extends SecuritySchema{

        /**
         *
         **/
         //settings
         settings(  ):APIKeySettings
}

export class APIKeyImpl extends SecuritySchemaImpl implements APIKey{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createAPIKey(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "APIKeyImpl";}


        /**
         *
         **/
         //settings
         settings(  ):APIKeySettings{
             return <APIKeySettings>super.element('settings');
         }
}

export interface Basic extends SecuritySchema{}

export class BasicImpl extends SecuritySchemaImpl implements Basic{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createBasic(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "BasicImpl";}
}

export interface Digest extends SecuritySchema{}

export class DigestImpl extends SecuritySchemaImpl implements Digest{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createDigest(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "DigestImpl";}
}

export interface Custom extends SecuritySchema{}

export class CustomImpl extends SecuritySchemaImpl implements Custom{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createCustom(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "CustomImpl";}
}

export interface ResourceBase extends RAMLLanguageElement{

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
         uriParameters(  ):DataElement[]
}

export class ResourceBaseImpl extends RAMLLanguageElementImpl implements ResourceBase{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createResourceBase(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ResourceBaseImpl";}


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
         uriParameters(  ):DataElement[]{
             return <DataElement[]>super.elements('uriParameters');
         }
}

export interface ResourceType extends ResourceBase{

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

export class ResourceTypeImpl extends ResourceBaseImpl implements ResourceType{

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
}

export interface Resource extends ResourceBase{

        /**
         *
         **/
         //signature
         signature(  ):SchemaString


        /**
         *
         **/
         //relativeUri
         relativeUri(  ):RelativeUri


        /**
         *
         **/
         //resources
         resources(  ):Resource[]
}

export class ResourceImpl extends ResourceBaseImpl implements Resource{

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
         //signature
         signature(  ):SchemaString{
             return <SchemaString>super.attribute('signature', (attr:hl.IAttribute)=>new SchemaStringImpl(attr));
         }


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
         //resources
         resources(  ):Resource[]{
             return <Resource[]>super.elements('resources');
         }
}

export interface AnnotationType extends RAMLLanguageElement{

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
         //parameters
         parameters(  ):DataElement[]


        /**
         *
         **/
         //allowMultiple
         allowMultiple(  ):boolean


        /**
         *
         **/
         //allowedTargets
         allowedTargets(  ):AnnotationTarget[]
}

export class AnnotationTypeImpl extends RAMLLanguageElementImpl implements AnnotationType{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createAnnotationType(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "AnnotationTypeImpl";}


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
         //parameters
         parameters(  ):DataElement[]{
             return <DataElement[]>super.elements('parameters');
         }


        /**
         *
         **/
         //allowMultiple
         allowMultiple(  ):boolean{
             return <boolean>super.attribute('allowMultiple');
         }


        /**
         *
         **/
         //setAllowMultiple
         setAllowMultiple( param:boolean ){
        {
        this.highLevel().attrOrCreate("allowMultiple").setValue(""+param);
        return this;
        }
        }


        /**
         *
         **/
         //allowedTargets
         allowedTargets(  ):AnnotationTarget[]{
             return <AnnotationTarget[]>super.attributes('allowedTargets', (attr:hl.IAttribute)=>new AnnotationTargetImpl(attr));
         }
}

export interface Library extends RAMLLanguageElement{

        /**
         *
         **/
         //title
         title(  ):string


        /**
         *
         **/
         //name
         name(  ):string


        /**
         *
         **/
         //schemas
         schemas(  ):GlobalSchema[]


        /**
         *
         **/
         //types
         types(  ):DataElement[]


        /**
         *
         **/
         //traits
         traits(  ):Trait[]


        /**
         *
         **/
         //resourceTypes
         resourceTypes(  ):ResourceType[]


        /**
         *
         **/
         //annotationTypes
         annotationTypes(  ):AnnotationType[]


        /**
         *
         **/
         //securitySchemaTypes
         securitySchemaTypes(  ):SecuritySchemaType[]


        /**
         *
         **/
         //securitySchemes
         securitySchemes(  ):SecuritySchema[]


        /**
         *
         **/
         //uses
         uses(  ):Library[]
}

export class LibraryImpl extends RAMLLanguageElementImpl implements Library{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createLibrary(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "LibraryImpl";}


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
         //schemas
         schemas(  ):GlobalSchema[]{
             return <GlobalSchema[]>super.elements('schemas');
         }


        /**
         *
         **/
         //types
         types(  ):DataElement[]{
             return <DataElement[]>super.elements('types');
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
         //resourceTypes
         resourceTypes(  ):ResourceType[]{
             return <ResourceType[]>super.elements('resourceTypes');
         }


        /**
         *
         **/
         //annotationTypes
         annotationTypes(  ):AnnotationType[]{
             return <AnnotationType[]>super.elements('annotationTypes');
         }


        /**
         *
         **/
         //securitySchemaTypes
         securitySchemaTypes(  ):SecuritySchemaType[]{
             return <SecuritySchemaType[]>super.elements('securitySchemaTypes');
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
         //uses
         uses(  ):Library[]{
             return <Library[]>super.elements('uses');
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

export interface ImportDeclaration extends RAMLSimpleElement{

        /**
         *
         **/
         //key
         key(  ):string


        /**
         *
         **/
         //value
         value(  ):Library
}

export class ImportDeclarationImpl extends RAMLSimpleElementImpl implements ImportDeclaration{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createImportDeclaration(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ImportDeclarationImpl";}


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
         value(  ):Library{
             return <Library>super.element('value');
         }
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

export interface Api extends Library{

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
         baseUriParameters(  ):DataElement[]


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
         //securedBy
         securedBy(  ):SecuritySchemaRef[]


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

export class ApiImpl extends LibraryImpl implements Api{

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
         baseUriParameters(  ):DataElement[]{
             return <DataElement[]>super.elements('baseUriParameters');
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
         //securedBy
         securedBy(  ):SecuritySchemaRef[]{
             return <SecuritySchemaRef[]>super.attributes('securedBy', (attr:hl.IAttribute)=>new SecuritySchemaRefImpl(attr));
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

export interface Overlay extends Api{

        /**
         *
         **/
         //masterRef
         masterRef(  ):string
}

export class OverlayImpl extends ApiImpl implements Overlay{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createOverlay(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "OverlayImpl";}


        /**
         *
         **/
         //masterRef
         masterRef(  ):string{
             return <string>super.attribute('masterRef');
         }


        /**
         *
         **/
         //setMasterRef
         setMasterRef( param:string ){
        {
        this.highLevel().attrOrCreate("masterRef").setValue(""+param);
        return this;
        }
        }
}

export interface Extension extends Api{

        /**
         *
         **/
         //masterRef
         masterRef(  ):string
}

export class ExtensionImpl extends ApiImpl implements Extension{

        /**
         *
         **/
         //constructor
         constructor( protected nodeOrKey:hl.IHighLevelNode|string ){super((typeof  nodeOrKey=="string")?createExtension(<string>nodeOrKey):<hl.IHighLevelNode>nodeOrKey)}


        /**
         *
         **/
         //wrapperClassName
         wrapperClassName(  ):string{return "ExtensionImpl";}


        /**
         *
         **/
         //masterRef
         masterRef(  ):string{
             return <string>super.attribute('masterRef');
         }


        /**
         *
         **/
         //setMasterRef
         setMasterRef( param:string ){
        {
        this.highLevel().attrOrCreate("masterRef").setValue(""+param);
        return this;
        }
        }
}

function createApi(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("Api");
    var node=nc.createStubNode(null,key);
    return node;
}

function createLibrary(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("Library");
    var node=nc.createStubNode(null,key);
    return node;
}

function createRAMLLanguageElement(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("RAMLLanguageElement");
    var node=nc.createStubNode(null,key);
    return node;
}

function createDocumentationItem(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("DocumentationItem");
    var node=nc.createStubNode(null,key);
    return node;
}

function createScriptSpec(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("ScriptSpec");
    var node=nc.createStubNode(null,key);
    return node;
}

function createApiDescription(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("ApiDescription");
    var node=nc.createStubNode(null,key);
    return node;
}

function createCallbackAPIDescription(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("CallbackAPIDescription");
    var node=nc.createStubNode(null,key);
    return node;
}

function createRAMLProject(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("RAMLProject");
    var node=nc.createStubNode(null,key);
    return node;
}

function createSecuritySchemaType(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("SecuritySchemaType");
    var node=nc.createStubNode(null,key);
    return node;
}

function createDataElement(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("DataElement");
    var node=nc.createStubNode(null,key);
    return node;
}

function createExampleSpec(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("ExampleSpec");
    var node=nc.createStubNode(null,key);
    return node;
}

function createFileParameter(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("FileParameter");
    var node=nc.createStubNode(null,key);
    return node;
}

function createArrayField(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("ArrayField");
    var node=nc.createStubNode(null,key);
    return node;
}

function createUnionField(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("UnionField");
    var node=nc.createStubNode(null,key);
    return node;
}

function createObjectField(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("ObjectField");
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

function createValueElement(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("ValueElement");
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

function createRAMLPointerElement(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("RAMLPointerElement");
    var node=nc.createStubNode(null,key);
    return node;
}

function createRAMLExpression(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("RAMLExpression");
    var node=nc.createStubNode(null,key);
    return node;
}

function createScriptHookElement(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("ScriptHookElement");
    var node=nc.createStubNode(null,key);
    return node;
}

function createSchemaElement(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("SchemaElement");
    var node=nc.createStubNode(null,key);
    return node;
}

function createDateElement(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("DateElement");
    var node=nc.createStubNode(null,key);
    return node;
}

function createSecuritySchemaPart(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("SecuritySchemaPart");
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

function createResponse(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("Response");
    var node=nc.createStubNode(null,key);
    return node;
}

function createTrait(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("Trait");
    var node=nc.createStubNode(null,key);
    return node;
}

function createMethod(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("Method");
    var node=nc.createStubNode(null,key);
    return node;
}

function createSecuritySchemaSettings(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("SecuritySchemaSettings");
    var node=nc.createStubNode(null,key);
    return node;
}

function createSecuritySchemaHook(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("SecuritySchemaHook");
    var node=nc.createStubNode(null,key);
    return node;
}

function createOath1SecuritySchemaSettings(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("Oath1SecuritySchemaSettings");
    var node=nc.createStubNode(null,key);
    return node;
}

function createOath2SecurySchemaSettings(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("Oath2SecurySchemaSettings");
    var node=nc.createStubNode(null,key);
    return node;
}

function createAPIKeySettings(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("APIKeySettings");
    var node=nc.createStubNode(null,key);
    return node;
}

function createSecuritySchema(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("SecuritySchema");
    var node=nc.createStubNode(null,key);
    return node;
}

function createOath2(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("Oath2");
    var node=nc.createStubNode(null,key);
    return node;
}

function createOath1(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("Oath1");
    var node=nc.createStubNode(null,key);
    return node;
}

function createAPIKey(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("APIKey");
    var node=nc.createStubNode(null,key);
    return node;
}

function createBasic(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("Basic");
    var node=nc.createStubNode(null,key);
    return node;
}

function createDigest(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("Digest");
    var node=nc.createStubNode(null,key);
    return node;
}

function createCustom(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("Custom");
    var node=nc.createStubNode(null,key);
    return node;
}

function createResourceBase(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("ResourceBase");
    var node=nc.createStubNode(null,key);
    return node;
}

function createResourceType(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("ResourceType");
    var node=nc.createStubNode(null,key);
    return node;
}

function createResource(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("Resource");
    var node=nc.createStubNode(null,key);
    return node;
}

function createAnnotationType(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("AnnotationType");
    var node=nc.createStubNode(null,key);
    return node;
}

function createGlobalSchema(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("GlobalSchema");
    var node=nc.createStubNode(null,key);
    return node;
}

function createRAMLSimpleElement(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("RAMLSimpleElement");
    var node=nc.createStubNode(null,key);
    return node;
}

function createImportDeclaration(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("ImportDeclaration");
    var node=nc.createStubNode(null,key);
    return node;
}

function createOverlay(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("Overlay");
    var node=nc.createStubNode(null,key);
    return node;
}

function createExtension(key:string){
    var universe=hl.universeProvider("RAML10");
    var nc=<def.NodeClass>universe.getType("Extension");
    var node=nc.createStubNode(null,key);
    return node;
}
