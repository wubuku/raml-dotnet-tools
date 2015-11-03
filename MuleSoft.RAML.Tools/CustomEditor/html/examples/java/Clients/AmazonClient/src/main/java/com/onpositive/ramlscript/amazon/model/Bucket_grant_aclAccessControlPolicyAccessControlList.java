package com.onpositive.ramlscript.amazon.model;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;

public class Bucket_grant_aclAccessControlPolicyAccessControlList {

    @XmlElement(name="Grant")
    public List<Bucket_grant_aclAccessControlPolicyAccessControlListGrant> Grant;

}