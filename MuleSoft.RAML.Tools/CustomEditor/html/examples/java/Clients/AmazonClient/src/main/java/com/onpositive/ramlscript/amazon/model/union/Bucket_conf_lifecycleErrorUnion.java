package com.onpositive.ramlscript.amazon.model.union;

import com.onpositive.ramlscript.amazon.client.executor.UnionType;
import com.onpositive.ramlscript.amazon.model.Bucket_conf_lifecycle;
import com.onpositive.ramlscript.amazon.model.Error;
import javax.xml.bind.annotation.XmlElement;

public class Bucket_conf_lifecycleErrorUnion extends UnionType {

    @XmlElement(name="Bucket_conf_lifecycle")
    public Bucket_conf_lifecycle Bucket_conf_lifecycle;


    @XmlElement(name="Error")
    public Error Error;

}