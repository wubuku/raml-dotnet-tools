package com.onpositive.parse.client;

import com.onpositive.parse.client.executor.JavaExecutor;
import com.onpositive.parse.model.ClassGet;
import com.onpositive.parse.model.ClassPut;
import com.onpositive.parse.model.CollectionPutResponse;

public class ObjectIdResource {

    ObjectIdResource(JavaExecutor executor, String version, String className, String objectId){

         this.executor = executor;

         this.version = version;

         this.className = className;

         this.objectId = objectId;

         this.uri = this.uri.replace("{version}",this.version);

         this.uri = this.uri.replace("{className}",this.className);

         this.uri = this.uri.replace("{objectId}",this.objectId);

    }


    private String uri = "https://api.parse.com/{version}/classes/{className}/{objectId}";
private String[] relativeUriSegments = new String[]{"/classes", "/{className}", "/{objectId}"};


    private final JavaExecutor executor;


    private final String version;


    private final String className;


    private final String objectId;




    public ClassGet get(){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", ClassGet.class, null, null, null, null);
    }


    public CollectionPutResponse put(ClassPut payload){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "put", CollectionPutResponse.class, ClassPut.class, null, payload, null);
    }


    public Object delete(){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "delete", Object.class, null, null, null, null);
    }




}