package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class Bucket_conf_lifecycleLifecycleConfigurationRuleTransition {

    @XmlElement(name="Days")
    public Double Days;


    @XmlElement(name="StorageClass")
    public String StorageClass;

}