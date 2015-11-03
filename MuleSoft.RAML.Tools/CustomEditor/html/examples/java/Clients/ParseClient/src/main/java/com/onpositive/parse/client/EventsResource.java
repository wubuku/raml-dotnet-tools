package com.onpositive.parse.client;

import com.onpositive.parse.client.executor.JavaExecutor;

public class EventsResource {

    EventsResource(JavaExecutor executor, String version){

         this.executor = executor;

         this.version = version;

         this.uri = this.uri.replace("{version}",this.version);

         this.AppOpened = new AppOpenedResource(this.executor, this.version);

    }


    private String uri = "https://api.parse.com/{version}/events";
private String[] relativeUriSegments = new String[]{"/events"};


    private final JavaExecutor executor;


    private final String version;


    final public AppOpenedResource AppOpened;




    public EventNameResource eventName(String eventName){
        return new EventNameResource(this.executor, this.version, eventName);
    }




}