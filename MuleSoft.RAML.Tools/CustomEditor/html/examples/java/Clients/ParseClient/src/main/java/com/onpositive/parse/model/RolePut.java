package com.onpositive.parse.model;

import javax.xml.bind.annotation.XmlElement;

public class RolePut {

    @XmlElement(name="roles")
    public RolePutRoles roles;


    @XmlElement(name="users")
    public RolePutUsers users;

}