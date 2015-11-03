package com.onpositive.ramlscript.amazon.model.union;

import com.onpositive.ramlscript.amazon.client.executor.UnionType;
import com.onpositive.ramlscript.amazon.model.Error;
import com.onpositive.ramlscript.amazon.model.RequestPayement_configuration;
import javax.xml.bind.annotation.XmlElement;

public class RequestPayement_configurationErrorUnion extends UnionType {

    @XmlElement(name="RequestPayement_configuration")
    public RequestPayement_configuration RequestPayement_configuration;


    @XmlElement(name="Error")
    public Error Error;

}