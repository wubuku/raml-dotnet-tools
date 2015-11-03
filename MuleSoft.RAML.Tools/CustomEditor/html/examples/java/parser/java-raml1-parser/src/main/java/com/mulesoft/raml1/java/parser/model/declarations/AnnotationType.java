package com.mulesoft.raml1.java.parser.model.declarations;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.model.common.RAMLLanguageElement;
import com.mulesoft.raml1.java.parser.model.datamodel.DataElement;



public interface AnnotationType extends RAMLLanguageElement {

    @XmlElement(name="name")
    String name();


    @XmlElement(name="usage")
    String usage();


    @XmlElement(name="parameters")
    List<DataElement> parameters();


    @XmlElement(name="allowMultiple")
    Boolean allowMultiple();


    @XmlElement(name="allowedTargets")
    List<AnnotationTarget> allowedTargets();

}