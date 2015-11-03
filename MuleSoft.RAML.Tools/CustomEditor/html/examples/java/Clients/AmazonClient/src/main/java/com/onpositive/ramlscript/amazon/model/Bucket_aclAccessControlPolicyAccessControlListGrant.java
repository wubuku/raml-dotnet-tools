package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class Bucket_aclAccessControlPolicyAccessControlListGrant {

    @XmlElement(name="Grantee")
    public Bucket_aclAccessControlPolicyAccessControlListGrantGrantee Grantee;


    @XmlElement(name="Permission")
    public String Permission;

}