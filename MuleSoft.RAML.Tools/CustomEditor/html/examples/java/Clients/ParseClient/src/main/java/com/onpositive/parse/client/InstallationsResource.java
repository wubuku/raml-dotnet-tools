package com.onpositive.parse.client;

import com.onpositive.parse.client.executor.JavaExecutor;
import com.onpositive.parse.model.CollectionPostResponse;
import com.onpositive.parse.model.GetInstallationsFormBody;
import com.onpositive.parse.model.InstallationPost;
import com.onpositive.parse.model.InstallationsQuery;

public class InstallationsResource {

    InstallationsResource(JavaExecutor executor, String version){

         this.executor = executor;

         this.version = version;

         this.uri = this.uri.replace("{version}",this.version);

    }


    private String uri = "https://api.parse.com/{version}/installations";
private String[] relativeUriSegments = new String[]{"/installations"};


    private final JavaExecutor executor;


    private final String version;




    public ObjectIdResource3 objectId(String objectId){
        return new ObjectIdResource3(this.executor, this.version, objectId);
    }


    public InstallationsQuery get(GetInstallationsFormBody payload){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", InstallationsQuery.class, GetInstallationsFormBody.class, null, payload, null);
    }


    public CollectionPostResponse post(InstallationPost payload){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "post", CollectionPostResponse.class, InstallationPost.class, null, payload, null);
    }




}