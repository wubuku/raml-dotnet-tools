package com.onpositive.ramlscript.amazon.model.union;

import com.onpositive.ramlscript.amazon.client.executor.UnionType;
import com.onpositive.ramlscript.amazon.model.Bucket_acl;
import com.onpositive.ramlscript.amazon.model.Error;
import javax.xml.bind.annotation.XmlElement;

public class Bucket_aclErrorUnion extends UnionType {

    @XmlElement(name="Bucket_acl")
    public Bucket_acl Bucket_acl;


    @XmlElement(name="Error")
    public Error Error;

}