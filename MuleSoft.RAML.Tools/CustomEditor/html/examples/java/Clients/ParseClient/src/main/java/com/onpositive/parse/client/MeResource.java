package com.onpositive.parse.client;

import com.onpositive.parse.client.executor.JavaExecutor;
import com.onpositive.parse.model.UserGet;

public class MeResource {

    MeResource(JavaExecutor executor, String version){

         this.executor = executor;

         this.version = version;

         this.uri = this.uri.replace("{version}",this.version);

    }


    private String uri = "https://api.parse.com/{version}/users/me";
private String[] relativeUriSegments = new String[]{"/users", "/me"};


    private final JavaExecutor executor;


    private final String version;




    public UserGet get(){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", UserGet.class, null, null, null, null);
    }




}