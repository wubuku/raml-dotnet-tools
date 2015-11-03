package com.mulesoft.raml1.java.parser.model.parameters;

import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.model.common.RAMLLanguageElement;



public interface Parameter extends RAMLLanguageElement {

    @XmlElement(name="name")
    String name();


    @XmlElement(name="displayName")
    String displayName();


    @XmlElement(name="type")
    String type();


    @XmlElement(name="location")
    ParameterLocation location();


    @XmlElement(name="required")
    Boolean required();


    @XmlElement(name="default")
    String default_();


    @XmlElement(name="example")
    String example();


    @XmlElement(name="repeat")
    Boolean repeat();

}