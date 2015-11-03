package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class Bucket_delete_multipleDeleteObject {

    @XmlElement(name="Key")
    public String Key;


    @XmlElement(name="VersionId")
    public Double VersionId;

}