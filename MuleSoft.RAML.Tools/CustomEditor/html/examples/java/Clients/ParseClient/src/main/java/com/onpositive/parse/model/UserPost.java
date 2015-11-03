package com.onpositive.parse.model;

import javax.xml.bind.annotation.XmlElement;

public class UserPost {

    @XmlElement(name="username")
    public String username;


    @XmlElement(name="password")
    public String password;

}