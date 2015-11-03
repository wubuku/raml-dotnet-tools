package com.onpositive.ramlscript.amazon.client;

import com.onpositive.ramlscript.amazon.client.executor.JavaExecutor;
import com.onpositive.ramlscript.amazon.model.Bucket_lifecycle_put;
import com.onpositive.ramlscript.amazon.model.Error;
import com.onpositive.ramlscript.amazon.model.union.Bucket_conf_lifecycleErrorUnion;
import com.onpositive.ramlscript.amazon.options._lifecycleResourceDeleteOptions;
import com.onpositive.ramlscript.amazon.options._lifecycleResourceGetOptions;
import com.onpositive.ramlscript.amazon.options._lifecycleResourcePutOptions;

public class _lifecycleResource {

    _lifecycleResource(JavaExecutor executor, String bucketName, String region){

         this.executor = executor;

         this.bucketName = bucketName;

         this.region = region;

         this.uri = this.uri.replace("{bucketName}",this.bucketName);

         this.uri = this.uri.replace("{region}",this.region);

    }


    private String uri = "https://{bucketName}.{region}.amazonaws.com/?lifecycle";
private String[] relativeUriSegments = new String[]{"/?lifecycle"};


    private final JavaExecutor executor;


    private final String bucketName;


    private final String region;




    public Bucket_conf_lifecycleErrorUnion get(_lifecycleResourceGetOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", Bucket_conf_lifecycleErrorUnion.class, null, _lifecycleResourceGetOptions.class, null, options);
    }


    public Error put(Bucket_lifecycle_put payload, _lifecycleResourcePutOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "put", Error.class, Bucket_lifecycle_put.class, _lifecycleResourcePutOptions.class, payload, options);
    }


    public Error delete(_lifecycleResourceDeleteOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "delete", Error.class, null, _lifecycleResourceDeleteOptions.class, null, options);
    }




}