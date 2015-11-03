package com.onpositive.ramlscript.amazon.model;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;

public class UpladPartsListPartsResult {

    @XmlElement(name="Bucket")
    public String Bucket;


    @XmlElement(name="Key")
    public String Key;


    @XmlElement(name="UploadId")
    public String UploadId;


    @XmlElement(name="Initiator")
    public UpladPartsListPartsResultInitiator Initiator;


    @XmlElement(name="Owner")
    public UpladPartsListPartsResultOwner Owner;


    @XmlElement(name="StorageClass")
    public String StorageClass;


    @XmlElement(name="PartNumberMarker")
    public Double PartNumberMarker;


    @XmlElement(name="NextPartNumberMarker")
    public Double NextPartNumberMarker;


    @XmlElement(name="MaxParts")
    public Double MaxParts;


    @XmlElement(name="IsTruncated")
    public Boolean IsTruncated;


    @XmlElement(name="Part")
    public List<UpladPartsListPartsResultPart> Part;

}