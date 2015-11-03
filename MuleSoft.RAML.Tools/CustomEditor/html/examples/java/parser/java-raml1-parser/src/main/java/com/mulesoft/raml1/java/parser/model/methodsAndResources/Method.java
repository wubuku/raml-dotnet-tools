package com.mulesoft.raml1.java.parser.model.methodsAndResources;

import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.model.systemTypes.SchemaString;



public interface Method extends MethodBase {

    @XmlElement(name="signature")
    SchemaString signature();


    @XmlElement(name="method")
    String method();

}