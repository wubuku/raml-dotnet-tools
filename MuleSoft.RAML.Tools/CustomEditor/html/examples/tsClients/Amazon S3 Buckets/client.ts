export function createApi(bucketName:string, region:string):Api {
    return new ApiImpl(bucketName, region);
}
import fs=require("fs")
import path=require("path")
import RamlWrapper=require("./deps/src/raml1/artifacts/raml003parser")
import executor=require("./deps/src/ramlscript/executor")
import env=require("./deps/src/ramlscript/executionEnvironment")
import authManager=require("./deps/src/ramlscript/authenticationManager")
import endpoints=require("./deps/src/ramlscript/endpoints")
import JsonModel=require('./deps/src/raml1/jsyaml/json2lowLevel')
import lowLevel=require("./deps/src/raml1/lowLevelAST")
import highLevel=require("./deps/src/raml1/highLevelAST")
import highLevelImpl=require("./deps/src/raml1/highLevelImpl")
import raml2ts1=require("./deps/src/ramlscript/raml2ts1")
import AnnotationsImpl=require("./deps/src/raml1/annotationsImpl")
import apiProvider=require("./deps/apiProvider")

import securityScript0=require('./script/configurator')


var universe10 = require("./deps/src/raml1/universeProvider")("RAML10");
var apiType = universe10.type("Api")

env.setPath(__dirname);
env.getReportManager().setLogPath(__dirname);


class _corsResourceImpl {


    invoke(path:string, method:string, canonicPath:string[], obj:any) {
        return this._parent.invoke(path, method, canonicPath, obj);
    }


    constructor(private bucketName, private region,
                private _parent:{invoke(path:string, method:string, canonicPath:string[], obj:any):void},
                private canonicPath:string[]) {

    }

    get = (options?:_corsResourceGetOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?cors`, 'get', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*get*/
    }
    put = (payload:Bucket_conf_cors, options?:_corsResourcePutOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?cors`, 'put', this.canonicPath, {
                "payload": payload
                , "options": options
            });
        return res;
        /*d*put*/
    }
    delete = (options?:_corsResourceDeleteOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?cors`, 'delete', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*delete*/
    }

    /* type ending */
}
class _lifecycleResourceImpl {


    invoke(path:string, method:string, canonicPath:string[], obj:any) {
        return this._parent.invoke(path, method, canonicPath, obj);
    }


    constructor(private bucketName, private region,
                private _parent:{invoke(path:string, method:string, canonicPath:string[], obj:any):void},
                private canonicPath:string[]) {

    }

    get = (options?:_lifecycleResourceGetOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?lifecycle`, 'get', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*get*/
    }
    put = (payload:Bucket_lifecycle_put, options:_lifecycleResourcePutOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?lifecycle`, 'put', this.canonicPath, {
                "payload": payload
                , "options": options
            });
        return res;
        /*d*put*/
    }
    delete = (options?:_lifecycleResourceDeleteOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?lifecycle`, 'delete', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*delete*/
    }

    /* type ending */
}
class _policyResourceImpl {


    invoke(path:string, method:string, canonicPath:string[], obj:any) {
        return this._parent.invoke(path, method, canonicPath, obj);
    }


    constructor(private bucketName, private region,
                private _parent:{invoke(path:string, method:string, canonicPath:string[], obj:any):void},
                private canonicPath:string[]) {

    }

    get = (options?:_policyResourceGetOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?policy`, 'get', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*get*/
    }
    put = (payload:Bucket_policy_put_schema, options:_policyResourcePutOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?policy`, 'put', this.canonicPath, {
                "payload": payload
                , "options": options
            });
        return res;
        /*d*put*/
    }
    delete = (options?:_policyResourceDeleteOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?policy`, 'delete', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*delete*/
    }

    /* type ending */
}
class _taggingResourceImpl {


    invoke(path:string, method:string, canonicPath:string[], obj:any) {
        return this._parent.invoke(path, method, canonicPath, obj);
    }


    constructor(private bucketName, private region,
                private _parent:{invoke(path:string, method:string, canonicPath:string[], obj:any):void},
                private canonicPath:string[]) {

    }

    get = (options?:_taggingResourceGetOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?tagging`, 'get', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*get*/
    }
    put = (payload:Bucket_conf_tagging, options?:_taggingResourcePutOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?tagging`, 'put', this.canonicPath, {
                "payload": payload
                , "options": options
            });
        return res;
        /*d*put*/
    }
    delete = (options?:_taggingResourceDeleteOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?tagging`, 'delete', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*delete*/
    }

    /* type ending */
}
class _websiteResourceImpl {


    invoke(path:string, method:string, canonicPath:string[], obj:any) {
        return this._parent.invoke(path, method, canonicPath, obj);
    }


    constructor(private bucketName, private region,
                private _parent:{invoke(path:string, method:string, canonicPath:string[], obj:any):void},
                private canonicPath:string[]) {

    }

    get = (options?:_websiteResourceGetOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?website`, 'get', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*get*/
    }
    put = (payload:Bucket_conf_website, options?:_websiteResourcePutOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?website`, 'put', this.canonicPath, {
                "payload": payload
                , "options": options
            });
        return res;
        /*d*put*/
    }
    delete = (options?:_websiteResourceDeleteOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?website`, 'delete', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*delete*/
    }

    /* type ending */
}
class _versionsResourceImpl {


    invoke(path:string, method:string, canonicPath:string[], obj:any) {
        return this._parent.invoke(path, method, canonicPath, obj);
    }


    constructor(private bucketName, private region,
                private _parent:{invoke(path:string, method:string, canonicPath:string[], obj:any):void},
                private canonicPath:string[]) {

    }

    get = (options?:_versionsResourceGetOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?versions`, 'get', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*get*/
    }

    /* type ending */
}
class _aclResourceImpl {


    invoke(path:string, method:string, canonicPath:string[], obj:any) {
        return this._parent.invoke(path, method, canonicPath, obj);
    }


    constructor(private bucketName, private region,
                private _parent:{invoke(path:string, method:string, canonicPath:string[], obj:any):void},
                private canonicPath:string[]) {

    }

    get = (options?:_aclResourceGetOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?acl`, 'get', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*get*/
    }
    put = (payload:Bucket_grant_acl, options:_aclResourcePutOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?acl`, 'put', this.canonicPath, {
                "payload": payload
                , "options": options
            });
        return res;
        /*d*put*/
    }

    /* type ending */
}
class _versioningResourceImpl {


    invoke(path:string, method:string, canonicPath:string[], obj:any) {
        return this._parent.invoke(path, method, canonicPath, obj);
    }


    constructor(private bucketName, private region,
                private _parent:{invoke(path:string, method:string, canonicPath:string[], obj:any):void},
                private canonicPath:string[]) {

    }

    get = (options?:_versioningResourceGetOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?versioning`, 'get', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*get*/
    }
    put = (payload:Bucket_versioning, options:_versioningResourcePutOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?versioning`, 'put', this.canonicPath, {
                "payload": payload
                , "options": options
            });
        return res;
        /*d*put*/
    }

    /* type ending */
}
class _deleteResourceImpl {


    invoke(path:string, method:string, canonicPath:string[], obj:any) {
        return this._parent.invoke(path, method, canonicPath, obj);
    }


    constructor(private bucketName, private region,
                private _parent:{invoke(path:string, method:string, canonicPath:string[], obj:any):void},
                private canonicPath:string[]) {

    }

    post = (payload:Bucket_delete_multiple, options:_deleteResourcePostOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?delete`, 'post', this.canonicPath, {
                "payload": payload
                , "options": options
            });
        return res;
        /*d*post*/
    }

    /* type ending */
}
class _locationResourceImpl {


    invoke(path:string, method:string, canonicPath:string[], obj:any) {
        return this._parent.invoke(path, method, canonicPath, obj);
    }


    constructor(private bucketName, private region,
                private _parent:{invoke(path:string, method:string, canonicPath:string[], obj:any):void},
                private canonicPath:string[]) {

    }

    get = (options?:_locationResourceGetOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?location`, 'get', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*get*/
    }

    /* type ending */
}
class _loggingResourceImpl {


    invoke(path:string, method:string, canonicPath:string[], obj:any) {
        return this._parent.invoke(path, method, canonicPath, obj);
    }


    constructor(private bucketName, private region,
                private _parent:{invoke(path:string, method:string, canonicPath:string[], obj:any):void},
                private canonicPath:string[]) {

    }

    get = (options?:_loggingResourceGetOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?logging`, 'get', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*get*/
    }
    put = (payload:Bucket_logging, options?:_loggingResourcePutOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?logging`, 'put', this.canonicPath, {
                "payload": payload
                , "options": options
            });
        return res;
        /*d*put*/
    }

    /* type ending */
}
class _notificationResourceImpl {


    invoke(path:string, method:string, canonicPath:string[], obj:any) {
        return this._parent.invoke(path, method, canonicPath, obj);
    }


    constructor(private bucketName, private region,
                private _parent:{invoke(path:string, method:string, canonicPath:string[], obj:any):void},
                private canonicPath:string[]) {

    }

    get = (options?:_notificationResourceGetOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?notification`, 'get', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*get*/
    }
    put = (payload:Notification_configuration, options?:_notificationResourcePutOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?notification`, 'put', this.canonicPath, {
                "payload": payload
                , "options": options
            });
        return res;
        /*d*put*/
    }

    /* type ending */
}
class _requestPaymentResourceImpl {


    invoke(path:string, method:string, canonicPath:string[], obj:any) {
        return this._parent.invoke(path, method, canonicPath, obj);
    }


    constructor(private bucketName, private region,
                private _parent:{invoke(path:string, method:string, canonicPath:string[], obj:any):void},
                private canonicPath:string[]) {

    }

    get = (options?:_requestPaymentResourceGetOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?requestPayment`, 'get', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*get*/
    }
    put = (payload:RequestPayement_configuration, options?:_requestPaymentResourcePutOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?requestPayment`, 'put', this.canonicPath, {
                "payload": payload
                , "options": options
            });
        return res;
        /*d*put*/
    }

    /* type ending */
}
class _uploadsResourceImpl {


    invoke(path:string, method:string, canonicPath:string[], obj:any) {
        return this._parent.invoke(path, method, canonicPath, obj);
    }


    constructor(private bucketName, private region,
                private _parent:{invoke(path:string, method:string, canonicPath:string[], obj:any):void},
                private canonicPath:string[]) {

    }

    get = (options?:_uploadsResourceGetOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/?uploads`, 'get', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*get*/
    }

    /* type ending */
}
class _aclResource1Impl {


    invoke(path:string, method:string, canonicPath:string[], obj:any) {
        return this._parent.invoke(path, method, canonicPath, obj);
    }


    constructor(private bucketName, private region, private objectName,
                private _parent:{invoke(path:string, method:string, canonicPath:string[], obj:any):void},
                private canonicPath:string[]) {

    }

    get = (options?:_aclResource1GetOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/${this.objectName}/?acl`, 'get', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*get*/
    }
    put = (payload:Bucket_grant_acl, options:_aclResource1PutOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/${this.objectName}/?acl`, 'put', this.canonicPath, {
                "payload": payload
                , "options": options
            });
        return res;
        /*d*put*/
    }

    /* type ending */
}
class _torrentResourceImpl {


    invoke(path:string, method:string, canonicPath:string[], obj:any) {
        return this._parent.invoke(path, method, canonicPath, obj);
    }


    constructor(private bucketName, private region, private objectName,
                private _parent:{invoke(path:string, method:string, canonicPath:string[], obj:any):void},
                private canonicPath:string[]) {

    }

    get = (options?:_torrentResourceGetOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/${this.objectName}/?torrent`, 'get', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*get*/
    }

    /* type ending */
}
class _restoreResourceImpl {


    invoke(path:string, method:string, canonicPath:string[], obj:any) {
        return this._parent.invoke(path, method, canonicPath, obj);
    }


    constructor(private bucketName, private region, private objectName,
                private _parent:{invoke(path:string, method:string, canonicPath:string[], obj:any):void},
                private canonicPath:string[]) {

    }

    post = (payload:ObjectRestore, options?:_restoreResourcePostOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/${this.objectName}/?restore`, 'post', this.canonicPath, {
                "payload": payload
                , "options": options
            });
        return res;
        /*d*post*/
    }

    /* type ending */
}
class _uploadsResource1Impl {


    invoke(path:string, method:string, canonicPath:string[], obj:any) {
        return this._parent.invoke(path, method, canonicPath, obj);
    }


    constructor(private bucketName, private region, private objectName,
                private _parent:{invoke(path:string, method:string, canonicPath:string[], obj:any):void},
                private canonicPath:string[]) {

    }

    post = (options?:_uploadsResource1PostOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/${this.objectName}/?uploads`, 'post', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*post*/
    }

