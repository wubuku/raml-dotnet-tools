package com.onpositive.ramlscript.amazon.client;

import com.onpositive.ramlscript.amazon.client.executor.JavaExecutor;
import com.onpositive.ramlscript.amazon.model.Bucket_logging;
import com.onpositive.ramlscript.amazon.model.Error;
import com.onpositive.ramlscript.amazon.model.union.Bucket_loggingErrorUnion;
import com.onpositive.ramlscript.amazon.options._loggingResourceGetOptions;
import com.onpositive.ramlscript.amazon.options._loggingResourcePutOptions;

public class _loggingResource {

    _loggingResource(JavaExecutor executor, String bucketName, String region){

         this.executor = executor;

         this.bucketName = bucketName;

         this.region = region;

         this.uri = this.uri.replace("{bucketName}",this.bucketName);

         this.uri = this.uri.replace("{region}",this.region);

    }


    private String uri = "https://{bucketName}.{region}.amazonaws.com/?logging";
private String[] relativeUriSegments = new String[]{"/?logging"};


    private final JavaExecutor executor;


    private final String bucketName;


    private final String region;




    public Bucket_loggingErrorUnion get(_loggingResourceGetOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", Bucket_loggingErrorUnion.class, null, _loggingResourceGetOptions.class, null, options);
    }


    public Error put(Bucket_logging payload, _loggingResourcePutOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "put", Error.class, Bucket_logging.class, _loggingResourcePutOptions.class, payload, options);
    }




}