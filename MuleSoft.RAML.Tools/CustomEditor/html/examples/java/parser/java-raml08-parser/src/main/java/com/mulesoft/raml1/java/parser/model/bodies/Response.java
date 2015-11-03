package com.mulesoft.raml1.java.parser.model.bodies;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.model.common.RAMLLanguageElement;
import com.mulesoft.raml1.java.parser.model.systemTypes.StatusCode;
import com.mulesoft.raml1.java.parser.model.parameters.Parameter;



public interface Response extends RAMLLanguageElement {

    @XmlElement(name="code")
    StatusCode code();


    @XmlElement(name="headers")
    List<Parameter> headers();


    @XmlElement(name="body")
    List<BodyLike> body();

}