package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class ErrorError {

    @XmlElement(name="Code")
    public String Code;


    @XmlElement(name="Message")
    public String Message;


    @XmlElement(name="Resource")
    public String Resource;


    @XmlElement(name="RequestId")
    public String RequestId;

}