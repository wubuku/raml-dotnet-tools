package com.mulesoft.raml1.java.parser.impl.methodsAndResources;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.JavaNodeFactory;
import com.mulesoft.raml1.java.parser.impl.common.RAMLLanguageElementImpl;
import com.mulesoft.raml1.java.parser.model.methodsAndResources.Resource;
import com.mulesoft.raml1.java.parser.model.systemTypes.RelativeUri;
import com.mulesoft.raml1.java.parser.impl.systemTypes.RelativeUriImpl;
import com.mulesoft.raml1.java.parser.model.methodsAndResources.ResourceTypeRef;
import com.mulesoft.raml1.java.parser.impl.methodsAndResources.ResourceTypeRefImpl;
import com.mulesoft.raml1.java.parser.model.methodsAndResources.TraitRef;
import com.mulesoft.raml1.java.parser.impl.methodsAndResources.TraitRefImpl;
import com.mulesoft.raml1.java.parser.model.methodsAndResources.SecuritySchemaRef;
import com.mulesoft.raml1.java.parser.impl.methodsAndResources.SecuritySchemaRefImpl;
import com.mulesoft.raml1.java.parser.model.parameters.Parameter;
import com.mulesoft.raml1.java.parser.impl.parameters.ParameterImpl;
import com.mulesoft.raml1.java.parser.model.methodsAndResources.Method;
import com.mulesoft.raml1.java.parser.impl.methodsAndResources.MethodImpl;
import com.mulesoft.raml1.java.parser.impl.methodsAndResources.ResourceImpl;



public class ResourceImpl extends RAMLLanguageElementImpl implements Resource {

    public ResourceImpl(Object jsNode, JavaNodeFactory factory){
        super(jsNode,factory);
    }

    protected ResourceImpl(){
        super();
    }


    @XmlElement(name="relativeUri")
    public RelativeUri relativeUri(){
        return super.getAttribute("relativeUri", RelativeUriImpl.class);
    }


    @XmlElement(name="type")
    public ResourceTypeRef type(){
        return super.getAttribute("type", ResourceTypeRefImpl.class);
    }


    @XmlElement(name="is")
    public List<TraitRef> is(){
        return super.getAttributes("is", TraitRefImpl.class);
    }


    @XmlElement(name="securedBy")
    public List<SecuritySchemaRef> securedBy(){
        return super.getAttributes("securedBy", SecuritySchemaRefImpl.class);
    }


    @XmlElement(name="uriParameters")
    public List<Parameter> uriParameters(){
        return super.getElements("uriParameters", ParameterImpl.class);
    }


    @XmlElement(name="methods")
    public List<Method> methods(){
        return super.getElements("methods", MethodImpl.class);
    }


    @XmlElement(name="resources")
    public List<Resource> resources(){
        return super.getElements("resources", ResourceImpl.class);
    }


    @XmlElement(name="displayName")
    public String displayName(){
        return super.getAttribute("displayName", String.class);
    }


    @XmlElement(name="baseUriParameters")
    public List<Parameter> baseUriParameters(){
        return super.getElements("baseUriParameters", ParameterImpl.class);
    }
}