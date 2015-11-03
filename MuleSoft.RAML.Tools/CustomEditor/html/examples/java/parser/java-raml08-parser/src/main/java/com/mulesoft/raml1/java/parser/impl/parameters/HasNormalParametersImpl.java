package com.mulesoft.raml1.java.parser.impl.parameters;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.JavaNodeFactory;
import com.mulesoft.raml1.java.parser.impl.common.RAMLLanguageElementImpl;
import com.mulesoft.raml1.java.parser.model.parameters.HasNormalParameters;
import com.mulesoft.raml1.java.parser.model.parameters.Parameter;
import com.mulesoft.raml1.java.parser.impl.parameters.ParameterImpl;



public class HasNormalParametersImpl extends RAMLLanguageElementImpl implements HasNormalParameters {

    public HasNormalParametersImpl(Object jsNode, JavaNodeFactory factory){
        super(jsNode,factory);
    }

    protected HasNormalParametersImpl(){
        super();
    }


    @XmlElement(name="queryParameters")
    public List<Parameter> queryParameters(){
        return super.getElements("queryParameters", ParameterImpl.class);
    }


    @XmlElement(name="displayName")
    public String displayName(){
        return super.getAttribute("displayName", String.class);
    }


    @XmlElement(name="headers")
    public List<Parameter> headers(){
        return super.getElements("headers", ParameterImpl.class);
    }
}