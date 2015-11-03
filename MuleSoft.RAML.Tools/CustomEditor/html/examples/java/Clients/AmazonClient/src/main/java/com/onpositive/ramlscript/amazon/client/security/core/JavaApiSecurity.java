package com.onpositive.ramlscript.amazon.client.security.core;

import com.onpositive.ramlscript.amazon.client.executor.JavaExecutor;

public class JavaApiSecurity {

	public JavaApiSecurity(JavaExecutor executor) {
		this.executor = executor;
	}
	
	protected JavaExecutor executor;

	protected void setValue(String paramName, String paramValue, String schemaName) {
		this.executor.setSecurityParameter(paramName, paramValue, schemaName);
	}

}
