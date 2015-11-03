package com.onpositive.parse.client;

import com.onpositive.parse.client.executor.JavaExecutor;
import com.onpositive.parse.model.RequestPasswordReset;

public class RequestPasswordResetResource {

    RequestPasswordResetResource(JavaExecutor executor, String version){

         this.executor = executor;

         this.version = version;

         this.uri = this.uri.replace("{version}",this.version);

    }


    private String uri = "https://api.parse.com/{version}/requestPasswordReset";
private String[] relativeUriSegments = new String[]{"/requestPasswordReset"};


    private final JavaExecutor executor;


    private final String version;




    public Object post(RequestPasswordReset payload){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "post", Object.class, RequestPasswordReset.class, null, payload, null);
    }




}