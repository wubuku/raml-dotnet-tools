package com.onpositive.parse.client;

import com.onpositive.parse.client.executor.JavaExecutor;
import com.onpositive.parse.model.AnalyticsAppOpened;

public class AppOpenedResource {

    AppOpenedResource(JavaExecutor executor, String version){

         this.executor = executor;

         this.version = version;

         this.uri = this.uri.replace("{version}",this.version);

    }


    private String uri = "https://api.parse.com/{version}/events/AppOpened";
private String[] relativeUriSegments = new String[]{"/events", "/AppOpened"};


    private final JavaExecutor executor;


    private final String version;




    public Object post(AnalyticsAppOpened payload){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "post", Object.class, AnalyticsAppOpened.class, null, payload, null);
    }




}