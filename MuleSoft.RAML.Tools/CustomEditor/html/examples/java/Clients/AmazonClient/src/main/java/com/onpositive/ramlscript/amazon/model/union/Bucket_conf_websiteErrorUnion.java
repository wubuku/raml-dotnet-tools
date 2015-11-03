package com.onpositive.ramlscript.amazon.model.union;

import com.onpositive.ramlscript.amazon.client.executor.UnionType;
import com.onpositive.ramlscript.amazon.model.Bucket_conf_website;
import com.onpositive.ramlscript.amazon.model.Error;
import javax.xml.bind.annotation.XmlElement;

public class Bucket_conf_websiteErrorUnion extends UnionType {

    @XmlElement(name="Bucket_conf_website")
    public Bucket_conf_website Bucket_conf_website;


    @XmlElement(name="Error")
    public Error Error;

}