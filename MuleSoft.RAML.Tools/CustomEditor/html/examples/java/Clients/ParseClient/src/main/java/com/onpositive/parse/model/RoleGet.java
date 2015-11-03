package com.onpositive.parse.model;

import javax.xml.bind.annotation.XmlElement;

public class RoleGet {

    @XmlElement(name="name")
    public String name;


    @XmlElement(name="ACL")
    public RoleGetACL ACL;


    @XmlElement(name="objectId")
    public String objectId;


    @XmlElement(name="createdAt")
    public String createdAt;


    @XmlElement(name="updatedAt")
    public String updatedAt;


    @XmlElement(name="users")
    public RoleGetUsers users;


    @XmlElement(name="roles")
    public RoleGetRoles roles;


    @XmlElement(name="__$harEntry__")
    public HarEntry __$harEntry__;

}