package com.mulesoft.raml1.java.parser.impl.parameters;

import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.JavaNodeFactory;
import com.mulesoft.raml1.java.parser.model.parameters.DateElement;



public class DateElementImpl extends ParameterImpl implements DateElement {

    public DateElementImpl(Object jsNode, JavaNodeFactory factory){
        super(jsNode,factory);
    }

    protected DateElementImpl(){
        super();
    }



}