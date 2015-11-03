package com.mulesoft.raml1.java.parser.impl.datamodel;

import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.JavaNodeFactory;
import com.mulesoft.raml1.java.parser.impl.common.RAMLLanguageElementImpl;
import com.mulesoft.raml1.java.parser.model.datamodel.ExampleSpec;



public class ExampleSpecImpl extends RAMLLanguageElementImpl implements ExampleSpec {

    public ExampleSpecImpl(Object jsNode, JavaNodeFactory factory){
        super(jsNode,factory);
    }

    protected ExampleSpecImpl(){
        super();
    }


    @XmlElement(name="content")
    public String content(){
        return super.getAttribute("content", String.class);
    }


    @XmlElement(name="strict")
    public Boolean strict(){
        return super.getAttribute("strict", Boolean.class);
    }


    @XmlElement(name="name")
    public String name(){
        return super.getAttribute("name", String.class);
    }
}