package com.onpositive.ramlscript.amazon.model;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;

public class Bucket_delete_multipleDelete {

    @XmlElement(name="Quiet")
    public String Quiet;


    @XmlElement(name="Object")
    public List<Bucket_delete_multipleDeleteObject> Object;

}