package com.onpositive.parse.model;

import javax.xml.bind.annotation.XmlElement;

public class FileResponse {

    @XmlElement(name="url")
    public String url;


    @XmlElement(name="name")
    public String name;


    @XmlElement(name="__$harEntry__")
    public HarEntry __$harEntry__;

}