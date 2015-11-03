package com.onpositive.parse.client.security;

import com.onpositive.parse.client.executor.JavaExecutor;
import com.onpositive.parse.client.security.core.JavaApiSecurity;

public class ApiSecurity extends JavaApiSecurity{

    public ApiSecurity(JavaExecutor executor){
        super(executor);
    }





    public Basic getBasic(){
        return new Basic(this.executor);
    }

    public CommonSchema getCommonSchema(){
        return new CommonSchema(this.executor);
    }

    public SessionTokenSchema getSessionTokenSchema(){
        return new SessionTokenSchema(this.executor);
    }

    public MasterSchema getMasterSchema(){
        return new MasterSchema(this.executor);
    }

    public void setParamValue(String paramName, String paramValue){
        super.setValue(paramName, paramValue, null);
    }

}