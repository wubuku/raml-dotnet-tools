package com.onpositive.parse.model;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;

public class UsersQuery {

    @XmlElement(name="results")
    public List<UsersQueryResults> results;


    @XmlElement(name="__$harEntry__")
    public HarEntry __$harEntry__;

}