package com.mulesoft.raml.annotation.exmaples;

import javax.xml.bind.annotation.XmlElement;

import com.mulesoft.raml1.java.parser.core.CustomType;

public class paging extends CustomType{

    @XmlElement(name="pageSize")
    public String pageSize;


    @XmlElement(name="offset")
    public String offset;

}