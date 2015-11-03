package com.onpositive.parse.client;

import com.onpositive.parse.client.executor.JavaExecutor;

public class ClassesResource {

    ClassesResource(JavaExecutor executor, String version){

         this.executor = executor;

         this.version = version;

         this.uri = this.uri.replace("{version}",this.version);

    }


    private String uri = "https://api.parse.com/{version}/classes";
private String[] relativeUriSegments = new String[]{"/classes"};


    private final JavaExecutor executor;


    private final String version;




    public ClassNameResource className(String className){
        return new ClassNameResource(this.executor, this.version, className);
    }




}