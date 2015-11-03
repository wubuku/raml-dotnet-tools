package com.onpositive.parse.executor.wrapper;

import java.io.StringReader;
import java.io.StringWriter;
import java.lang.reflect.Array;
import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Comparator;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.script.Bindings;
import javax.script.ScriptEngine;
import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Marshaller;
import javax.xml.bind.PropertyException;
import javax.xml.bind.Unmarshaller;
import javax.xml.transform.stream.StreamSource;

import org.eclipse.persistence.jaxb.MarshallerProperties;
import org.eclipse.persistence.jaxb.UnmarshallerProperties;

import com.mulesoft.raml.webpack.holders.AbstractJSWrapper;
import com.onpositive.parse.client.executor.UnionType;



@SuppressWarnings("restriction")
public class ExecutorWrapper extends AbstractJSWrapper{

	public ExecutorWrapper(ScriptEngine engine) {
		super(engine);
	}

	public Object executeMethod;
	
	public Object assignSecurityParameterMethod;

	public void setExecuteMethod(Object executeMethod) {
		this.executeMethod = executeMethod;
	}
	
	public void setAssignSecurityParameterMethod(Object assignSecurityParameterMethod){
	    this.assignSecurityParameterMethod = assignSecurityParameterMethod;
	}
	
	public <R,P,O> R invoke(String uri, String[] relativeUriSegments, String method, Class<R> rangeClass, Class<P> payloadClass, Class<O> optionsClass, Object... args){
		
		try {
			JAXBContext ctx = buildJaxbContext(rangeClass, payloadClass, optionsClass);
			String bodyString = buildBodyString(payloadClass, optionsClass, ctx, args);
			String uriSegmentsString = this.toArrayString(relativeUriSegments);
			
			Bindings bindings = this.getBindings();
			bindings.put("environment", this);
			bindings.put("uri", uri);
			bindings.put("method", method);
			bindings.put("bodyString", bodyString);
			String script = "environment.executeMethod(uri,method," + uriSegmentsString +",bodyString);";
			
			String resultStr = this.eval(script, bindings).toString();
			R result;
			if(UnionType.class.isAssignableFrom(rangeClass)){
				result = unmarshalUnionTypeInstance(rangeClass, ctx, resultStr);
			}
			else{
				result = unmarshalClassInstance(rangeClass, ctx, resultStr);
			}
			return result;
		}
		catch(Exception e){
			e.printStackTrace();
		}
		
		return null; 
	}

	private <R>R unmarshalUnionTypeInstance(Class<R> rangeClass, JAXBContext ctx, String resultStr) {

		try {
			HashMap<String,Object> fieldValues = new HashMap<String, Object>();
			List<Field> fields = Arrays.asList(rangeClass.getFields());
			for(Field f : fields){
				try {
					Object fValue = unmarshalClassInstance(f.getType(), ctx, resultStr);
					fieldValues.put(f.getName(), fValue);
				} catch (PropertyException e) {
					e.printStackTrace();
				} catch (JAXBException e) {
					e.printStackTrace();
				}
			}
			final HashMap<String,Double> fieldRanks = new HashMap<String, Double>();
			for(Map.Entry<String,Object> e : fieldValues.entrySet()){
				String key = e.getKey();
				Object value = e.getValue();
				Field[] componentFields = value.getClass().getFields();
				int count = 0;
				for(Field f : componentFields){
					count += f.get(value) != null ? 1 : 0 ;
				}
				fieldRanks.put(key, (0.0 + count)/componentFields.length);
			}
			Collections.sort(fields,new Comparator<Field>() {

				public int compare(Field o1, Field o2) {
					double doubleValue = fieldRanks.get(o1.getName())-fieldRanks.get(o2.getName());
					int result = 0;
					if(Math.abs(doubleValue)>Double.MIN_VALUE*2){
						result = doubleValue > 0 ? -1 : 1;
					}
					return result;
				}
			});			
			R result = rangeClass.newInstance();
			Field actualField = fields.get(0);
			actualField.set(result, fieldValues.get(actualField.getName()));
			return result;
		} catch (InstantiationException e) {
			e.printStackTrace();
		} catch (IllegalAccessException e) {
			e.printStackTrace();
		}
		return null;
	}

