package com.onpositive.ramlscript.amazon.model.union;

import com.onpositive.ramlscript.amazon.client.executor.UnionType;
import com.onpositive.ramlscript.amazon.model.Bucket_object_versions;
import com.onpositive.ramlscript.amazon.model.Error;
import javax.xml.bind.annotation.XmlElement;

public class Bucket_object_versionsErrorUnion extends UnionType {

    @XmlElement(name="Bucket_object_versions")
    public Bucket_object_versions Bucket_object_versions;


    @XmlElement(name="Error")
    public Error Error;

}