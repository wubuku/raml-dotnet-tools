package com.mulesoft.raml1.java.parser.model.bodies;

import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.model.systemTypes.JSonSchemaString;



public interface JSONBody extends BodyLike {

    @XmlElement(name="schema")
    JSonSchemaString schema();

}