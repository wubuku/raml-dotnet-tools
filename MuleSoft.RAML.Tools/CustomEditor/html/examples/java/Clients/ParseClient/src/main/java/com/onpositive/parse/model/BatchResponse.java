package com.onpositive.parse.model;

import javax.xml.bind.annotation.XmlElement;

public class BatchResponse {

    @XmlElement(name="success")
    public BatchResponseSuccess success;


    @XmlElement(name="__$harEntry__")
    public HarEntry __$harEntry__;

}