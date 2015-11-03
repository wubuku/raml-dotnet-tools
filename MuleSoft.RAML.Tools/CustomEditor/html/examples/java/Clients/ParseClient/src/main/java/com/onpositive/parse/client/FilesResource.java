package com.onpositive.parse.client;

import com.onpositive.parse.client.executor.JavaExecutor;

public class FilesResource {

    FilesResource(JavaExecutor executor, String version){

         this.executor = executor;

         this.version = version;

         this.uri = this.uri.replace("{version}",this.version);

    }


    private String uri = "https://api.parse.com/{version}/files";
private String[] relativeUriSegments = new String[]{"/files"};


    private final JavaExecutor executor;


    private final String version;




    public FileNameResource fileName(String fileName){
        return new FileNameResource(this.executor, this.version, fileName);
    }




}