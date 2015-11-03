package com.onpositive.parse.client;

import com.onpositive.parse.client.executor.JavaExecutor;
import com.onpositive.parse.model.CollectionPostResponse;
import com.onpositive.parse.model.GetRolesFormBody;
import com.onpositive.parse.model.RolePost;
import com.onpositive.parse.model.RolesQuery;

public class RolesResource {

    RolesResource(JavaExecutor executor, String version){

         this.executor = executor;

         this.version = version;

         this.uri = this.uri.replace("{version}",this.version);

    }


    private String uri = "https://api.parse.com/{version}/roles";
private String[] relativeUriSegments = new String[]{"/roles"};


    private final JavaExecutor executor;


    private final String version;




    public ObjectIdResource2 objectId(String objectId){
        return new ObjectIdResource2(this.executor, this.version, objectId);
    }


    public RolesQuery get(GetRolesFormBody payload){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", RolesQuery.class, GetRolesFormBody.class, null, payload, null);
    }


    public CollectionPostResponse post(RolePost payload){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "post", CollectionPostResponse.class, RolePost.class, null, payload, null);
    }




}