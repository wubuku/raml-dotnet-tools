package com.onpositive.ramlscript.amazon.client;

import com.onpositive.ramlscript.amazon.client.executor.JavaExecutor;
import com.onpositive.ramlscript.amazon.model.Error;
import com.onpositive.ramlscript.amazon.options.ObjectNameResourceDeleteOptions;
import com.onpositive.ramlscript.amazon.options.ObjectNameResourceGetOptions;
import com.onpositive.ramlscript.amazon.options.ObjectNameResourceHeadOptions;
import com.onpositive.ramlscript.amazon.options.ObjectNameResourcePutOptions;

public class ObjectNameResource {

    ObjectNameResource(JavaExecutor executor, String bucketName, String region, String objectName){

         this.executor = executor;

         this.bucketName = bucketName;

         this.region = region;

         this.objectName = objectName;

         this.uri = this.uri.replace("{bucketName}",this.bucketName);

         this.uri = this.uri.replace("{region}",this.region);

         this.uri = this.uri.replace("{objectName}",this.objectName);

         this._acl = new _aclResource1(this.executor, this.bucketName, this.region, this.objectName);

         this._torrent = new _torrentResource(this.executor, this.bucketName, this.region, this.objectName);

         this._restore = new _restoreResource(this.executor, this.bucketName, this.region, this.objectName);

         this._uploads = new _uploadsResource1(this.executor, this.bucketName, this.region, this.objectName);

    }


    private String uri = "https://{bucketName}.{region}.amazonaws.com/{objectName}";
private String[] relativeUriSegments = new String[]{"/{objectName}"};


    private final JavaExecutor executor;


    private final String bucketName;


    private final String region;


    private final String objectName;


    final public _aclResource1 _acl;


    final public _torrentResource _torrent;


    final public _restoreResource _restore;


    final public _uploadsResource1 _uploads;




    public _partNumber_partNumber_uploadId_uploadIdResource _partNumber_partNumber_uploadId_uploadId(String partNumber, String uploadId){
        return new _partNumber_partNumber_uploadId_uploadIdResource(this.executor, this.bucketName, this.region, this.objectName, partNumber, uploadId);
    }


    public _uploadId_UploadIdResource _uploadId_UploadId(String UploadId){
        return new _uploadId_UploadIdResource(this.executor, this.bucketName, this.region, this.objectName, UploadId);
    }


    public Error get(ObjectNameResourceGetOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", Error.class, null, ObjectNameResourceGetOptions.class, null, options);
    }


    public Error put(ObjectNameResourcePutOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "put", Error.class, null, ObjectNameResourcePutOptions.class, null, options);
    }


    public Error delete(ObjectNameResourceDeleteOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "delete", Error.class, null, ObjectNameResourceDeleteOptions.class, null, options);
    }


    public Error head(ObjectNameResourceHeadOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "head", Error.class, null, ObjectNameResourceHeadOptions.class, null, options);
    }




}