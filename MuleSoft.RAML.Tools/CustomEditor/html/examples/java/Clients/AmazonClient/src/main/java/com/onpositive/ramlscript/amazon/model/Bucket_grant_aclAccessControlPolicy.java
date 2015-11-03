package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class Bucket_grant_aclAccessControlPolicy {

    @XmlElement(name="Owner")
    public Bucket_grant_aclAccessControlPolicyOwner Owner;


    @XmlElement(name="AccessControlList")
    public Bucket_grant_aclAccessControlPolicyAccessControlList AccessControlList;

}