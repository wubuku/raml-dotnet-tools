package com.onpositive.parse.client.executor;

import java.io.InputStreamReader;
import java.io.Reader;

import javax.script.Bindings;
import javax.script.Compilable;
import javax.script.CompiledScript;
import javax.script.ScriptEngine;
import javax.script.ScriptEngineManager;
import javax.script.SimpleBindings;

import com.onpositive.parse.executor.wrapper.ExecutorWrapper;

import com.mulesoft.raml.webpack.holders.JSHolder;
import com.mulesoft.raml.webpack.holders.JSChildProcess;
import com.mulesoft.raml.webpack.holders.JSConsole;
import com.mulesoft.raml.webpack.holders.JSFileSystem;
import com.mulesoft.raml.webpack.holders.JSHttp;
import com.mulesoft.raml.webpack.holders.JSPlatformExecution;
import com.mulesoft.raml.webpack.holders.JSReportManager;
import com.mulesoft.raml.webpack.holders.JSSchema;
import com.mulesoft.raml.webpack.holders.JSZ;

@SuppressWarnings("restriction")
public class JavaExecutor{

	private String rootPath = "C:/workspaces/JavaRamlScript/java-ramlscript-amazon/src/main/resources";

	public JavaExecutor(){

		Bindings bindings = new SimpleBindings();

		bindings.put("endpoints", new Object());
		bindings.put("z", new JSZ(engine));
		bindings.put("schema", new JSSchema(engine));
		bindings.put("fs", JSFileSystem.getInstance(engine,this.rootPath));
		bindings.put("executionReportManager", new JSReportManager(engine));
		bindings.put("http", new JSHttp(engine, false));
		bindings.put("https", new JSHttp(engine, true));
		bindings.put("platformExecution", new JSPlatformExecution(engine));
		bindings.put("child_process", new JSChildProcess(engine,this.rootPath));
		bindings.put("console", JSConsole.getInstance(engine));
		bindings.put("executorWrapper", this.exec);
		bindings.put("_spawn_sync", new JSChildProcess(engine,this.rootPath));
		bindings.put("typescript", new JSHolder(engine));
		bindings.put("_try_thread_sleep", new JSHolder(engine));
		bindings.put("apiProvider", new ApiProvider());

		Reader scriptSrc = new InputStreamReader(this.getClass().getClassLoader().getResourceAsStream("bundle.js"));
		try {
			CompiledScript script = ((Compilable) engine).compile(scriptSrc);

			bindings.put("script", script);

			script.eval(bindings);

		} catch (Throwable t) {
			t.printStackTrace();
		}

	}

	private ScriptEngine engine = new ScriptEngineManager().getEngineByName("nashorn");

	public ExecutorWrapper exec = new ExecutorWrapper(this.engine);

	public <R,P,O> R invoke(String uri, String[] relativeUriSegments, String method, Class<R> rangeClass, Class<P> payloadClass, Class<O> optionsClass, Object... args){
        return (R) this.exec.invoke(uri, relativeUriSegments, method, rangeClass, payloadClass, optionsClass, args);
    }

    public void setSecurityParameter(String paramName, String paramValue, String schemaName){
        this.exec.setSecurityParameter(paramName, paramValue, schemaName);
    }

}