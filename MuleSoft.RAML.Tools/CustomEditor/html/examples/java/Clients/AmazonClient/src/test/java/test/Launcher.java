package test;

import com.onpositive.ramlscript.amazon.client.Api;
import com.onpositive.ramlscript.amazon.client.security.Sign;
import com.onpositive.ramlscript.amazon.model.Bucket_conf_website;
import com.onpositive.ramlscript.amazon.model.Bucket_conf_websiteWebsiteConfiguration;
import com.onpositive.ramlscript.amazon.model.Bucket_conf_websiteWebsiteConfigurationErrorDocument;
import com.onpositive.ramlscript.amazon.model.Bucket_conf_websiteWebsiteConfigurationIndexDocument;
import com.onpositive.ramlscript.amazon.model.Error;
import com.onpositive.ramlscript.amazon.model.union.Bucket_object_versionsErrorUnion;

public class Launcher {

	private static final String AMAZON_ACCESS_KEY = "Your Access Key here";
	private static final String AMAZON_SECRET_KEY = "Your Secret Key here";

	public static void main(String[] args) {
		
		Api amazon = new Api("TestNotebookBucket","s3");
		
		Sign signSchema = amazon.security().getSign();
		signSchema.setACCESS_KEY(AMAZON_ACCESS_KEY);
		signSchema.setSECRET_KEY(AMAZON_SECRET_KEY);
		
		Error createBucket = amazon.put(null);
		
		System.out.println(createBucket.toString());
		
		
		Bucket_conf_website ws = new Bucket_conf_website();
		ws.WebsiteConfiguration = new Bucket_conf_websiteWebsiteConfiguration();
		ws.WebsiteConfiguration.ErrorDocument = new Bucket_conf_websiteWebsiteConfigurationErrorDocument();
		ws.WebsiteConfiguration.ErrorDocument.Key = "404.html";
		ws.WebsiteConfiguration.IndexDocument = new Bucket_conf_websiteWebsiteConfigurationIndexDocument();
		ws.WebsiteConfiguration.IndexDocument.Suffix = "index.html";
		
		Error err = amazon._website.put(ws);
		System.out.println(err.toString());
		
		Bucket_object_versionsErrorUnion versions = amazon._versions.get(null);
		System.out.println(versions.toString());
		
		
	}

}
