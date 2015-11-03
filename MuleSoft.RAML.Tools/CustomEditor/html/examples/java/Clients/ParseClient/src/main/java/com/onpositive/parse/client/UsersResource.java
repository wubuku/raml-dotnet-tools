package com.onpositive.parse.client;

import com.onpositive.parse.client.executor.JavaExecutor;
import com.onpositive.parse.model.GetUsersFormBody;
import com.onpositive.parse.model.UserPost;
import com.onpositive.parse.model.UsersPostResponse;
import com.onpositive.parse.model.UsersQuery;

public class UsersResource {

    UsersResource(JavaExecutor executor, String version){

         this.executor = executor;

         this.version = version;

         this.uri = this.uri.replace("{version}",this.version);

         this.me = new MeResource(this.executor, this.version);

    }


    private String uri = "https://api.parse.com/{version}/users";
private String[] relativeUriSegments = new String[]{"/users"};


    private final JavaExecutor executor;


    private final String version;


    final public MeResource me;




    public ObjectIdResource1 objectId(String objectId){
        return new ObjectIdResource1(this.executor, this.version, objectId);
    }


    public UsersQuery get(GetUsersFormBody payload){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", UsersQuery.class, GetUsersFormBody.class, null, payload, null);
    }


    public UsersPostResponse post(UserPost payload){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "post", UsersPostResponse.class, UserPost.class, null, payload, null);
    }




}