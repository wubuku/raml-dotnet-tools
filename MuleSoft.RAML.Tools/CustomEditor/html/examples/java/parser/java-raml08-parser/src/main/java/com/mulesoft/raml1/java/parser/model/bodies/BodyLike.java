package com.mulesoft.raml1.java.parser.model.bodies;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.model.common.RAMLLanguageElement;
import com.mulesoft.raml1.java.parser.model.systemTypes.SchemaString;
import com.mulesoft.raml1.java.parser.model.systemTypes.ExampleString;
import com.mulesoft.raml1.java.parser.model.parameters.Parameter;



public interface BodyLike extends RAMLLanguageElement {

    @XmlElement(name="name")
    String name();


    @XmlElement(name="schema")
    SchemaString schema();


    @XmlElement(name="example")
    ExampleString example();


    @XmlElement(name="formParameters")
    List<Parameter> formParameters();

}