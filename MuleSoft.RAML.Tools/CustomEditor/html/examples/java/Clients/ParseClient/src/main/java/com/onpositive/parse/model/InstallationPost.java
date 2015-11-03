package com.onpositive.parse.model;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;

public class InstallationPost {

    @XmlElement(name="deviceType")
    public String deviceType;


    @XmlElement(name="installationId")
    public String installationId;


    @XmlElement(name="deviceToken")
    public String deviceToken;


    @XmlElement(name="badge")
    public Double badge;


    @XmlElement(name="timeZone")
    public String timeZone;


    @XmlElement(name="channels")
    public List<String> channels;

}