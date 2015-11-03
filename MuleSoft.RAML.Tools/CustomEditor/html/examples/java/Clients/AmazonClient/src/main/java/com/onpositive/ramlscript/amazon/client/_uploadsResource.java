package com.onpositive.ramlscript.amazon.client;

import com.onpositive.ramlscript.amazon.client.executor.JavaExecutor;
import com.onpositive.ramlscript.amazon.model.union.ListMultipartUploadsResultErrorUnion;
import com.onpositive.ramlscript.amazon.options._uploadsResourceGetOptions;

public class _uploadsResource {

    _uploadsResource(JavaExecutor executor, String bucketName, String region){

         this.executor = executor;

         this.bucketName = bucketName;

         this.region = region;

         this.uri = this.uri.replace("{bucketName}",this.bucketName);

         this.uri = this.uri.replace("{region}",this.region);

    }


    private String uri = "https://{bucketName}.{region}.amazonaws.com/?uploads";
private String[] relativeUriSegments = new String[]{"/?uploads"};


    private final JavaExecutor executor;


    private final String bucketName;


    private final String region;




    public ListMultipartUploadsResultErrorUnion get(_uploadsResourceGetOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", ListMultipartUploadsResultErrorUnion.class, null, _uploadsResourceGetOptions.class, null, options);
    }




}