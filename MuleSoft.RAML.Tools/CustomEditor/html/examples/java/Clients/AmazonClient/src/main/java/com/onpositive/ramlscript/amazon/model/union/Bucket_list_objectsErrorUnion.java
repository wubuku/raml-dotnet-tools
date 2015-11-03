package com.onpositive.ramlscript.amazon.model.union;

import com.onpositive.ramlscript.amazon.client.executor.UnionType;
import com.onpositive.ramlscript.amazon.model.Bucket_list_objects;
import com.onpositive.ramlscript.amazon.model.Error;
import javax.xml.bind.annotation.XmlElement;

public class Bucket_list_objectsErrorUnion extends UnionType {

    @XmlElement(name="Bucket_list_objects")
    public Bucket_list_objects Bucket_list_objects;


    @XmlElement(name="Error")
    public Error Error;

}