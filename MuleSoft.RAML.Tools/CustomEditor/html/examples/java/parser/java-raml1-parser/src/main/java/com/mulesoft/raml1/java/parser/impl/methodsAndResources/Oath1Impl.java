package com.mulesoft.raml1.java.parser.impl.methodsAndResources;

import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.JavaNodeFactory;
import com.mulesoft.raml1.java.parser.model.methodsAndResources.Oath1;
import com.mulesoft.raml1.java.parser.model.methodsAndResources.Oath1SecuritySchemaSettings;
import com.mulesoft.raml1.java.parser.impl.methodsAndResources.Oath1SecuritySchemaSettingsImpl;



public class Oath1Impl extends SecuritySchemaImpl implements Oath1 {

    public Oath1Impl(Object jsNode, JavaNodeFactory factory){
        super(jsNode,factory);
    }

    protected Oath1Impl(){
        super();
    }


    @XmlElement(name="settings")
    public Oath1SecuritySchemaSettings settings(){
        return super.getElement("settings", Oath1SecuritySchemaSettingsImpl.class);
    }
}