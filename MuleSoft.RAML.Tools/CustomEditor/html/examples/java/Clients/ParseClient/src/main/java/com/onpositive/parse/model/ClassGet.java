package com.onpositive.parse.model;

import javax.xml.bind.annotation.XmlElement;

public class ClassGet {

    @XmlElement(name="objectId")
    public String objectId;


    @XmlElement(name="createdAt")
    public String createdAt;


    @XmlElement(name="updatedAt")
    public String updatedAt;


    @XmlElement(name="__$harEntry__")
    public HarEntry __$harEntry__;

}