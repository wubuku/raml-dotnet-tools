package com.onpositive.parse.model;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;

public class ClassesQuery {

    @XmlElement(name="results")
    public List<ClassesQueryResults> results;


    @XmlElement(name="__$harEntry__")
    public HarEntry __$harEntry__;

}