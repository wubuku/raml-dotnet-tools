package com.onpositive.parse.model;

import javax.xml.bind.annotation.XmlElement;

public class InstallationsQueryResults {

    @XmlElement(name="deviceType")
    public String deviceType;


    @XmlElement(name="createdAt")
    public String createdAt;


    @XmlElement(name="objectId")
    public String objectId;


    @XmlElement(name="updatedAt")
    public String updatedAt;


    @XmlElement(name="deviceToken")
    public String deviceToken;

}