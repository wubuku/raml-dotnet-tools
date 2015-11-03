package com.mulesoft.raml1.java.parser.model.datamodel;

import javax.xml.bind.annotation.XmlElement;



public interface ScriptHookElement extends DataElement {

    @XmlElement(name="declararedIn")
    String declararedIn();


    @XmlElement(name="interfaceName")
    String interfaceName();

}