package com.onpositive.parse.client;

import com.onpositive.parse.client.executor.JavaExecutor;
import com.onpositive.parse.model.Functions;

public class Functions_functionNameResource {

    Functions_functionNameResource(JavaExecutor executor, String version, String functionName){

         this.executor = executor;

         this.version = version;

         this.functionName = functionName;

         this.uri = this.uri.replace("{version}",this.version);

         this.uri = this.uri.replace("{functionName}",this.functionName);

    }


    private String uri = "https://api.parse.com/{version}/functions/{functionName}";
private String[] relativeUriSegments = new String[]{"/functions/{functionName}"};


    private final JavaExecutor executor;


    private final String version;


    private final String functionName;




    public Object post(Functions payload){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "post", Object.class, Functions.class, null, payload, null);
    }




}