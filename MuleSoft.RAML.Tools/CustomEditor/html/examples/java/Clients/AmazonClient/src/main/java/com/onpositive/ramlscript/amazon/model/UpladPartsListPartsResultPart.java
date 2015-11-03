package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class UpladPartsListPartsResultPart {

    @XmlElement(name="PartNumber")
    public Double PartNumber;


    @XmlElement(name="LastModified")
    public String LastModified;


    @XmlElement(name="ETag")
    public String ETag;


    @XmlElement(name="Size")
    public Double Size;

}