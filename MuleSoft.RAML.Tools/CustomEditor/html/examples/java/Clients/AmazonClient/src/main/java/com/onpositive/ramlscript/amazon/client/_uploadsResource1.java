package com.onpositive.ramlscript.amazon.client;

import com.onpositive.ramlscript.amazon.client.executor.JavaExecutor;
import com.onpositive.ramlscript.amazon.model.union.ErrorPost_uploadsXmlResponseUnion;
import com.onpositive.ramlscript.amazon.options._uploadsResource1PostOptions;

public class _uploadsResource1 {

    _uploadsResource1(JavaExecutor executor, String bucketName, String region, String objectName){

         this.executor = executor;

         this.bucketName = bucketName;

         this.region = region;

         this.objectName = objectName;

         this.uri = this.uri.replace("{bucketName}",this.bucketName);

         this.uri = this.uri.replace("{region}",this.region);

         this.uri = this.uri.replace("{objectName}",this.objectName);

    }


    private String uri = "https://{bucketName}.{region}.amazonaws.com/{objectName}/?uploads";
private String[] relativeUriSegments = new String[]{"/{objectName}", "/?uploads"};


    private final JavaExecutor executor;


    private final String bucketName;


    private final String region;


    private final String objectName;




    public ErrorPost_uploadsXmlResponseUnion post(_uploadsResource1PostOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "post", ErrorPost_uploadsXmlResponseUnion.class, null, _uploadsResource1PostOptions.class, null, options);
    }




}