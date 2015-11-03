package com.mulesoft.raml1.java.parser.impl.methodsAndResources;

import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.JavaNodeFactory;
import com.mulesoft.raml1.java.parser.model.methodsAndResources.Trait;



public class TraitImpl extends MethodBaseImpl implements Trait {

    public TraitImpl(Object jsNode, JavaNodeFactory factory){
        super(jsNode,factory);
    }

    protected TraitImpl(){
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