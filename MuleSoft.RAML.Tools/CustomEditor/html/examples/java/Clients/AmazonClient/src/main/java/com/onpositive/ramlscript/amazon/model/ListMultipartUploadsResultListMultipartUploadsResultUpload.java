package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class ListMultipartUploadsResultListMultipartUploadsResultUpload {

    @XmlElement(name="Key")
    public String Key;


    @XmlElement(name="UploadId")
    public String UploadId;


    @XmlElement(name="Initiator")
    public ListMultipartUploadsResultListMultipartUploadsResultUploadInitiator Initiator;


    @XmlElement(name="Owner")
    public ListMultipartUploadsResultListMultipartUploadsResultUploadOwner Owner;


    @XmlElement(name="StorageClass")
    public String StorageClass;


    @XmlElement(name="Initiated")
    public String Initiated;

}