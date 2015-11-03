package com.mulesoft.raml1.java.parser.impl.parameters;

import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.JavaNodeFactory;
import com.mulesoft.raml1.java.parser.model.parameters.NumberElement;



public class NumberElementImpl extends ParameterImpl implements NumberElement {

    public NumberElementImpl(Object jsNode, JavaNodeFactory factory){
        super(jsNode,factory);
    }

    protected NumberElementImpl(){
        super();
    }


    @XmlElement(name="minimum")
    public Double minimum(){
        return super.getAttribute("minimum", Double.class);
    }


    @XmlElement(name="maximum")
    public Double maximum(){
        return super.getAttribute("maximum", Double.class);
    }
}