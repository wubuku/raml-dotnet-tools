package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class Bucket_conf_lifecycleLifecycleConfigurationRule {

    @XmlElement(name="ID")
    public String ID;


    @XmlElement(name="Prefix")
    public String Prefix;


    @XmlElement(name="Status")
    public String Status;


    @XmlElement(name="Transition")
    public Bucket_conf_lifecycleLifecycleConfigurationRuleTransition Transition;


    @XmlElement(name="Expiration")
    public Bucket_conf_lifecycleLifecycleConfigurationRuleExpiration Expiration;

}