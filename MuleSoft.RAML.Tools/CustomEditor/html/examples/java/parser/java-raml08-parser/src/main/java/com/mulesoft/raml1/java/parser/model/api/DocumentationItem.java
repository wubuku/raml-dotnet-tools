package com.mulesoft.raml1.java.parser.model.api;

import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.model.common.RAMLSimpleElement;
import com.mulesoft.raml1.java.parser.model.systemTypes.MarkdownString;



public interface DocumentationItem extends RAMLSimpleElement {

    @XmlElement(name="title")
    String title();


    @XmlElement(name="content")
    MarkdownString content();

}