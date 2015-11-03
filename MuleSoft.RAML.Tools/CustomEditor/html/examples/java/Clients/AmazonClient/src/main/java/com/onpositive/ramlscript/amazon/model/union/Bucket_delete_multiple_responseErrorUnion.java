package com.onpositive.ramlscript.amazon.model.union;

import com.onpositive.ramlscript.amazon.client.executor.UnionType;
import com.onpositive.ramlscript.amazon.model.Bucket_delete_multiple_response;
import com.onpositive.ramlscript.amazon.model.Error;
import javax.xml.bind.annotation.XmlElement;

public class Bucket_delete_multiple_responseErrorUnion extends UnionType {

    @XmlElement(name="Bucket_delete_multiple_response")
    public Bucket_delete_multiple_response Bucket_delete_multiple_response;


    @XmlElement(name="Error")
    public Error Error;

}