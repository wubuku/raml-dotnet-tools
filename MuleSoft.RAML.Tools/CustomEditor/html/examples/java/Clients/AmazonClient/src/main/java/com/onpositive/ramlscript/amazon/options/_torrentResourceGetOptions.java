package com.onpositive.ramlscript.amazon.options;

import javax.xml.bind.annotation.XmlElement;

public class _torrentResourceGetOptions {

    @XmlElement(name="header_Range")
    public String header_Range;


    @XmlElement(name="header_If-Modified-Since")
    public String header_If_Modified_Since;


    @XmlElement(name="header_If-Unmodified-Since")
    public String header_If_Unmodified_Since;


    @XmlElement(name="header_If-Match")
    public String header_If_Match;


    @XmlElement(name="header_If-None-Match")
    public String header_If_None_Match;


    @XmlElement(name="header_Content-Type")
    public String header_Content_Type;


    @XmlElement(name="header_Expect")
    public String header_Expect;


    @XmlElement(name="header_Host")
    public String header_Host;


    @XmlElement(name="header_x-amz-security-token")
    public String header_x_amz_security_token;


    @XmlElement(name="header_Authorization")
    public String header_Authorization;


    @XmlElement(name="header_Date")
    public String header_Date;


    @XmlElement(name="header_x-amz-date")
    public String header_x_amz_date;

}