package com.onpositive.ramlscript.amazon.model;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;

public class Bucket_policy_put_schema {

    @XmlElement(name="Id")
    public String Id;


    @XmlElement(name="Statement")
    public List<Bucket_policy_put_schemaStatement> Statement;


    @XmlElement(name="Version")
    public String Version;

}