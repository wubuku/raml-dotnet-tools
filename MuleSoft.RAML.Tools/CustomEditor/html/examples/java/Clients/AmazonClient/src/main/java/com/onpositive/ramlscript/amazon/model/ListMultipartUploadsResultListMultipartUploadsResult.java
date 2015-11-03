package com.onpositive.ramlscript.amazon.model;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;

public class ListMultipartUploadsResultListMultipartUploadsResult {

    @XmlElement(name="Bucket")
    public String Bucket;


    @XmlElement(name="KeyMarker")
    public String KeyMarker;


    @XmlElement(name="UploadIdMarker")
    public String UploadIdMarker;


    @XmlElement(name="NextKeyMarker")
    public String NextKeyMarker;


    @XmlElement(name="NextUploadIdMarker")
    public String NextUploadIdMarker;


    @XmlElement(name="MaxUploads")
    public Double MaxUploads;


    @XmlElement(name="IsTruncated")
    public String IsTruncated;


    @XmlElement(name="Upload")
    public List<ListMultipartUploadsResultListMultipartUploadsResultUpload> Upload;

}