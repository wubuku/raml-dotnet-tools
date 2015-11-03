package com.onpositive.ramlscript.amazon.model.union;

import com.onpositive.ramlscript.amazon.client.executor.UnionType;
import com.onpositive.ramlscript.amazon.model.Error;
import com.onpositive.ramlscript.amazon.model.Post_uploadsXmlResponse;
import javax.xml.bind.annotation.XmlElement;

public class ErrorPost_uploadsXmlResponseUnion extends UnionType {

    @XmlElement(name="Error")
    public Error Error;


    @XmlElement(name="Post_uploadsXmlResponse")
    public Post_uploadsXmlResponse Post_uploadsXmlResponse;

}