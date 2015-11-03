package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class Bucket_list_objectsListBucketResultContents {

    @XmlElement(name="Key")
    public String Key;


    @XmlElement(name="LastModified")
    public String LastModified;


    @XmlElement(name="ETag")
    public String ETag;


    @XmlElement(name="Size")
    public Double Size;


    @XmlElement(name="StorageClass")
    public String StorageClass;


    @XmlElement(name="Owner")
    public Bucket_list_objectsListBucketResultContentsOwner Owner;

}