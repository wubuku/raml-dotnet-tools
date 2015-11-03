package com.onpositive.ramlscript.amazon.client;

import com.onpositive.ramlscript.amazon.client.executor.JavaExecutor;
import com.onpositive.ramlscript.amazon.model.Error;
import com.onpositive.ramlscript.amazon.model.RequestPayement_configuration;
import com.onpositive.ramlscript.amazon.model.union.RequestPayement_configurationErrorUnion;
import com.onpositive.ramlscript.amazon.options._requestPaymentResourceGetOptions;
import com.onpositive.ramlscript.amazon.options._requestPaymentResourcePutOptions;

public class _requestPaymentResource {

    _requestPaymentResource(JavaExecutor executor, String bucketName, String region){

         this.executor = executor;

         this.bucketName = bucketName;

         this.region = region;

         this.uri = this.uri.replace("{bucketName}",this.bucketName);

         this.uri = this.uri.replace("{region}",this.region);

    }


    private String uri = "https://{bucketName}.{region}.amazonaws.com/?requestPayment";
private String[] relativeUriSegments = new String[]{"/?requestPayment"};


    private final JavaExecutor executor;


    private final String bucketName;


    private final String region;




    public RequestPayement_configurationErrorUnion get(_requestPaymentResourceGetOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", RequestPayement_configurationErrorUnion.class, null, _requestPaymentResourceGetOptions.class, null, options);
    }


    public Error put(RequestPayement_configuration payload, _requestPaymentResourcePutOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "put", Error.class, RequestPayement_configuration.class, _requestPaymentResourcePutOptions.class, payload, options);
    }




}