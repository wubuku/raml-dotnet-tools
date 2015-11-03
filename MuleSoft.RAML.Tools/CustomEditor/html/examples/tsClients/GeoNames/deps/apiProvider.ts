/// <reference path="./typings/tsd.d.ts" />
var apiEncoded = require('../apiEncoded.json')

export function api():string{
    return JSON.stringify(apiEncoded);
}
    