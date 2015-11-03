package com.onpositive.ramlscript.amazon.client;

import com.onpositive.ramlscript.amazon.client.executor.JavaExecutor;
import com.onpositive.ramlscript.amazon.model.Bucket_delete_multiple;
import com.onpositive.ramlscript.amazon.model.union.Bucket_delete_multiple_responseErrorUnion;
import com.onpositive.ramlscript.amazon.options._deleteResourcePostOptions;

public class _deleteResource {

    _deleteResource(JavaExecutor executor, String bucketName, String region){

         this.executor = executor;

         this.bucketName = bucketName;

         this.region = region;

         this.uri = this.uri.replace("{bucketName}",this.bucketName);

         this.uri = this.uri.replace("{region}",this.region);

    }


    private String uri = "https://{bucketName}.{region}.amazonaws.com/?delete";
private String[] relativeUriSegments = new String[]{"/?delete"};


    private final JavaExecutor executor;


    private final String bucketName;


    private final String region;




    public Bucket_delete_multiple_responseErrorUnion post(Bucket_delete_multiple payload, _deleteResourcePostOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "post", Bucket_delete_multiple_responseErrorUnion.class, Bucket_delete_multiple.class, _deleteResourcePostOptions.class, payload, options);
    }




}