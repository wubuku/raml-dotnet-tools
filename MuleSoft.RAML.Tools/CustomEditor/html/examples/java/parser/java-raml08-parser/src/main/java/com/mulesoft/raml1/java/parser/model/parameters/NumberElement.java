package com.mulesoft.raml1.java.parser.model.parameters;

import javax.xml.bind.annotation.XmlElement;



public interface NumberElement extends Parameter {

    @XmlElement(name="minimum")
    Double minimum();


    @XmlElement(name="maximum")
    Double maximum();

}