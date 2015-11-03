package com.onpositive.ramlscript.amazon.model;

import javax.xml.bind.annotation.XmlElement;

public class PostFormBody {

    @XmlElement(name="AWSAccessKeyId")
    public String AWSAccessKeyId;


    @XmlElement(name="acl")
    public String acl;


    @XmlElement(name="file")
    public String file;


    @XmlElement(name="key")
    public String key;


    @XmlElement(name="policy")
    public String policy;


    @XmlElement(name="success_action_redirect")
    public String success_action_redirect;


    @XmlElement(name="redirect")
    public String redirect;


    @XmlElement(name="success_action_status")
    public String success_action_status;


    @XmlElement(name="x-amz-storage-class")
    public String x_amz_storage_class;


    @XmlElement(name="x-amz-meta-{*}")
    public String x_amz_meta____;


    @XmlElement(name="x-amz-security-token")
    public String x_amz_security_token;


    @XmlElement(name="x-amz-server-side-encryption")
    public String x_amz_server_side_encryption;


    @XmlElement(name="x-amz-website-redirect-location")
    public String x_amz_website_redirect_location;

}