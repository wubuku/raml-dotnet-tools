package com.onpositive.ramlscript.amazon.client;

import com.onpositive.ramlscript.amazon.client.executor.JavaExecutor;
import com.onpositive.ramlscript.amazon.model.union.Location_constraintErrorUnion;
import com.onpositive.ramlscript.amazon.options._locationResourceGetOptions;

public class _locationResource {

    _locationResource(JavaExecutor executor, String bucketName, String region){

         this.executor = executor;

         this.bucketName = bucketName;

         this.region = region;

         this.uri = this.uri.replace("{bucketName}",this.bucketName);

         this.uri = this.uri.replace("{region}",this.region);

    }


    private String uri = "https://{bucketName}.{region}.amazonaws.com/?location";
private String[] relativeUriSegments = new String[]{"/?location"};


    private final JavaExecutor executor;


    private final String bucketName;


    private final String region;




    public Location_constraintErrorUnion get(_locationResourceGetOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", Location_constraintErrorUnion.class, null, _locationResourceGetOptions.class, null, options);
    }




}