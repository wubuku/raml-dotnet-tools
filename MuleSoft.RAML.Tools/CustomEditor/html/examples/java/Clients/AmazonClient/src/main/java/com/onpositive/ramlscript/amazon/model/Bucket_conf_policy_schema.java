package com.onpositive.ramlscript.amazon.model;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;

public class Bucket_conf_policy_schema {

    @XmlElement(name="Id")
    public String Id;


    @XmlElement(name="Statement")
    public List<Bucket_conf_policy_schemaStatement> Statement;


    @XmlElement(name="Version")
    public String Version;


    @XmlElement(name="__$harEntry__")
    public HarEntry __$harEntry__;

}