	private <R> R unmarshalClassInstance(Class<R> rangeClass, JAXBContext ctx, String resultStr)
			throws JAXBException, PropertyException {
		Unmarshaller um = ctx.createUnmarshaller();
		um.setProperty(UnmarshallerProperties.MEDIA_TYPE, "application/json");
		um.setProperty(UnmarshallerProperties.JSON_INCLUDE_ROOT, false);
		R result = um.unmarshal(new StreamSource(new StringReader(resultStr)),rangeClass).getValue();
		return result;
	}
	
	private String toArrayString(String[] relativeUriSegments) {
		StringBuilder bld =new StringBuilder("[");
		for(String s : relativeUriSegments){
			bld.append("\"").append(s).append("\",");
		}
		if(relativeUriSegments.length>0){
			bld.setCharAt(bld.length()-1, ']');
		}
		else{
			bld.append("]");
		}
		return bld.toString();
	}

	public void setSecurityParameter(String paramName, String paramValue, String schemaName){
		
		Bindings bindings = this.getBindings();
		bindings.put("environment", this);
		bindings.put("paramName", paramName);
		bindings.put("paramValue", paramValue);
		bindings.put("schemaName", schemaName);
		
		String script = "environment.assignSecurityParameterMethod(paramName,paramValue,schemaName)";
		
		this.eval(script, bindings);
		//System.out.println(string);
	}

	private <P,O> String buildBodyString(Class<P> payloadClass, Class<O> optionsClass, JAXBContext ctx, Object... args)
			throws JAXBException {
		
		Marshaller m = ctx.createMarshaller();
		m.setProperty(MarshallerProperties.MEDIA_TYPE, "application/json");
		m.setProperty(Marshaller.JAXB_FORMATTED_OUTPUT, true);
		
		ArrayList<String> bodyFields = new ArrayList<String>();
		if (args != null && args.length > 0) {
			Object payload = null;
			ArrayList<Object> options = new ArrayList<Object>();
			for(Object arg : args){
				if(arg==null){
					continue;
				}
				ArrayList<Object> buf = new ArrayList<Object>();
				if(arg.getClass().isArray()){
					int l = Array.getLength(arg);
					for(int i = 0 ; i < l ; i++){
						buf.add(Array.get(arg, i));
					}
				}
				else{
					buf.add(arg);
				}
				if(buf.size()>0){
					if(payloadClass!=null && payloadClass.isInstance(buf.get(0))){
						payload = arg;
					}
					else if(optionsClass!=null && optionsClass.isInstance(buf.get(0))){
						options.addAll(buf);
					}
				}
			}				
			
			if (payload!=null) {
				StringWriter sw = new StringWriter();
				m.marshal(payload,sw);
				bodyFields.add("\"payload\": " + sw.toString());
			}
			if (options.size()>0) {
				StringWriter sw = new StringWriter();
				m.marshal(options.get(0), sw);
				bodyFields.add("\"options\": " + sw.toString());
			}
		}
		
		StringBuilder bld = new StringBuilder("{\n");
		if(!bodyFields.isEmpty()){
			for(String f : bodyFields){
				bld.append(f).append(',');
			}
			bld.deleteCharAt(bld.length()-1);
		}
		bld.append("\n}");
		String bodyString = bld.toString();
		return bodyString;
	}

	private <R, P, O> JAXBContext buildJaxbContext(Class<R> rangeClass, Class<P> payloadClass, Class<O> optionsClass)
			throws JAXBException {
		ArrayList<Class<?>> classes = new ArrayList<Class<?>>();
		if(rangeClass!=null){
			classes.add(rangeClass);
		}
		if(payloadClass!=null){
			classes.add(payloadClass);
		}
		if(optionsClass!=null){
			classes.add(optionsClass);
		}
		JAXBContext ctx = JAXBContext.newInstance(classes.toArray(new Class<?>[classes.size()]));
		return ctx;
	}
	
}
