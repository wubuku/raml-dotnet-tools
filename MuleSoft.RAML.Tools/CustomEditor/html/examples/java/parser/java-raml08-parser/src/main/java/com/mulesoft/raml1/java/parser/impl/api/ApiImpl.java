package com.mulesoft.raml1.java.parser.impl.api;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.JavaNodeFactory;
import com.mulesoft.raml1.java.parser.impl.common.RAMLLanguageElementImpl;
import com.mulesoft.raml1.java.parser.model.api.Api;
import com.mulesoft.raml1.java.parser.model.systemTypes.FullUriTemplate;
import com.mulesoft.raml1.java.parser.impl.systemTypes.FullUriTemplateImpl;
import com.mulesoft.raml1.java.parser.model.parameters.Parameter;
import com.mulesoft.raml1.java.parser.impl.parameters.ParameterImpl;
import com.mulesoft.raml1.java.parser.model.bodies.MimeType;
import com.mulesoft.raml1.java.parser.impl.bodies.MimeTypeImpl;
import com.mulesoft.raml1.java.parser.model.api.GlobalSchema;
import com.mulesoft.raml1.java.parser.impl.api.GlobalSchemaImpl;
import com.mulesoft.raml1.java.parser.model.methodsAndResources.Trait;
import com.mulesoft.raml1.java.parser.impl.methodsAndResources.TraitImpl;
import com.mulesoft.raml1.java.parser.model.methodsAndResources.SecuritySchemaRef;
import com.mulesoft.raml1.java.parser.impl.methodsAndResources.SecuritySchemaRefImpl;
import com.mulesoft.raml1.java.parser.model.methodsAndResources.SecuritySchema;
import com.mulesoft.raml1.java.parser.impl.methodsAndResources.SecuritySchemaImpl;
import com.mulesoft.raml1.java.parser.model.methodsAndResources.ResourceType;
import com.mulesoft.raml1.java.parser.impl.methodsAndResources.ResourceTypeImpl;
import com.mulesoft.raml1.java.parser.model.methodsAndResources.Resource;
import com.mulesoft.raml1.java.parser.impl.methodsAndResources.ResourceImpl;
import com.mulesoft.raml1.java.parser.model.api.DocumentationItem;
import com.mulesoft.raml1.java.parser.impl.api.DocumentationItemImpl;



public class ApiImpl extends RAMLLanguageElementImpl implements Api {

    public ApiImpl(Object jsNode, JavaNodeFactory factory){
        super(jsNode,factory);
    }

    protected ApiImpl(){
        super();
    }


    @XmlElement(name="title")
    public String title(){
        return super.getAttribute("title", String.class);
    }


    @XmlElement(name="version")
    public String version(){
        return super.getAttribute("version", String.class);
    }


    @XmlElement(name="baseUri")
    public FullUriTemplate baseUri(){
        return super.getAttribute("baseUri", FullUriTemplateImpl.class);
    }


    @XmlElement(name="baseUriParameters")
    public List<Parameter> baseUriParameters(){
        return super.getElements("baseUriParameters", ParameterImpl.class);
    }


    @XmlElement(name="uriParameters")
    public List<Parameter> uriParameters(){
        return super.getElements("uriParameters", ParameterImpl.class);
    }


    @XmlElement(name="protocols")
    public List<String> protocols(){
        return super.getAttributes("protocols", String.class);
    }


    @XmlElement(name="mediaType")
    public MimeType mediaType(){
        return super.getAttribute("mediaType", MimeTypeImpl.class);
    }


    @XmlElement(name="schemas")
    public List<GlobalSchema> schemas(){
        return super.getElements("schemas", GlobalSchemaImpl.class);
    }


    @XmlElement(name="traits")
    public List<Trait> traits(){
        return super.getElements("traits", TraitImpl.class);
    }


    @XmlElement(name="securedBy")
    public List<SecuritySchemaRef> securedBy(){
        return super.getAttributes("securedBy", SecuritySchemaRefImpl.class);
    }


    @XmlElement(name="securitySchemes")
    public List<SecuritySchema> securitySchemes(){
        return super.getElements("securitySchemes", SecuritySchemaImpl.class);
    }


    @XmlElement(name="resourceTypes")
    public List<ResourceType> resourceTypes(){
        return super.getElements("resourceTypes", ResourceTypeImpl.class);
    }


    @XmlElement(name="resources")
    public List<Resource> resources(){
        return super.getElements("resources", ResourceImpl.class);
    }


    @XmlElement(name="documentation")
    public List<DocumentationItem> documentation(){
        return super.getElements("documentation", DocumentationItemImpl.class);
    }
}