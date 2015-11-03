package com.onpositive.ramlscript.amazon.client.security;

import com.onpositive.ramlscript.amazon.client.executor.JavaExecutor;
import com.onpositive.ramlscript.amazon.client.security.core.JavaApiSecurity;

public class ApiSecurity extends JavaApiSecurity{

    public ApiSecurity(JavaExecutor executor){
        super(executor);
    }





    public Sign getSign(){
        return new Sign(this.executor);
    }

    public void setParamValue(String paramName, String paramValue){
        super.setValue(paramName, paramValue, null);
    }

}