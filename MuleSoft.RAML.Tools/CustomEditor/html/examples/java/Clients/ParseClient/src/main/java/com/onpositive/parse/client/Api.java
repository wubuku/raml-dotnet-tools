package com.onpositive.parse.client;

import com.onpositive.parse.client.executor.JavaExecutor;
import com.onpositive.parse.client.security.ApiSecurity;

public class Api {

    public Api(){

         this.executor = new JavaExecutor();

         this.version = "1";

         this.uri = this.uri.replace("{version}",this.version);

         this.classes = new ClassesResource(this.executor, this.version);

         this.batch = new BatchResource(this.executor, this.version);

         this.users = new UsersResource(this.executor, this.version);

         this.login = new LoginResource(this.executor, this.version);

         this.requestPasswordReset = new RequestPasswordResetResource(this.executor, this.version);

         this.roles = new RolesResource(this.executor, this.version);

         this.files = new FilesResource(this.executor, this.version);

         this.events = new EventsResource(this.executor, this.version);

         this.push = new PushResource(this.executor, this.version);

         this.installations = new InstallationsResource(this.executor, this.version);

    }


    private String uri = "https://api.parse.com/{version}";
private String[] relativeUriSegments = new String[]{};


    private final JavaExecutor executor;


    private final String version;


    final public ClassesResource classes;


    final public BatchResource batch;


    final public UsersResource users;


    final public LoginResource login;


    final public RequestPasswordResetResource requestPasswordReset;


    final public RolesResource roles;


    final public FilesResource files;


    final public EventsResource events;


    final public PushResource push;


    final public InstallationsResource installations;




    public Functions_functionNameResource functions_functionName(String functionName){
        return new Functions_functionNameResource(this.executor, this.version, functionName);
    }





    public ApiSecurity security(){
        return new ApiSecurity(this.executor);
    }

}