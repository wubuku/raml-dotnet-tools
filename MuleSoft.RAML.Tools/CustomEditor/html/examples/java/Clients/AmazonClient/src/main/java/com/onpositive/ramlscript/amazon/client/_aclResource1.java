package com.onpositive.ramlscript.amazon.client;

import com.onpositive.ramlscript.amazon.client.executor.JavaExecutor;
import com.onpositive.ramlscript.amazon.model.Bucket_grant_acl;
import com.onpositive.ramlscript.amazon.model.Error;
import com.onpositive.ramlscript.amazon.model.union.Bucket_aclErrorUnion;
import com.onpositive.ramlscript.amazon.options._aclResource1GetOptions;
import com.onpositive.ramlscript.amazon.options._aclResource1PutOptions;

public class _aclResource1 {

    _aclResource1(JavaExecutor executor, String bucketName, String region, String objectName){

         this.executor = executor;

         this.bucketName = bucketName;

         this.region = region;

         this.objectName = objectName;

         this.uri = this.uri.replace("{bucketName}",this.bucketName);

         this.uri = this.uri.replace("{region}",this.region);

         this.uri = this.uri.replace("{objectName}",this.objectName);

    }


    private String uri = "https://{bucketName}.{region}.amazonaws.com/{objectName}/?acl";
private String[] relativeUriSegments = new String[]{"/{objectName}", "/?acl"};


    private final JavaExecutor executor;


    private final String bucketName;


    private final String region;


    private final String objectName;




    public Bucket_aclErrorUnion get(_aclResource1GetOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", Bucket_aclErrorUnion.class, null, _aclResource1GetOptions.class, null, options);
    }


    public Error put(Bucket_grant_acl payload, _aclResource1PutOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "put", Error.class, Bucket_grant_acl.class, _aclResource1PutOptions.class, payload, options);
    }




}