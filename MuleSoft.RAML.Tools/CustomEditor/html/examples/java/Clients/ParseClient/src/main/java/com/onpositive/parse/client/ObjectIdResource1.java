package com.onpositive.parse.client;

import com.onpositive.parse.client.executor.JavaExecutor;
import com.onpositive.parse.model.CollectionPutResponse;
import com.onpositive.parse.model.UserGet;
import com.onpositive.parse.model.UserPut;

public class ObjectIdResource1 {

    ObjectIdResource1(JavaExecutor executor, String version, String objectId){

         this.executor = executor;

         this.version = version;

         this.objectId = objectId;

         this.uri = this.uri.replace("{version}",this.version);

         this.uri = this.uri.replace("{objectId}",this.objectId);

    }


    private String uri = "https://api.parse.com/{version}/users/{objectId}";
private String[] relativeUriSegments = new String[]{"/users", "/{objectId}"};


    private final JavaExecutor executor;


    private final String version;


    private final String objectId;




    public UserGet get(){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", UserGet.class, null, null, null, null);
    }


    public CollectionPutResponse put(UserPut payload){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "put", CollectionPutResponse.class, UserPut.class, null, payload, null);
    }


    public Object delete(){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "delete", Object.class, null, null, null, null);
    }




}