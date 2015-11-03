package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class Bucket_delete_multiple_responseDeleteResult {

    @XmlElement(name="Deleted")
    public Bucket_delete_multiple_responseDeleteResultDeleted Deleted;


    @XmlElement(name="Error")
    public Bucket_delete_multiple_responseDeleteResultError Error;

}