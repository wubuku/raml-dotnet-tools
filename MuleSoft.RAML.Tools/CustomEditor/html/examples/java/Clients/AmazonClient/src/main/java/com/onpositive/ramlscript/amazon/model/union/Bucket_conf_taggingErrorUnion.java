package com.onpositive.ramlscript.amazon.model.union;

import com.onpositive.ramlscript.amazon.client.executor.UnionType;
import com.onpositive.ramlscript.amazon.model.Bucket_conf_tagging;
import com.onpositive.ramlscript.amazon.model.Error;
import javax.xml.bind.annotation.XmlElement;

public class Bucket_conf_taggingErrorUnion extends UnionType {

    @XmlElement(name="Bucket_conf_tagging")
    public Bucket_conf_tagging Bucket_conf_tagging;


    @XmlElement(name="Error")
    public Error Error;

}