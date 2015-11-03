package com.onpositive.parse.client.executor;

import java.io.BufferedInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.UnsupportedEncodingException;

public class ApiProvider {
	
	public Object api(){
		InputStream is = this.getClass().getClassLoader().getResourceAsStream("apiEncoded.json");
		BufferedInputStream bis = new BufferedInputStream(is);
		ByteArrayOutputStream baos =  new ByteArrayOutputStream();
		byte[] buf = new byte[1024];
		int l;
		try {
			while((l=bis.read(buf))>=0){
				baos.write(buf, 0, l);
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
		finally{
			try {
				bis.close();
			} catch (IOException e) {
				e.printStackTrace();
			}
		}
		byte[] bytes = baos.toByteArray();
		String result = null;
		try {
			result = new String(bytes,"UTF-8");
		} catch (UnsupportedEncodingException e) {
			e.printStackTrace();
		}
		return result;
	}
	
	

}
