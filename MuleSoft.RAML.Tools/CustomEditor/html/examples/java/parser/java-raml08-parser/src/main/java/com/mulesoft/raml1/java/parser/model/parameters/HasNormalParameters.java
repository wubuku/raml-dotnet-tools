package com.mulesoft.raml1.java.parser.model.parameters;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.model.common.RAMLLanguageElement;



public interface HasNormalParameters extends RAMLLanguageElement {

    @XmlElement(name="queryParameters")
    List<Parameter> queryParameters();


    @XmlElement(name="displayName")
    String displayName();


    @XmlElement(name="headers")
    List<Parameter> headers();

}