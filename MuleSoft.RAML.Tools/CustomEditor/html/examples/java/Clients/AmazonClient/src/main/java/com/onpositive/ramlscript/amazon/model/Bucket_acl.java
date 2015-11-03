package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class Bucket_acl {

    @XmlElement(name="AccessControlPolicy")
    public Bucket_aclAccessControlPolicy AccessControlPolicy;


    @XmlElement(name="__$harEntry__")
    public HarEntry __$harEntry__;

}