    /* type ending */
}
class _partNumber_partNumber_uploadId_uploadIdResourceImpl {


    invoke(path:string, method:string, canonicPath:string[], obj:any) {
        return this._parent.invoke(path, method, canonicPath, obj);
    }


    constructor(private bucketName, private region, private objectName, private partNumber, private uploadId,
                private _parent:{invoke(path:string, method:string, canonicPath:string[], obj:any):void},
                private canonicPath:string[]) {

    }

    put = (options?:_partNumber_partNumber_uploadId_uploadIdResourcePutOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/${this.objectName}/?partNumber=${this.partNumber}&uploadId=${this.uploadId}`, 'put', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*put*/
    }

    /* type ending */
}
class _uploadId_UploadIdResourceImpl {


    invoke(path:string, method:string, canonicPath:string[], obj:any) {
        return this._parent.invoke(path, method, canonicPath, obj);
    }


    constructor(private bucketName, private region, private objectName, private UploadId,
                private _parent:{invoke(path:string, method:string, canonicPath:string[], obj:any):void},
                private canonicPath:string[]) {

    }

    get = (options?:_uploadId_UploadIdResourceGetOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/${this.objectName}/?uploadId=${this.UploadId}`, 'get', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*get*/
    }
    post = (payload:Complete_multipart_upload, options?:_uploadId_UploadIdResourcePostOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/${this.objectName}/?uploadId=${this.UploadId}`, 'post', this.canonicPath, {
                "payload": payload
                , "options": options
            });
        return res;
        /*d*post*/
    }
    delete = (options?:_uploadId_UploadIdResourceDeleteOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/${this.objectName}/?uploadId=${this.UploadId}`, 'delete', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*delete*/
    }

    /* type ending */
}
class ObjectNameResourceImpl {


    invoke(path:string, method:string, canonicPath:string[], obj:any) {
        return this._parent.invoke(path, method, canonicPath, obj);
    }


    constructor(private bucketName, private region, private objectName,
                private _parent:{invoke(path:string, method:string, canonicPath:string[], obj:any):void},
                private canonicPath:string[]) {

    }

    _acl = new _aclResource1Impl(this.bucketName, this.region, this.objectName, this, this.canonicPath.concat('/?acl'))
    _torrent = new _torrentResourceImpl(this.bucketName, this.region, this.objectName, this, this.canonicPath.concat('/?torrent'))
    _restore = new _restoreResourceImpl(this.bucketName, this.region, this.objectName, this, this.canonicPath.concat('/?restore'))
    _uploads = new _uploadsResource1Impl(this.bucketName, this.region, this.objectName, this, this.canonicPath.concat('/?uploads'))
    _partNumber_partNumber_uploadId_uploadId = (partNumber:string, uploadId:string)=> {
        var res = <any>
            new _partNumber_partNumber_uploadId_uploadIdResourceImpl(this.bucketName, this.region, this.objectName, partNumber, uploadId, this, this.canonicPath.concat('/?partNumber={partNumber}&uploadId={uploadId}'))
        return res;
        /*d*_partNumber_partNumber_uploadId_uploadId*/
    }
    _uploadId_UploadId = (UploadId:string)=> {
        var res = <any>
            new _uploadId_UploadIdResourceImpl(this.bucketName, this.region, this.objectName, UploadId, this, this.canonicPath.concat('/?uploadId={UploadId}'))
        return res;
        /*d*_uploadId_UploadId*/
    }
    get = (options?:ObjectNameResourceGetOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/${this.objectName}`, 'get', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*get*/
    }
    put = (options:ObjectNameResourcePutOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/${this.objectName}`, 'put', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*put*/
    }
    delete = (options?:ObjectNameResourceDeleteOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/${this.objectName}`, 'delete', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*delete*/
    }
    head = (options?:ObjectNameResourceHeadOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/${this.objectName}`, 'head', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*head*/
    }

    /* type ending */
}
export interface Api {
    declaration():RamlWrapper.Api;
    securityProvider():authManager.SecurityParametersProvider;
    authenticate(schemaName?:string, options?:any):any;
    log(vName:string, val:any)

    /**
     *  This implementation of the GET operation returns some or all (up to 1000) of the objects in a bucket. You can use the request parameters as selection criteria to return a subset of the objects in a bucket. To use this implementation of the operation, you must have READ access to the bucket. Syntax ------     GET /BucketName/ HTTP/1.1     Host: s3.amazonaws.com     Date: date     Authorization: signatureValue
     * @ramlpath /  get
     **/
    get(options?:ApiGetOptions):Bucket_list_objects | Error


    /**
     *  The POST operation adds an object to a specified bucket using HTML forms. POST is an alternate form of PUT that enables browser-based uploads as a way of putting objects in buckets. Parameters that are passed to PUT via HTTP Headers are instead passed as form fields to POST in the multipart/form-data encoded message body. You must have WRITE access on a bucket to add an object to it. Amazon S3 never stores partial objects: if you receive a successful response, you can be confident the entire object was stored. Amazon S3 is a distributed system. If Amazon S3 receives multiple write requests for the same object simultaneously, all but the las object written will be overwritten. To ensure that data is not corrupted traversing the network, use the Content-MD5 form field. When you use the Content-MD5 form field, Amazon S3 checks the object against the provided MD5 value. If they do not match, Amazon S3 returns an error. Additionally, you can calculate the MD5 while posting an object to Amazon S3 and compare the returned ETag to the calculated MD5 value. The ETag only reflects changes to the contents of an object, not its metadata.
     * @ramlpath /  post
     **/
    post(payload:PostFormBody, options:ApiPostOptions):Error


    /**
     *  This implementation of the PUT operation creates a new bucket. To create a bucket, you must register with Amazon S3 and have       a valid AWS Access Key ID to authenticate requests. Anonymous requests are never allowed to create buckets.       By creating the bucket, you become the bucket owner.              Not every string is an acceptable bucket name. For information on bucket naming restrictions, see Working with Amazon S3 Buckets.              By default, the bucket is created in the US Standard region. You can optionally specify a region in the request body.       You might choose a Region to optimize latency, minimize costs, or address regulatory requirements. For example, if you reside       in Europe, you will probably find it advantageous to create buckets in the EU (Ireland) Region.              **Note**              If you create a bucket in a region other than US Standard, your application must be able to handle 307 redirect.              When creating a bucket using this operation, you can optionally specify the accounts or groups that should be granted specific       permissions on the bucket. There are two ways to grant the appropriate permissions using the request headers.              * Specify a canned ACL using the x-amz-acl request header.       * Specify access permissions explicitly using the x-amz-grant-read, x-amz-grant-write, x-amz-grant-read-acp, x-amz-grant-write-acp,         x-amz-grant-full-control headers. These headers map to the set of permissions Amazon S3 supports in an ACL.              **Note**       You can use either a canned ACL or specify access permissions explicitly. You cannot do both.              Syntax       ------           PUT /BucketName/ HTTP/1.1           Host: s3.amazonaws.com           Content-Length: length           Date: date           Authorization: signatureValue                  <CreateBucketConfiguration xmlns="http://s3.amazonaws.com/doc/2006-03-01/">             <LocationConstraint>BucketRegion</LocationConstraint>           </CreateBucketConfiguration>
     * @ramlpath /  put
     **/
    put(payload:Bucket, options:ApiPutOptions):Error


    /**
     *  This implementation of the DELETE operation deletes the bucket named in the URI. All objects (including all object versions and Delete Markers) in the bucket must be deleted before the bucket itself can be deleted. Syntax ------     DELETE /BucketName/ HTTP/1.1     Host: s3.amazonaws.com     Date: date     Authorization: signatureValue
     * @ramlpath /  delete
     **/
    "delete"(options?:ApiDeleteOptions):Error


    /**
     *  HEAD Bucket This operation is useful to determine if a bucket exists and you have permission to access it. The operation returns a 200 OK if the bucket exists and you have permission to access it. Otherwise, the operation might return responses such as 404 Not Found and 403 Forbidden. Syntax ------     HEAD /BucketName/ HTTP/1.1     Host: s3.amazonaws.com     Date: date     Authorization: signatureValue
     * @ramlpath /  head
     **/
    head(options?:ApiHeadOptions):Error


    /**
     *  This is base resource type described common request and response headers and error response codes
     * @ramlpath /?cors
     **/


    _cors:_corsResource


    /**
     *  This is base resource type described common request and response headers and error response codes
     * @ramlpath /?lifecycle
     **/


    _lifecycle:_lifecycleResource


    /**
     *  This is base resource type described common request and response headers and error response codes
     * @ramlpath /?policy
     **/


    _policy:_policyResource


    /**
     *  This is base resource type described common request and response headers and error response codes
     * @ramlpath /?tagging
     **/


    _tagging:_taggingResource


    /**
     *  This is base resource type described common request and response headers and error response codes
     * @ramlpath /?website
     **/


    _website:_websiteResource


    /**
     *  This is base resource type described common request and response headers and error response codes
     * @ramlpath /?versions
     **/


    _versions:_versionsResource


    /**
     *  This is base resource type described common request and response headers and error response codes
     * @ramlpath /?acl
     **/


    _acl:_aclResource


    /**
     *  This is base resource type described common request and response headers and error response codes
     * @ramlpath /?versioning
     **/


    _versioning:_versioningResource


    /**
     *  This is base resource type described common request and response headers and error response codes
     * @ramlpath /?delete
     **/


    _delete:_deleteResource


    /**
     *  This is base resource type described common request and response headers and error response codes
     * @ramlpath /?location
     **/


    _location:_locationResource


    /**
     *  This is base resource type described common request and response headers and error response codes
     * @ramlpath /?logging
     **/


    _logging:_loggingResource


    /**
     *  This is base resource type described common request and response headers and error response codes
     * @ramlpath /?notification
     **/


    _notification:_notificationResource


    /**
     *  This is base resource type described common request and response headers and error response codes
     * @ramlpath /?requestPayment
     **/


    _requestPayment:_requestPaymentResource


    /**
     *  This is base resource type described common request and response headers and error response codes
     * @ramlpath /?uploads
     **/


    _uploads:_uploadsResource


    /**
     *  This is extented resource type for resources involved in operation with object. Inherits base resource and contains additonal headers
     * @ramlpath /{objectName}
     **/


    objectName(objectName:string):ObjectNameResource
}
export interface ApiGetOptions {

    /**
     *
     **/
        //delimiter
    delimiter?:string


    /**
     *
     **/
        //marker
    marker?:string


    /**
     *
     **/
        //max-keys
        "max-keys"?:string


    /**
     *
     **/
        //prefix
    prefix?:string


    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export interface Bucket_list_objects {

    /**
     *
     **/
        //ListBucketResult
    ListBucketResult?:{

        /**
         *
         **/
            //Name
        Name?:string


        /**
         *
         **/
            //Prefix
        Prefix?:string


        /**
         *
         **/
            //Marker
        Marker?:string


        /**
         *
         **/
            //MaxKeys
        MaxKeys?:number


        /**
         *
         **/
            //IsTruncated
        IsTruncated?:string


        /**
         *
         **/
            //Contents
        Contents?:{

            /**
             *
             **/
                //Key
            Key?:string


            /**
             *
             **/
                //LastModified
            LastModified?:string


            /**
             *
             **/
                //ETag
            ETag?:string


            /**
             *
             **/
                //Size
            Size?:number


            /**
             *
             **/
                //StorageClass
            StorageClass?:string


            /**
             *
             **/
                //Owner
            Owner?:{

                /**
                 *
                 **/
                    //ID
                ID?:string


                /**
                 *
                 **/
                    //DisplayName
                DisplayName?:string
            }
        }[]
    }


    /**
     *
     **/
        //__$harEntry__
    __$harEntry__?:HarEntry
}
export interface HarEntry {

    /**
     *
     **/
        //response
    response:{

        /**
         *
         **/
            //content
        content:{

            /**
             *
             **/
                //text
            text:string
        }


        /**
         *
         **/
            //status
        status:number
    }


    /**
     *
     **/
        //request
    request:{}
}
export interface Error {

    /**
     *
     **/
        //Error
    Error?:{

        /**
         *
         **/
            //Code
        Code?:string


        /**
         *
         **/
            //Message
        Message?:string


        /**
         *
         **/
            //Resource
        Resource?:string


        /**
         *
         **/
            //RequestId
        RequestId?:string
    }


