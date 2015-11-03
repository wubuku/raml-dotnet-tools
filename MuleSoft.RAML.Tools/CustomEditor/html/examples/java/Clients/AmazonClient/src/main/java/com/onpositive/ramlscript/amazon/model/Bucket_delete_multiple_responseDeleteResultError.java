package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class Bucket_delete_multiple_responseDeleteResultError {

    @XmlElement(name="Key")
    public String Key;


    @XmlElement(name="VersionId")
    public String VersionId;


    @XmlElement(name="Code")
    public String Code;


    @XmlElement(name="Message")
    public String Message;

}