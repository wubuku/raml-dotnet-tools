package com.onpositive.parse.model;

import javax.xml.bind.annotation.XmlElement;

public class RolePost {

    @XmlElement(name="ACL")
    public RolePostACL ACL;


    @XmlElement(name="name")
    public String name;


    @XmlElement(name="roles")
    public RolePostRoles roles;


    @XmlElement(name="users")
    public RolePostUsers users;

}