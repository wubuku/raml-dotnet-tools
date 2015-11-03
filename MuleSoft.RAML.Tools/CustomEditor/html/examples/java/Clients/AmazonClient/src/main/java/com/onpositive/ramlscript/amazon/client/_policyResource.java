package com.onpositive.ramlscript.amazon.client;

import com.onpositive.ramlscript.amazon.client.executor.JavaExecutor;
import com.onpositive.ramlscript.amazon.model.Bucket_policy_put_schema;
import com.onpositive.ramlscript.amazon.model.Error;
import com.onpositive.ramlscript.amazon.model.union.Bucket_conf_policy_schemaErrorUnion;
import com.onpositive.ramlscript.amazon.options._policyResourceDeleteOptions;
import com.onpositive.ramlscript.amazon.options._policyResourceGetOptions;
import com.onpositive.ramlscript.amazon.options._policyResourcePutOptions;

public class _policyResource {

    _policyResource(JavaExecutor executor, String bucketName, String region){

         this.executor = executor;

         this.bucketName = bucketName;

         this.region = region;

         this.uri = this.uri.replace("{bucketName}",this.bucketName);

         this.uri = this.uri.replace("{region}",this.region);

    }


    private String uri = "https://{bucketName}.{region}.amazonaws.com/?policy";
private String[] relativeUriSegments = new String[]{"/?policy"};


    private final JavaExecutor executor;


    private final String bucketName;


    private final String region;




    public Bucket_conf_policy_schemaErrorUnion get(_policyResourceGetOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", Bucket_conf_policy_schemaErrorUnion.class, null, _policyResourceGetOptions.class, null, options);
    }


    public Error put(Bucket_policy_put_schema payload, _policyResourcePutOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "put", Error.class, Bucket_policy_put_schema.class, _policyResourcePutOptions.class, payload, options);
    }


    public Error delete(_policyResourceDeleteOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "delete", Error.class, null, _policyResourceDeleteOptions.class, null, options);
    }




}