package com.mulesoft.raml1.java.parser.model.methodsAndResources;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.model.parameters.HasNormalParameters;
import com.mulesoft.raml1.java.parser.model.bodies.Response;
import com.mulesoft.raml1.java.parser.model.bodies.BodyLike;



public interface MethodBase extends HasNormalParameters {

    @XmlElement(name="responses")
    List<Response> responses();


    @XmlElement(name="body")
    List<BodyLike> body();


    @XmlElement(name="is")
    List<TraitRef> is();


    @XmlElement(name="securedBy")
    List<SecuritySchemaRef> securedBy();

}