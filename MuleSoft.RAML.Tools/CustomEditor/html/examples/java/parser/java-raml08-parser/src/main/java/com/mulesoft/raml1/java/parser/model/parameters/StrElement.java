package com.mulesoft.raml1.java.parser.model.parameters;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;



public interface StrElement extends Parameter {

    @XmlElement(name="pattern")
    String pattern();


    @XmlElement(name="enum")
    List<String> enum_();


    @XmlElement(name="minLength")
    Double minLength();


    @XmlElement(name="maxLength")
    Double maxLength();

}