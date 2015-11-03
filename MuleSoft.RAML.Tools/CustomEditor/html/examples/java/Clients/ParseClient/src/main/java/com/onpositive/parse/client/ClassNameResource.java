package com.onpositive.parse.client;

import com.onpositive.parse.client.executor.JavaExecutor;
import com.onpositive.parse.model.ClassPost;
import com.onpositive.parse.model.ClassesQuery;
import com.onpositive.parse.model.CollectionPostResponse;
import com.onpositive.parse.model.Get_classname_FormBody;

public class ClassNameResource {

    ClassNameResource(JavaExecutor executor, String version, String className){

         this.executor = executor;

         this.version = version;

         this.className = className;

         this.uri = this.uri.replace("{version}",this.version);

         this.uri = this.uri.replace("{className}",this.className);

    }


    private String uri = "https://api.parse.com/{version}/classes/{className}";
private String[] relativeUriSegments = new String[]{"/classes", "/{className}"};


    private final JavaExecutor executor;


    private final String version;


    private final String className;




    public ObjectIdResource objectId(String objectId){
        return new ObjectIdResource(this.executor, this.version, this.className, objectId);
    }


    public ClassesQuery get(Get_classname_FormBody payload){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", ClassesQuery.class, Get_classname_FormBody.class, null, payload, null);
    }


    public CollectionPostResponse post(ClassPost payload){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "post", CollectionPostResponse.class, ClassPost.class, null, payload, null);
    }




}