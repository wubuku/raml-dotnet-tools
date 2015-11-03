package com.onpositive.ramlscript.amazon.client;

import com.onpositive.ramlscript.amazon.client.executor.JavaExecutor;
import com.onpositive.ramlscript.amazon.client.security.ApiSecurity;
import com.onpositive.ramlscript.amazon.model.Bucket;
import com.onpositive.ramlscript.amazon.model.Error;
import com.onpositive.ramlscript.amazon.model.PostFormBody;
import com.onpositive.ramlscript.amazon.model.union.Bucket_list_objectsErrorUnion;
import com.onpositive.ramlscript.amazon.options.ApiDeleteOptions;
import com.onpositive.ramlscript.amazon.options.ApiGetOptions;
import com.onpositive.ramlscript.amazon.options.ApiHeadOptions;
import com.onpositive.ramlscript.amazon.options.ApiPostOptions;
import com.onpositive.ramlscript.amazon.options.ApiPutOptions;

public class Api {

    public Api(String bucketName, String region){

         this.executor = new JavaExecutor();

         this.bucketName = bucketName;

         this.region = region;

         this.uri = this.uri.replace("{bucketName}",this.bucketName);

         this.uri = this.uri.replace("{region}",this.region);

         this._cors = new _corsResource(this.executor, this.bucketName, this.region);

         this._lifecycle = new _lifecycleResource(this.executor, this.bucketName, this.region);

         this._policy = new _policyResource(this.executor, this.bucketName, this.region);

         this._tagging = new _taggingResource(this.executor, this.bucketName, this.region);

         this._website = new _websiteResource(this.executor, this.bucketName, this.region);

         this._versions = new _versionsResource(this.executor, this.bucketName, this.region);

         this._acl = new _aclResource(this.executor, this.bucketName, this.region);

         this._versioning = new _versioningResource(this.executor, this.bucketName, this.region);

         this._delete = new _deleteResource(this.executor, this.bucketName, this.region);

         this._location = new _locationResource(this.executor, this.bucketName, this.region);

         this._logging = new _loggingResource(this.executor, this.bucketName, this.region);

         this._notification = new _notificationResource(this.executor, this.bucketName, this.region);

         this._requestPayment = new _requestPaymentResource(this.executor, this.bucketName, this.region);

         this._uploads = new _uploadsResource(this.executor, this.bucketName, this.region);

    }


    private String uri = "https://{bucketName}.{region}.amazonaws.com/";
private String[] relativeUriSegments = new String[]{};


    private final JavaExecutor executor;


    private final String bucketName;


    private final String region;


    final public _corsResource _cors;


    final public _lifecycleResource _lifecycle;


    final public _policyResource _policy;


    final public _taggingResource _tagging;


    final public _websiteResource _website;


    final public _versionsResource _versions;


    final public _aclResource _acl;


    final public _versioningResource _versioning;


    final public _deleteResource _delete;


    final public _locationResource _location;


    final public _loggingResource _logging;


    final public _notificationResource _notification;


    final public _requestPaymentResource _requestPayment;


    final public _uploadsResource _uploads;




    public ObjectNameResource objectName(String objectName){
        return new ObjectNameResource(this.executor, this.bucketName, this.region, objectName);
    }


    public Bucket_list_objectsErrorUnion get(ApiGetOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "get", Bucket_list_objectsErrorUnion.class, null, ApiGetOptions.class, null, options);
    }


    public Error post(PostFormBody payload, ApiPostOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "post", Error.class, PostFormBody.class, ApiPostOptions.class, payload, options);
    }


    public Error put(Bucket payload, ApiPutOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "put", Error.class, Bucket.class, ApiPutOptions.class, payload, options);
    }


    public Error delete(ApiDeleteOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "delete", Error.class, null, ApiDeleteOptions.class, null, options);
    }


    public Error head(ApiHeadOptions... options){
        return this.executor.invoke(this.uri, this.relativeUriSegments, "head", Error.class, null, ApiHeadOptions.class, null, options);
    }





    public ApiSecurity security(){
        return new ApiSecurity(this.executor);
    }

}