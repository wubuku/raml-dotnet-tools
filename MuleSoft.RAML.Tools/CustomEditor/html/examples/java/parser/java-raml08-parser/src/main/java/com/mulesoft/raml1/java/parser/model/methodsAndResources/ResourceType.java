package com.mulesoft.raml1.java.parser.model.methodsAndResources;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.model.common.RAMLLanguageElement;
import com.mulesoft.raml1.java.parser.model.parameters.Parameter;



public interface ResourceType extends RAMLLanguageElement {

    @XmlElement(name="name")
    String name();


    @XmlElement(name="usage")
    String usage();


    @XmlElement(name="methods")
    List<Method> methods();


    @XmlElement(name="is")
    List<TraitRef> is();


    @XmlElement(name="type")
    ResourceTypeRef type();


    @XmlElement(name="securedBy")
    List<SecuritySchemaRef> securedBy();


    @XmlElement(name="uriParameters")
    List<Parameter> uriParameters();

}