    /**
     *
     **/
        //__$harEntry__
    __$harEntry__?:HarEntry
}
export interface ApiPostOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string


    /**
     *
     **/
        //header_Content-Length
        "header_Content-Length":string


    /**
     *
     **/
        //header_Content-MD5
        "header_Content-MD5"?:string
}
export interface PostFormBody {

    /**
     *
     **/
        //AWSAccessKeyId
    AWSAccessKeyId:String


    /**
     *
     **/
        //acl
    acl:String


    /**
     *
     **/
        //file
    file:String


    /**
     *
     **/
        //key
    key:String


    /**
     *
     **/
        //policy
    policy:String


    /**
     *
     **/
        //success_action_redirect
    success_action_redirect:String


    /**
     *
     **/
        //redirect
    redirect:String


    /**
     *
     **/
        //success_action_status
    success_action_status:String


    /**
     *
     **/
        //x-amz-storage-class
        "x-amz-storage-class":String


    /**
     *
     **/
        //x-amz-meta-{*}
        "x-amz-meta-{*}":String


    /**
     *
     **/
        //x-amz-security-token
        "x-amz-security-token":String


    /**
     *
     **/
        //x-amz-server-side-encryption
        "x-amz-server-side-encryption":String


    /**
     *
     **/
        //x-amz-website-redirect-location
        "x-amz-website-redirect-location":String
}
export interface ApiPutOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string


    /**
     *
     **/
        //header_Content-Length
        "header_Content-Length":string


    /**
     *
     **/
        //header_Content-MD5
        "header_Content-MD5"?:string


    /**
     *
     **/
        //header_x-amz-acl
        "header_x-amz-acl"?:string


    /**
     *
     **/
        //header_x-amz-grant-read
        "header_x-amz-grant-read"?:string


    /**
     *
     **/
        //header_x-amz-grant-write
        "header_x-amz-grant-write"?:string


    /**
     *
     **/
        //header_x-amz-grant-read-acp
        "header_x-amz-grant-read-acp"?:string


    /**
     *
     **/
        //header_x-amz-grant-write-acp
        "header_x-amz-grant-write-acp"?:string


    /**
     *
     **/
        //header_x-amz-grant-full-control
        "header_x-amz-grant-full-control"?:string
}
export interface Bucket {

    /**
     *
     **/
        //CreateBucketConfiguration
    CreateBucketConfiguration?:{

        /**
         *
         **/
            //LocationConstraint
        LocationConstraint?:string
    }
}
export interface ApiDeleteOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export interface ApiHeadOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type ApiHeadOptions1=ApiDeleteOptions
export interface _corsResource {

    /**
     *  Returns the cors configuration information set for the bucket. To use this operation, you must have permission to perform the s3:GetBucketCORS action. By default, the bucket owner has this permission and can grant it to others. Syntax ------     GET /BucketName/?cors HTTP/1.1     Host: s3.amazonaws.com     Date: date     Authorization: signatureValue
     * @ramlpath /?cors  get
     **/
    get(options?:_corsResourceGetOptions):Bucket_conf_cors | Error


    /**
     *  Sets the cors configuration for your bucket. If the configuration exists, Amazon S3 replaces it. To use this operation, you must be allowed to perform the s3:PutBucketCORS action. By default, the bucket owner has this permission and can grant it to others. You set this configuration on a bucket so that the bucket can service cross-origin requests. For example, you might want to enable a request whose origin is http://www.example.com to access your Amazon S3 bucket at my.example.bucket.com by using the browser's XMLHttpRequest capability. To enable cross-origin resource sharing (CORS) on a bucket, you add the cors subresource to the bucket. The cors subresource is an XML document in which you configure rules that identify origins and the HTTP methods that can be executed on your bucket. The document is limited to 64 KB in size.
     * @ramlpath /?cors  put
     **/
    put(payload:Bucket_conf_cors, options?:_corsResourcePutOptions):Error


    /**
     *  Deletes the cors configuration information set for the bucket. To use this operation, you must have permission to perform the s3:PutCORSConfiguration action. The bucket owner has this permission by default and can grant this permission to others. Syntax ------     DELETE /BucketName/?cors HTTP/1.1     Host: s3.amazonaws.com     Date: date     Authorization: signatureValue
     * @ramlpath /?cors  delete
     **/
    "delete"(options?:_corsResourceDeleteOptions):Error
}
export interface _corsResourceGetOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _corsResourceGetOptions1=ApiDeleteOptions
export interface Bucket_conf_cors {

    /**
     *
     **/
        //CORSConfiguration
    CORSConfiguration?:{

        /**
         *
         **/
            //CORSRule
        CORSRule?:{

            /**
             *
             **/
                //ID
            ID?:string


            /**
             *
             **/
                //AllowedOrigin
            AllowedOrigin?:string


            /**
             *
             **/
                //AllowedMethod
            AllowedMethod?:string


            /**
             *
             **/
                //MaxAgeSeconds
            MaxAgeSeconds?:number


            /**
             *
             **/
                //ExposeHeader
            ExposeHeader?:string
        }
    }


    /**
     *
     **/
        //__$harEntry__
    __$harEntry__?:HarEntry
}
export interface _corsResourcePutOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _corsResourcePutOptions1=ApiDeleteOptions
export interface _corsResourceDeleteOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _corsResourceDeleteOptions1=ApiDeleteOptions
export interface _lifecycleResource {

    /**
     *  Returns the lifecycle configuration information set on the bucket. To use this operation, you must have permission to perform the s3:GetLifecycleConfiguration action. The bucket owner has this permission, by default. The bucket owner can grant this permission to others. Syntax ------     GET /BucketName/?lifecycle HTTP/1.1     Host: s3.amazonaws.com     Date: date     Authorization: signatureValue
     * @ramlpath /?lifecycle  get
     **/
    get(options?:_lifecycleResourceGetOptions):Bucket_conf_lifecycle | Error


    /**
     *  Creates a new lifecycle configuration for the bucket or replaces an existing lifecycle configuration. To use this operation, you must be allowed to perform the s3:PutLifecycleConfiguration action. By default, the bucket owner has this permission and can grant this permission to others. **Note** If your bucket is version-enabled or versioning is suspended, you cannot add a lifecycle configuration. If you want to block users or accounts from removing or deleting objects from your bucket, you must deny them permissions for the following actions: * s3:DeleteObjec * s3:DeleteObjectVersion and * s3:PutLifecycleConfiguration If you want to block users or accounts from managing lifecycle configurations, you must deny permission for the s3:PutLifecycleConfiguration action. Syntax ------     PUT /bucketname/?lifecycle HTTP/1.1     Host: s3.amazonaws.com     Content-Length: length     Date: date     Authorization: signatureValue     Content-MD5: MD5     Lifecycle configuration in the request body
     * @ramlpath /?lifecycle  put
     **/
    put(payload:Bucket_lifecycle_put, options:_lifecycleResourcePutOptions):Error


    /**
     *  Deletes the lifecycle configuration from the specified bucket. Amazon S3 removes all the lifecycle configuration rules in the lifecycle subresource associated with the bucket. Your objects never expire, and Amazon S3 no longer automatically deletes any objects on the basis of rules contained in the deleted lifecycle configuration. To use this operation, you must have permission to perform the s3:PutLifecycleConfiguration action. By default, the bucket owner has this permission and the bucket owner can grant this permission to others. There is usually some time lag before lifecycle configuration deletion is fully propagated to all the Amazon S3 systems. Syntax ------     DELETE /BucketName/?lifecycle HTTP/1.1     Host: s3.amazonaws.com     Date: date     Authorization: signatureValue
     * @ramlpath /?lifecycle  delete
     **/
    "delete"(options?:_lifecycleResourceDeleteOptions):Error
}
export interface _lifecycleResourceGetOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _lifecycleResourceGetOptions1=ApiDeleteOptions
export interface Bucket_conf_lifecycle {

    /**
     *
     **/
        //LifecycleConfiguration
    LifecycleConfiguration?:{

        /**
         *
         **/
            //Rule
        Rule?:{

            /**
             *
             **/
                //ID
            ID?:string


            /**
             *
             **/
                //Prefix
            Prefix?:string


            /**
             *
             **/
                //Status
            Status?:string


            /**
             *
             **/
                //Transition
            Transition?:{

                /**
                 *
                 **/
                    //Days
                Days?:number


                /**
                 *
                 **/
                    //StorageClass
                StorageClass?:string
            }


            /**
             *
             **/
                //Expiration
            Expiration?:{

                /**
                 *
                 **/
                    //Days
                Days?:number
            }
        }
    }


    /**
     *
     **/
        //__$harEntry__
    __$harEntry__?:HarEntry
}
export interface _lifecycleResourcePutOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string


    /**
     *
     **/
        //header_Content-Length
        "header_Content-Length":string


    /**
     *
     **/
        //header_Content-MD5
        "header_Content-MD5":string
}
export interface Bucket_lifecycle_put {

    /**
     *
     **/
        //LifecycleConfiguration
    LifecycleConfiguration?:{

        /**
         *
         **/
            //Rule
        Rule?:{

            /**
             *
             **/
                //ID
            ID?:string


            /**
             *
             **/
                //Prefix
            Prefix?:string


            /**
             *
             **/
                //Status
            Status?:string


            /**
             *
             **/
                //Transition
            Transition?:{

                /**
                 *
                 **/
                    //Days
                Days?:number


                /**
                 *
                 **/
                    //StorageClass
                StorageClass?:string
            }
        }
    }
}
export interface _lifecycleResourceDeleteOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _lifecycleResourceDeleteOptions1=ApiDeleteOptions
export interface _policyResource {

    /**
     *  This implementation of the GET operation uses the policy subresource to return the policy of a specified bucket. To use this operation, you must have GetPolicy permissions on the specified bucket, and you must be the bucket owner. If you don't have GetPolicy permissions, Amazon S3 returns a 403 Access Denied error. If you have the correct permissions, but you're not the bucket owner, Amazon S3 returns a 405 Method Not Allowed error. If the bucket does not have a policy, Amazon S3 returns a 404 Policy Not found error. There are restrictions about who can create bucket policies and which objects in a bucket they can apply to. Syntax ------     GET /BucketName/?policy HTTP/1.1     Host: s3.amazonaws.com     Date: date     Authorization: signatureValue
     * @ramlpath /?policy  get
     **/
    get(options?:_policyResourceGetOptions):Bucket_conf_policy_schema | Error


    /**
     *  This implementation of the PUT operation uses the policy subresource to add to or replace a policy on a bucket. If the bucket already has a policy, the one in this request completely replaces it. To perform this operation, you must be the bucket owner. If you are not the bucket owner but have PutBucketPolicy permissions on the bucket, Amazon S3 returns a 405 Method Not Allowed. In all other cases for a PUT bucket policy request that is not from the bucket owner, Amazon S3 returns 403 Access Denied. There are restrictions about who can create bucket policies and which objects in a bucket they can apply to. Syntax ------     PUT /bucketname/?policy HTTP/1.1     Host: s3.amazonaws.com     Content-Length: length     Date: date     Authorization: signatureValue     Policy written in JSON
     * @ramlpath /?policy  put
     **/
    put(payload:Bucket_policy_put_schema, options:_policyResourcePutOptions):Error


    /**
     *  This implementation of the DELETE operation uses the policy subresource to delete the policy on a specified bucket. To use the operation, you must have DeletePolicy permissions on the specified bucket and be the bucket owner. If you do not have DeletePolicy permissions, Amazon S3 returns a 403 Access Denied error. If you have the correct permissions, but are not the bucket owner , Amazon S3 returns a 405 Method Not Allowed error. If the bucket doesn't have a policy, Amazon S3 returns a 204 No Content error. There are restrictions about who can create bucket policies and which objects in a bucket they can apply to. Syntax ------     DELETE /BucketName/?policy HTTP/1.1     Host: s3.amazonaws.com     Date: date     Authorization: signatureValue
     * @ramlpath /?policy  delete
     **/
    "delete"(options?:_policyResourceDeleteOptions):Error
}
export interface _policyResourceGetOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _policyResourceGetOptions1=ApiDeleteOptions
export interface Bucket_conf_policy_schema {

    /**
     *
     **/
        //Id
    Id?:string


    /**
     *
     **/
        //Statement
    Statement?:{

        /**
         *
         **/
            //Action
        Action?:string[]


        /**
         *
         **/
            //Effect
        Effect?:string


        /**
         *
         **/
            //Principal
        Principal?:{

            /**
             *
             **/
                //AWS
            AWS?:string[]
        }


        /**
         *
         **/
            //Resource
        Resource?:string


        /**
         *
         **/
            //Sid
        Sid?:string
    }[]


