package com.onpositive.ramlscript.amazon.model.union;

import com.onpositive.ramlscript.amazon.client.executor.UnionType;
import com.onpositive.ramlscript.amazon.model.Bucket_conf_cors;
import com.onpositive.ramlscript.amazon.model.Error;
import javax.xml.bind.annotation.XmlElement;

public class Bucket_conf_corsErrorUnion extends UnionType {

    @XmlElement(name="Bucket_conf_cors")
    public Bucket_conf_cors Bucket_conf_cors;


    @XmlElement(name="Error")
    public Error Error;

}