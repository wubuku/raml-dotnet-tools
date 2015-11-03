package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class Bucket_conf_corsCORSConfigurationCORSRule {

    @XmlElement(name="ID")
    public String ID;


    @XmlElement(name="AllowedOrigin")
    public String AllowedOrigin;


    @XmlElement(name="AllowedMethod")
    public String AllowedMethod;


    @XmlElement(name="MaxAgeSeconds")
    public Double MaxAgeSeconds;


    @XmlElement(name="ExposeHeader")
    public String ExposeHeader;

}