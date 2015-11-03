package com.onpositive.parse.model;

import javax.xml.bind.annotation.XmlElement;

public class Get_classname_FormBody {

    @XmlElement(name="include")
    public String include;


    @XmlElement(name="where")
    public String where;


    @XmlElement(name="order")
    public String order;


    @XmlElement(name="limit")
    public Double limit;


    @XmlElement(name="skip")
    public Double skip;


    @XmlElement(name="keys")
    public String keys;


    @XmlElement(name="count")
    public Double count;

}