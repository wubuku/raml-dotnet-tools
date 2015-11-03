/// <reference path="./deps/typings/tsd.d.ts" />
import Amazon = require('./client')

var amazon = Amazon.createApi('NotebookTestBucket'/* + new Date().getTime()*/, 's3');

amazon.securityProvider().writeValue('ACCESS_KEY','Your Access key here');
amazon.securityProvider().writeValue('SECRET_KEY','Your Secret Key here');

var createBucket = amazon.put(null,null);
console.log(JSON.stringify(createBucket,null,2));

var bucket:Amazon.Bucket_conf_website= {
    'WebsiteConfiguration': {
        '@xmlns': 'http://s3.amazonaws.com/doc/2006-03-01/',
        'IndexDocument': {
            'Suffix': 'index.html'
        },
        'ErrorDocument': {
            'Key': '404.html'
        }
    }
};

var configWebSite = amazon._website.put(bucket);
console.log(JSON.stringify(configWebSite,null,2));

var versions = amazon._versions.get();
console.log(JSON.stringify(versions,null,2));