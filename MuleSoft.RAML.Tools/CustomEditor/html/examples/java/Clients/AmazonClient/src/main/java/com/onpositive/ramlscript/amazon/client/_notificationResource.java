package com.onpositive.ramlscript.amazon.client;

import com.onpositive.ramlscript.amazon.client.executor.JavaExecutor;
import com.onpositive.ramlscript.amazon.model.Error;
import com.onpositive.ramlscript.amazon.model.Notification_configuration;
import com.onpositive.ramlscript.amazon.model.union.Notification_configurationErrorUnion;
import com.onpositive.ramlscript.amazon.options._notificationResourceGetOptions;
import com.onpositive.ramlscript.amazon.options._notificationResourcePutOptions;

public class _notificationResource {

    _notificationResource(JavaExecutor executor, String bucketName, String region){

         this.executor = executor;

         this.bucketName = bucketName;

         this.region = region;

         this.uri = this.uri.replace("{bucketName}",this.bucketName);

         this.uri = this.uri.replace("{region}",this.region);

    }


    private String uri = "https://{bucketName}.{region}.amazonaws.com/?notification";
private String[] relativeUriSegments = new String[]{"/?notification"};


    private final JavaExecutor executor;


    private final String bucketName;


    private final String region;




    public Notification_configurationErrorUnion get(_notificationResourceGetOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", Notification_configurationErrorUnion.class, null, _notificationResourceGetOptions.class, null, options);
    }


    public Error put(Notification_configuration payload, _notificationResourcePutOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "put", Error.class, Notification_configuration.class, _notificationResourcePutOptions.class, payload, options);
    }




}