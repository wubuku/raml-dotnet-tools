package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class Bucket_aclAccessControlPolicy {

    @XmlElement(name="Owner")
    public Bucket_aclAccessControlPolicyOwner Owner;


    @XmlElement(name="AccessControlList")
    public Bucket_aclAccessControlPolicyAccessControlList AccessControlList;

}