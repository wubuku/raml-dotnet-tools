package com.onpositive.parse.model;

import javax.xml.bind.annotation.XmlElement;

public class Analytics {

    @XmlElement(name="at")
    public AnalyticsAt at;


    @XmlElement(name="dimensions")
    public AnalyticsDimensions dimensions;

}