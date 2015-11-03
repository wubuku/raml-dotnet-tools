/// <reference path="./deps/typings/tsd.d.ts" />
import GeoNames = require('./client')

var geonames = GeoNames.createApi();

geonames.securityProvider().getSubProvider('usernameSchema').writeValue('username','demo');


var postalCodeSearchResponse = geonames.postalCodeSearch('JSON').get({
    postalcode: "10003"
});

console.log(postalCodeSearchResponse,null,2);


var extendedFindNearbyResponse = geonames.extendedFindNearby.get({
    lat: 47.3,
    lng: 9,
    'header_Content-Type': 'application/json'
});

console.log(extendedFindNearbyResponse,null,2);


var astergdemResponse = geonames.astergdem('JSON').get({
    lat: 50.01,
    lng: 10.2
});

console.log(astergdemResponse,null,2);


var childrenResponse = geonames.children('JSON').get({
    geonameId: '2593110',
    hierarchy: 'tourism'
});

console.log(childrenResponse,null,2);

