package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class Bucket {

    @XmlElement(name="CreateBucketConfiguration")
    public BucketCreateBucketConfiguration CreateBucketConfiguration;

}