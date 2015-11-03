package com.onpositive.ramlscript.amazon.model.union;

import com.onpositive.ramlscript.amazon.client.executor.UnionType;
import com.onpositive.ramlscript.amazon.model.Bucket_conf_policy_schema;
import com.onpositive.ramlscript.amazon.model.Error;
import javax.xml.bind.annotation.XmlElement;

public class Bucket_conf_policy_schemaErrorUnion extends UnionType {

    @XmlElement(name="Bucket_conf_policy_schema")
    public Bucket_conf_policy_schema Bucket_conf_policy_schema;


    @XmlElement(name="Error")
    public Error Error;

}