package com.mulesoft.raml1.java.parser.impl.methodsAndResources;

import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.JavaNodeFactory;
import com.mulesoft.raml1.java.parser.impl.common.RAMLSimpleElementImpl;
import com.mulesoft.raml1.java.parser.model.methodsAndResources.SecuritySchemaPart;



public class SecuritySchemaPartImpl extends RAMLSimpleElementImpl implements SecuritySchemaPart {

    public SecuritySchemaPartImpl(Object jsNode, JavaNodeFactory factory){
        super(jsNode,factory);
    }

    protected SecuritySchemaPartImpl(){
        super();
    }



}