package com.mulesoft.raml1.java.parser.model.methodsAndResources;

import javax.xml.bind.annotation.XmlElement;



public interface ResourceType extends ResourceBase {

    @XmlElement(name="name")
    String name();


    @XmlElement(name="usage")
    String usage();

}