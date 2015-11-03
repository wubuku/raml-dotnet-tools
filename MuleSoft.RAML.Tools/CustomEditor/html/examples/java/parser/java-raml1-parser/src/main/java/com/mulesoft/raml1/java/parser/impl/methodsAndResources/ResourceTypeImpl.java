package com.mulesoft.raml1.java.parser.impl.methodsAndResources;

import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.JavaNodeFactory;
import com.mulesoft.raml1.java.parser.model.methodsAndResources.ResourceType;



public class ResourceTypeImpl extends ResourceBaseImpl implements ResourceType {

    public ResourceTypeImpl(Object jsNode, JavaNodeFactory factory){
        super(jsNode,factory);
    }

    protected ResourceTypeImpl(){
        super();
    }


    @XmlElement(name="name")
    public String name(){
        return super.getAttribute("name", String.class);
    }


    @XmlElement(name="usage")
    public String usage(){
        return super.getAttribute("usage", String.class);
    }
}