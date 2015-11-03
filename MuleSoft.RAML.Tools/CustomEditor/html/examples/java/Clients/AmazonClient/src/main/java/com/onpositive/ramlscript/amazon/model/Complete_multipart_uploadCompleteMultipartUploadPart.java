package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class Complete_multipart_uploadCompleteMultipartUploadPart {

    @XmlElement(name="PartNumber")
    public Double PartNumber;


    @XmlElement(name="ETag")
    public String ETag;

}