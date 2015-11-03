package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class UpladParts {

    @XmlElement(name="ListPartsResult")
    public UpladPartsListPartsResult ListPartsResult;


    @XmlElement(name="__$harEntry__")
    public HarEntry __$harEntry__;

}