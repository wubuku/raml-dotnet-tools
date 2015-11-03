package com.mulesoft.raml1.java.parser.impl.parameters;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.JavaNodeFactory;
import com.mulesoft.raml1.java.parser.model.parameters.StrElement;



public class StrElementImpl extends ParameterImpl implements StrElement {

    public StrElementImpl(Object jsNode, JavaNodeFactory factory){
        super(jsNode,factory);
    }

    protected StrElementImpl(){
        super();
    }


    @XmlElement(name="pattern")
    public String pattern(){
        return super.getAttribute("pattern", String.class);
    }


    @XmlElement(name="enum")
    public List<String> enum_(){
        return super.getAttributes("enum", String.class);
    }


    @XmlElement(name="minLength")
    public Double minLength(){
        return super.getAttribute("minLength", Double.class);
    }


    @XmlElement(name="maxLength")
    public Double maxLength(){
        return super.getAttribute("maxLength", Double.class);
    }
}