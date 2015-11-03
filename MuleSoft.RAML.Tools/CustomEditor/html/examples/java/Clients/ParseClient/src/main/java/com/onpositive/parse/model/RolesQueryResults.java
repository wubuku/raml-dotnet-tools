package com.onpositive.parse.model;

import javax.xml.bind.annotation.XmlElement;

public class RolesQueryResults {

    @XmlElement(name="name")
    public String name;


    @XmlElement(name="ACL")
    public RolesQueryResultsACL ACL;


    @XmlElement(name="objectId")
    public String objectId;


    @XmlElement(name="createdAt")
    public String createdAt;


    @XmlElement(name="updatedAt")
    public String updatedAt;


    @XmlElement(name="users")
    public RolesQueryResultsUsers users;


    @XmlElement(name="roles")
    public RolesQueryResultsRoles roles;

}