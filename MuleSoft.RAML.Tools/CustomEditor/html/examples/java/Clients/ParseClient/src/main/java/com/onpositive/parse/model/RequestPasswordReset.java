package com.onpositive.parse.model;

import javax.xml.bind.annotation.XmlElement;

public class RequestPasswordReset {

    @XmlElement(name="email")
    public String email;

}