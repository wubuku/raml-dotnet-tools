package com.mulesoft.raml1.java.parser.impl.bodies;

import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.JavaNodeFactory;
import com.mulesoft.raml1.java.parser.model.bodies.XMLBody;
import com.mulesoft.raml1.java.parser.model.systemTypes.XMLSchemaString;
import com.mulesoft.raml1.java.parser.impl.systemTypes.XMLSchemaStringImpl;



public class XMLBodyImpl extends BodyLikeImpl implements XMLBody {

    public XMLBodyImpl(Object jsNode, JavaNodeFactory factory){
        super(jsNode,factory);
    }

    protected XMLBodyImpl(){
        super();
    }


    @XmlElement(name="schema")
    public XMLSchemaString schema(){
        return super.getAttribute("schema", XMLSchemaStringImpl.class);
    }
}