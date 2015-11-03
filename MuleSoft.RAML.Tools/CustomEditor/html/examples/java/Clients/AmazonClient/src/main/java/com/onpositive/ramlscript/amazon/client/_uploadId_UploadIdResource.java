package com.onpositive.ramlscript.amazon.client;

import com.onpositive.ramlscript.amazon.client.executor.JavaExecutor;
import com.onpositive.ramlscript.amazon.model.Complete_multipart_upload;
import com.onpositive.ramlscript.amazon.model.Error;
import com.onpositive.ramlscript.amazon.model.union.ErrorPost_uploadid__uploadid_XmlResponseUnion;
import com.onpositive.ramlscript.amazon.model.union.UpladPartsErrorUnion;
import com.onpositive.ramlscript.amazon.options._uploadId_UploadIdResourceDeleteOptions;
import com.onpositive.ramlscript.amazon.options._uploadId_UploadIdResourceGetOptions;
import com.onpositive.ramlscript.amazon.options._uploadId_UploadIdResourcePostOptions;

public class _uploadId_UploadIdResource {

    _uploadId_UploadIdResource(JavaExecutor executor, String bucketName, String region, String objectName, String UploadId){

         this.executor = executor;

         this.bucketName = bucketName;

         this.region = region;

         this.objectName = objectName;

         this.UploadId = UploadId;

         this.uri = this.uri.replace("{bucketName}",this.bucketName);

         this.uri = this.uri.replace("{region}",this.region);

         this.uri = this.uri.replace("{objectName}",this.objectName);

         this.uri = this.uri.replace("{UploadId}",this.UploadId);

    }


    private String uri = "https://{bucketName}.{region}.amazonaws.com/{objectName}/?uploadId={UploadId}";
private String[] relativeUriSegments = new String[]{"/{objectName}", "/?uploadId={UploadId}"};


    private final JavaExecutor executor;


    private final String bucketName;


    private final String region;


    private final String objectName;


    private final String UploadId;




    public UpladPartsErrorUnion get(_uploadId_UploadIdResourceGetOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", UpladPartsErrorUnion.class, null, _uploadId_UploadIdResourceGetOptions.class, null, options);
    }


    public ErrorPost_uploadid__uploadid_XmlResponseUnion post(Complete_multipart_upload payload, _uploadId_UploadIdResourcePostOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "post", ErrorPost_uploadid__uploadid_XmlResponseUnion.class, Complete_multipart_upload.class, _uploadId_UploadIdResourcePostOptions.class, payload, options);
    }


    public Error delete(_uploadId_UploadIdResourceDeleteOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "delete", Error.class, null, _uploadId_UploadIdResourceDeleteOptions.class, null, options);
    }




}