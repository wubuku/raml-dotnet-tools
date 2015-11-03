package com.onpositive.parse.client;

import com.onpositive.parse.client.executor.JavaExecutor;
import com.onpositive.parse.model.Analytics;

public class EventNameResource {

    EventNameResource(JavaExecutor executor, String version, String eventName){

         this.executor = executor;

         this.version = version;

         this.eventName = eventName;

         this.uri = this.uri.replace("{version}",this.version);

         this.uri = this.uri.replace("{eventName}",this.eventName);

    }


    private String uri = "https://api.parse.com/{version}/events/{eventName}";
private String[] relativeUriSegments = new String[]{"/events", "/{eventName}"};


    private final JavaExecutor executor;


    private final String version;


    private final String eventName;




    public Object post(Analytics payload){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "post", Object.class, Analytics.class, null, payload, null);
    }




}