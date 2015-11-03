package com.mulesoft.raml1.java.parser.impl.bodies;

import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.JavaNodeFactory;
import com.mulesoft.raml1.java.parser.model.bodies.JSONBody;
import com.mulesoft.raml1.java.parser.model.systemTypes.JSonSchemaString;
import com.mulesoft.raml1.java.parser.impl.systemTypes.JSonSchemaStringImpl;



public class JSONBodyImpl extends BodyLikeImpl implements JSONBody {

    public JSONBodyImpl(Object jsNode, JavaNodeFactory factory){
        super(jsNode,factory);
    }

    protected JSONBodyImpl(){
        super();
    }


    @XmlElement(name="schema")
    public JSonSchemaString schema(){
        return super.getAttribute("schema", JSonSchemaStringImpl.class);
    }
}