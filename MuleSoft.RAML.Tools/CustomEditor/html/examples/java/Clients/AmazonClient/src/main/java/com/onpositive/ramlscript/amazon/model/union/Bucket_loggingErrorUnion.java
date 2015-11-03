package com.onpositive.ramlscript.amazon.model.union;

import com.onpositive.ramlscript.amazon.client.executor.UnionType;
import com.onpositive.ramlscript.amazon.model.Bucket_logging;
import com.onpositive.ramlscript.amazon.model.Error;
import javax.xml.bind.annotation.XmlElement;

public class Bucket_loggingErrorUnion extends UnionType {

    @XmlElement(name="Bucket_logging")
    public Bucket_logging Bucket_logging;


    @XmlElement(name="Error")
    public Error Error;

}