package com.mulesoft.raml1.java.parser.model.methodsAndResources;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.model.common.RAMLLanguageElement;
import com.mulesoft.raml1.java.parser.model.systemTypes.RelativeUri;
import com.mulesoft.raml1.java.parser.model.parameters.Parameter;



public interface Resource extends RAMLLanguageElement {

    @XmlElement(name="relativeUri")
    RelativeUri relativeUri();


    @XmlElement(name="type")
    ResourceTypeRef type();


    @XmlElement(name="is")
    List<TraitRef> is();


    @XmlElement(name="securedBy")
    List<SecuritySchemaRef> securedBy();


    @XmlElement(name="uriParameters")
    List<Parameter> uriParameters();


    @XmlElement(name="methods")
    List<Method> methods();


    @XmlElement(name="resources")
    List<Resource> resources();


    @XmlElement(name="displayName")
    String displayName();


    @XmlElement(name="baseUriParameters")
    List<Parameter> baseUriParameters();

}