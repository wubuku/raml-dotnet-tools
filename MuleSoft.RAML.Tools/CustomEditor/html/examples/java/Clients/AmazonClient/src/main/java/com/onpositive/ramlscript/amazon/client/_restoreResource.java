package com.onpositive.ramlscript.amazon.client;

import com.onpositive.ramlscript.amazon.client.executor.JavaExecutor;
import com.onpositive.ramlscript.amazon.model.Error;
import com.onpositive.ramlscript.amazon.model.ObjectRestore;
import com.onpositive.ramlscript.amazon.options._restoreResourcePostOptions;

public class _restoreResource {

    _restoreResource(JavaExecutor executor, String bucketName, String region, String objectName){

         this.executor = executor;

         this.bucketName = bucketName;

         this.region = region;

         this.objectName = objectName;

         this.uri = this.uri.replace("{bucketName}",this.bucketName);

         this.uri = this.uri.replace("{region}",this.region);

         this.uri = this.uri.replace("{objectName}",this.objectName);

    }


    private String uri = "https://{bucketName}.{region}.amazonaws.com/{objectName}/?restore";
private String[] relativeUriSegments = new String[]{"/{objectName}", "/?restore"};


    private final JavaExecutor executor;


    private final String bucketName;


    private final String region;


    private final String objectName;




    public Error post(ObjectRestore payload, _restoreResourcePostOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "post", Error.class, ObjectRestore.class, _restoreResourcePostOptions.class, payload, options);
    }




}