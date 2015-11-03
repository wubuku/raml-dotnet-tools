package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class Bucket_delete_multiple_responseDeleteResultDeleted {

    @XmlElement(name="Key")
    public String Key;


    @XmlElement(name="VersionId")
    public String VersionId;


    @XmlElement(name="DeleteMarker")
    public Boolean DeleteMarker;


    @XmlElement(name="DeleteMarkerVersionId")
    public String DeleteMarkerVersionId;

}