package com.onpositive.ramlscript.amazon.client;

import com.onpositive.ramlscript.amazon.client.executor.JavaExecutor;
import com.onpositive.ramlscript.amazon.model.Bucket_conf_website;
import com.onpositive.ramlscript.amazon.model.Error;
import com.onpositive.ramlscript.amazon.model.union.Bucket_conf_websiteErrorUnion;
import com.onpositive.ramlscript.amazon.options._websiteResourceDeleteOptions;
import com.onpositive.ramlscript.amazon.options._websiteResourceGetOptions;
import com.onpositive.ramlscript.amazon.options._websiteResourcePutOptions;

public class _websiteResource {

    _websiteResource(JavaExecutor executor, String bucketName, String region){

         this.executor = executor;

         this.bucketName = bucketName;

         this.region = region;

         this.uri = this.uri.replace("{bucketName}",this.bucketName);

         this.uri = this.uri.replace("{region}",this.region);

    }


    private String uri = "https://{bucketName}.{region}.amazonaws.com/?website";
private String[] relativeUriSegments = new String[]{"/?website"};


    private final JavaExecutor executor;


    private final String bucketName;


    private final String region;




    public Bucket_conf_websiteErrorUnion get(_websiteResourceGetOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", Bucket_conf_websiteErrorUnion.class, null, _websiteResourceGetOptions.class, null, options);
    }


    public Error put(Bucket_conf_website payload, _websiteResourcePutOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "put", Error.class, Bucket_conf_website.class, _websiteResourcePutOptions.class, payload, options);
    }


    public Error delete(_websiteResourceDeleteOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "delete", Error.class, null, _websiteResourceDeleteOptions.class, null, options);
    }




}