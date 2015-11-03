package com.mulesoft.raml1.java.parser.impl.parameters;

import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.JavaNodeFactory;
import com.mulesoft.raml1.java.parser.model.parameters.IntegerElement;



public class IntegerElementImpl extends NumberElementImpl implements IntegerElement {

    public IntegerElementImpl(Object jsNode, JavaNodeFactory factory){
        super(jsNode,factory);
    }

    protected IntegerElementImpl(){
        super();
    }



}