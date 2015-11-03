package com.onpositive.parse.client.security;

import com.onpositive.parse.client.executor.JavaExecutor;
import com.onpositive.parse.client.security.core.JavaApiSecurity;

public class MasterSchema extends JavaApiSecurity{

    public MasterSchema(JavaExecutor executor){
        super(executor);
    }


    private String schemaName = "masterSchema";


    public void setX_Parse_Application_Id(String paramValue){
        super.setValue("X-Parse-Application-Id", paramValue, this.schemaName);
    }

    public void setX_Parse_Master_Key(String paramValue){
        super.setValue("X-Parse-Master-Key", paramValue, this.schemaName);
    }

}