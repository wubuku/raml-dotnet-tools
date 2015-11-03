package com.onpositive.ramlscript.amazon.model.union;

import com.onpositive.ramlscript.amazon.client.executor.UnionType;
import com.onpositive.ramlscript.amazon.model.Error;
import com.onpositive.ramlscript.amazon.model.ListMultipartUploadsResult;
import javax.xml.bind.annotation.XmlElement;

public class ListMultipartUploadsResultErrorUnion extends UnionType {

    @XmlElement(name="ListMultipartUploadsResult")
    public ListMultipartUploadsResult ListMultipartUploadsResult;


    @XmlElement(name="Error")
    public Error Error;

}