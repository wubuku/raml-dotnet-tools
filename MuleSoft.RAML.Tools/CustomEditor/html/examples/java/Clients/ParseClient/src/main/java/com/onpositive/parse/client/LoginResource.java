package com.onpositive.parse.client;

import com.onpositive.parse.client.executor.JavaExecutor;
import com.onpositive.parse.model.GetLoginFormBody;
import com.onpositive.parse.model.UserLogin;

public class LoginResource {

    LoginResource(JavaExecutor executor, String version){

         this.executor = executor;

         this.version = version;

         this.uri = this.uri.replace("{version}",this.version);

    }


    private String uri = "https://api.parse.com/{version}/login";
private String[] relativeUriSegments = new String[]{"/login"};


    private final JavaExecutor executor;


    private final String version;




    public UserLogin get(GetLoginFormBody payload){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", UserLogin.class, GetLoginFormBody.class, null, payload, null);
    }




}