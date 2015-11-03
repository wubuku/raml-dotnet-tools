package com.onpositive.parse.model;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;

public class RolePutRoles {

    @XmlElement(name="__op")
    public String __op;


    @XmlElement(name="objects")
    public List<RolePutRolesObjects> objects;

}