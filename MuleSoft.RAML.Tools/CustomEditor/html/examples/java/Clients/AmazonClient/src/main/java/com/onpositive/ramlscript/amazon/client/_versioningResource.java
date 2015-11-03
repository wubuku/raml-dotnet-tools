package com.onpositive.ramlscript.amazon.client;

import com.onpositive.ramlscript.amazon.client.executor.JavaExecutor;
import com.onpositive.ramlscript.amazon.model.Bucket_versioning;
import com.onpositive.ramlscript.amazon.model.Error;
import com.onpositive.ramlscript.amazon.model.union.Bucket_versioningErrorUnion;
import com.onpositive.ramlscript.amazon.options._versioningResourceGetOptions;
import com.onpositive.ramlscript.amazon.options._versioningResourcePutOptions;

public class _versioningResource {

    _versioningResource(JavaExecutor executor, String bucketName, String region){

         this.executor = executor;

         this.bucketName = bucketName;

         this.region = region;

         this.uri = this.uri.replace("{bucketName}",this.bucketName);

         this.uri = this.uri.replace("{region}",this.region);

    }


    private String uri = "https://{bucketName}.{region}.amazonaws.com/?versioning";
private String[] relativeUriSegments = new String[]{"/?versioning"};


    private final JavaExecutor executor;


    private final String bucketName;


    private final String region;




    public Bucket_versioningErrorUnion get(_versioningResourceGetOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", Bucket_versioningErrorUnion.class, null, _versioningResourceGetOptions.class, null, options);
    }


    public Error put(Bucket_versioning payload, _versioningResourcePutOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "put", Error.class, Bucket_versioning.class, _versioningResourcePutOptions.class, payload, options);
    }




}