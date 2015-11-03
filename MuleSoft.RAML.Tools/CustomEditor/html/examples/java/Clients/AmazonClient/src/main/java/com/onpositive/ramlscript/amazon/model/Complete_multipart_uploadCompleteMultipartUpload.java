package com.onpositive.ramlscript.amazon.model;

import java.util.List;
import javax.xml.bind.annotation.XmlElement;

public class Complete_multipart_uploadCompleteMultipartUpload {

    @XmlElement(name="Part")
    public List<Complete_multipart_uploadCompleteMultipartUploadPart> Part;

}