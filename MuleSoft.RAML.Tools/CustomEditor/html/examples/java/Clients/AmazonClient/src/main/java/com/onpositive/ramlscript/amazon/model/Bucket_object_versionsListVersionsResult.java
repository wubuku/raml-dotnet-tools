package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class Bucket_object_versionsListVersionsResult {

    @XmlElement(name="Name")
    public String Name;


    @XmlElement(name="Prefix")
    public String Prefix;


    @XmlElement(name="KeyMarker")
    public String KeyMarker;


    @XmlElement(name="VersionIdMarker")
    public String VersionIdMarker;


    @XmlElement(name="MaxKeys")
    public Double MaxKeys;


    @XmlElement(name="IsTruncated")
    public String IsTruncated;


    @XmlElement(name="Version")
    public Bucket_object_versionsListVersionsResultVersion Version;


    @XmlElement(name="DeleteMarker")
    public Bucket_object_versionsListVersionsResultDeleteMarker DeleteMarker;

}