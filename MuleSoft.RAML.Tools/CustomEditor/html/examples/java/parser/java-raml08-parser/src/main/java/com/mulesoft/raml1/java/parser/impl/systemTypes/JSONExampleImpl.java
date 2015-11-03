package com.mulesoft.raml1.java.parser.impl.systemTypes;

import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.JavaNodeFactory;
import com.mulesoft.raml1.java.parser.model.systemTypes.JSONExample;



public class JSONExampleImpl extends ExampleStringImpl implements JSONExample {

    public JSONExampleImpl(Object jsNode, JavaNodeFactory factory){
        super(jsNode,factory);
    }

    protected JSONExampleImpl(){
        super();
    }



}