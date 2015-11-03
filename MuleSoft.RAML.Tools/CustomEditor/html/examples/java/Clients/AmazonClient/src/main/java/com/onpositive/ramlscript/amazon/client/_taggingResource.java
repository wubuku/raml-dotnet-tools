package com.onpositive.ramlscript.amazon.client;

import com.onpositive.ramlscript.amazon.client.executor.JavaExecutor;
import com.onpositive.ramlscript.amazon.model.Bucket_conf_tagging;
import com.onpositive.ramlscript.amazon.model.Error;
import com.onpositive.ramlscript.amazon.model.union.Bucket_conf_taggingErrorUnion;
import com.onpositive.ramlscript.amazon.options._taggingResourceDeleteOptions;
import com.onpositive.ramlscript.amazon.options._taggingResourceGetOptions;
import com.onpositive.ramlscript.amazon.options._taggingResourcePutOptions;

public class _taggingResource {

    _taggingResource(JavaExecutor executor, String bucketName, String region){

         this.executor = executor;

         this.bucketName = bucketName;

         this.region = region;

         this.uri = this.uri.replace("{bucketName}",this.bucketName);

         this.uri = this.uri.replace("{region}",this.region);

    }


    private String uri = "https://{bucketName}.{region}.amazonaws.com/?tagging";
private String[] relativeUriSegments = new String[]{"/?tagging"};


    private final JavaExecutor executor;


    private final String bucketName;


    private final String region;




    public Bucket_conf_taggingErrorUnion get(_taggingResourceGetOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", Bucket_conf_taggingErrorUnion.class, null, _taggingResourceGetOptions.class, null, options);
    }


    public Error put(Bucket_conf_tagging payload, _taggingResourcePutOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "put", Error.class, Bucket_conf_tagging.class, _taggingResourcePutOptions.class, payload, options);
    }


    public Error delete(_taggingResourceDeleteOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "delete", Error.class, null, _taggingResourceDeleteOptions.class, null, options);
    }




}