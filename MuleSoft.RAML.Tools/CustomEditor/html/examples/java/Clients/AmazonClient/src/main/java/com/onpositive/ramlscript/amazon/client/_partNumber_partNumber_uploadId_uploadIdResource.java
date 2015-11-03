package com.onpositive.ramlscript.amazon.client;

import com.onpositive.ramlscript.amazon.client.executor.JavaExecutor;
import com.onpositive.ramlscript.amazon.model.Error;
import com.onpositive.ramlscript.amazon.options._partNumber_partNumber_uploadId_uploadIdResourcePutOptions;

public class _partNumber_partNumber_uploadId_uploadIdResource {

    _partNumber_partNumber_uploadId_uploadIdResource(JavaExecutor executor, String bucketName, String region, String objectName, String partNumber, String uploadId){

         this.executor = executor;

         this.bucketName = bucketName;

         this.region = region;

         this.objectName = objectName;

         this.partNumber = partNumber;

         this.uploadId = uploadId;

         this.uri = this.uri.replace("{bucketName}",this.bucketName);

         this.uri = this.uri.replace("{region}",this.region);

         this.uri = this.uri.replace("{objectName}",this.objectName);

         this.uri = this.uri.replace("{partNumber}",this.partNumber);

         this.uri = this.uri.replace("{uploadId}",this.uploadId);

    }


    private String uri = "https://{bucketName}.{region}.amazonaws.com/{objectName}/?partNumber={partNumber}&uploadId={uploadId}";
private String[] relativeUriSegments = new String[]{"/{objectName}", "/?partNumber={partNumber}&uploadId={uploadId}"};


    private final JavaExecutor executor;


    private final String bucketName;


    private final String region;


    private final String objectName;


    private final String partNumber;


    private final String uploadId;




    public Error put(_partNumber_partNumber_uploadId_uploadIdResourcePutOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "put", Error.class, null, _partNumber_partNumber_uploadId_uploadIdResourcePutOptions.class, null, options);
    }




}