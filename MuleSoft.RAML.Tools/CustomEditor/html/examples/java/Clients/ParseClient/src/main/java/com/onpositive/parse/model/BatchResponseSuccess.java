package com.onpositive.parse.model;

import javax.xml.bind.annotation.XmlElement;

public class BatchResponseSuccess {

    @XmlElement(name="createdAt")
    public String createdAt;


    @XmlElement(name="objectId")
    public String objectId;

}