    /**
     *
     **/
        //Version
    Version?:string


    /**
     *
     **/
        //__$harEntry__
    __$harEntry__?:HarEntry
}
export interface _policyResourcePutOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string


    /**
     *
     **/
        //header_Content-Length
        "header_Content-Length":string


    /**
     *
     **/
        //header_Content-MD5
        "header_Content-MD5"?:string
}
export type _policyResourcePutOptions1=ApiPostOptions
export interface Bucket_policy_put_schema {

    /**
     *
     **/
        //Id
    Id?:string


    /**
     *
     **/
        //Statement
    Statement?:{

        /**
         *
         **/
            //Action
        Action?:string[]


        /**
         *
         **/
            //Effect
        Effect?:string


        /**
         *
         **/
            //Principal
        Principal?:{

            /**
             *
             **/
                //AWS
            AWS?:string[]
        }


        /**
         *
         **/
            //Resource
        Resource?:string


        /**
         *
         **/
            //Sid
        Sid?:string
    }[]


    /**
     *
     **/
        //Version
    Version?:string
}
export interface _policyResourceDeleteOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _policyResourceDeleteOptions1=ApiDeleteOptions
export interface _taggingResource {

    /**
     *  This implementation of the GET operation uses the tagging subresource to return the tag set associated with the bucket. To use this operation, you must have permission to perform the s3:GetBucketTagging action. By default, the bucket owner has this permission and can grant this permission to others. Syntax ------     GET /BucketName/?tagging HTTP/1.1     Host: s3.amazonaws.com     Date: date     Authorization: signatureValue
     * @ramlpath /?tagging  get
     **/
    get(options?:_taggingResourceGetOptions):Bucket_conf_tagging | Error


    /**
     *  This implementation of the PUT operation uses the tagging subresource to add a set of tags to an existing bucket. Use tags to organize your AWS bill to reflect your own cost structure. To do this, sign up to get your AWS account bill with tag key values included. Then, to see the cost of combined resources, organize your billing information according to resources with the same tag key values. For example, you can tag several resources with a specific application name, and then organize your billing information to see the total cost of that application across several services. For more information, see Cost Allocation and Tagging in About AWS Billing and Cost Management. To use this operation, you must have permission to perform the s3:PutBucketTagging action. By default, the bucket owner has this permission and can grant this permission to others.
     * @ramlpath /?tagging  put
     **/
    put(payload:Bucket_conf_tagging, options?:_taggingResourcePutOptions):Error


    /**
     *  This implementation of the DELETE operation uses the tagging subresource to remove a tag set from the specified bucket. To use this operation, you must have permission to perform the s3:PutBucketTagging action. By default, the bucke owner has this permission and can grant this permission to others. Syntax ------     DELETE /BucketName/?tagging HTTP/1.1     Host: s3.amazonaws.com     Date: date     Authorization: signatureValue
     * @ramlpath /?tagging  delete
     **/
    "delete"(options?:_taggingResourceDeleteOptions):Error
}
export interface _taggingResourceGetOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _taggingResourceGetOptions1=ApiDeleteOptions
export interface Bucket_conf_tagging {

    /**
     *
     **/
        //Tagging
    Tagging?:{

        /**
         *
         **/
            //TagSet
        TagSet?:{

            /**
             *
             **/
                //Tag
            Tag?:{

                /**
                 *
                 **/
                    //Key
                Key?:string


                /**
                 *
                 **/
                    //Value
                Value?:string
            }[]
        }
    }


    /**
     *
     **/
        //__$harEntry__
    __$harEntry__?:HarEntry
}
export interface _taggingResourcePutOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _taggingResourcePutOptions1=ApiDeleteOptions
export interface _taggingResourceDeleteOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _taggingResourceDeleteOptions1=ApiDeleteOptions
export interface _websiteResource {

    /**
     *  This implementation of the GET operation returns the website configuration associated with a bucket. To host website on Amazon S3, you can configure a bucket as website by adding a website configuration. This GET operation requires the S3:GetBucketWebsite permission. By default, only the bucket owner can read the bucket website configuration. However, bucket owners can allow other users to read the website configuration by writing a bucket policy granting them the S3:GetBucketWebsite permission. Syntax ------     GET /BucketName/?website HTTP/1.1     Host: s3.amazonaws.com     Date: date     Authorization: signatureValue
     * @ramlpath /?website  get
     **/
    get(options?:_websiteResourceGetOptions):Bucket_conf_website | Error


    /**
     *  Sets the configuration of the website that is specified in the website subresource. To configure a bucket as a website, you can add this subresource on the bucket with website configuration information such as the file name of the index document and any redirect rules. For more information, go to Hosting Websites on Amazon S3 in the Amazon Simple Storage Service Developer Guide. This PUT operation requires the S3:PutBucketWebsite permission. By default, only the bucket owner can configure the website attached to a bucket; however, bucket owners can allow other users to set the website configuration by writing a bucket policy that grants them the S3:PutBucketWebsite permission.
     * @ramlpath /?website  put
     **/
    put(payload:Bucket_conf_website, options?:_websiteResourcePutOptions):Error


    /**
     *  This operation removes the website configuration for a bucket. Amazon S3 returns a 200 OK response upon successfully deleting a website configuration on the specified bucket. You will get a 200 OK response if the website configuration you are trying to delete does not exist on the bucket. Amazon S3 returns a 404 response if the bucket specified in the request does not exist. This DELETE operation requires the S3:DeleteBucketWebsite permission. By default, only the bucket owner can delete the website configuration attached to a bucket. However, bucket owners can grant other users permission to delete the website configuration by writing a bucket policy granting them the S3:DeleteBucketWebsite permission. Syntax ------     DELETE /BucketName/?website HTTP/1.1     Host: s3.amazonaws.com     Date: date     Authorization: signatureValue
     * @ramlpath /?website  delete
     **/
    "delete"(options?:_websiteResourceDeleteOptions):Error
}
export interface _websiteResourceGetOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _websiteResourceGetOptions1=ApiDeleteOptions
export interface Bucket_conf_website {

    /**
     *
     **/
        //WebsiteConfiguration
    WebsiteConfiguration?:{

        /**
         *
         **/
            //IndexDocument
        IndexDocument?:{

            /**
             *
             **/
                //Suffix
            Suffix?:string
        }


        /**
         *
         **/
            //ErrorDocument
        ErrorDocument?:{

            /**
             *
             **/
                //Key
            Key?:string
        }
    }


    /**
     *
     **/
        //__$harEntry__
    __$harEntry__?:HarEntry
}
export interface _websiteResourcePutOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _websiteResourcePutOptions1=ApiDeleteOptions
export interface _websiteResourceDeleteOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _websiteResourceDeleteOptions1=ApiDeleteOptions
export interface _versionsResource {

    /**
     *  You can use the versions subresource to list metadata about all of the versions of objects in a bucket. You can also use request parameters as selection criteria to return metadata about a subset of all the object versions. To use this operation, you must have READ access to the bucket. Syntax ------     GET /BucketName/?versions HTTP/1.1     Host: s3.amazonaws.com     Date: date     Authorization: signatureValue
     * @ramlpath /?versions  get
     **/
    get(options?:_versionsResourceGetOptions):Bucket_object_versions | Error
}
export interface _versionsResourceGetOptions {

    /**
     *
     **/
        //delimiter
    delimiter?:string


    /**
     *
     **/
        //key-marker
        "key-marker"?:string


    /**
     *
     **/
        //max-keys
        "max-keys"?:string


    /**
     *
     **/
        //prefix
    prefix?:string


    /**
     *
     **/
        //version-id-marker
        "version-id-marker"?:string


    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export interface Bucket_object_versions {

    /**
     *
     **/
        //ListVersionsResult
    ListVersionsResult?:{

        /**
         *
         **/
            //Name
        Name?:string


        /**
         *
         **/
            //Prefix
        Prefix?:string


        /**
         *
         **/
            //KeyMarker
        KeyMarker?:string


        /**
         *
         **/
            //VersionIdMarker
        VersionIdMarker?:string


        /**
         *
         **/
            //MaxKeys
        MaxKeys?:number


        /**
         *
         **/
            //IsTruncated
        IsTruncated?:string


        /**
         *
         **/
            //Version
        Version?:{

            /**
             *
             **/
                //Key
            Key?:string


            /**
             *
             **/
                //VersionId
            VersionId?:string


            /**
             *
             **/
                //IsLatest
            IsLatest?:string


            /**
             *
             **/
                //LastModified
            LastModified?:string


            /**
             *
             **/
                //ETag
            ETag?:string


            /**
             *
             **/
                //Size
            Size?:number


            /**
             *
             **/
                //StorageClass
            StorageClass?:string


            /**
             *
             **/
                //Owner
            Owner?:{

                /**
                 *
                 **/
                    //ID
                ID?:string


                /**
                 *
                 **/
                    //DisplayName
                DisplayName?:string
            }
        }


        /**
         *
         **/
            //DeleteMarker
        DeleteMarker?:{

            /**
             *
             **/
                //Key
            Key?:string


            /**
             *
             **/
                //VersionId
            VersionId?:string


            /**
             *
             **/
                //IsLatest
            IsLatest?:string


            /**
             *
             **/
                //LastModified
            LastModified?:string


            /**
             *
             **/
                //ETag
            ETag?:string


            /**
             *
             **/
                //Size
            Size?:number


            /**
             *
             **/
                //StorageClass
            StorageClass?:string


            /**
             *
             **/
                //Owner
            Owner?:{

                /**
                 *
                 **/
                    //ID
                ID?:string


                /**
                 *
                 **/
                    //DisplayName
                DisplayName?:string
            }
        }
    }


    /**
     *
     **/
        //__$harEntry__
    __$harEntry__?:HarEntry
}
export interface _aclResource {

    /**
     *  This implementation of the GET operation uses the acl subresource to return the access control list (ACL) of a bucket. To use GET to return the ACL of the bucket, you must have READ_ACP access to the bucket. If READ_ACP permission is granted to the anonymous user, you can return the ACL of the bucket withou using an authorization header. Syntax ------     GET /BucketName/?acl HTTP/1.1     Host: s3.amazonaws.com     Date: date     Authorization: signatureValue
     * @ramlpath /?acl  get
     **/
    get(options?:_aclResourceGetOptions):Bucket_acl | Error


    /**
     *  This implementation of the PUT operation uses the acl subresource to set the permissions on an existing bucket using access control lists (ACL). For more information, go to Using ACLs. To set the ACL of a bucket, you must have WRITE_ACP permission. You can use one of the following two ways to set a bucket's permissions: * Specify the ACL in the request body * Specify permissions using request headers **Note** You cannot specify access permission using both the body and the request headers. Depending on your application needs, you may choose to set the ACL on a bucket using either the request body or the headers. For example, if you have an existing application that updates a bucket ACL using the request body, then you can continue to use that approach. Syntax ------ With request body     PUT /BucketName/?acl HTTP/1.1     Host: s3.amazonaws.com     Date: date     Authorization: signatureValue     <AccessControlPolicy>       <Owner>         <ID>ID</ID>         <DisplayName>EmailAddress</DisplayName>       </Owner>       <AccessControlList>         <Grant>           <Grantee xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:type="CanonicalUser">             <ID>ID</ID>             <DisplayName>EmailAddress</DisplayName>           </Grantee>           <Permission>Permission</Permission>         </Grant>       ...       </AccessControlList>     </AccessControlPolicy> With headers     PUT /BucketName/?acl HTTP/1.1     Host: s3.amazonaws.com     Date: date     x-amz-grant-write: uri="uri1", emailAddress="email"     x-amz-grant-read: uri="uri2"     Authorization: signatureValue
     * @ramlpath /?acl  put
     **/
    put(payload:Bucket_grant_acl, options:_aclResourcePutOptions):Error
}
export interface _aclResourceGetOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _aclResourceGetOptions1=ApiDeleteOptions
export interface Bucket_acl {

    /**
     *
     **/
        //AccessControlPolicy
    AccessControlPolicy?:{

        /**
         *
         **/
            //Owner
        Owner?:{

            /**
             *
             **/
                //ID
            ID?:string


            /**
             *
             **/
                //DisplayName
            DisplayName?:string
        }


        /**
         *
         **/
            //AccessControlList
        AccessControlList?:{

            /**
             *
             **/
                //Grant
            Grant?:{

                /**
                 *
                 **/
                    //Grantee
                Grantee?:{

                    /**
                     *
                     **/
                        //ID
                    ID?:string


                    /**
                     *
                     **/
                        //DisplayName
                    DisplayName?:string
                }


                /**
                 *
                 **/
                    //Permission
                Permission?:string
            }
        }
    }


