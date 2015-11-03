package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class Bucket_lifecycle_putLifecycleConfigurationRule {

    @XmlElement(name="ID")
    public String ID;


    @XmlElement(name="Prefix")
    public String Prefix;


    @XmlElement(name="Status")
    public String Status;


    @XmlElement(name="Transition")
    public Bucket_lifecycle_putLifecycleConfigurationRuleTransition Transition;

}