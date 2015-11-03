package com.onpositive.ramlscript.amazon.client;

import com.onpositive.ramlscript.amazon.client.executor.JavaExecutor;
import com.onpositive.ramlscript.amazon.model.Bucket_grant_acl;
import com.onpositive.ramlscript.amazon.model.Error;
import com.onpositive.ramlscript.amazon.model.union.Bucket_aclErrorUnion;
import com.onpositive.ramlscript.amazon.options._aclResourceGetOptions;
import com.onpositive.ramlscript.amazon.options._aclResourcePutOptions;

public class _aclResource {

    _aclResource(JavaExecutor executor, String bucketName, String region){

         this.executor = executor;

         this.bucketName = bucketName;

         this.region = region;

         this.uri = this.uri.replace("{bucketName}",this.bucketName);

         this.uri = this.uri.replace("{region}",this.region);

    }


    private String uri = "https://{bucketName}.{region}.amazonaws.com/?acl";
private String[] relativeUriSegments = new String[]{"/?acl"};


    private final JavaExecutor executor;


    private final String bucketName;


    private final String region;




    public Bucket_aclErrorUnion get(_aclResourceGetOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", Bucket_aclErrorUnion.class, null, _aclResourceGetOptions.class, null, options);
    }


    public Error put(Bucket_grant_acl payload, _aclResourcePutOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "put", Error.class, Bucket_grant_acl.class, _aclResourcePutOptions.class, payload, options);
    }




}