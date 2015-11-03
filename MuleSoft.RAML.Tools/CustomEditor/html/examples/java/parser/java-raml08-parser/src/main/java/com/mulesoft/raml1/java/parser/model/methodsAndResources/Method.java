package com.mulesoft.raml1.java.parser.model.methodsAndResources;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;



public interface Method extends MethodBase {

    @XmlElement(name="method")
    String method();


    @XmlElement(name="protocols")
    List<String> protocols();


    @XmlElement(name="securedBy")
    List<SecuritySchemaRef> securedBy();

}