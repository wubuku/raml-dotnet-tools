package com.mulesoft.raml1.java.parser.impl.bodies;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.JavaNodeFactory;
import com.mulesoft.raml1.java.parser.impl.common.RAMLLanguageElementImpl;
import com.mulesoft.raml1.java.parser.model.bodies.BodyLike;
import com.mulesoft.raml1.java.parser.model.systemTypes.SchemaString;
import com.mulesoft.raml1.java.parser.impl.systemTypes.SchemaStringImpl;
import com.mulesoft.raml1.java.parser.model.systemTypes.ExampleString;
import com.mulesoft.raml1.java.parser.impl.systemTypes.ExampleStringImpl;
import com.mulesoft.raml1.java.parser.model.parameters.Parameter;
import com.mulesoft.raml1.java.parser.impl.parameters.ParameterImpl;



public class BodyLikeImpl extends RAMLLanguageElementImpl implements BodyLike {

    public BodyLikeImpl(Object jsNode, JavaNodeFactory factory){
        super(jsNode,factory);
    }

    protected BodyLikeImpl(){
        super();
    }


    @XmlElement(name="name")
    public String name(){
        return super.getAttribute("name", String.class);
    }


    @XmlElement(name="schema")
    public SchemaString schema(){
        return super.getAttribute("schema", SchemaStringImpl.class);
    }


    @XmlElement(name="example")
    public ExampleString example(){
        return super.getAttribute("example", ExampleStringImpl.class);
    }


    @XmlElement(name="formParameters")
    public List<Parameter> formParameters(){
        return super.getElements("formParameters", ParameterImpl.class);
    }
}