package com.onpositive.parse.model;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;

public class Push {

    @XmlElement(name="channels")
    public List<String> channels;


    @XmlElement(name="data")
    public PushData data;


    @XmlElement(name="expiration_interval")
    public Double expiration_interval;


    @XmlElement(name="push_time")
    public String push_time;


    @XmlElement(name="where")
    public PushWhere where;

}