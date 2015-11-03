package com.onpositive.parse.model;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;

public class BatchPost {

    @XmlElement(name="requests")
    public List<BatchPostRequests> requests;

}