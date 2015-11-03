package com.onpositive.parse.model;

import javax.xml.bind.annotation.XmlElement;

public class UserGet {

    @XmlElement(name="username")
    public String username;


    @XmlElement(name="email")
    public String email;


    @XmlElement(name="phone")
    public String phone;


    @XmlElement(name="objectId")
    public String objectId;


    @XmlElement(name="createdAt")
    public String createdAt;


    @XmlElement(name="updatedAt")
    public String updatedAt;


    @XmlElement(name="__$harEntry__")
    public HarEntry __$harEntry__;

}