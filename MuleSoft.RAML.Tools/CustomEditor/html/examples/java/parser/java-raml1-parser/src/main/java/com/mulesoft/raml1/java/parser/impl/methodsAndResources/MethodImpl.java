package com.mulesoft.raml1.java.parser.impl.methodsAndResources;

import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.JavaNodeFactory;
import com.mulesoft.raml1.java.parser.model.methodsAndResources.Method;
import com.mulesoft.raml1.java.parser.model.systemTypes.SchemaString;
import com.mulesoft.raml1.java.parser.impl.systemTypes.SchemaStringImpl;



public class MethodImpl extends MethodBaseImpl implements Method {

    public MethodImpl(Object jsNode, JavaNodeFactory factory){
        super(jsNode,factory);
    }

    protected MethodImpl(){
        super();
    }


    @XmlElement(name="signature")
    public SchemaString signature(){
        return super.getAttribute("signature", SchemaStringImpl.class);
    }


    @XmlElement(name="method")
    public String method(){
        return super.getAttribute("method", String.class);
    }
}