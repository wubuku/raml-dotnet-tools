package com.onpositive.parse.client;

import com.onpositive.parse.client.executor.JavaExecutor;
import com.onpositive.parse.model.BatchPost;
import com.onpositive.parse.model.BatchResponse;

public class BatchResource {

    BatchResource(JavaExecutor executor, String version){

         this.executor = executor;

         this.version = version;

         this.uri = this.uri.replace("{version}",this.version);

    }


    private String uri = "https://api.parse.com/{version}/batch";
private String[] relativeUriSegments = new String[]{"/batch"};


    private final JavaExecutor executor;


    private final String version;




    public BatchResponse post(BatchPost payload){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "post", BatchResponse.class, BatchPost.class, null, payload, null);
    }




}