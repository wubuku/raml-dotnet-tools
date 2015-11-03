package com.mulesoft.raml1.java.parser.model.datamodel;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;
import com.mulesoft.raml1.java.parser.model.common.RAMLLanguageElement;



public interface DataElement extends RAMLLanguageElement {

    @XmlElement(name="name")
    String name();


    @XmlElement(name="facets")
    List<DataElement> facets();


    @XmlElement(name="schema")
    String schema();


    @XmlElement(name="usage")
    String usage();


    @XmlElement(name="type")
    List<String> type();


    @XmlElement(name="location")
    ModelLocation location();


    @XmlElement(name="locationKind")
    LocationKind locationKind();


    @XmlElement(name="default")
    String default_();


    @XmlElement(name="example")
    String example();


    @XmlElement(name="repeat")
    Boolean repeat();


    @XmlElement(name="examples")
    List<ExampleSpec> examples();


    @XmlElement(name="required")
    Boolean required();

}