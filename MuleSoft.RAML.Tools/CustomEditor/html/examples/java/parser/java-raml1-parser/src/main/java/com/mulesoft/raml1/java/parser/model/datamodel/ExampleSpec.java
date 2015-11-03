package com.mulesoft.raml1.java.parser.model.datamodel;

import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.model.common.RAMLLanguageElement;



public interface ExampleSpec extends RAMLLanguageElement {

    @XmlElement(name="content")
    String content();


    @XmlElement(name="strict")
    Boolean strict();


    @XmlElement(name="name")
    String name();

}