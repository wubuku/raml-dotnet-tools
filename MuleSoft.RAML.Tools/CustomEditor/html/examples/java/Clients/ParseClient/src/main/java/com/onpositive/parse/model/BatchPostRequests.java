package com.onpositive.parse.model;

import javax.xml.bind.annotation.XmlElement;

public class BatchPostRequests {

    @XmlElement(name="body")
    public BatchPostRequestsBody body;


    @XmlElement(name="method")
    public String method;


    @XmlElement(name="path")
    public String path;

}