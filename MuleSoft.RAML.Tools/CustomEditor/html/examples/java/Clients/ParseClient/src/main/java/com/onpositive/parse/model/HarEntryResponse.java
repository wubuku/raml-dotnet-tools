package com.onpositive.parse.model;

import javax.xml.bind.annotation.XmlElement;

public class HarEntryResponse {

    @XmlElement(name="content")
    public HarEntryResponseContent content;


    @XmlElement(name="status")
    public Double status;

}