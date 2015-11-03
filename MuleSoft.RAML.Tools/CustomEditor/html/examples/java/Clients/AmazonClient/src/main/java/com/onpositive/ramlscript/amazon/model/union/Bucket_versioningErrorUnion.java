package com.onpositive.ramlscript.amazon.model.union;

import com.onpositive.ramlscript.amazon.client.executor.UnionType;
import com.onpositive.ramlscript.amazon.model.Bucket_versioning;
import com.onpositive.ramlscript.amazon.model.Error;
import javax.xml.bind.annotation.XmlElement;

public class Bucket_versioningErrorUnion extends UnionType {

    @XmlElement(name="Bucket_versioning")
    public Bucket_versioning Bucket_versioning;


    @XmlElement(name="Error")
    public Error Error;

}