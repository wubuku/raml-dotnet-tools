package com.mulesoft.raml1.java.parser.impl.parameters;

import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.JavaNodeFactory;
import com.mulesoft.raml1.java.parser.model.parameters.FileElement;



public class FileElementImpl extends ParameterImpl implements FileElement {

    public FileElementImpl(Object jsNode, JavaNodeFactory factory){
        super(jsNode,factory);
    }

    protected FileElementImpl(){
        super();
    }



}