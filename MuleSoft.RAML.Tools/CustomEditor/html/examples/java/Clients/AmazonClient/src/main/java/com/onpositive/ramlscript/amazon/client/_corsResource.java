package com.onpositive.ramlscript.amazon.client;

import com.onpositive.ramlscript.amazon.client.executor.JavaExecutor;
import com.onpositive.ramlscript.amazon.model.Bucket_conf_cors;
import com.onpositive.ramlscript.amazon.model.Error;
import com.onpositive.ramlscript.amazon.model.union.Bucket_conf_corsErrorUnion;
import com.onpositive.ramlscript.amazon.options._corsResourceDeleteOptions;
import com.onpositive.ramlscript.amazon.options._corsResourceGetOptions;
import com.onpositive.ramlscript.amazon.options._corsResourcePutOptions;

public class _corsResource {

    _corsResource(JavaExecutor executor, String bucketName, String region){

         this.executor = executor;

         this.bucketName = bucketName;

         this.region = region;

         this.uri = this.uri.replace("{bucketName}",this.bucketName);

         this.uri = this.uri.replace("{region}",this.region);

    }


    private String uri = "https://{bucketName}.{region}.amazonaws.com/?cors";
private String[] relativeUriSegments = new String[]{"/?cors"};


    private final JavaExecutor executor;


    private final String bucketName;


    private final String region;




    public Bucket_conf_corsErrorUnion get(_corsResourceGetOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", Bucket_conf_corsErrorUnion.class, null, _corsResourceGetOptions.class, null, options);
    }


    public Error put(Bucket_conf_cors payload, _corsResourcePutOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "put", Error.class, Bucket_conf_cors.class, _corsResourcePutOptions.class, payload, options);
    }


    public Error delete(_corsResourceDeleteOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "delete", Error.class, null, _corsResourceDeleteOptions.class, null, options);
    }




}