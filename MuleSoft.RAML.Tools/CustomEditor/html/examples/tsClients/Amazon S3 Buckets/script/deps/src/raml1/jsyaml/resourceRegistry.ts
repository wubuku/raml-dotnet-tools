/// <reference path="../../../typings/tsd.d.ts" />


var spawnSync = require('child_process').spawnSync || require('spawn-sync');
var HttpResponse = require('http-response-object');
require('concat-stream');
require('then-request');
import  lru=require("lrucache")

var globalCache=lru(50);

//Function('', fs.readFileSync(require.resolve('./lib/worker.js'), 'utf8'));

function doRequest(method, url, options) {
    var req = JSON.stringify({
        method: method,
        url: url,
        options: options
    });
    var res = spawnSync('/usr/local/bin/node', [require.resolve('./worker.js')], {input: req});
    if (!res){
        return null;
    }
    if (res.status !== 0) {
        throw new Error(res.stderr.toString());
    }
    if (res.error) {
        if (typeof res.error === 'string') res.error = new Error(res.error);
        throw res.error;
    }
    var response = JSON.parse(res.stdout);
    if (response.success) {
        return new HttpResponse(response.response.statusCode, response.response.headers, response.response.body);
    } else {
        throw new Error(response.error.message || response.error || response);
    }
}
export function readFromCacheOrGet(url:string){
    var res=globalCache.get(url);
    if (res){
        if (res==readFromCacheOrGet){
            return null;
        }
        return res;
    }
    try {
        var res = doRequest("GET", url, {timeout: 3000, socketTimeout: 5000, retry: true});
        res = new Buffer(res.body.data).toString();
        globalCache.set(url, res);
        return res;
    } catch (e){
        globalCache.set(url,readFromCacheOrGet);
        return null;
    }
}