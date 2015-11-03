package com.onpositive.parse.model;

import javax.xml.bind.annotation.XmlElement;

public class UsersPostResponse {

    @XmlElement(name="createdAt")
    public String createdAt;


    @XmlElement(name="objectId")
    public String objectId;


    @XmlElement(name="sessionToken")
    public String sessionToken;


    @XmlElement(name="__$harEntry__")
    public HarEntry __$harEntry__;

}