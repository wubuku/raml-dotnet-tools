package com.onpositive.ramlscript.amazon.client.security;

import com.onpositive.ramlscript.amazon.client.executor.JavaExecutor;
import com.onpositive.ramlscript.amazon.client.security.core.JavaApiSecurity;

public class Sign extends JavaApiSecurity{

    public Sign(JavaExecutor executor){
        super(executor);
    }


    private String schemaName = "sign";


    public void setACCESS_KEY(String paramValue){
        super.setValue("ACCESS_KEY", paramValue, this.schemaName);
    }

    public void setSECRET_KEY(String paramValue){
        super.setValue("SECRET_KEY", paramValue, this.schemaName);
    }

}