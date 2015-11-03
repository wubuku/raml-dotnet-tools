package com.onpositive.parse.client;

import com.onpositive.parse.client.executor.JavaExecutor;
import com.onpositive.parse.model.ClassPut;
import com.onpositive.parse.model.CollectionPutResponse;
import com.onpositive.parse.model.InstallationGet;

public class ObjectIdResource3 {

    ObjectIdResource3(JavaExecutor executor, String version, String objectId){

         this.executor = executor;

         this.version = version;

         this.objectId = objectId;

         this.uri = this.uri.replace("{version}",this.version);

         this.uri = this.uri.replace("{objectId}",this.objectId);

    }


    private String uri = "https://api.parse.com/{version}/installations/{objectId}";
private String[] relativeUriSegments = new String[]{"/installations", "/{objectId}"};


    private final JavaExecutor executor;


    private final String version;


    private final String objectId;




    public InstallationGet get(){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", InstallationGet.class, null, null, null, null);
    }


    public CollectionPutResponse put(ClassPut payload){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "put", CollectionPutResponse.class, ClassPut.class, null, payload, null);
    }


    public Object delete(){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "delete", Object.class, null, null, null, null);
    }




}