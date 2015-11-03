package com.mulesoft.raml1.java.parser.impl.parameters;

import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.JavaNodeFactory;
import com.mulesoft.raml1.java.parser.impl.common.RAMLLanguageElementImpl;
import com.mulesoft.raml1.java.parser.model.parameters.Parameter;
import com.mulesoft.raml1.java.parser.model.parameters.ParameterLocation;



public class ParameterImpl extends RAMLLanguageElementImpl implements Parameter {

    public ParameterImpl(Object jsNode, JavaNodeFactory factory){
        super(jsNode,factory);
    }

    protected ParameterImpl(){
        super();
    }


    @XmlElement(name="name")
    public String name(){
        return super.getAttribute("name", String.class);
    }


    @XmlElement(name="displayName")
    public String displayName(){
        return super.getAttribute("displayName", String.class);
    }


    @XmlElement(name="type")
    public String type(){
        return super.getAttribute("type", String.class);
    }


    @XmlElement(name="location")
    public ParameterLocation location(){
        return super.getAttribute("location", ParameterLocation.class);
    }


    @XmlElement(name="required")
    public Boolean required(){
        return super.getAttribute("required", Boolean.class);
    }


    @XmlElement(name="default")
    public String default_(){
        return super.getAttribute("default", String.class);
    }


    @XmlElement(name="example")
    public String example(){
        return super.getAttribute("example", String.class);
    }


    @XmlElement(name="repeat")
    public Boolean repeat(){
        return super.getAttribute("repeat", Boolean.class);
    }
}