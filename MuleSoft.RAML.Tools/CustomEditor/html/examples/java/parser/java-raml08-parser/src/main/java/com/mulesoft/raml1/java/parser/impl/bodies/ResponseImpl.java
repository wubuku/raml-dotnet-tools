package com.mulesoft.raml1.java.parser.impl.bodies;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.JavaNodeFactory;
import com.mulesoft.raml1.java.parser.impl.common.RAMLLanguageElementImpl;
import com.mulesoft.raml1.java.parser.model.bodies.Response;
import com.mulesoft.raml1.java.parser.model.systemTypes.StatusCode;
import com.mulesoft.raml1.java.parser.impl.systemTypes.StatusCodeImpl;
import com.mulesoft.raml1.java.parser.model.parameters.Parameter;
import com.mulesoft.raml1.java.parser.impl.parameters.ParameterImpl;
import com.mulesoft.raml1.java.parser.model.bodies.BodyLike;
import com.mulesoft.raml1.java.parser.impl.bodies.BodyLikeImpl;



public class ResponseImpl extends RAMLLanguageElementImpl implements Response {

    public ResponseImpl(Object jsNode, JavaNodeFactory factory){
        super(jsNode,factory);
    }

    protected ResponseImpl(){
        super();
    }


    @XmlElement(name="code")
    public StatusCode code(){
        return super.getAttribute("code", StatusCodeImpl.class);
    }


    @XmlElement(name="headers")
    public List<Parameter> headers(){
        return super.getElements("headers", ParameterImpl.class);
    }


    @XmlElement(name="body")
    public List<BodyLike> body(){
        return super.getElements("body", BodyLikeImpl.class);
    }
}