    /**
     *
     **/
        //__$harEntry__
    __$harEntry__?:HarEntry
}
export interface _aclResourcePutOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string


    /**
     *
     **/
        //header_Content-Length
        "header_Content-Length":string


    /**
     *
     **/
        //header_Content-MD5
        "header_Content-MD5"?:string


    /**
     *
     **/
        //header_x-amz-acl
        "header_x-amz-acl"?:string


    /**
     *
     **/
        //header_x-amz-grant-read
        "header_x-amz-grant-read"?:string


    /**
     *
     **/
        //header_x-amz-grant-write
        "header_x-amz-grant-write"?:string


    /**
     *
     **/
        //header_x-amz-grant-read-acp
        "header_x-amz-grant-read-acp"?:string


    /**
     *
     **/
        //header_x-amz-grant-write-acp
        "header_x-amz-grant-write-acp"?:string


    /**
     *
     **/
        //header_x-amz-grant-full-control
        "header_x-amz-grant-full-control"?:string
}
export type _aclResourcePutOptions1=ApiPutOptions
export interface Bucket_grant_acl {

    /**
     *
     **/
        //AccessControlPolicy
    AccessControlPolicy?:{

        /**
         *
         **/
            //Owner
        Owner?:{

            /**
             *
             **/
                //ID
            ID?:string


            /**
             *
             **/
                //DisplayName
            DisplayName?:string
        }


        /**
         *
         **/
            //AccessControlList
        AccessControlList?:{

            /**
             *
             **/
                //Grant
            Grant?:{

                /**
                 *
                 **/
                    //Grantee
                Grantee?:{

                    /**
                     *
                     **/
                        //ID
                    ID?:string


                    /**
                     *
                     **/
                        //DisplayName
                    DisplayName?:string


                    /**
                     *
                     **/
                        //URI
                    URI?:string


                    /**
                     *
                     **/
                        //EmailAddress
                    EmailAddress?:string
                }


                /**
                 *
                 **/
                    //Permission
                Permission?:string
            }[]
        }
    }
}
export interface _versioningResource {

    /**
     *  This implementation of the GET operation uses the versioning subresource to return the versioning state of a bucket. To retrieve the versioning state of a bucket, you must be the bucket owner.  This implementation also returns the MFA Delete status of the versioning state, i.e., if the MFA Delete status  is enabled, the bucket owner must use an authentication device to change the versioning state of the bucket.  There are three versioning states:  * If you enabled versioning on a bucket, the response is:      <VersioningConfiguration xmlns="http://s3.amazonaws.com/doc/2006-03-01/">        <Status>Enabled</Status>      </VersioningConfiguration>  * If you suspended versioning on a bucket, the response is:      <VersioningConfiguration xmlns="http://s3.amazonaws.com/doc/2006-03-01/">        <Status>Suspended</Status>      </VersioningConfiguration>  * If you never enabled (or suspended) versioning on a bucket, the response is:      <VersioningConfiguration xmlns="http://s3.amazonaws.com/doc/2006-03-01/"/> Syntax ------     GET /BucketName/?versioning HTTP/1.1     Host: s3.amazonaws.com     Date: date     Authorization: signatureValue
     * @ramlpath /?versioning  get
     **/
    get(options?:_versioningResourceGetOptions):Bucket_versioning | Error


    /**
     *  This implementation of the PUT operation uses the versioning subresource to set the versioning state of an existing bucket. To set the versioning state, you must be the bucket owner. You can set the versioning state with one of the following values: * **Enabled** Enables versioning for the objects in the bucke   All objects added to the bucket receive a unique version ID. * **Suspended** Disables versioning for the objects in the bucke   All objects added to the bucket receive the version ID null. If the versioning state has never been set on a bucket, it has no versioning state; a GET versioning request does no return a versioning state value. If the bucket owner enables MFA Delete in the bucket versioning configuration, the bucket owner must include the x-amz-mfa request header and the Status and the MfaDelete request elements in a request to set the versioning state of the bucket. Syntax ------     PUT /?versioning HTTP/1.1     Host: BucketName.s3.amazonaws.com     Content-Length: length     Date: date     Authorization: signatureValue     x-amz-mfa: [SerialNumber] [TokenCode]     <VersioningConfiguration xmlns="http://s3.amazonaws.com/doc/2006-03-01/">       <Status>VersioningState</Status>       <MfaDelete>MfaDeleteState</MfaDelete>     </VersioningConfiguration>
     * @ramlpath /?versioning  put
     **/
    put(payload:Bucket_versioning, options:_versioningResourcePutOptions):Error
}
export interface _versioningResourceGetOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _versioningResourceGetOptions1=ApiDeleteOptions
export interface Bucket_versioning {

    /**
     *
     **/
        //VersioningConfiguration
    VersioningConfiguration?:{

        /**
         *
         **/
            //Status
        Status?:string
    }


    /**
     *
     **/
        //__$harEntry__
    __$harEntry__?:HarEntry
}
export interface _versioningResourcePutOptions {

    /**
     *
     **/
        //header_x-amz-mfa
        "header_x-amz-mfa"?:string


    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string


    /**
     *
     **/
        //header_Content-Length
        "header_Content-Length":string


    /**
     *
     **/
        //header_Content-MD5
        "header_Content-MD5"?:string
}
export interface _deleteResource {

    /**
     *  The Multi-Object Delete operation enables you to delete multiple objects from a bucket using a single HTTP request. If you know the object keys that you want to delete, then this operation provides a suitable alternative to sending individual delete requests (see DELETE Object), reducing per-request overhead. The Multi-Object Delete request contains a list of up to 1000 keys that you want to delete. In the XML, you provide the object key names, and optionally, version IDs if you want to delete a specific version of the object from a versioning-enabled bucket. For each key, Amazon S3 performs a delete operation and returns the result of that delete, success, or failure, in the response. Note that, if the object specified in the request is not found, Amazon S3 returns the result as deleted. The Multi-Object Delete operation supports two modes for the response; verbose and quiet. By default, the operation uses verbose mode in which the response includes the result of deletion of each key in your request. In quiet mode the response includes only keys where the delete operation encountered an error. For a successful deletion, the operation does not return any information about the delete in the response body. When performing a Multi-Object Delete operation on an MFA Delete enabled bucket, that attempts to delete any versioned objects, you must include an MFA token. If you do not provide one, the entire request will fail, even if there are non versioned objects you are attempting to delete. If you provide an invalid token, whether there are versioned keys in the request or not, the entire Multi-Object Delete request will fail. Finally, the Content-MD5 header is required for all Multi-Object Delete requests. Amazon S3 uses the header value to ensure that your reques body has not be altered in transit. Syntax ------     POST /bucketname/?delete HTTP/1.1     Host: s3.amazonaws.com     Authorization: Signature     Content-Length: Size     Content-MD5: MD5     <?xml version="1.0" encoding="UTF-8"?>       <Delete>         <Quiet>true</Quiet>         <Object>           <Key>Key</Key>           <VersionId>VersionId</VersionId>         </Object>         <Object>           <Key>Key</Key>         </Object>         ...       </Delete>
     * @ramlpath /?delete  post
     **/
    post(payload:Bucket_delete_multiple, options:_deleteResourcePostOptions):Bucket_delete_multiple_response | Error
}
export interface _deleteResourcePostOptions {

    /**
     *
     **/
        //header_x-amz-mfa
        "header_x-amz-mfa"?:string


    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string


    /**
     *
     **/
        //header_Content-Length
        "header_Content-Length":string


    /**
     *
     **/
        //header_Content-MD5
        "header_Content-MD5":string
}
export interface Bucket_delete_multiple_response {

    /**
     *
     **/
        //DeleteResult
    DeleteResult?:{

        /**
         *
         **/
            //Deleted
        Deleted?:{

            /**
             *
             **/
                //Key
            Key?:string


            /**
             *
             **/
                //VersionId
            VersionId?:string


            /**
             *
             **/
                //DeleteMarker
            DeleteMarker?:boolean


            /**
             *
             **/
                //DeleteMarkerVersionId
            DeleteMarkerVersionId?:string
        }


        /**
         *
         **/
            //Error
        Error?:{

            /**
             *
             **/
                //Key
            Key?:string


            /**
             *
             **/
                //VersionId
            VersionId?:string


            /**
             *
             **/
                //Code
            Code?:string


            /**
             *
             **/
                //Message
            Message?:string
        }
    }


    /**
     *
     **/
        //__$harEntry__
    __$harEntry__?:HarEntry
}
export interface Bucket_delete_multiple {

    /**
     *
     **/
        //Delete
    Delete?:{

        /**
         *
         **/
            //Quiet
        Quiet?:string


        /**
         *
         **/
            //Object
        Object:{

            /**
             *
             **/
                //Key
            Key:string


            /**
             *
             **/
                //VersionId
            VersionId?:number
        }[]
    }
}
export interface _locationResource {

    /**
     *  This implementation of the GET operation uses the location subresource to return a bucket's Region. You set the bucket's Region using the LocationContraint request parameter in a PUT Bucket request. For more information, see PUT Bucket. To use this implementation of the operation, you must be the bucket owner.
     * @ramlpath /?location  get
     **/
    get(options?:_locationResourceGetOptions):Location_constraint | Error
}
export interface _locationResourceGetOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _locationResourceGetOptions1=ApiDeleteOptions
export interface Location_constraint {

    /**
     *
     **/
        //LocationConstraint
    LocationConstraint?:string


    /**
     *
     **/
        //__$harEntry__
    __$harEntry__?:HarEntry
}
export interface _loggingResource {

    /**
     *  This implementation of the GET operation uses the logging subresource to return the logging status of a bucket and the permissions users have to view and modify that status. To use GET, you must be the bucket owner.
     * @ramlpath /?logging  get
     **/
    get(options?:_loggingResourceGetOptions):Bucket_logging | Error


    /**
     *  This implementation of the PUT operation uses the logging subresource to set the logging parameters for a bucket and to specify permissions for who can view and modify the logging parameters. To set the logging status of a bucket, you must be the bucket owner. The bucket owner is automatically granted FULL_CONTROL to all logs. You use the Grantee request element to grant access to other people. The Permissions request element specifies the kind of access the grantee has to the logs.
     * @ramlpath /?logging  put
     **/
    put(payload:Bucket_logging, options?:_loggingResourcePutOptions):Error
}
export interface _loggingResourceGetOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _loggingResourceGetOptions1=ApiDeleteOptions
export interface Bucket_logging {

    /**
     *
     **/
        //BucketLoggingStatus
    BucketLoggingStatus?:{

        /**
         *
         **/
            //LoggingEnabled
        LoggingEnabled?:{

            /**
             *
             **/
                //TargetBucket
            TargetBucket?:string


            /**
             *
             **/
                //TargetPrefix
            TargetPrefix?:string


            /**
             *
             **/
                //TargetGrants
            TargetGrants?:{

                /**
                 *
                 **/
                    //Grant
                Grant?:{

                    /**
                     *
                     **/
                        //Grantee
                    Grantee?:{

                        /**
                         *
                         **/
                            //EmailAddress
                        EmailAddress?:string
                    }


                    /**
                     *
                     **/
                        //Permission
                    Permission?:string
                }
            }
        }
    }


    /**
     *
     **/
        //__$harEntry__
    __$harEntry__?:HarEntry
}
export interface _loggingResourcePutOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _loggingResourcePutOptions1=ApiDeleteOptions
export interface _notificationResource {

    /**
     *  This implementation of the GET operation uses the notification subresource to return the notification configuration of a bucket. Currently, the s3:ReducedRedundancyLostObject event is the only event supported for notifications. The s3:ReducedRedundancyLostObject event is triggered when Amazon S3 detects that it has lost all replicas of a Reduced Redundancy Storage object and can no longer service requests for that object. If notifications are not enabled on the bucket, the operation returns an empty NotificatonConfiguration element. By default, you must be the bucket owner to read the notification configuration of a bucket. However, the bucket owner can use a bucket policy to grant permission to other users to read this configuration with the s3:GetBucketNotification permission. For more information about setting and reading the notification configuration on a bucket, see Setting Up Notification of Bucket Events. For more information about bucket policies, see Using Bucket Policies.
     * @ramlpath /?notification  get
     **/
    get(options?:_notificationResourceGetOptions):Notification_configuration | Error


