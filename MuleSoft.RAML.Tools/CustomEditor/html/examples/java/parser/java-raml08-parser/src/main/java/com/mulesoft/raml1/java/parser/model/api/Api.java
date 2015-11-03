package com.mulesoft.raml1.java.parser.model.api;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.model.common.RAMLLanguageElement;
import com.mulesoft.raml1.java.parser.model.systemTypes.FullUriTemplate;
import com.mulesoft.raml1.java.parser.model.parameters.Parameter;
import com.mulesoft.raml1.java.parser.model.bodies.MimeType;
import com.mulesoft.raml1.java.parser.model.methodsAndResources.Trait;
import com.mulesoft.raml1.java.parser.model.methodsAndResources.SecuritySchemaRef;
import com.mulesoft.raml1.java.parser.model.methodsAndResources.SecuritySchema;
import com.mulesoft.raml1.java.parser.model.methodsAndResources.ResourceType;
import com.mulesoft.raml1.java.parser.model.methodsAndResources.Resource;



public interface Api extends RAMLLanguageElement {

    @XmlElement(name="title")
    String title();


    @XmlElement(name="version")
    String version();


    @XmlElement(name="baseUri")
    FullUriTemplate baseUri();


    @XmlElement(name="baseUriParameters")
    List<Parameter> baseUriParameters();


    @XmlElement(name="uriParameters")
    List<Parameter> uriParameters();


    @XmlElement(name="protocols")
    List<String> protocols();


    @XmlElement(name="mediaType")
    MimeType mediaType();


    @XmlElement(name="schemas")
    List<GlobalSchema> schemas();


    @XmlElement(name="traits")
    List<Trait> traits();


    @XmlElement(name="securedBy")
    List<SecuritySchemaRef> securedBy();


    @XmlElement(name="securitySchemes")
    List<SecuritySchema> securitySchemes();


    @XmlElement(name="resourceTypes")
    List<ResourceType> resourceTypes();


    @XmlElement(name="resources")
    List<Resource> resources();


    @XmlElement(name="documentation")
    List<DocumentationItem> documentation();

}