package com.onpositive.ramlscript.amazon.model.union;

import com.onpositive.ramlscript.amazon.client.executor.UnionType;
import com.onpositive.ramlscript.amazon.model.Error;
import com.onpositive.ramlscript.amazon.model.Location_constraint;
import javax.xml.bind.annotation.XmlElement;

public class Location_constraintErrorUnion extends UnionType {

    @XmlElement(name="Location_constraint")
    public Location_constraint Location_constraint;


    @XmlElement(name="Error")
    public Error Error;

}