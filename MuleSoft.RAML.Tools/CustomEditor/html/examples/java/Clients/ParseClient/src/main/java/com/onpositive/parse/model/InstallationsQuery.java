package com.onpositive.parse.model;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;

public class InstallationsQuery {

    @XmlElement(name="results")
    public List<InstallationsQueryResults> results;


    @XmlElement(name="__$harEntry__")
    public HarEntry __$harEntry__;

}