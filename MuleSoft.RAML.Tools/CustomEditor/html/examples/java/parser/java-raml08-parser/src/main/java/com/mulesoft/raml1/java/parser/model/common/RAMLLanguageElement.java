package com.mulesoft.raml1.java.parser.model.common;

import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.IJavaElementNode;
import com.mulesoft.raml1.java.parser.model.systemTypes.MarkdownString;



public interface RAMLLanguageElement extends IJavaElementNode {

    @XmlElement(name="description")
    MarkdownString description();

}