package com.mulesoft.raml1.java.parser.model.bodies;

import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.model.systemTypes.XMLSchemaString;



public interface XMLBody extends BodyLike {

    @XmlElement(name="schema")
    XMLSchemaString schema();

}