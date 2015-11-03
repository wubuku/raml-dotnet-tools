package com.mulesoft.raml1.java.parser.impl.methodsAndResources;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.JavaNodeFactory;
import com.mulesoft.raml1.java.parser.model.methodsAndResources.Method;
import com.mulesoft.raml1.java.parser.model.methodsAndResources.SecuritySchemaRef;
import com.mulesoft.raml1.java.parser.impl.methodsAndResources.SecuritySchemaRefImpl;



public class MethodImpl extends MethodBaseImpl implements Method {

    public MethodImpl(Object jsNode, JavaNodeFactory factory){
        super(jsNode,factory);
    }

    protected MethodImpl(){
        super();
    }


    @XmlElement(name="method")
    public String method(){
        return super.getAttribute("method", String.class);
    }


    @XmlElement(name="protocols")
    public List<String> protocols(){
        return super.getAttributes("protocols", String.class);
    }


    @XmlElement(name="securedBy")
    public List<SecuritySchemaRef> securedBy(){
        return super.getAttributes("securedBy", SecuritySchemaRefImpl.class);
    }
}