    /**
     *  This implementation of the PUT operation uses the notification subresource to enable notifications of specified events for a bucket. Currently, the s3:ReducedRedundancyLostObject event is the only event supported for notifications. The s3:ReducedRedundancyLostObject event is triggered when Amazon S3 detects that it has lost all replicas of an object and can no longer service requests for that object. If the bucket owner and Amazon SNS topic owner are the same, the bucket owner has permission to publish notifications to the topic by default. Otherwise, the owner of the topic must create a policy to enable the bucket owner to publish to the topic. For more information about creating this policy, go to Example Cases for Amazon SNS Access Control. By default, only the bucket owner can configure notifications on a bucket. However, bucket owners can use a bucket policy to grant permission to other users to set this configuration with s3:PutBucketNotification permission. After you call the PUT operation to configure notifications on a bucket, Amazon S3 publishes a test notification to ensure that the topic exists and that the bucket owner has permission to publish to the specified topic. If the notification is successfully published to the SNS topic, the PUT operation updates the bucket configuration and returns the 200 OK response with a x-amz-sns-test-message-id header containing the message ID of the test notification sent to topic. To turn off notifications on a bucket, you specify an empty NotificationConfiguration element in your request: <NotificationConfiguration /> For more information about setting and reading the notification configuration on a bucket, see Setting Up Notification of Bucket Events. For more information about bucket policies, see Using Bucket Policies.
     * @ramlpath /?notification  put
     **/
    put(payload:Notification_configuration, options?:_notificationResourcePutOptions):Error
}
export interface _notificationResourceGetOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _notificationResourceGetOptions1=ApiDeleteOptions
export interface Notification_configuration {

    /**
     *
     **/
        //NotificationConfiguration
    NotificationConfiguration?:{

        /**
         *
         **/
            //TopicConfiguration
        TopicConfiguration?:{

            /**
             *
             **/
                //Topic
            Topic?:string


            /**
             *
             **/
                //Event
            Event?:string
        }
    }


    /**
     *
     **/
        //__$harEntry__
    __$harEntry__?:HarEntry
}
export interface _notificationResourcePutOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _notificationResourcePutOptions1=ApiDeleteOptions
export interface _requestPaymentResource {

    /**
     *  This implementation of the GET operation uses the requestPayment subresource to return the request payment configuration of a bucket. To use this version of the operation, you must be the bucket owner. For more information, see Requester Pays Buckets.
     * @ramlpath /?requestPayment  get
     **/
    get(options?:_requestPaymentResourceGetOptions):RequestPayement_configuration | Error


    /**
     *  This implementation of the PUT operation uses the requestPayment subresource to set the request payment configuration of a bucket. By default, the bucket owner pays for downloads from the bucket. This configuration parameter enables the bucket owner (only) to specify that the person requesting the download will be charged for the download. For more information, see Requester Pays Buckets.
     * @ramlpath /?requestPayment  put
     **/
    put(payload:RequestPayement_configuration, options?:_requestPaymentResourcePutOptions):Error
}
export interface _requestPaymentResourceGetOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _requestPaymentResourceGetOptions1=ApiDeleteOptions
export interface RequestPayement_configuration {

    /**
     *
     **/
        //RequestPaymentConfiguration
    RequestPaymentConfiguration?:{

        /**
         *
         **/
            //Payer
        Payer?:string
    }


    /**
     *
     **/
        //__$harEntry__
    __$harEntry__?:HarEntry
}
export interface _requestPaymentResourcePutOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _requestPaymentResourcePutOptions1=ApiDeleteOptions
export interface _uploadsResource {

    /**
     *  This operation lists in-progress multipart uploads. An in-progress multipart upload is a multipart upload that has been initiated, using the Initiate Multipart Upload request, but has not yet been completed or aborted. This operation returns at most 1,000 multipart uploads in the response. 1,000 multipart uploads is the maximum number of uploads a response can include, which is also the default value. You can further limit the number of uploads in a response by specifying the max-uploads parameter in the response. If additional multipart uploads satisfy the list criteria, the response will contain an IsTruncated element with the value true. To list the additional multipart uploads, use the key-marker and upload-id-marker request parameters. In the response, the uploads are sorted by key. If your application has initiated more than one multipart upload using the same object key, then uploads in the response are first sorted by key. Additionally, uploads are sorted in ascending order within each key by the upload initiation time.
     * @ramlpath /?uploads  get
     **/
    get(options?:_uploadsResourceGetOptions):ListMultipartUploadsResult | Error
}
export interface _uploadsResourceGetOptions {

    /**
     *
     **/
        //delimeter
    delimeter?:string


    /**
     *
     **/
        //encoding-type
        "encoding-type"?:string


    /**
     *
     **/
        //max-uploads
        "max-uploads"?:number


    /**
     *
     **/
        //key-marker
        "key-marker"?:string


    /**
     *
     **/
        //prefix
    prefix?:string


    /**
     *
     **/
        //upload-id-marker
        "upload-id-marker"?:string


    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export interface ListMultipartUploadsResult {

    /**
     *
     **/
        //ListMultipartUploadsResult
    ListMultipartUploadsResult?:{

        /**
         *
         **/
            //Bucket
        Bucket?:string


        /**
         *
         **/
            //KeyMarker
        KeyMarker?:string


        /**
         *
         **/
            //UploadIdMarker
        UploadIdMarker?:string


        /**
         *
         **/
            //NextKeyMarker
        NextKeyMarker?:string


        /**
         *
         **/
            //NextUploadIdMarker
        NextUploadIdMarker?:string


        /**
         *
         **/
            //MaxUploads
        MaxUploads?:number


        /**
         *
         **/
            //IsTruncated
        IsTruncated?:string


        /**
         *
         **/
            //Upload
        Upload?:{

            /**
             *
             **/
                //Key
            Key?:string


            /**
             *
             **/
                //UploadId
            UploadId?:string


            /**
             *
             **/
                //Initiator
            Initiator?:{

                /**
                 *
                 **/
                    //ID
                ID?:string


                /**
                 *
                 **/
                    //DisplayName
                DisplayName?:string
            }


            /**
             *
             **/
                //Owner
            Owner?:{

                /**
                 *
                 **/
                    //ID
                ID?:string


                /**
                 *
                 **/
                    //DisplayName
                DisplayName?:string
            }


            /**
             *
             **/
                //StorageClass
            StorageClass?:string


            /**
             *
             **/
                //Initiated
            Initiated?:string
        }[]
    }


    /**
     *
     **/
        //__$harEntry__
    __$harEntry__?:HarEntry
}
export interface ObjectNameResource {

    /**
     *  This is extented resource type for resources involved in operation with object. Inherits base resource and contains additonal headers
     * @ramlpath /{objectName}/?acl
     **/


    _acl:_aclResource1


    /**
     *  This is extented resource type for resources involved in operation with object. Inherits base resource and contains additonal headers
     * @ramlpath /{objectName}/?torrent
     **/


    _torrent:_torrentResource


    /**
     *  This is extented resource type for resources involved in operation with object. Inherits base resource and contains additonal headers
     * @ramlpath /{objectName}/?restore
     **/


    _restore:_restoreResource


    /**
     *  This is extented resource type for resources involved in operation with object. Inherits base resource and contains additonal headers
     * @ramlpath /{objectName}/?uploads
     **/


    _uploads:_uploadsResource1


    /**
     *  This is extented resource type for resources involved in operation with object. Inherits base resource and contains additonal headers
     * @ramlpath /{objectName}/?partNumber={partNumber}&uploadId={uploadId}
     **/


    _partNumber_partNumber_uploadId_uploadId(partNumber:string, uploadId:string):_partNumber_partNumber_uploadId_uploadIdResource


    /**
     *  This is extented resource type for resources involved in operation with object. Inherits base resource and contains additonal headers
     * @ramlpath /{objectName}/?uploadId={UploadId}
     **/


    _uploadId_UploadId(UploadId:string):_uploadId_UploadIdResource


    /**
     *  This implementation of the GET operation retrieves objects from Amazon S3. To use GET, you must have READ access to the object. If you grant READ access to the anonymous user, you can return the object without using an authorization header. An Amazon S3 bucket has no directory hierarchy such as you would find in a typical computer file system. You can, however, create a logical hierarchy by using object key names that imply a folder structure. For example, instead of naming an object sample.jpg , you can name it photos/2006/February/sample.jpg. To get an object from such a logical hierarchy, specify the full key name for the object in the GET operation. For a virtual hosted-style request example, if you have the object photos/2006/February/sample.jpg, specify the resource as /photos/2006/February/sample.jpg. For a path-style request example, if you have the object photos/2006/February/sample.jpg in the bucket named examplebucket, specify the resource as /examplebucket/photos/2006/February/sample.jpg. To distribute large files to many people, you can save bandwidth costs using BitTorrent. If the object you are retrieving is a GLACIER storage class object, the object is archived in Amazon Glacier. You must firs restore a copy using the POST Object restore API before you can retrieve the object. Otherwise, this operation returns InvalidObjectStateError error. Syntax ------     GET /BucketName/ObjectName HTTP/1.1     Host: s3.amazonaws.com     Date: date     Authorization: signatureValue     Range:bytes=byte_range
     * @ramlpath /{objectName}  get
     **/
    get(options?:ObjectNameResourceGetOptions):Error


    /**
     *  This implementation of the PUT operation adds an object to a bucket. You must have WRITE permissions on a bucket to add an object to it. Amazon S3 never adds partial objects; if you receive a success response, Amazon S3 added the entire object to the bucket. Amazon S3 is a distributed system. If it receives multiple write requests for the same object simultaneously, it overwrites all but the last object written. Amazon S3 does not provide object locking; if you need this, make sure to build it into your application layer or use versioning instead. To ensure that data is not corrupted traversing the network, use the Content-MD5 header. When you use this header, Amazon S3 checks the object against the provided MD5 value and, if they do not match, returns an error. Additionally, you can calculate the MD5 while putting an object to Amazon S3 and compare the returned ETag to the calculated MD5 value. **Note** To configure your application to send the Request Headers prior to sending the request body, use the 100-continue HTTP status code. For PUT operations, this helps you avoid sending the message body if the message is rejected based on the headers (e.g., because of authentication failure or redirect). Syntax ------     PUT /BucketName/ObjectName HTTP/1.1     Host: s3.amazonaws.com     Date: date     Authorization: signatureValue
     * @ramlpath /{objectName}  put
     **/
    put(options:ObjectNameResourcePutOptions):Error


    /**
     *  The DELETE operation removes the null version (if there is one) of an object and inserts a delete marker, which becomes the lates version of the object. If there isn't a null version, Amazon S3 does not remove any objects. To remove a specific version, you must be the bucket owner and you must use the versionId subresource. Using this subresource permanently deletes the version. If the object deleted is a Delete Marker, Amazon S3 sets the response header, x-amz-delete-marker, to true. If the object you want to delete is in a bucket where the bucket versioning configuration is MFA Delete enabled, you must include the x-amz-mfa request header in the DELETE versionId request. Requests that include x-amz-mfa must use HTTPS. Syntax ------     DELETE /BucketName/ObjectName HTTP/1.1     Host: s3.amazonaws.com     Date: date     Authorization: signatureValue
     * @ramlpath /{objectName}  delete
     **/
    "delete"(options?:ObjectNameResourceDeleteOptions):Error


    /**
     *  The HEAD operation retrieves metadata from an object without returning the object itself. This operation is useful if you are interested only in an object's metadata. To use HEAD, you must have READ access to the object. A HEAD request has the same options as a GET operation on an object. The response is identical to the GET response except tha there is no response body. Syntax ------     HEAD /BucketName/ObjectName HTTP/1.1     Host: s3.amazonaws.com     Authorization: signatureValue     Date: date
     * @ramlpath /{objectName}  head
     **/
    head(options?:ObjectNameResourceHeadOptions):Error
}
export interface _aclResource1 {

    /**
     *  This implementation of the GET operation uses the acl subresource to return the access control list (ACL) of an object. To use this operation, you must have READ_ACP access to the object. Syntax ------     GET /BucketName/ObjectName?acl HTTP/1.1     Host: s3.amazonaws.com     Date: date     Authorization: signatureValue     Range:bytes=byte_range
     * @ramlpath /{objectName}/?acl  get
     **/
    get(options?:_aclResource1GetOptions):Bucket_acl | Error


