package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class Error {

    @XmlElement(name="Error")
    public ErrorError Error;


    @XmlElement(name="__$harEntry__")
    public HarEntry __$harEntry__;

}