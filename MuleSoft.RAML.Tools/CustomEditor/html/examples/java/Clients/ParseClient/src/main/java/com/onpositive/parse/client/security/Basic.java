package com.onpositive.parse.client.security;

import com.onpositive.parse.client.executor.JavaExecutor;
import com.onpositive.parse.client.security.core.JavaApiSecurity;

public class Basic extends JavaApiSecurity{

    public Basic(JavaExecutor executor){
        super(executor);
    }


    private String schemaName = "basic";


    public void setUsername(String paramValue){
        super.setValue("username", paramValue, this.schemaName);
    }

    public void setPassword(String paramValue){
        super.setValue("password", paramValue, this.schemaName);
    }

}