    /**
     *  This implementation of the PUT operation uses the acl subresource to set the access control list (ACL) permissions for an object that already exists in a bucket. You must have WRITE_ACP permission to set the ACL of an object. You can use one of the following two ways to set an object's permissions: * Specify the ACL in the request body, or * Specify permissions using request headers Depending on your application needs, you may choose to set the ACL on an object using either the request body or the headers. For example, if you have an existing application that updates an object ACL using the request body, then you can continue to use that approach. Syntax ------ With request body     PUT /BucketName/ObjectName?acl HTTP/1.1     Host: s3.amazonaws.com     Date: date     Authorization: signatureValue     <AccessControlPolicy>       <Owner>         <ID>ID</ID>         <DisplayName>EmailAddress</DisplayName>       </Owner>       <AccessControlList>         <Grant>           <Grantee xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:type="CanonicalUser">             <ID>ID</ID>             <DisplayName>EmailAddress</DisplayName>           </Grantee>           <Permission>Permission</Permission>         </Grant>       ...       </AccessControlList>     </AccessControlPolicy> With headers     PUT /BucketName/ObjectName?acl HTTP/1.1     Host: s3.amazonaws.com     Date: date     x-amz-grant-write: uri="uri1", emailAddress="email"     x-amz-grant-read: uri="uri2"     Authorization: signatureValue
     * @ramlpath /{objectName}/?acl  put
     **/
    put(payload:Bucket_grant_acl, options:_aclResource1PutOptions):Error
}
export interface _aclResource1GetOptions {

    /**
     *
     **/
        //header_Range
    header_Range?:string


    /**
     *
     **/
        //header_If-Modified-Since
        "header_If-Modified-Since"?:string


    /**
     *
     **/
        //header_If-Unmodified-Since
        "header_If-Unmodified-Since"?:string


    /**
     *
     **/
        //header_If-Match
        "header_If-Match"?:string


    /**
     *
     **/
        //header_If-None-Match
        "header_If-None-Match"?:string


    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export interface _aclResource1PutOptions {

    /**
     *
     **/
        //header_x-amz-version-id
        "header_x-amz-version-id"?:string


    /**
     *
     **/
        //header_Cache-Control
        "header_Cache-Control"?:string


    /**
     *
     **/
        //header_Content-Disposition
        "header_Content-Disposition"?:string


    /**
     *
     **/
        //header_Content-Encoding
        "header_Content-Encoding"?:string


    /**
     *
     **/
        //header_Content-Length
        "header_Content-Length":string


    /**
     *
     **/
        //header_Content-MD5
        "header_Content-MD5"?:string


    /**
     *
     **/
        //header_Expires
    header_Expires?:string


    /**
     *
     **/
        //header_x-amz-meta-{*}
        "header_x-amz-meta-{*}"?:string


    /**
     *
     **/
        //header_x-amz-server-side-encryption
        "header_x-amz-server-side-encryption"?:string


    /**
     *
     **/
        //header_x-amz-storage-class
        "header_x-amz-storage-class"?:string


    /**
     *
     **/
        //header_x-amz-website-redirect-location
        "header_x-amz-website-redirect-location"?:string


    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string


    /**
     *
     **/
        //header_x-amz-acl
        "header_x-amz-acl"?:string


    /**
     *
     **/
        //header_x-amz-grant-read
        "header_x-amz-grant-read"?:string


    /**
     *
     **/
        //header_x-amz-grant-write
        "header_x-amz-grant-write"?:string


    /**
     *
     **/
        //header_x-amz-grant-read-acp
        "header_x-amz-grant-read-acp"?:string


    /**
     *
     **/
        //header_x-amz-grant-write-acp
        "header_x-amz-grant-write-acp"?:string


    /**
     *
     **/
        //header_x-amz-grant-full-control
        "header_x-amz-grant-full-control"?:string
}
export interface _torrentResource {

    /**
     *  This implementation of the GET operation uses the torrent subresource to return torrent files from a bucket. BitTorrent can save you bandwidth when you're distributing large files. For more information about BitTorrent, see Amazon S3 Torrent. Note You can get torrent only for objects that are less than 5 GB in size and that are not encrypted using server-side encryption with customer-provided encryption key. To use GET, you must have READ access to the object.
     * @ramlpath /{objectName}/?torrent  get
     **/
    get(options?:_torrentResourceGetOptions):Error
}
export interface _torrentResourceGetOptions {

    /**
     *
     **/
        //header_Range
    header_Range?:string


    /**
     *
     **/
        //header_If-Modified-Since
        "header_If-Modified-Since"?:string


    /**
     *
     **/
        //header_If-Unmodified-Since
        "header_If-Unmodified-Since"?:string


    /**
     *
     **/
        //header_If-Match
        "header_If-Match"?:string


    /**
     *
     **/
        //header_If-None-Match
        "header_If-None-Match"?:string


    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _torrentResourceGetOptions1=_aclResource1GetOptions
export interface _restoreResource {

    /**
     *  Restores a temporary copy of an archived object. You can optionally provide version ID to restore specific object version. If version ID is not provided, it will restore the current version. In the request, you specify the number of days that you want the restored copy to exist. After the specified period, Amazon S3 deletes the temporary copy. Note that the object remains archived; Amazon S3 deletes only the restored copy. An object in the Glacier storage class is an archived object. To access the object, you must first initiate a restore request, which restores a copy of the archived object. Restore jobs typically complete in three to five hours. For more information about archiving objects, go to Object Lifecycle Management in Amazon Simple Storage Service Developer Guide. You can obtain restoration status by sending a HEAD request. In the response, these operations return the x-amz-restore header with restoration status information. After restoring an object copy, you can update the restoration period by reissuing this request with the new period. Amazon S3 updates the restoration period relative to the current time and charges only for the request, and there are no data transfer charges. You cannot issue another restore request when Amazon S3 is actively processing your first restore request for the same object; however, after Amazon S3 restores a copy of the object, you can send restore requests to update the expiration period of the restored object copy. If your bucket has a lifecycle configuration with a rule that includes an expiration action, the object expiration overrides the life span that you specify in a restore request. For example, if you restore an object copy for 10 days but the object is scheduled to expire in 3 days, Amazon S3 deletes the object in 3 days. For more information about lifecycle configuration, see PUT Bucket lifecycle. To use this action, you must have s3:RestoreObject permissions on the specified object. For more information, go to Access Control section in the Amazon S3 Developer Guide.
     * @ramlpath /{objectName}/?restore  post
     **/
    post(payload:ObjectRestore, options?:_restoreResourcePostOptions):Error
}
export interface _restoreResourcePostOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _restoreResourcePostOptions1=ApiDeleteOptions
export interface ObjectRestore {

    /**
     *
     **/
        //RestoreRequest
    RestoreRequest?:{

        /**
         *
         **/
            //Days
        Days?:number
    }
}
export interface _uploadsResource1 {

    /**
     *  This operation initiates a multipart upload and returns an upload ID. This upload ID is used to associate all the parts in the specific multipart upload. You specify this upload ID in each of your subsequent upload part requests (see Upload Part). You also include this upload ID in the final request to either complete or abort the multipart upload request. Note: After you initiate multipart upload and upload one or more parts, you must either complete or abort multipart upload in order to stop getting charged for storage of the uploaded parts. Only after you either complete or abort multipart upload, Amazon S3 frees up the parts storage and stops charging you for the parts storage.
     * @ramlpath /{objectName}/?uploads  post
     **/
    post(options?:_uploadsResource1PostOptions):Error | Post_uploadsXmlResponse
}
export interface _uploadsResource1PostOptions {

    /**
     *
     **/
        //header_Cache-Control
        "header_Cache-Control"?:string


    /**
     *
     **/
        //header_Content-Disposition
        "header_Content-Disposition"?:string


    /**
     *
     **/
        //header_Content-Encoding
        "header_Content-Encoding"?:string


    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expires
    header_Expires?:string


    /**
     *
     **/
        //header_x-amz-meta-
        "header_x-amz-meta-"?:string


    /**
     *
     **/
        //header_x-amz-server-side-encryption
        "header_x-amz-server-side-encryption"?:string


    /**
     *
     **/
        //header_x-amz-storage-class
        "header_x-amz-storage-class"?:string


    /**
     *
     **/
        //header_x-amz-website-redirect-location
        "header_x-amz-website-redirect-location"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export interface Post_uploadsXmlResponse {
}
export interface _partNumber_partNumber_uploadId_uploadIdResource {

    /**
     *  This operation uploads a part in a multipart upload. Note: In this operation you provide part data in your request. However, you have an option to specify your existing Amazon S3 object as data source for the part your are uploading. To upload a part from an existing object you use the Upload Part (Copy) operation. For more information, see Upload Part - Copy. You must initiate a multipart upload (see Initiate Multipart Upload) before you can upload any part. In response to your initiate request. Amazon S3 returns an upload ID, a unique identifier, that you must include in your upload part request. Part numbers can be any number from 1 to 10,000, inclusive. A part number uniquely identifies a part and also defines its position within the object being created. If you upload a new part using the same part number that was used with a previous part, the previously uploaded part is overwritten. Each part must be at least 5 MB in size, except the last part. There is no size limit on the last part of your multipart upload. To ensure that data is not corrupted when traversing the network, specify the Content-MD5 header in the upload part request. Amazon S3 checks the part data against the provided MD5 value. If they do not match, Amazon S3 returns an error. Note: After you initiate multipart upload and upload one or more parts, you must either complete or abort multipart upload in order to stop getting charged for storage of the uploaded parts. Only after you either complete or abort multipart upload, Amazon S3 frees up the parts storage and stops charging you for the parts storage.
     * @ramlpath /{objectName}/?partNumber={partNumber}&uploadId={uploadId}  put
     **/
    put(options?:_partNumber_partNumber_uploadId_uploadIdResourcePutOptions):Error
}
export interface _partNumber_partNumber_uploadId_uploadIdResourcePutOptions {

    /**
     *
     **/
        //header_Content-Length
        "header_Content-Length"?:string


    /**
     *
     **/
        //header_Content-MD5
        "header_Content-MD5"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_x-amz-copy-source
        "header_x-amz-copy-source"?:string


    /**
     *
     **/
        //header_x-amz-copy-source-range
        "header_x-amz-copy-source-range"?:string


    /**
     *
     **/
        //header_Cache-Control
        "header_Cache-Control"?:string


    /**
     *
     **/
        //header_Content-Disposition
        "header_Content-Disposition"?:string


    /**
     *
     **/
        //header_Content-Encoding
        "header_Content-Encoding"?:string


    /**
     *
     **/
        //header_Expires
    header_Expires?:string


    /**
     *
     **/
        //header_x-amz-meta-{*}
        "header_x-amz-meta-{*}"?:string


    /**
     *
     **/
        //header_x-amz-server-side-encryption
        "header_x-amz-server-side-encryption"?:string


    /**
     *
     **/
        //header_x-amz-storage-class
        "header_x-amz-storage-class"?:string


    /**
     *
     **/
        //header_x-amz-website-redirect-location
        "header_x-amz-website-redirect-location"?:string


    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export interface _uploadId_UploadIdResource {

    /**
     *  This operation lists the parts that have been uploaded for a specific multipart upload. This operation must include the upload ID, which you obtain by sending the initiate multipart upload request (see Initiate Multipart Upload). This request returns a maximum of 1,000 uploaded parts. The default number of parts returned is 1,000 parts. You can restrict the number of parts returned by specifying the max-parts request parameter. If your multipart upload consists of more than 1,000 parts, the response returns an IsTruncated field with the value of true, and a NextPartNumberMarker element. In subsequent List Parts requests you can include the part-number-marker query string parameter and set its value to the NextPartNumberMarker field value from the previous response. For more information on multipart uploads, go to Uploading Objects Using Multipart Upload in the Amazon Simple Storage Service Developer Guide . For information on permissions required to use the multipart upload API, go to Multipart Upload API and Permissions in the Amazon Simple Storage Service Developer Guide .
     * @ramlpath /{objectName}/?uploadId={UploadId}  get
     **/
    get(options?:_uploadId_UploadIdResourceGetOptions):UpladParts | Error


    /**
     *  This operation completes a multipart upload by assembling previously uploaded parts.
     * @ramlpath /{objectName}/?uploadId={UploadId}  post
     **/
    post(payload:Complete_multipart_upload, options?:_uploadId_UploadIdResourcePostOptions):Error | Post_uploadid__uploadid_XmlResponse


    /**
     *  This operation aborts a multipart upload. After a multipart upload is aborted, no additional parts can be uploaded using that upload ID. The storage consumed by any previously uploaded parts will be freed. However, if any part uploads are currently in progress, those part uploads might or might not succeed. As a result, it might be necessary to abort a given multipart upload multiple times in order to completely free all storage consumed by all parts. To verify that all parts have been removed, so you don't get charged for the part storage, you should call the List Parts operation and ensure the parts list is empty.
     * @ramlpath /{objectName}/?uploadId={UploadId}  delete
     **/
    "delete"(options?:_uploadId_UploadIdResourceDeleteOptions):Error
}
export interface _uploadId_UploadIdResourceGetOptions {

