package com.mulesoft.raml1.java.parser.impl.systemTypes;

import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.JavaNodeFactory;
import com.mulesoft.raml1.java.parser.model.systemTypes.XMLExample;



public class XMLExampleImpl extends ExampleStringImpl implements XMLExample {

    public XMLExampleImpl(Object jsNode, JavaNodeFactory factory){
        super(jsNode,factory);
    }

    protected XMLExampleImpl(){
        super();
    }



}