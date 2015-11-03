package com.onpositive.ramlscript.amazon.model.union;

import com.onpositive.ramlscript.amazon.client.executor.UnionType;
import com.onpositive.ramlscript.amazon.model.Error;
import com.onpositive.ramlscript.amazon.model.Post_uploadid__uploadid_XmlResponse;
import javax.xml.bind.annotation.XmlElement;

public class ErrorPost_uploadid__uploadid_XmlResponseUnion extends UnionType {

    @XmlElement(name="Error")
    public Error Error;


    @XmlElement(name="Post_uploadid__uploadid_XmlResponse")
    public Post_uploadid__uploadid_XmlResponse Post_uploadid__uploadid_XmlResponse;

}