    /**
     *
     **/
        //header_Range
    header_Range?:string


    /**
     *
     **/
        //header_If-Modified-Since
        "header_If-Modified-Since"?:string


    /**
     *
     **/
        //header_If-Unmodified-Since
        "header_If-Unmodified-Since"?:string


    /**
     *
     **/
        //header_If-Match
        "header_If-Match"?:string


    /**
     *
     **/
        //header_If-None-Match
        "header_If-None-Match"?:string


    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _uploadId_UploadIdResourceGetOptions1=_aclResource1GetOptions
export interface UpladParts {

    /**
     *
     **/
        //ListPartsResult
    ListPartsResult?:{

        /**
         *
         **/
            //Bucket
        Bucket?:string


        /**
         *
         **/
            //Key
        Key?:string


        /**
         *
         **/
            //UploadId
        UploadId?:string


        /**
         *
         **/
            //Initiator
        Initiator?:{

            /**
             *
             **/
                //ID
            ID?:string


            /**
             *
             **/
                //DisplayName
            DisplayName?:string
        }


        /**
         *
         **/
            //Owner
        Owner?:{

            /**
             *
             **/
                //ID
            ID?:string


            /**
             *
             **/
                //DisplayName
            DisplayName?:string
        }


        /**
         *
         **/
            //StorageClass
        StorageClass?:string


        /**
         *
         **/
            //PartNumberMarker
        PartNumberMarker?:number


        /**
         *
         **/
            //NextPartNumberMarker
        NextPartNumberMarker?:number


        /**
         *
         **/
            //MaxParts
        MaxParts?:number


        /**
         *
         **/
            //IsTruncated
        IsTruncated?:boolean


        /**
         *
         **/
            //Part
        Part?:{

            /**
             *
             **/
                //PartNumber
            PartNumber?:number


            /**
             *
             **/
                //LastModified
            LastModified?:string


            /**
             *
             **/
                //ETag
            ETag?:string


            /**
             *
             **/
                //Size
            Size?:number
        }[]
    }


    /**
     *
     **/
        //__$harEntry__
    __$harEntry__?:HarEntry
}
export interface _uploadId_UploadIdResourcePostOptions {

    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type _uploadId_UploadIdResourcePostOptions1=ApiDeleteOptions
export interface Post_uploadid__uploadid_XmlResponse {
}
export interface Complete_multipart_upload {

    /**
     *
     **/
        //CompleteMultipartUpload
    CompleteMultipartUpload?:{

        /**
         *
         **/
            //Part
        Part?:{

            /**
             *
             **/
                //PartNumber
            PartNumber?:number


            /**
             *
             **/
                //ETag
            ETag?:string
        }[]
    }
}
export interface _uploadId_UploadIdResourceDeleteOptions {

    /**
     *
     **/
        //header_x-amz-mfa
        "header_x-amz-mfa"?:string


    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export interface ObjectNameResourceGetOptions {

    /**
     *
     **/
        //response-content-type
        "response-content-type"?:string


    /**
     *
     **/
        //response-content-language
        "response-content-language"?:string


    /**
     *
     **/
        //response-expires
        "response-expires"?:string


    /**
     *
     **/
        //response-cache-control
        "response-cache-control"?:string


    /**
     *
     **/
        //response-content-disposition
        "response-content-disposition"?:string


    /**
     *
     **/
        //response-content-encoding
        "response-content-encoding"?:string


    /**
     *
     **/
        //header_Range
    header_Range?:string


    /**
     *
     **/
        //header_If-Modified-Since
        "header_If-Modified-Since"?:string


    /**
     *
     **/
        //header_If-Unmodified-Since
        "header_If-Unmodified-Since"?:string


    /**
     *
     **/
        //header_If-Match
        "header_If-Match"?:string


    /**
     *
     **/
        //header_If-None-Match
        "header_If-None-Match"?:string


    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export interface ObjectNameResourcePutOptions {

    /**
     *
     **/
        //header_Cache-Control
        "header_Cache-Control"?:string


    /**
     *
     **/
        //header_Content-Disposition
        "header_Content-Disposition"?:string


    /**
     *
     **/
        //header_Content-Encoding
        "header_Content-Encoding"?:string


    /**
     *
     **/
        //header_Content-Length
        "header_Content-Length":string


    /**
     *
     **/
        //header_Content-MD5
        "header_Content-MD5"?:string


    /**
     *
     **/
        //header_Expires
    header_Expires?:string


    /**
     *
     **/
        //header_x-amz-meta-{*}
        "header_x-amz-meta-{*}"?:string


    /**
     *
     **/
        //header_x-amz-server-side-encryption
        "header_x-amz-server-side-encryption"?:string


    /**
     *
     **/
        //header_x-amz-storage-class
        "header_x-amz-storage-class"?:string


    /**
     *
     **/
        //header_x-amz-website-redirect-location
        "header_x-amz-website-redirect-location"?:string


    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string


    /**
     *
     **/
        //header_x-amz-acl
        "header_x-amz-acl"?:string


    /**
     *
     **/
        //header_x-amz-grant-read
        "header_x-amz-grant-read"?:string


    /**
     *
     **/
        //header_x-amz-grant-write
        "header_x-amz-grant-write"?:string


    /**
     *
     **/
        //header_x-amz-grant-read-acp
        "header_x-amz-grant-read-acp"?:string


    /**
     *
     **/
        //header_x-amz-grant-write-acp
        "header_x-amz-grant-write-acp"?:string


    /**
     *
     **/
        //header_x-amz-grant-full-control
        "header_x-amz-grant-full-control"?:string
}
export interface ObjectNameResourceDeleteOptions {

    /**
     *
     **/
        //header_x-amz-mfa
        "header_x-amz-mfa"?:string


    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type ObjectNameResourceDeleteOptions1=_uploadId_UploadIdResourceDeleteOptions
export interface ObjectNameResourceHeadOptions {

    /**
     *
     **/
        //header_Range
    header_Range?:string


    /**
     *
     **/
        //header_If-Modified-Since
        "header_If-Modified-Since"?:string


    /**
     *
     **/
        //header_If-Unmodified-Since
        "header_If-Unmodified-Since"?:string


    /**
     *
     **/
        //header_If-Match
        "header_If-Match"?:string


    /**
     *
     **/
        //header_If-None-Match
        "header_If-None-Match"?:string


    /**
     *
     **/
        //header_Content-Type
        "header_Content-Type"?:string


    /**
     *
     **/
        //header_Expect
    header_Expect?:string


    /**
     *
     **/
        //header_Host
    header_Host?:string


    /**
     *
     **/
        //header_x-amz-security-token
        "header_x-amz-security-token"?:string
}
export type ObjectNameResourceHeadOptions1=_aclResource1GetOptions


export interface UnknownResponse { __$harEntry__ : har.Entry
}
export interface payloadType {
}
export interface responseType {
}
export interface invoker { (url:String, method:string, options:any):any;
}
export class ApiImpl implements Api {
    private baseUrl:string = 'https://{bucketName}.{region}.amazonaws.com'
    private cfgEncoded = /*CONFIGENCODEDSTART*/{
        "numberIsString": true,
        "createTypesForResources": true,
        "queryParametersSecond": true,
        "collapseGet": false,
        "collapseOneMethod": false,
        "collapseMediaTypes": false,
        "methodNamesAsPrefixes": false,
        "storeHarEntry": true,
        "createTypesForParameters": true,
        "reuseTypeForParameters": true,
        "createTypesForSchemaElements": true,
        "reuseTypesForSchemaElements": true,
        "throwExceptionOnIncorrectStatus": false,
        "async": false,
        "debugOptions": {
            "generateImplementation": true,
            "generateSchemas": false,
            "generateInterface": true,
            "resourcePathFilter": null
        },
        "overwriteModules": true
    };
    /*CONFIGENCODEDEND*/

    declaration():RamlWrapper.Api {
        var unit = new JsonModel.CompilationUnit(null, null, apiProvider.api(), null, true);
        var highLevelNode:highLevel.IHighLevelNode = new highLevelImpl.ASTNodeImpl(unit.ast(), null, <any>apiType, null);
        var api:RamlWrapper.Api = new RamlWrapper.ApiImpl(highLevelNode);
        endpoints.setApi(api);
        return api;
    }

    securityProvider():authManager.SecurityParametersProvider {
        var api:RamlWrapper.Api = this.declaration();
        return env.getSecurityProvider().getSubProvider(api.title()).getSubProvider(api.version());
    }

    authentificate(schemaName:string, options?:any) {
    }

    log(vName:string, val:any) {
        this.inv.log(vName, val);
        return val;
    }

    baseUrlResolved():string {
        var burl = this.baseUrl;
        burl = burl.replace('{bucketName}', this.bucketName)
        burl = burl.replace('{region}', this.region)

        return burl;
    }

    private inv:executor.APIExecutor
    private options:any
    private canonicPath:string[] = []

    invoke(path:string, method:string, canonicPath:string[], obj:any) {
        env.registerApi(this.declaration())
        env.getAuthManager().registerSchemes(this.declaration(), [<any>securityScript0.createSchema('sign')])
        return this.inv.execute(path, method, obj, canonicPath)
    }

    authenticate(schemaName?:string, options?:any):any {
        return null;
    }

    constructor(private bucketName:string, private region:string) {
        this.inv = new executor.APIExecutor(this.declaration(), this.baseUrlResolved(), <any>this.cfgEncoded);

    }

    get = (options?:ApiGetOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/`, 'get', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*get*/
    }
    post = (payload:PostFormBody, options:ApiPostOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/`, 'post', this.canonicPath, {
                "payload": payload
                , "options": options
            });
        return res;
        /*d*post*/
    }
    put = (payload:Bucket, options:ApiPutOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/`, 'put', this.canonicPath, {
                "payload": payload
                , "options": options
            });
        return res;
        /*d*put*/
    }
    delete = (options?:ApiDeleteOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/`, 'delete', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*delete*/
    }
    head = (options?:ApiHeadOptions)=> {
        var res = <any>
            this.invoke(`https://${this.bucketName}.${this.region}.amazonaws.com/`, 'head', this.canonicPath, {
                "options": options
            });
        return res;
        /*d*head*/
    }
    _cors = new _corsResourceImpl(this.bucketName, this.region, this, this.canonicPath.concat('/?cors'))
    _lifecycle = new _lifecycleResourceImpl(this.bucketName, this.region, this, this.canonicPath.concat('/?lifecycle'))
    _policy = new _policyResourceImpl(this.bucketName, this.region, this, this.canonicPath.concat('/?policy'))
    _tagging = new _taggingResourceImpl(this.bucketName, this.region, this, this.canonicPath.concat('/?tagging'))
    _website = new _websiteResourceImpl(this.bucketName, this.region, this, this.canonicPath.concat('/?website'))
    _versions = new _versionsResourceImpl(this.bucketName, this.region, this, this.canonicPath.concat('/?versions'))
    _acl = new _aclResourceImpl(this.bucketName, this.region, this, this.canonicPath.concat('/?acl'))
    _versioning = new _versioningResourceImpl(this.bucketName, this.region, this, this.canonicPath.concat('/?versioning'))
    _delete = new _deleteResourceImpl(this.bucketName, this.region, this, this.canonicPath.concat('/?delete'))
    _location = new _locationResourceImpl(this.bucketName, this.region, this, this.canonicPath.concat('/?location'))
    _logging = new _loggingResourceImpl(this.bucketName, this.region, this, this.canonicPath.concat('/?logging'))
    _notification = new _notificationResourceImpl(this.bucketName, this.region, this, this.canonicPath.concat('/?notification'))
    _requestPayment = new _requestPaymentResourceImpl(this.bucketName, this.region, this, this.canonicPath.concat('/?requestPayment'))
    _uploads = new _uploadsResourceImpl(this.bucketName, this.region, this, this.canonicPath.concat('/?uploads'))
    objectName = (objectName:string)=> {
        var res = <any>
            new ObjectNameResourceImpl(this.bucketName, this.region, objectName, this, this.canonicPath.concat('/{objectName}'))
        return res;
        /*d*objectName*/
    }

    /* type ending */
}


var meta = {}
