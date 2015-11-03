package com.onpositive.ramlscript.amazon.model;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;

public class Bucket_policy_put_schemaStatement {

    @XmlElement(name="Action")
    public List<String> Action;


    @XmlElement(name="Effect")
    public String Effect;


    @XmlElement(name="Principal")
    public Bucket_policy_put_schemaStatementPrincipal Principal;


    @XmlElement(name="Resource")
    public String Resource;


    @XmlElement(name="Sid")
    public String Sid;

}