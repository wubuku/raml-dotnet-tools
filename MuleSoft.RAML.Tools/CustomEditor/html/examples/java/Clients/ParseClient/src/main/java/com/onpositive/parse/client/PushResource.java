package com.onpositive.parse.client;

import com.onpositive.parse.client.executor.JavaExecutor;
import com.onpositive.parse.model.Push;

public class PushResource {

    PushResource(JavaExecutor executor, String version){

         this.executor = executor;

         this.version = version;

         this.uri = this.uri.replace("{version}",this.version);

    }


    private String uri = "https://api.parse.com/{version}/push";
private String[] relativeUriSegments = new String[]{"/push"};


    private final JavaExecutor executor;


    private final String version;




    public Object post(Push payload){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "post", Object.class, Push.class, null, payload, null);
    }




}