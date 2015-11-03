package com.onpositive.parse.model;

import javax.xml.bind.annotation.XmlElement;

public class UsersQueryResults {

    @XmlElement(name="username")
    public String username;


    @XmlElement(name="createdAt")
    public String createdAt;


    @XmlElement(name="objectId")
    public String objectId;


    @XmlElement(name="updatedAt")
    public String updatedAt;

}