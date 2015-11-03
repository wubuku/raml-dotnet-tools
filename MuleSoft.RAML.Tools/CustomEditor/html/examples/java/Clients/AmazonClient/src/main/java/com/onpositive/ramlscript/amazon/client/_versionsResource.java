package com.onpositive.ramlscript.amazon.client;

import com.onpositive.ramlscript.amazon.client.executor.JavaExecutor;
import com.onpositive.ramlscript.amazon.model.union.Bucket_object_versionsErrorUnion;
import com.onpositive.ramlscript.amazon.options._versionsResourceGetOptions;

public class _versionsResource {

    _versionsResource(JavaExecutor executor, String bucketName, String region){

         this.executor = executor;

         this.bucketName = bucketName;

         this.region = region;

         this.uri = this.uri.replace("{bucketName}",this.bucketName);

         this.uri = this.uri.replace("{region}",this.region);

    }


    private String uri = "https://{bucketName}.{region}.amazonaws.com/?versions";
private String[] relativeUriSegments = new String[]{"/?versions"};


    private final JavaExecutor executor;


    private final String bucketName;


    private final String region;




    public Bucket_object_versionsErrorUnion get(_versionsResourceGetOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", Bucket_object_versionsErrorUnion.class, null, _versionsResourceGetOptions.class, null, options);
    }




}