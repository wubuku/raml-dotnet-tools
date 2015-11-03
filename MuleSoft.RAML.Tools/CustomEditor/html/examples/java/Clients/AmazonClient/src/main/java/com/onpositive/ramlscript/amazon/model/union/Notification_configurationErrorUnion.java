package com.onpositive.ramlscript.amazon.model.union;

import com.onpositive.ramlscript.amazon.client.executor.UnionType;
import com.onpositive.ramlscript.amazon.model.Error;
import com.onpositive.ramlscript.amazon.model.Notification_configuration;
import javax.xml.bind.annotation.XmlElement;

public class Notification_configurationErrorUnion extends UnionType {

    @XmlElement(name="Notification_configuration")
    public Notification_configuration Notification_configuration;


    @XmlElement(name="Error")
    public Error Error;

}