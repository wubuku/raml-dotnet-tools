package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class Bucket_loggingBucketLoggingStatusLoggingEnabledTargetGrantsGrant {

    @XmlElement(name="Grantee")
    public Bucket_loggingBucketLoggingStatusLoggingEnabledTargetGrantsGrantGrantee Grantee;


    @XmlElement(name="Permission")
    public String Permission;

}