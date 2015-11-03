package com.onpositive.parse.model;

import javax.xml.bind.annotation.XmlElement;

public class RoleGetACL_ {

    @XmlElement(name="read")
    public Boolean read;


    @XmlElement(name="write")
    public Boolean write;

}