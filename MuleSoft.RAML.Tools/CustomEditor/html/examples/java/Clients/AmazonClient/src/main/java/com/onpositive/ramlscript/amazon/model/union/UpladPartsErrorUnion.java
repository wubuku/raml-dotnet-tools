package com.onpositive.ramlscript.amazon.model.union;

import com.onpositive.ramlscript.amazon.client.executor.UnionType;
import com.onpositive.ramlscript.amazon.model.Error;
import com.onpositive.ramlscript.amazon.model.UpladParts;
import javax.xml.bind.annotation.XmlElement;

public class UpladPartsErrorUnion extends UnionType {

    @XmlElement(name="UpladParts")
    public UpladParts UpladParts;


    @XmlElement(name="Error")
    public Error Error;

}