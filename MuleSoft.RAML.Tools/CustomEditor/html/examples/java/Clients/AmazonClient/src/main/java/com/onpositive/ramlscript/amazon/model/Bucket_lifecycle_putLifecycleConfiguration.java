package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class Bucket_lifecycle_putLifecycleConfiguration {

    @XmlElement(name="Rule")
    public Bucket_lifecycle_putLifecycleConfigurationRule Rule;

}