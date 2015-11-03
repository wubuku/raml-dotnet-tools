package com.onpositive.parse.client;

import com.onpositive.parse.client.executor.JavaExecutor;
import com.onpositive.parse.model.FileResponse;

public class FileNameResource {

    FileNameResource(JavaExecutor executor, String version, String fileName){

         this.executor = executor;

         this.version = version;

         this.fileName = fileName;

         this.uri = this.uri.replace("{version}",this.version);

         this.uri = this.uri.replace("{fileName}",this.fileName);

    }


    private String uri = "https://api.parse.com/{version}/files/{fileName}";
private String[] relativeUriSegments = new String[]{"/files", "/{fileName}"};


    private final JavaExecutor executor;


    private final String version;


    private final String fileName;




    public FileResponse post(){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "post", FileResponse.class, null, null, null, null);
    }




}