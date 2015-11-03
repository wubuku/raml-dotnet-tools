package com.mulesoft.raml1.java.parser.impl.methodsAndResources;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.core.JavaNodeFactory;
import com.mulesoft.raml1.java.parser.model.methodsAndResources.Resource;
import com.mulesoft.raml1.java.parser.model.systemTypes.SchemaString;
import com.mulesoft.raml1.java.parser.impl.systemTypes.SchemaStringImpl;
import com.mulesoft.raml1.java.parser.model.systemTypes.RelativeUri;
import com.mulesoft.raml1.java.parser.impl.systemTypes.RelativeUriImpl;
import com.mulesoft.raml1.java.parser.impl.methodsAndResources.ResourceImpl;



public class ResourceImpl extends ResourceBaseImpl implements Resource {

    public ResourceImpl(Object jsNode, JavaNodeFactory factory){
        super(jsNode,factory);
    }

    protected ResourceImpl(){
        super();
    }


    @XmlElement(name="signature")
    public SchemaString signature(){
        return super.getAttribute("signature", SchemaStringImpl.class);
    }


    @XmlElement(name="relativeUri")
    public RelativeUri relativeUri(){
        return super.getAttribute("relativeUri", RelativeUriImpl.class);
    }


    @XmlElement(name="resources")
    public List<Resource> resources(){
        return super.getElements("resources", ResourceImpl.class);
    }
}