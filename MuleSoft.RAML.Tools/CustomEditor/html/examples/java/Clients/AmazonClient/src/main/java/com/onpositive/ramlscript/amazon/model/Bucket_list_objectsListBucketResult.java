package com.onpositive.ramlscript.amazon.model;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;

public class Bucket_list_objectsListBucketResult {

    @XmlElement(name="Name")
    public String Name;


    @XmlElement(name="Prefix")
    public String Prefix;


    @XmlElement(name="Marker")
    public String Marker;


    @XmlElement(name="MaxKeys")
    public Double MaxKeys;


    @XmlElement(name="IsTruncated")
    public String IsTruncated;


    @XmlElement(name="Contents")
    public List<Bucket_list_objectsListBucketResultContents> Contents;

}