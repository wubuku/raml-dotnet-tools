package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class Bucket_loggingBucketLoggingStatusLoggingEnabled {

    @XmlElement(name="TargetBucket")
    public String TargetBucket;


    @XmlElement(name="TargetPrefix")
    public String TargetPrefix;


    @XmlElement(name="TargetGrants")
    public Bucket_loggingBucketLoggingStatusLoggingEnabledTargetGrants TargetGrants;

}