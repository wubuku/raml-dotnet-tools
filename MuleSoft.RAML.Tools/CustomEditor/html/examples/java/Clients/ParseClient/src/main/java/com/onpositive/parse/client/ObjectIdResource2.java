package com.onpositive.parse.client;

import com.onpositive.parse.client.executor.JavaExecutor;
import com.onpositive.parse.model.CollectionPutResponse;
import com.onpositive.parse.model.RoleGet;
import com.onpositive.parse.model.RolePut;

public class ObjectIdResource2 {

    ObjectIdResource2(JavaExecutor executor, String version, String objectId){

         this.executor = executor;

         this.version = version;

         this.objectId = objectId;

         this.uri = this.uri.replace("{version}",this.version);

         this.uri = this.uri.replace("{objectId}",this.objectId);

    }


    private String uri = "https://api.parse.com/{version}/roles/{objectId}";
private String[] relativeUriSegments = new String[]{"/roles", "/{objectId}"};


    private final JavaExecutor executor;


    private final String version;


    private final String objectId;




    public RoleGet get(){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", RoleGet.class, null, null, null, null);
    }


    public CollectionPutResponse put(RolePut payload){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "put", CollectionPutResponse.class, RolePut.class, null, payload, null);
    }


    public Object delete(){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "delete", Object.class, null, null, null, null);
    }




}