export function createApi():Api{
    return new ApiImpl();
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



var universe10 = require("./deps/src/raml1/universeProvider")("RAML10");
var apiType = universe10.type("Api")

env.setPath(__dirname);
env.getReportManager().setLogPath(__dirname);


class PostalCodeSearchResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:PostalCodeSearchResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/postalCodeSearch${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class ExtendedFindNearbyResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:ExtendedFindNearbyResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/extendedFindNearby`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class AstergdemResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:AstergdemResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/astergdem${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}
post=( options?:AstergdemResourcePostOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/astergdem${this.mediaTypeSuffix}`,'post',this.canonicPath,{
"options":options
});
return res;/*d*post*/}

 /* type ending */ }
class ChildrenResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:ChildrenResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/children${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class CitiesResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:CitiesResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/cities${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class FindNearbyPostalCodesResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:FindNearbyPostalCodesResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/findNearbyPostalCodes${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class FindNearbyPlaceNameResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:FindNearbyPlaceNameResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/findNearbyPlaceName${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class FindNearbyResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:FindNearbyResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/findNearby${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class HierarchyResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:HierarchyResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/hierarchy${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class ContainsResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:ContainsResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/contains${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class SiblingsResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:SiblingsResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/siblings${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class FindNearbyWikipediaResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:FindNearbyWikipediaResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/findNearbyWikipedia${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class WikipediaSearchResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:WikipediaSearchResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/wikipediaSearch${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class WikipediaBoundingBoxResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:WikipediaBoundingBoxResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/wikipediaBoundingBox${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class FindNearByWeatherResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:FindNearByWeatherResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/findNearByWeather${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class FindNearestAddressResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get:FindNearestAddress | FindNearestAddress_xml
                
 /* type ending */ }
class FindNearestIntersectionResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:FindNearestIntersectionResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/findNearestIntersection${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class FindNearbyStreetsResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:FindNearbyStreetsResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/findNearbyStreets${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class FindNearestIntersectionOSMResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:FindNearestIntersectionOSMResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/findNearestIntersectionOSM${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class FindNearbyStreetsOSMResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:FindNearbyStreetsOSMResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/findNearbyStreetsOSM${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class FindNearbyPOIsOSMResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:FindNearbyPOIsOSMResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/findNearbyPOIsOSM${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class CountrySubdivisionResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:CountrySubdivisionResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/countrySubdivision${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class OceanResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:OceanResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/ocean${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class NeighbourhoodResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:NeighbourhoodResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/neighbourhood${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class TimezoneResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:TimezoneResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/timezone${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class Gtopo30ResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:Gtopo30ResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/gtopo30${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class CountryInfoResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:CountryInfoResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/countryInfo${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class SearchResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options:SearchResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/search${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class CountryCodeResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:CountryCodeResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/countryCode${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class EarthquakesResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:EarthquakesResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/earthquakes${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class GetResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:GetResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/get${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class NeighboursResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:NeighboursResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/neighbours${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class PostalCodeCountryInfoResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get:PostalCodeCountryInfo | CountryInfo_xml
                
 /* type ending */ }
class Srtm3ResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:Srtm3ResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/srtm3${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}
post=( options?:Srtm3ResourcePostOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/srtm3${this.mediaTypeSuffix}`,'post',this.canonicPath,{
"options":options
});
return res;/*d*post*/}

 /* type ending */ }
class WeatherResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:WeatherResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/weather${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class WeatherIcaoResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:WeatherIcaoResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/weatherIcao${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class RssToGeoRSSResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options:RssToGeoRSSResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/rssToGeoRSS`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
class PostalCodeLookupResourceImpl

{


            invoke(path:string,method:string,canonicPath:string[],obj:any){
                return this._parent.invoke(path,method,canonicPath,obj);
            }


            constructor(private mediaTypeSuffix, 
                private _parent:{invoke(path:string,method:string,canonicPath:string[],obj:any):void},
                private canonicPath:string[]){
                
            }
get=( options?:PostalCodeLookupResourceGetOptions )=>{
var res=<any> 
this.invoke(`http://api.geonames.org/postalCodeLookup${this.mediaTypeSuffix}`,'get',this.canonicPath,{
"options":options
});
return res;/*d*get*/}

 /* type ending */ }
export interface Api{
declaration():RamlWrapper.Api;
securityProvider():authManager.SecurityParametersProvider;
authenticate(schemaName?:string,options?:any):any;
log(vName:string,val:any)

        /**
         *  
         * @ramlpath /postalCodeSearch{mediaTypeSuffix}
         **/


         postalCodeSearch( mediaTypeSuffix?:string ):PostalCodeSearchResource


        /**
         *  
         * @ramlpath /extendedFindNearby
         **/


         extendedFindNearby:ExtendedFindNearbyResource


        /**
         *  
         * @ramlpath /astergdem{mediaTypeSuffix}
         **/


         astergdem( mediaTypeSuffix?:string ):AstergdemResource


        /**
         *  
         * @ramlpath /children{mediaTypeSuffix}
         **/


         children( mediaTypeSuffix?:string ):ChildrenResource


        /**
         *  
         * @ramlpath /cities{mediaTypeSuffix}
         **/


         cities( mediaTypeSuffix?:string ):CitiesResource


        /**
         *  
         * @ramlpath /findNearbyPostalCodes{mediaTypeSuffix}
         **/


         findNearbyPostalCodes( mediaTypeSuffix?:string ):FindNearbyPostalCodesResource


        /**
         *  
         * @ramlpath /findNearbyPlaceName{mediaTypeSuffix}
         **/


         findNearbyPlaceName( mediaTypeSuffix?:string ):FindNearbyPlaceNameResource


        /**
         *  
         * @ramlpath /findNearby{mediaTypeSuffix}
         **/


         findNearby( mediaTypeSuffix?:string ):FindNearbyResource


        /**
         *  
         * @ramlpath /hierarchy{mediaTypeSuffix}
         **/


         hierarchy( mediaTypeSuffix?:string ):HierarchyResource


        /**
         *  
         * @ramlpath /contains{mediaTypeSuffix}
         **/


         contains( mediaTypeSuffix?:string ):ContainsResource


        /**
         *  
         * @ramlpath /siblings{mediaTypeSuffix}
         **/


         siblings( mediaTypeSuffix?:string ):SiblingsResource


        /**
         *  
         * @ramlpath /findNearbyWikipedia{mediaTypeSuffix}
         **/


         findNearbyWikipedia( mediaTypeSuffix?:string ):FindNearbyWikipediaResource


        /**
         *  
         * @ramlpath /wikipediaSearch{mediaTypeSuffix}
         **/


         wikipediaSearch( mediaTypeSuffix?:string ):WikipediaSearchResource


        /**
         *  
         * @ramlpath /wikipediaBoundingBox{mediaTypeSuffix}
         **/


         wikipediaBoundingBox( mediaTypeSuffix?:string ):WikipediaBoundingBoxResource


        /**
         *  
         * @ramlpath /findNearByWeather{mediaTypeSuffix}
         **/


         findNearByWeather( mediaTypeSuffix?:string ):FindNearByWeatherResource


        /**
         *  
         * @ramlpath /findNearestAddress{mediaTypeSuffix}
         **/


         findNearestAddress( mediaTypeSuffix?:string ):FindNearestAddressResource


        /**
         *  
         * @ramlpath /findNearestIntersection{mediaTypeSuffix}
         **/


         findNearestIntersection( mediaTypeSuffix?:string ):FindNearestIntersectionResource


        /**
         *  
         * @ramlpath /findNearbyStreets{mediaTypeSuffix}
         **/


         findNearbyStreets( mediaTypeSuffix?:string ):FindNearbyStreetsResource


        /**
         *  
         * @ramlpath /findNearestIntersectionOSM{mediaTypeSuffix}
         **/


         findNearestIntersectionOSM( mediaTypeSuffix?:string ):FindNearestIntersectionOSMResource


        /**
         *  
         * @ramlpath /findNearbyStreetsOSM{mediaTypeSuffix}
         **/


         findNearbyStreetsOSM( mediaTypeSuffix?:string ):FindNearbyStreetsOSMResource


        /**
         *  
         * @ramlpath /findNearbyPOIsOSM{mediaTypeSuffix}
         **/


         findNearbyPOIsOSM( mediaTypeSuffix?:string ):FindNearbyPOIsOSMResource


        /**
         *  
         * @ramlpath /countrySubdivision{mediaTypeSuffix}
         **/


         countrySubdivision( mediaTypeSuffix?:string ):CountrySubdivisionResource


        /**
         *  
         * @ramlpath /ocean{mediaTypeSuffix}
         **/


         ocean( mediaTypeSuffix?:string ):OceanResource


        /**
         *  
         * @ramlpath /neighbourhood{mediaTypeSuffix}
         **/


         neighbourhood( mediaTypeSuffix?:string ):NeighbourhoodResource


        /**
         *  
         * @ramlpath /timezone{mediaTypeSuffix}
         **/


         timezone( mediaTypeSuffix?:string ):TimezoneResource


        /**
         *  
         * @ramlpath /gtopo30{mediaTypeSuffix}
         **/


         gtopo30( mediaTypeSuffix?:string ):Gtopo30Resource


        /**
         *  
         * @ramlpath /countryInfo{mediaTypeSuffix}
         **/


         countryInfo( mediaTypeSuffix?:string ):CountryInfoResource


        /**
         *  
         * @ramlpath /search{mediaTypeSuffix}
         **/


         search( mediaTypeSuffix?:string ):SearchResource


        /**
         *  
         * @ramlpath /countryCode{mediaTypeSuffix}
         **/


         countryCode( mediaTypeSuffix?:string ):CountryCodeResource


        /**
         *  
         * @ramlpath /earthquakes{mediaTypeSuffix}
         **/


         earthquakes( mediaTypeSuffix?:string ):EarthquakesResource


        /**
         *  
         * @ramlpath /get{mediaTypeSuffix}
         **/


         get( mediaTypeSuffix?:string ):GetResource


        /**
         *  
         * @ramlpath /neighbours{mediaTypeSuffix}
         **/


         neighbours( mediaTypeSuffix?:string ):NeighboursResource


        /**
         *  
         * @ramlpath /postalCodeCountryInfo{mediaTypeSuffix}
         **/


         postalCodeCountryInfo( mediaTypeSuffix?:string ):PostalCodeCountryInfoResource


        /**
         *  
         * @ramlpath /srtm3{mediaTypeSuffix}
         **/


         srtm3( mediaTypeSuffix?:string ):Srtm3Resource


        /**
         *  
         * @ramlpath /weather{mediaTypeSuffix}
         **/


         weather( mediaTypeSuffix?:string ):WeatherResource


        /**
         *  
         * @ramlpath /weatherIcao{mediaTypeSuffix}
         **/


         weatherIcao( mediaTypeSuffix?:string ):WeatherIcaoResource


        /**
         *  
         * @ramlpath /rssToGeoRSS
         **/


         rssToGeoRSS:RssToGeoRSSResource


        /**
         *  
         * @ramlpath /postalCodeLookup{mediaTypeSuffix}
         **/


         postalCodeLookup( mediaTypeSuffix?:string ):PostalCodeLookupResource
}
export interface PostalCodeSearchResource{

        /**
         *  Webservice for the GeoNames full text search in xml and json format.  Returns a list of postal codes and places for the placename/postalcode query as xml document  For the US the first returned zip code is determined using zip code area shapes, the following zip codes are based on the centroid. For all other supported countries all returned postal codes are based on centroids.
         * @ramlpath /postalCodeSearch{mediaTypeSuffix}  get
         **/
         get( options?:PostalCodeSearchResourceGetOptions ):Postal_codes | Postal_codes_xml
}
export interface PostalCodeSearchResourceGetOptions{

        /**
         *
         **/
         //postalcode
         postalcode?:string


        /**
         *
         **/
         //postalcode_startsWith
         postalcode_startsWith?:string


        /**
         *
         **/
         //placename
         placename?:string


        /**
         *
         **/
         //placename_startsWith  
         placename_startsWith  ?:string


        /**
         *
         **/
         //country
         country?:string


        /**
         *
         **/
         //countryBias
         countryBias?:string


        /**
         *
         **/
         //maxRows
         maxRows?:number


        /**
         *
         **/
         //operator
         operator?:string


        /**
         *
         **/
         //isReduced
         isReduced?:boolean


        /**
         *
         **/
         //east
         east?:number


        /**
         *
         **/
         //west
         west?:number


        /**
         *
         **/
         //north
         north?:string


        /**
         *
         **/
         //south
         south?:number


        /**
         *
         **/
         //style
         style?:string
}
export interface Postal_codes{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //parsererror
         parsererror?:{

        /**
         *
         **/
         //-style
         "-style"?:string


        /**
         *
         **/
         //h3
         h3?:string[]


        /**
         *
         **/
         //div
         div?:{

        /**
         *
         **/
         //-style
         "-style"?:string


        /**
         *
         **/
         //#text
         "#text"?:string
}
}


        /**
         *
         **/
         //totalResultsCount
         totalResultsCount?:string


        /**
         *
         **/
         //code
         code?:{

        /**
         *
         **/
         //postalcode
         postalcode?:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //lat
         lat?:string


        /**
         *
         **/
         //lng
         lng?:string


        /**
         *
         **/
         //adminCode1
         adminCode1?:string


        /**
         *
         **/
         //adminName1
         adminName1?:string


        /**
         *
         **/
         //adminCode2
         adminCode2?:string


        /**
         *
         **/
         //adminName2
         adminName2?:string


        /**
         *
         **/
         //adminCode3
         adminCode3?:string


        /**
         *
         **/
         //adminName3
         adminName3?:string
} | {

        /**
         *
         **/
         //postalcode
         postalcode?:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //lat
         lat?:string


        /**
         *
         **/
         //lng
         lng?:string


        /**
         *
         **/
         //adminCode1
         adminCode1?:string


        /**
         *
         **/
         //adminName1
         adminName1?:string
}[]
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface HarEntry{

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
export interface Postal_codes_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //totalResultsCount
         totalResultsCount?:number


        /**
         *
         **/
         //code
         code?:{

        /**
         *
         **/
         //postalcode
         postalcode?:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //adminCode1
         adminCode1?:{}


        /**
         *
         **/
         //adminName1
         adminName1?:string


        /**
         *
         **/
         //adminCode2
         adminCode2?:string


        /**
         *
         **/
         //adminName2
         adminName2?:string


        /**
         *
         **/
         //adminCode3
         adminCode3?:string


        /**
         *
         **/
         //adminName3
         adminName3?:string
}[]
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface ExtendedFindNearbyResource{

        /**
         *  Result : returns the most detailed information available for the lat/lng query as xml document  It is a combination of several services.
         * @ramlpath /extendedFindNearby  get
         **/
         get( options?:ExtendedFindNearbyResourceGetOptions ):ExtendedFindNearby_xml
}
export interface ExtendedFindNearbyResourceGetOptions{

        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number
}
export interface ExtendedFindNearby_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //geoname
         geoname?:{

        /**
         *
         **/
         //toponymName
         toponymName?:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //geonameId
         geonameId?:number


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //fcl
         fcl?:string


        /**
         *
         **/
         //fcode
         fcode?:string
}[]
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface AstergdemResource{

        /**
         *   Result : a single number giving the elevation in meters according to aster gdem, ocean areas have been masked as "no data" and have been assigned a value of -9999 
         * @ramlpath /astergdem{mediaTypeSuffix}  get
         **/
         get( options?:AstergdemResourceGetOptions ):Astergdem | Astergdem_xml


        /**
         *   Result : a single number giving the elevation in meters according to aster gdem, ocean areas have been masked as "no data" and have been assigned a value of -9999 
         * @ramlpath /astergdem{mediaTypeSuffix}  post
         **/
         post( options?:AstergdemResourcePostOptions ):Astergdem | Astergdem_xml
}
export interface AstergdemResourceGetOptions{

        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number
}
export type AstergdemResourceGetOptions1=ExtendedFindNearbyResourceGetOptions
export interface Astergdem{

        /**
         *
         **/
         //astergdem
         astergdem?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface Astergdem_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //astergdem
         astergdem?:number


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface AstergdemResourcePostOptions{

        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number
}
export type AstergdemResourcePostOptions1=ExtendedFindNearbyResourceGetOptions
export interface ChildrenResource{

        /**
         *  Result: returns a list of GeoName records 
         * @ramlpath /children{mediaTypeSuffix}  get
         **/
         get( options?:ChildrenResourceGetOptions ):Records | Records_xml
}
export interface ChildrenResourceGetOptions{

        /**
         *
         **/
         //geonameId
         geonameId?:string


        /**
         *
         **/
         //maxRows
         maxRows?:number


        /**
         *
         **/
         //hierarchy
         hierarchy?:string
}
export interface Records{

        /**
         *
         **/
         //totalResultsCount
         totalResultsCount?:number


        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //countryId
         countryId?:string


        /**
         *
         **/
         //adminCode1
         adminCode1?:string


        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //fclName
         fclName?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //lng
         lng?:string


        /**
         *
         **/
         //fcodeName
         fcodeName?:string


        /**
         *
         **/
         //toponymName
         toponymName?:string


        /**
         *
         **/
         //fcl
         fcl?:string


        /**
         *
         **/
         //numberOfChildren
         numberOfChildren?:number


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //fcode
         fcode?:string


        /**
         *
         **/
         //geonameId
         geonameId?:number


        /**
         *
         **/
         //lat
         lat?:string


        /**
         *
         **/
         //adminName1
         adminName1?:string


        /**
         *
         **/
         //population
         population?:number
}[]


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface Records_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //totalResultsCount
         totalResultsCount?:number


        /**
         *
         **/
         //geoname
         geoname?:{

        /**
         *
         **/
         //toponymName
         toponymName?:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //geonameId
         geonameId?:number


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //fcl
         fcl?:string


        /**
         *
         **/
         //fcode
         fcode?:string


        /**
         *
         **/
         //numberOfChildren
         numberOfChildren?:number
}[]


        /**
         *
         **/
         //@style
         "@style":string
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface CitiesResource{

        /**
         *  Result : returns a list of cities and placenames in the bounding box, ordered by relevancy (capital/population). Placenames close together are filterered out and only the larger name is included in the resulting list.
         * @ramlpath /cities{mediaTypeSuffix}  get
         **/
         get( options?:CitiesResourceGetOptions ):Cities | Cities_xml
}
export interface CitiesResourceGetOptions{

        /**
         *
         **/
         //callback 
         callback ?:string


        /**
         *
         **/
         //lang 
         lang ?:string


        /**
         *
         **/
         //maxRows 
         maxRows ?:number


        /**
         *
         **/
         //east
         east?:number


        /**
         *
         **/
         //west
         west?:number


        /**
         *
         **/
         //north
         north?:string


        /**
         *
         **/
         //south
         south?:number
}
export interface Cities{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //fcodeName
         fcodeName?:string


        /**
         *
         **/
         //toponymName
         toponymName?:string


        /**
         *
         **/
         //countrycode
         countrycode?:string


        /**
         *
         **/
         //fcl
         fcl?:string


        /**
         *
         **/
         //fclName
         fclName?:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //wikipedia
         wikipedia?:string


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //fcode
         fcode?:string


        /**
         *
         **/
         //geonameId
         geonameId?:number


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //population
         population?:number
}[]


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface Cities_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //geoname
         geoname?:{

        /**
         *
         **/
         //toponymName
         toponymName?:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //geonameId
         geonameId?:number


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //fcl
         fcl?:string


        /**
         *
         **/
         //fcode
         fcode?:string
}[]
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface FindNearbyPostalCodesResource{

        /**
         *  Result : returns a list of postalcodes and places for the lat/lng query as xml document. The result is sorted by distance. For Canada the FSA is returned (first 3 characters of full postal code) 
         * @ramlpath /findNearbyPostalCodes{mediaTypeSuffix}  get
         **/
         get( options?:FindNearbyPostalCodesResourceGetOptions ):FindNearByPostalCode | FindNearByPostalCode_xml
}
export interface FindNearbyPostalCodesResourceGetOptions{

        /**
         *
         **/
         //radius 
         radius ?:number


        /**
         *
         **/
         //style 
         style ?:string


        /**
         *
         **/
         //maxRows 
         maxRows ?:string


        /**
         *
         **/
         //postalcode
         postalcode?:string


        /**
         *
         **/
         //country
         country?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number
}
export interface FindNearByPostalCode{

        /**
         *
         **/
         //postalCodes
         postalCodes?:{

        /**
         *
         **/
         //adminCode3
         adminCode3?:string


        /**
         *
         **/
         //adminName2
         adminName2?:string


        /**
         *
         **/
         //adminName3
         adminName3?:string


        /**
         *
         **/
         //adminCode2
         adminCode2?:string


        /**
         *
         **/
         //distance
         distance?:string


        /**
         *
         **/
         //adminCode1
         adminCode1?:string


        /**
         *
         **/
         //postalCode
         postalCode?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //placeName
         placeName?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //adminName1
         adminName1?:string
}[]


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface FindNearByPostalCode_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //code
         code?:{

        /**
         *
         **/
         //postalcode
         postalcode?:number


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //adminCode1
         adminCode1?:string


        /**
         *
         **/
         //adminName1
         adminName1?:string


        /**
         *
         **/
         //adminCode2
         adminCode2?:number


        /**
         *
         **/
         //adminName2
         adminName2?:string


        /**
         *
         **/
         //adminCode3
         adminCode3?:number


        /**
         *
         **/
         //adminName3
         adminName3?:string


        /**
         *
         **/
         //distance
         distance?:number
}[]
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface FindNearbyPlaceNameResource{

        /**
         *  Result : returns the closest populated place (feature class=P) for the lat/lng query as xml document. The unit of the distance element is 'km'. 
         * @ramlpath /findNearbyPlaceName{mediaTypeSuffix}  get
         **/
         get( options?:FindNearbyPlaceNameResourceGetOptions ):FindNearbyPlaceName | FindNearbyPlaceName_xml
}
export interface FindNearbyPlaceNameResourceGetOptions{

        /**
         *
         **/
         //lang
         lang?:string


        /**
         *
         **/
         //radius
         radius?:string


        /**
         *
         **/
         //maxRows
         maxRows?:string


        /**
         *
         **/
         //localCountry
         localCountry?:boolean


        /**
         *
         **/
         //cities
         cities?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //style
         style?:string
}
export interface FindNearbyPlaceName{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //countryId
         countryId?:string


        /**
         *
         **/
         //adminCode1
         adminCode1?:string


        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //fclName
         fclName?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //lng
         lng?:string


        /**
         *
         **/
         //fcodeName
         fcodeName?:string


        /**
         *
         **/
         //distance
         distance?:string


        /**
         *
         **/
         //toponymName
         toponymName?:string


        /**
         *
         **/
         //fcl
         fcl?:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //fcode
         fcode?:string


        /**
         *
         **/
         //geonameId
         geonameId?:number


        /**
         *
         **/
         //lat
         lat?:string


        /**
         *
         **/
         //adminName1
         adminName1?:string


        /**
         *
         **/
         //population
         population?:number
}[]


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface FindNearbyPlaceName_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //geoname
         geoname?:{

        /**
         *
         **/
         //toponymName
         toponymName?:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //geonameId
         geonameId?:number


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //fcl
         fcl?:string


        /**
         *
         **/
         //fcode
         fcode?:string


        /**
         *
         **/
         //distance
         distance?:number
}
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface FindNearbyResource{

        /**
         *  Result : returns the closest toponym for the lat/lng query as xml document 
         * @ramlpath /findNearby{mediaTypeSuffix}  get
         **/
         get( options?:FindNearbyResourceGetOptions ):FindNearbyPlaceName | FindNearbyPlaceName_xml
}
export interface FindNearbyResourceGetOptions{

        /**
         *
         **/
         //featureClass
         featureClass?:string


        /**
         *
         **/
         //featureCode
         featureCode?:string


        /**
         *
         **/
         //radius
         radius?:string


        /**
         *
         **/
         //maxRows 
         maxRows ?:string


        /**
         *
         **/
         //localCountry
         localCountry?:string


        /**
         *
         **/
         //style
         style?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number
}
export interface HierarchyResource{

        /**
         *  Result : returns a list of GeoName records, ordered by hierarchy level. The top hierarchy (continent) is the first element in the list 
         * @ramlpath /hierarchy{mediaTypeSuffix}  get
         **/
         get( options?:HierarchyResourceGetOptions ):Hierarchy | ExtendedFindNearby_xml
}
export interface HierarchyResourceGetOptions{

        /**
         *
         **/
         //geonameId 
         geonameId ?:string
}
export interface Hierarchy{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //fcodeName
         fcodeName?:string


        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //toponymName
         toponymName?:string


        /**
         *
         **/
         //fcl
         fcl?:string


        /**
         *
         **/
         //fclName
         fclName?:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //lng
         lng?:string


        /**
         *
         **/
         //fcode
         fcode?:string


        /**
         *
         **/
         //geonameId
         geonameId?:number


        /**
         *
         **/
         //lat
         lat?:string


        /**
         *
         **/
         //adminName1
         adminName1?:string


        /**
         *
         **/
         //population
         population?:number
} | {

        /**
         *
         **/
         //countryId
         countryId?:string


        /**
         *
         **/
         //adminCode1
         adminCode1?:string


        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //fclName
         fclName?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //lng
         lng?:string


        /**
         *
         **/
         //fcodeName
         fcodeName?:string


        /**
         *
         **/
         //toponymName
         toponymName?:string


        /**
         *
         **/
         //fcl
         fcl?:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //fcode
         fcode?:string


        /**
         *
         **/
         //geonameId
         geonameId?:number


        /**
         *
         **/
         //lat
         lat?:string


        /**
         *
         **/
         //adminName1
         adminName1?:string


        /**
         *
         **/
         //population
         population?:number
}[]


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface ContainsResource{

        /**
         *  returns all features within the GeoName feature for the given geoNameId. It only returns contained features when a polygon boundary for the input feature is defined.
         * @ramlpath /contains{mediaTypeSuffix}  get
         **/
         get( options?:ContainsResourceGetOptions ):Features | Features_xml
}
export interface ContainsResourceGetOptions{

        /**
         *
         **/
         //geonameId 
         geonameId ?:string


        /**
         *
         **/
         //featureClass
         featureClass?:string


        /**
         *
         **/
         //featureCode
         featureCode?:string
}
export interface Features{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //countryId
         countryId?:string


        /**
         *
         **/
         //adminCode1
         adminCode1?:string


        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //fclName
         fclName?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //lng
         lng?:string


        /**
         *
         **/
         //fcodeName
         fcodeName?:string


        /**
         *
         **/
         //toponymName
         toponymName?:string


        /**
         *
         **/
         //fcl
         fcl?:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //fcode
         fcode?:string


        /**
         *
         **/
         //geonameId
         geonameId?:number


        /**
         *
         **/
         //lat
         lat?:string


        /**
         *
         **/
         //adminName1
         adminName1?:string


        /**
         *
         **/
         //population
         population?:number
}[]


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface Features_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //geoname
         geoname?:{

        /**
         *
         **/
         //toponymName
         toponymName?:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //geonameId
         geonameId?:number


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //fcl
         fcl?:string


        /**
         *
         **/
         //fcode
         fcode?:string
}[]


        /**
         *
         **/
         //@style
         "@style":string
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface SiblingsResource{

        /**
         *  Returns all siblings of a GeoNames toponym with feature class A.
         * @ramlpath /siblings{mediaTypeSuffix}  get
         **/
         get( options?:SiblingsResourceGetOptions ):Siblings | Siblings_xml
}
export interface SiblingsResourceGetOptions{

        /**
         *
         **/
         //geonameId
         geonameId?:string
}
export interface Siblings{

        /**
         *
         **/
         //totalResultsCount
         totalResultsCount?:number


        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //countryId
         countryId?:string


        /**
         *
         **/
         //adminCode1
         adminCode1?:string


        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //fclName
         fclName?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //lng
         lng?:string


        /**
         *
         **/
         //fcodeName
         fcodeName?:string


        /**
         *
         **/
         //toponymName
         toponymName?:string


        /**
         *
         **/
         //fcl
         fcl?:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //fcode
         fcode?:string


        /**
         *
         **/
         //geonameId
         geonameId?:number


        /**
         *
         **/
         //lat
         lat?:string


        /**
         *
         **/
         //adminName1
         adminName1?:string


        /**
         *
         **/
         //population
         population?:number
}[]


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface Siblings_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //totalResultsCount
         totalResultsCount?:number


        /**
         *
         **/
         //geoname
         geoname?:{

        /**
         *
         **/
         //toponymName
         toponymName?:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //geonameId
         geonameId?:number


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //fcl
         fcl?:string


        /**
         *
         **/
         //fcode
         fcode?:string
}[]
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface FindNearbyWikipediaResource{

        /**
         *  This service comes in two flavors. You can either pass the lat/long or a postalcode/placename. Result : returns a list of wikipedia entries as xml document 
         * @ramlpath /findNearbyWikipedia{mediaTypeSuffix}  get
         **/
         get( options?:FindNearbyWikipediaResourceGetOptions ):FindNearbyWikipedia | FindNearbyWikipedia_xml
}
export interface FindNearbyWikipediaResourceGetOptions{

        /**
         *
         **/
         //radius 
         radius ?:string


        /**
         *
         **/
         //maxRows 
         maxRows ?:string


        /**
         *
         **/
         //country 
         country ?:string


        /**
         *
         **/
         //postalcode
         postalcode?:string


        /**
         *
         **/
         //lang
         lang?:string


        /**
         *
         **/
         //title
         title?:string


        /**
         *
         **/
         //summary
         summary?:string


        /**
         *
         **/
         //feature
         feature?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //elevation
         elevation?:string


        /**
         *
         **/
         //population
         population?:string


        /**
         *
         **/
         //wikipediaUrl
         wikipediaUrl?:string


        /**
         *
         **/
         //thumbnailImg
         thumbnailImg?:string


        /**
         *
         **/
         //rank
         rank?:number


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number
}
export interface FindNearbyWikipedia{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //summary
         summary?:string


        /**
         *
         **/
         //distance
         distance?:string


        /**
         *
         **/
         //rank
         rank?:number


        /**
         *
         **/
         //title
         title?:string


        /**
         *
         **/
         //wikipediaUrl
         wikipediaUrl?:string


        /**
         *
         **/
         //elevation
         elevation?:number


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //feature
         feature?:string


        /**
         *
         **/
         //lang
         lang?:string


        /**
         *
         **/
         //lat
         lat?:number
}[]


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface FindNearbyWikipedia_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //entry
         entry?:{

        /**
         *
         **/
         //lang
         lang?:string


        /**
         *
         **/
         //title
         title?:string


        /**
         *
         **/
         //summary
         summary?:string


        /**
         *
         **/
         //feature
         feature?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //elevation
         elevation?:number


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //wikipediaUrl
         wikipediaUrl?:string


        /**
         *
         **/
         //thumbnailImg
         thumbnailImg?:string


        /**
         *
         **/
         //rank
         rank?:number


        /**
         *
         **/
         //distance
         distance?:number
}[]
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface WikipediaSearchResource{

        /**
         *  Result : returns the wikipedia entries found for the searchterm as xml document
         * @ramlpath /wikipediaSearch{mediaTypeSuffix}  get
         **/
         get( options?:WikipediaSearchResourceGetOptions ):WikipediaSearch | WikipediaSearch_xml
}
export interface WikipediaSearchResourceGetOptions{

        /**
         *
         **/
         //maxRows 
         maxRows ?:string


        /**
         *
         **/
         //q
         q?:string


        /**
         *
         **/
         //lang
         lang?:string


        /**
         *
         **/
         //title
         title?:string


        /**
         *
         **/
         //summary
         summary?:string


        /**
         *
         **/
         //feature
         feature?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //elevation
         elevation?:string


        /**
         *
         **/
         //population
         population?:string


        /**
         *
         **/
         //wikipediaUrl
         wikipediaUrl?:string


        /**
         *
         **/
         //thumbnailImg
         thumbnailImg?:string


        /**
         *
         **/
         //rank
         rank?:number


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number
}
export interface WikipediaSearch{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //summary
         summary?:string


        /**
         *
         **/
         //rank
         rank?:number


        /**
         *
         **/
         //title
         title?:string


        /**
         *
         **/
         //wikipediaUrl
         wikipediaUrl?:string


        /**
         *
         **/
         //elevation
         elevation?:number


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //feature
         feature?:string


        /**
         *
         **/
         //thumbnailImg
         thumbnailImg?:string


        /**
         *
         **/
         //lang
         lang?:string


        /**
         *
         **/
         //lat
         lat?:number
} | {

        /**
         *
         **/
         //summary
         summary?:string


        /**
         *
         **/
         //rank
         rank?:number


        /**
         *
         **/
         //title
         title?:string


        /**
         *
         **/
         //wikipediaUrl
         wikipediaUrl?:string


        /**
         *
         **/
         //elevation
         elevation?:number


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //feature
         feature?:string


        /**
         *
         **/
         //thumbnailImg
         thumbnailImg?:string


        /**
         *
         **/
         //geoNameId
         geoNameId?:number


        /**
         *
         **/
         //lang
         lang?:string


        /**
         *
         **/
         //lat
         lat?:number
}[]


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface WikipediaSearch_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //entry
         entry?:{

        /**
         *
         **/
         //lang
         lang?:string


        /**
         *
         **/
         //title
         title?:string


        /**
         *
         **/
         //summary
         summary?:string


        /**
         *
         **/
         //feature
         feature?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //elevation
         elevation?:number


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //wikipediaUrl
         wikipediaUrl?:string


        /**
         *
         **/
         //thumbnailImg
         thumbnailImg?:string


        /**
         *
         **/
         //rank
         rank?:number
}[]
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface WikipediaBoundingBoxResource{

        /**
         *  Result : returns the wikipedia entries within the bounding box as xml document 
         * @ramlpath /wikipediaBoundingBox{mediaTypeSuffix}  get
         **/
         get( options?:WikipediaBoundingBoxResourceGetOptions ):WikipediaBoundingBox | WikipediaSearch_xml
}
export interface WikipediaBoundingBoxResourceGetOptions{

        /**
         *
         **/
         //lang
         lang?:string


        /**
         *
         **/
         //title
         title?:string


        /**
         *
         **/
         //summary
         summary?:string


        /**
         *
         **/
         //feature
         feature?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //elevation
         elevation?:string


        /**
         *
         **/
         //population
         population?:string


        /**
         *
         **/
         //wikipediaUrl
         wikipediaUrl?:string


        /**
         *
         **/
         //thumbnailImg
         thumbnailImg?:string


        /**
         *
         **/
         //rank
         rank?:number


        /**
         *
         **/
         //east
         east?:number


        /**
         *
         **/
         //west
         west?:number


        /**
         *
         **/
         //north
         north?:string


        /**
         *
         **/
         //south
         south?:number
}
export interface WikipediaBoundingBox{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //summary
         summary?:string


        /**
         *
         **/
         //rank
         rank?:number


        /**
         *
         **/
         //title
         title?:string


        /**
         *
         **/
         //wikipediaUrl
         wikipediaUrl?:string


        /**
         *
         **/
         //elevation
         elevation?:number


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //feature
         feature?:string


        /**
         *
         **/
         //geoNameId
         geoNameId?:number


        /**
         *
         **/
         //lang
         lang?:string


        /**
         *
         **/
         //lat
         lat?:number
} | {

        /**
         *
         **/
         //summary
         summary?:string


        /**
         *
         **/
         //rank
         rank?:number


        /**
         *
         **/
         //title
         title?:string


        /**
         *
         **/
         //wikipediaUrl
         wikipediaUrl?:string


        /**
         *
         **/
         //elevation
         elevation?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //feature
         feature?:string


        /**
         *
         **/
         //thumbnailImg
         thumbnailImg?:string


        /**
         *
         **/
         //lang
         lang?:string


        /**
         *
         **/
         //lat
         lat?:number
} | {

        /**
         *
         **/
         //summary
         summary?:string


        /**
         *
         **/
         //rank
         rank?:number


        /**
         *
         **/
         //title
         title?:string


        /**
         *
         **/
         //wikipediaUrl
         wikipediaUrl?:string


        /**
         *
         **/
         //elevation
         elevation?:number


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //feature
         feature?:string


        /**
         *
         **/
         //thumbnailImg
         thumbnailImg?:string


        /**
         *
         **/
         //lang
         lang?:string


        /**
         *
         **/
         //lat
         lat?:number
} | {

        /**
         *
         **/
         //summary
         summary?:string


        /**
         *
         **/
         //rank
         rank?:number


        /**
         *
         **/
         //title
         title?:string


        /**
         *
         **/
         //wikipediaUrl
         wikipediaUrl?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //feature
         feature?:string


        /**
         *
         **/
         //thumbnailImg
         thumbnailImg?:string


        /**
         *
         **/
         //lang
         lang?:string


        /**
         *
         **/
         //lat
         lat?:number
} | {

        /**
         *
         **/
         //summary
         summary?:string


        /**
         *
         **/
         //rank
         rank?:number


        /**
         *
         **/
         //title
         title?:string


        /**
         *
         **/
         //wikipediaUrl
         wikipediaUrl?:string


        /**
         *
         **/
         //elevation
         elevation?:number


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //feature
         feature?:string


        /**
         *
         **/
         //geoNameId
         geoNameId?:number


        /**
         *
         **/
         //lang
         lang?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //population
         population?:number
} | {

        /**
         *
         **/
         //summary
         summary?:string


        /**
         *
         **/
         //rank
         rank?:number


        /**
         *
         **/
         //title
         title?:string


        /**
         *
         **/
         //wikipediaUrl
         wikipediaUrl?:string


        /**
         *
         **/
         //elevation
         elevation?:number


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //geoNameId
         geoNameId?:number


        /**
         *
         **/
         //lang
         lang?:string


        /**
         *
         **/
         //lat
         lat?:number
}[]


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface FindNearByWeatherResource{

        /**
         *  Result : returns a weather station with the most recent weather observation
         * @ramlpath /findNearByWeather{mediaTypeSuffix}  get
         **/
         get( options?:FindNearByWeatherResourceGetOptions ):FindNearByWeather | FindNearByWeather_xml
}
export interface FindNearByWeatherResourceGetOptions{

        /**
         *
         **/
         //callback 
         callback ?:string


        /**
         *
         **/
         //radius
         radius?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number
}
export interface FindNearByWeather{

        /**
         *
         **/
         //weatherObservation
         weatherObservation?:{

        /**
         *
         **/
         //weatherCondition
         weatherCondition?:string


        /**
         *
         **/
         //clouds
         clouds?:string


        /**
         *
         **/
         //observation
         observation?:string


        /**
         *
         **/
         //ICAO
         ICAO?:string


        /**
         *
         **/
         //elevation
         elevation?:number


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //cloudsCode
         cloudsCode?:string


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //temperature
         temperature?:string


        /**
         *
         **/
         //dewPoint
         dewPoint?:string


        /**
         *
         **/
         //windSpeed
         windSpeed?:string


        /**
         *
         **/
         //humidity
         humidity?:number


        /**
         *
         **/
         //stationName
         stationName?:string


        /**
         *
         **/
         //datetime
         datetime?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //hectoPascAltimeter
         hectoPascAltimeter?:number
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface FindNearByWeather_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //observation
         observation?:{

        /**
         *
         **/
         //observation
         observation?:string


        /**
         *
         **/
         //observationTime
         observationTime?:string


        /**
         *
         **/
         //stationName
         stationName?:string


        /**
         *
         **/
         //ICAO
         ICAO?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //elevation
         elevation?:number


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //temperature
         temperature?:number


        /**
         *
         **/
         //dewPoint
         dewPoint?:number


        /**
         *
         **/
         //humidity
         humidity?:number


        /**
         *
         **/
         //clouds
         clouds?:{}


        /**
         *
         **/
         //weatherCondition
         weatherCondition?:string


        /**
         *
         **/
         //hectoPascAltimeter
         hectoPascAltimeter?:number


        /**
         *
         **/
         //windSpeed
         windSpeed?:number
}
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface FindNearestAddressResource{

        /**
         *  Finds the nearest street and address for a given lat/lng pair. Result : returns the nearest address for the given latitude/longitude, the street number is an 'educated guess' using an interpolation of street number at the end of a street segment.
         * @ramlpath /findNearestAddress{mediaTypeSuffix}  get
         **/
         get:FindNearestAddress | FindNearestAddress_xml
}
export interface FindNearestAddress{

        /**
         *
         **/
         //address
         address?:{

        /**
         *
         **/
         //postalcode
         postalcode?:string


        /**
         *
         **/
         //adminCode2
         adminCode2?:string


        /**
         *
         **/
         //adminCode1
         adminCode1?:string


        /**
         *
         **/
         //street
         street?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //lng
         lng?:string


        /**
         *
         **/
         //placename
         placename?:string


        /**
         *
         **/
         //adminName2
         adminName2?:string


        /**
         *
         **/
         //distance
         distance?:string


        /**
         *
         **/
         //streetNumber
         streetNumber?:string


        /**
         *
         **/
         //mtfcc
         mtfcc?:string


        /**
         *
         **/
         //lat
         lat?:string


        /**
         *
         **/
         //adminName1
         adminName1?:string
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface FindNearestAddress_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //address
         address?:{

        /**
         *
         **/
         //street
         street?:string


        /**
         *
         **/
         //mtfcc
         mtfcc?:string


        /**
         *
         **/
         //streetNumber
         streetNumber?:number


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //distance
         distance?:number


        /**
         *
         **/
         //postalcode
         postalcode?:number


        /**
         *
         **/
         //placename
         placename?:string


        /**
         *
         **/
         //adminCode2
         adminCode2?:number


        /**
         *
         **/
         //adminName2
         adminName2?:string


        /**
         *
         **/
         //adminCode1
         adminCode1?:string


        /**
         *
         **/
         //adminName1
         adminName1?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string
}
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface FindNearestIntersectionResource{

        /**
         *  Result : returns the nearest intersection for the given latitude/longitude
         * @ramlpath /findNearestIntersection{mediaTypeSuffix}  get
         **/
         get( options?:FindNearestIntersectionResourceGetOptions ):FindNearestIntersection | FindNearestIntersection_xml
}
export interface FindNearestIntersectionResourceGetOptions{

        /**
         *
         **/
         //filter
         filter?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number
}
export interface FindNearestIntersection{

        /**
         *
         **/
         //intersection
         intersection?:{

        /**
         *
         **/
         //street2
         street2?:string


        /**
         *
         **/
         //street1
         street1?:string


        /**
         *
         **/
         //postalcode
         postalcode?:string


        /**
         *
         **/
         //adminCode1
         adminCode1?:string


        /**
         *
         **/
         //street1Bearing
         street1Bearing?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //mtfcc1
         mtfcc1?:string


        /**
         *
         **/
         //lng
         lng?:string


        /**
         *
         **/
         //mtfcc2
         mtfcc2?:string


        /**
         *
         **/
         //placename
         placename?:string


        /**
         *
         **/
         //adminName2
         adminName2?:string


        /**
         *
         **/
         //distance
         distance?:string


        /**
         *
         **/
         //street2Bearing
         street2Bearing?:string


        /**
         *
         **/
         //lat
         lat?:string


        /**
         *
         **/
         //adminName1
         adminName1?:string
}


        /**
         *
         **/
         //credits
         credits?:string


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface FindNearestIntersection_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //intersection
         intersection?:{

        /**
         *
         **/
         //street1
         street1?:string


        /**
         *
         **/
         //mtfcc1
         mtfcc1?:string


        /**
         *
         **/
         //street1Bearing
         street1Bearing?:number


        /**
         *
         **/
         //street2
         street2?:string


        /**
         *
         **/
         //mtfcc2
         mtfcc2?:string


        /**
         *
         **/
         //street2Bearing
         street2Bearing?:number


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //distance
         distance?:number


        /**
         *
         **/
         //postalcode
         postalcode?:number


        /**
         *
         **/
         //placename
         placename?:string


        /**
         *
         **/
         //adminName2
         adminName2?:string


        /**
         *
         **/
         //adminCode1
         adminCode1?:string


        /**
         *
         **/
         //adminName1
         adminName1?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string
}


        /**
         *
         **/
         //@credits
         "@credits":number
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface FindNearbyStreetsResource{

        /**
         *  Finds the nearest street for a given lat/lng pair. Result : returns the nearest street segments for the given latitude/longitude Restriction : this webservice is only available for the US.
         * @ramlpath /findNearbyStreets{mediaTypeSuffix}  get
         **/
         get( options?:FindNearbyStreetsResourceGetOptions ):FindNearbyStreets | FindNearbyStreets_xml
}
export interface FindNearbyStreetsResourceGetOptions{

        /**
         *
         **/
         //maxRows
         maxRows?:string


        /**
         *
         **/
         //radius
         radius?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number
}
export interface FindNearbyStreets{

        /**
         *
         **/
         //streetSegment
         streetSegment?:{

        /**
         *
         **/
         //postalcode
         postalcode?:string


        /**
         *
         **/
         //adminCode2
         adminCode2?:string


        /**
         *
         **/
         //adminCode1
         adminCode1?:string


        /**
         *
         **/
         //fraddl
         fraddl?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //line
         line?:string


        /**
         *
         **/
         //placename
         placename?:string


        /**
         *
         **/
         //fraddr
         fraddr?:string


        /**
         *
         **/
         //adminName2
         adminName2?:string


        /**
         *
         **/
         //distance
         distance?:string


        /**
         *
         **/
         //toaddl
         toaddl?:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //mtfcc
         mtfcc?:string


        /**
         *
         **/
         //toaddr
         toaddr?:string


        /**
         *
         **/
         //adminName1
         adminName1?:string
}[]


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface FindNearbyStreets_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //streetSegment
         streetSegment?:{

        /**
         *
         **/
         //line
         line?:string


        /**
         *
         **/
         //distance
         distance?:number


        /**
         *
         **/
         //mtfcc
         mtfcc?:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //fraddl
         fraddl?:number


        /**
         *
         **/
         //fraddr
         fraddr?:number


        /**
         *
         **/
         //toaddl
         toaddl?:number


        /**
         *
         **/
         //toaddr
         toaddr?:number


        /**
         *
         **/
         //postalcode
         postalcode?:number


        /**
         *
         **/
         //placename
         placename?:string


        /**
         *
         **/
         //adminCode2
         adminCode2?:number


        /**
         *
         **/
         //adminName2
         adminName2?:string


        /**
         *
         **/
         //adminCode1
         adminCode1?:string


        /**
         *
         **/
         //adminName1
         adminName1?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string
}[]
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface FindNearestIntersectionOSMResource{

        /**
         *  Finds the nearest street and the next crossing street for a given lat/lng pair. Result : returns the nearest intersection for the given latitude/longitude
         * @ramlpath /findNearestIntersectionOSM{mediaTypeSuffix}  get
         **/
         get( options?:FindNearestIntersectionOSMResourceGetOptions ):FindNearestIntersectionOSM | FindNearestIntersectionOSM_xml
}
export interface FindNearestIntersectionOSMResourceGetOptions{

        /**
         *
         **/
         //radius 
         radius ?:string


        /**
         *
         **/
         //maxRows 
         maxRows ?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number
}
export interface FindNearestIntersectionOSM{

        /**
         *
         **/
         //intersection
         intersection?:{

        /**
         *
         **/
         //street2
         street2?:string


        /**
         *
         **/
         //street1
         street1?:string


        /**
         *
         **/
         //distance
         distance?:string


        /**
         *
         **/
         //highway2
         highway2?:string


        /**
         *
         **/
         //highway1
         highway1?:string


        /**
         *
         **/
         //street2Bearing
         street2Bearing?:string


        /**
         *
         **/
         //street1Bearing
         street1Bearing?:string


        /**
         *
         **/
         //lng
         lng?:string


        /**
         *
         **/
         //lat
         lat?:string
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface FindNearestIntersectionOSM_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //intersection
         intersection?:{

        /**
         *
         **/
         //street1
         street1?:string


        /**
         *
         **/
         //highway1
         highway1?:string


        /**
         *
         **/
         //street1Bearing
         street1Bearing?:number


        /**
         *
         **/
         //street2
         street2?:string


        /**
         *
         **/
         //highway2
         highway2?:string


        /**
         *
         **/
         //street2Bearing
         street2Bearing?:number


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //distance
         distance?:number
}
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface FindNearbyStreetsOSMResource{

        /**
         *  Finds the nearest streets for a given lat/lng pair. Result: returns the nearest street segments for the given latitude/longitude
         * @ramlpath /findNearbyStreetsOSM{mediaTypeSuffix}  get
         **/
         get( options?:FindNearbyStreetsOSMResourceGetOptions ):FindNearbyStreetsOSM | FindNearbyStreetsOSM_xml
}
export interface FindNearbyStreetsOSMResourceGetOptions{

        /**
         *
         **/
         //radius 
         radius ?:string


        /**
         *
         **/
         //maxRows 
         maxRows ?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number
}
export type FindNearbyStreetsOSMResourceGetOptions1=FindNearestIntersectionOSMResourceGetOptions
export interface FindNearbyStreetsOSM{

        /**
         *
         **/
         //streetSegment
         streetSegment?:{

        /**
         *
         **/
         //distance
         distance?:string


        /**
         *
         **/
         //highway
         highway?:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //line
         line?:string


        /**
         *
         **/
         //wayId
         wayId?:string
} | {

        /**
         *
         **/
         //ref
         ref?:string


        /**
         *
         **/
         //distance
         distance?:string


        /**
         *
         **/
         //highway
         highway?:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //oneway
         oneway?:string


        /**
         *
         **/
         //line
         line?:string


        /**
         *
         **/
         //wayId
         wayId?:string


        /**
         *
         **/
         //maxspeed
         maxspeed?:string
}[]


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface FindNearbyStreetsOSM_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //streetSegment
         streetSegment?:{

        /**
         *
         **/
         //wayId
         wayId?:number


        /**
         *
         **/
         //line
         line?:string


        /**
         *
         **/
         //distance
         distance?:number


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //highway
         highway?:string
}[]
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface FindNearbyPOIsOSMResource{

        /**
         *  Finds the nearest points of interests for a given lat/lng pair.
         * @ramlpath /findNearbyPOIsOSM{mediaTypeSuffix}  get
         **/
         get( options?:FindNearbyPOIsOSMResourceGetOptions ):FindNearbyPOIsOSM | FindNearbyPOIsOSM_xml
}
export interface FindNearbyPOIsOSMResourceGetOptions{

        /**
         *
         **/
         //radius 
         radius ?:string


        /**
         *
         **/
         //maxRows 
         maxRows ?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number
}
export type FindNearbyPOIsOSMResourceGetOptions1=FindNearestIntersectionOSMResourceGetOptions
export interface FindNearbyPOIsOSM{

        /**
         *
         **/
         //poi
         poi?:{

        /**
         *
         **/
         //typeName
         typeName?:string


        /**
         *
         **/
         //distance
         distance?:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //lng
         lng?:string


        /**
         *
         **/
         //typeClass
         typeClass?:string


        /**
         *
         **/
         //lat
         lat?:string
}[]


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface FindNearbyPOIsOSM_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //poi
         poi?:{

        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //typeClass
         typeClass?:string


        /**
         *
         **/
         //typeName
         typeName?:string


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //distance
         distance?:number
}[]
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface CountrySubdivisionResource{

        /**
         *  The iso country code and the administrative subdivision of any given point. Result : returns the country and the administrative subdivison (state, province,...) for the given latitude/longitude
         * @ramlpath /countrySubdivision{mediaTypeSuffix}  get
         **/
         get( options?:CountrySubdivisionResourceGetOptions ):CountrySubdivision | CountrySubdivision_xml
}
export interface CountrySubdivisionResourceGetOptions{

        /**
         *
         **/
         //lang 
         lang ?:string


        /**
         *
         **/
         //radius 
         radius ?:string


        /**
         *
         **/
         //level
         level?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number
}
export interface CountrySubdivision{

        /**
         *
         **/
         //distance
         distance?:number


        /**
         *
         **/
         //adminCode1
         adminCode1?:string


        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //codes
         codes?:{

        /**
         *
         **/
         //code
         code?:string


        /**
         *
         **/
         //type
         "type"?:string
}[]


        /**
         *
         **/
         //adminName1
         adminName1?:string


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface CountrySubdivision_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //countrySubdivision
         countrySubdivision?:{

        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //adminCode1
         adminCode1?:number


        /**
         *
         **/
         //adminName1
         adminName1?:string


        /**
         *
         **/
         //code
         code?:{}[]


        /**
         *
         **/
         //distance
         distance?:number
}
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface OceanResource{

        /**
         *  The name of the ocean or sea. Result : returns the ocean or sea for the given latitude/longitude
         * @ramlpath /ocean{mediaTypeSuffix}  get
         **/
         get( options?:OceanResourceGetOptions ):Ocean | Ocean_xml
}
export interface OceanResourceGetOptions{

        /**
         *
         **/
         //radius
         radius?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number
}
export interface Ocean{

        /**
         *
         **/
         //ocean
         ocean?:{

        /**
         *
         **/
         //name
         name?:string
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface Ocean_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //ocean
         ocean?:{

        /**
         *
         **/
         //name
         name?:string
}
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface NeighbourhoodResource{

        /**
         *  The neighbourhood for US cities. Data provided by Zillow under cc-by-sa license. Result : returns the neighbourhood for the given latitude/longitude
         * @ramlpath /neighbourhood{mediaTypeSuffix}  get
         **/
         get( options?:NeighbourhoodResourceGetOptions ):Neighbourhood | Neighbourhood_xml
}
export interface NeighbourhoodResourceGetOptions{

        /**
         *
         **/
         //geonameId 
         geonameId ?:string


        /**
         *
         **/
         //country
         country?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number
}
export interface Neighbourhood{

        /**
         *
         **/
         //neighbourhood
         neighbourhood?:{

        /**
         *
         **/
         //adminName2
         adminName2?:string


        /**
         *
         **/
         //adminCode2
         adminCode2?:string


        /**
         *
         **/
         //adminCode1
         adminCode1?:string


        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //city
         city?:string


        /**
         *
         **/
         //adminName1
         adminName1?:string
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface Neighbourhood_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //totalResultsCount
         totalResultsCount?:number


        /**
         *
         **/
         //geoname
         geoname?:{

        /**
         *
         **/
         //toponymName
         toponymName?:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //geonameId
         geonameId?:number


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //fcl
         fcl?:string


        /**
         *
         **/
         //fcode
         fcode?:string
}[]


        /**
         *
         **/
         //@style
         "@style":string
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface TimezoneResource{

        /**
         *  Result : the timezone at the lat/lng with gmt offset (1. January) and dst offset (1. July) 
         * @ramlpath /timezone{mediaTypeSuffix}  get
         **/
         get( options?:TimezoneResourceGetOptions ):Timezone | Timezone_xml
}
export interface TimezoneResourceGetOptions{

        /**
         *
         **/
         //radius 
         radius ?:string


        /**
         *
         **/
         //date 
         date ?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number
}
export interface Timezone{

        /**
         *
         **/
         //time
         time?:string


        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //sunset
         sunset?:string


        /**
         *
         **/
         //rawOffset
         rawOffset?:number


        /**
         *
         **/
         //dstOffset
         dstOffset?:number


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //gmtOffset
         gmtOffset?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //sunrise
         sunrise?:string


        /**
         *
         **/
         //timezoneId
         timezoneId?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface Timezone_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //timezone
         timezone?:{

        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //timezoneId
         timezoneId?:string


        /**
         *
         **/
         //dstOffset
         dstOffset?:number


        /**
         *
         **/
         //gmtOffset
         gmtOffset?:number


        /**
         *
         **/
         //rawOffset
         rawOffset?:number


        /**
         *
         **/
         //time
         time?:string


        /**
         *
         **/
         //sunrise
         sunrise?:string


        /**
         *
         **/
         //sunset
         sunset?:string


        /**
         *
         **/
         //@tzversion
         "@tzversion":string
}
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface Gtopo30Resource{

        /**
         *  GTOPO30 is a global digital elevation model (DEM) with a horizontal grid spacing of 30 arc seconds (approximately 1 kilometer). GTOPO30 was derived from several raster and vector sources of topographic information.
         * @ramlpath /gtopo30{mediaTypeSuffix}  get
         **/
         get( options?:Gtopo30ResourceGetOptions ):Gtopo30 | Gtopo30_xml
}
export interface Gtopo30ResourceGetOptions{

        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number
}
export type Gtopo30ResourceGetOptions1=ExtendedFindNearbyResourceGetOptions
export interface Gtopo30{

        /**
         *
         **/
         //gtopo30
         gtopo30?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface Gtopo30_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //status
         status?:{}
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface CountryInfoResource{

        /**
         *  Result : Country information : Capital, Population, Area in square km, Bounding Box of mainland (excluding offshore islands)
         * @ramlpath /countryInfo{mediaTypeSuffix}  get
         **/
         get( options?:CountryInfoResourceGetOptions ):CountryInfo | CountryInfo_xml
}
export interface CountryInfoResourceGetOptions{

        /**
         *
         **/
         //country 
         country ?:string


        /**
         *
         **/
         //lang
         lang?:string
}
export interface CountryInfo{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //currencyCode
         currencyCode?:string


        /**
         *
         **/
         //fipsCode
         fipsCode?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //isoNumeric
         isoNumeric?:string


        /**
         *
         **/
         //north
         north?:number


        /**
         *
         **/
         //capital
         capital?:string


        /**
         *
         **/
         //continentName
         continentName?:string


        /**
         *
         **/
         //areaInSqKm
         areaInSqKm?:string


        /**
         *
         **/
         //languages
         languages?:string


        /**
         *
         **/
         //isoAlpha3
         isoAlpha3?:string


        /**
         *
         **/
         //continent
         continent?:string


        /**
         *
         **/
         //south
         south?:number


        /**
         *
         **/
         //east
         east?:number


        /**
         *
         **/
         //geonameId
         geonameId?:number


        /**
         *
         **/
         //west
         west?:number


        /**
         *
         **/
         //population
         population?:string
}[]


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface CountryInfo_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //country
         country?:{

        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //numPostalCodes
         numPostalCodes?:number


        /**
         *
         **/
         //minPostalCode
         minPostalCode?:string


        /**
         *
         **/
         //maxPostalCode
         maxPostalCode?:string
}[]
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface SearchResource{

        /**
         *  returns the names found for the searchterm as xml or json document, the search is using an AND operator
         * @ramlpath /search{mediaTypeSuffix}  get
         **/
         get( options:SearchResourceGetOptions ):Siblings | Search_xml
}
export interface SearchResourceGetOptions{

        /**
         *
         **/
         //q
         q:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //name_equals
         name_equals?:string


        /**
         *
         **/
         //name_startsWith
         name_startsWith?:string


        /**
         *
         **/
         //maxRows
         maxRows?:number


        /**
         *
         **/
         //startRow
         startRow?:number


        /**
         *
         **/
         //country
         country?:string


        /**
         *
         **/
         //countryBias
         countryBias?:string


        /**
         *
         **/
         //continentCode
         continentCode?:string


        /**
         *
         **/
         //adminCode1
         adminCode1?:string


        /**
         *
         **/
         //featureClass
         featureClass?:number


        /**
         *
         **/
         //featureCode
         featureCode?:string


        /**
         *
         **/
         //lang
         lang?:string


        /**
         *
         **/
         //type
         "type"?:string


        /**
         *
         **/
         //isNameRequired
         isNameRequired?:boolean


        /**
         *
         **/
         //tag
         tag?:string


        /**
         *
         **/
         //operator
         operator?:string


        /**
         *
         **/
         //charset
         charset?:string


        /**
         *
         **/
         //fuzzy
         fuzzy?:number


        /**
         *
         **/
         //searchlang
         searchlang?:string


        /**
         *
         **/
         //orderby
         orderby?:string


        /**
         *
         **/
         //style
         style?:string


        /**
         *
         **/
         //east
         east?:number


        /**
         *
         **/
         //west
         west?:number


        /**
         *
         **/
         //north
         north?:string


        /**
         *
         **/
         //south
         south?:number
}
export interface Search_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //totalResultsCount
         totalResultsCount?:number


        /**
         *
         **/
         //geoname
         geoname?:{

        /**
         *
         **/
         //toponymName
         toponymName?:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //geonameId
         geonameId?:number


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //fcl
         fcl?:string


        /**
         *
         **/
         //fcode
         fcode?:string
}[]


        /**
         *
         **/
         //@style
         "@style":string
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface CountryCodeResource{

        /**
         *  The iso country code of any given point. Result : returns the iso country code for the given latitude/longitude With the parameter type=xml this service returns an xml document with iso country code and country name. The optional parameter lang can be used to specify the language the country name should be in. JSON output is produced with type=JSON
         * @ramlpath /countryCode{mediaTypeSuffix}  get
         **/
         get( options?:CountryCodeResourceGetOptions ):CountryCode | CountryCode_xml
}
export interface CountryCodeResourceGetOptions{

        /**
         *
         **/
         //type
         "type"?:string


        /**
         *
         **/
         //lang
         lang?:string


        /**
         *
         **/
         //radius 
         radius ?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number
}
export interface CountryCode{

        /**
         *
         **/
         //languages
         languages?:string


        /**
         *
         **/
         //distance
         distance?:string


        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface CountryCode_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //country
         country?:{

        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //languages
         languages?:string


        /**
         *
         **/
         //distance
         distance?:number
}
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface EarthquakesResource{

        /**
         *  Result : returns a list of earthquakes, ordered by magnitude
         * @ramlpath /earthquakes{mediaTypeSuffix}  get
         **/
         get( options?:EarthquakesResourceGetOptions ):Earthquakes | Earthquakes_xml
}
export interface EarthquakesResourceGetOptions{

        /**
         *
         **/
         //callback 
         callback ?:string


        /**
         *
         **/
         //date 
         date ?:string


        /**
         *
         **/
         //minMagnitude 
         minMagnitude ?:string


        /**
         *
         **/
         //maxRows 
         maxRows ?:string


        /**
         *
         **/
         //east
         east?:number


        /**
         *
         **/
         //west
         west?:number


        /**
         *
         **/
         //north
         north?:string


        /**
         *
         **/
         //south
         south?:number
}
export interface Earthquakes{

        /**
         *
         **/
         //earthquakes
         earthquakes?:{

        /**
         *
         **/
         //eqid
         eqid?:string


        /**
         *
         **/
         //magnitude
         magnitude?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //src
         src?:string


        /**
         *
         **/
         //datetime
         datetime?:string


        /**
         *
         **/
         //depth
         depth?:number


        /**
         *
         **/
         //lat
         lat?:number
}[]


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface Earthquakes_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //earthquake
         earthquake?:{

        /**
         *
         **/
         //src
         src?:string


        /**
         *
         **/
         //eqid
         eqid?:string


        /**
         *
         **/
         //datetime
         datetime?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //magnitude
         magnitude?:number


        /**
         *
         **/
         //depth
         depth?:number
}[]
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface GetResource{

        /**
         *  Result : returns the attribute of the geoNames feature with the given geonameId as xml document
         * @ramlpath /get{mediaTypeSuffix}  get
         **/
         get( options?:GetResourceGetOptions ):Get | Get_xml
}
export interface GetResourceGetOptions{

        /**
         *
         **/
         //geonameId
         geonameId?:string


        /**
         *
         **/
         //lang 
         lang ?:string


        /**
         *
         **/
         //style
         style?:string
}
export interface Get{

        /**
         *
         **/
         //alternateNames
         alternateNames?:{

        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //lang
         lang?:string
}[]


        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //adminCode1
         adminCode1?:string


        /**
         *
         **/
         //lng
         lng?:string


        /**
         *
         **/
         //adminName2
         adminName2?:string


        /**
         *
         **/
         //fcodeName
         fcodeName?:string


        /**
         *
         **/
         //adminName3
         adminName3?:string


        /**
         *
         **/
         //timezone
         timezone?:{

        /**
         *
         **/
         //dstOffset
         dstOffset?:number


        /**
         *
         **/
         //gmtOffset
         gmtOffset?:number


        /**
         *
         **/
         //timeZoneId
         timeZoneId?:string
}


        /**
         *
         **/
         //adminName4
         adminName4?:string


        /**
         *
         **/
         //adminName5
         adminName5?:string


        /**
         *
         **/
         //bbox
         bbox?:{

        /**
         *
         **/
         //south
         south?:number


        /**
         *
         **/
         //east
         east?:number


        /**
         *
         **/
         //north
         north?:number


        /**
         *
         **/
         //west
         west?:number
}


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //fcode
         fcode?:string


        /**
         *
         **/
         //geonameId
         geonameId?:number


        /**
         *
         **/
         //lat
         lat?:string


        /**
         *
         **/
         //population
         population?:number


        /**
         *
         **/
         //adminName1
         adminName1?:string


        /**
         *
         **/
         //countryId
         countryId?:string


        /**
         *
         **/
         //adminId1
         adminId1?:string


        /**
         *
         **/
         //fclName
         fclName?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //srtm3
         srtm3?:number


        /**
         *
         **/
         //toponymName
         toponymName?:string


        /**
         *
         **/
         //fcl
         fcl?:string


        /**
         *
         **/
         //continentCode
         continentCode?:string


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface Get_xml{

        /**
         *
         **/
         //geoname
         geoname?:{

        /**
         *
         **/
         //toponymName
         toponymName?:string


        /**
         *
         **/
         //name
         name?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //geonameId
         geonameId?:number


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //fcl
         fcl?:string


        /**
         *
         **/
         //fcode
         fcode?:string


        /**
         *
         **/
         //fclName
         fclName?:string


        /**
         *
         **/
         //fcodeName
         fcodeName?:string


        /**
         *
         **/
         //population
         population?:string


        /**
         *
         **/
         //alternateNames
         alternateNames?:string


        /**
         *
         **/
         //elevation
         elevation?:string


        /**
         *
         **/
         //srtm3
         srtm3?:number


        /**
         *
         **/
         //continentCode
         continentCode?:string


        /**
         *
         **/
         //adminCode1
         adminCode1?:{}


        /**
         *
         **/
         //adminName1
         adminName1?:string


        /**
         *
         **/
         //adminCode2
         adminCode2?:string


        /**
         *
         **/
         //adminName2
         adminName2?:string


        /**
         *
         **/
         //alternateName
         alternateName?:{}


        /**
         *
         **/
         //timezone
         timezone?:{}


        /**
         *
         **/
         //bbox
         bbox?:{

        /**
         *
         **/
         //west
         west?:number


        /**
         *
         **/
         //north
         north?:number


        /**
         *
         **/
         //east
         east?:number


        /**
         *
         **/
         //south
         south?:number
}
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface NeighboursResource{

        /**
         *  Returns all neighbours for a country or administrative division. (coverage: all countries on country level, and lower levels as specified here: supported levels)
         * @ramlpath /neighbours{mediaTypeSuffix}  get
         **/
         get( options?:NeighboursResourceGetOptions ):Siblings | Neighbourhood_xml
}
export interface NeighboursResourceGetOptions{

        /**
         *
         **/
         //geonameId
         geonameId?:string
}
export type NeighboursResourceGetOptions1=SiblingsResourceGetOptions
export interface PostalCodeCountryInfoResource{

        /**
         *  Result : countries for which postal code geocoding is available.
         * @ramlpath /postalCodeCountryInfo{mediaTypeSuffix}  get
         **/
         get:PostalCodeCountryInfo | CountryInfo_xml
}
export interface PostalCodeCountryInfo{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //countryName
         countryName?:string


        /**
         *
         **/
         //numPostalCodes
         numPostalCodes?:number


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //maxPostalCode
         maxPostalCode?:string


        /**
         *
         **/
         //minPostalCode
         minPostalCode?:string
}[]


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface Srtm3Resource{

        /**
         *  sample area: ca 90m x 90m Result : a single number giving the elevation in meters according to srtm3, ocean areas have been masked as "no data" and have been assigned a value of -32768 
         * @ramlpath /srtm3{mediaTypeSuffix}  get
         **/
         get( options?:Srtm3ResourceGetOptions ):Srtm | Srtm_xml


        /**
         *  sample area: ca 90m x 90m Result : a single number giving the elevation in meters according to srtm3, ocean areas have been masked as "no data" and have been assigned a value of -32768 
         * @ramlpath /srtm3{mediaTypeSuffix}  post
         **/
         post( options?:Srtm3ResourcePostOptions ):PostSrtm3_mediatypesuffix_XmlResponse | PostSrtm3_mediatypesuffix_JsonResponse
}
export interface Srtm3ResourceGetOptions{

        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number
}
export type Srtm3ResourceGetOptions1=ExtendedFindNearbyResourceGetOptions
export interface Srtm{

        /**
         *
         **/
         //srtm3
         srtm3?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface Srtm_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //srtm3
         srtm3?:number


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface Srtm3ResourcePostOptions{

        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number
}
export type Srtm3ResourcePostOptions1=ExtendedFindNearbyResourceGetOptions
export interface PostSrtm3_mediatypesuffix_XmlResponse{}
export interface PostSrtm3_mediatypesuffix_JsonResponse{}
export interface WeatherResource{

        /**
         *  Result : returns a list of weather stations with the most recent weather observation
         * @ramlpath /weather{mediaTypeSuffix}  get
         **/
         get( options?:WeatherResourceGetOptions ):Weather | Weather_xml
}
export interface WeatherResourceGetOptions{

        /**
         *
         **/
         //callback 
         callback ?:string


        /**
         *
         **/
         //maxRows 
         maxRows ?:string


        /**
         *
         **/
         //east
         east?:number


        /**
         *
         **/
         //west
         west?:number


        /**
         *
         **/
         //north
         north?:string


        /**
         *
         **/
         //south
         south?:number
}
export interface Weather{

        /**
         *
         **/
         //weatherObservations
         weatherObservations?:{

        /**
         *
         **/
         //clouds
         clouds?:string


        /**
         *
         **/
         //weatherCondition
         weatherCondition?:string


        /**
         *
         **/
         //observation
         observation?:string


        /**
         *
         **/
         //windDirection
         windDirection?:number


        /**
         *
         **/
         //ICAO
         ICAO?:string


        /**
         *
         **/
         //seaLevelPressure
         seaLevelPressure?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //temperature
         temperature?:string


        /**
         *
         **/
         //dewPoint
         dewPoint?:string


        /**
         *
         **/
         //windSpeed
         windSpeed?:string


        /**
         *
         **/
         //humidity
         humidity?:number


        /**
         *
         **/
         //stationName
         stationName?:string


        /**
         *
         **/
         //datetime
         datetime?:string


        /**
         *
         **/
         //lat
         lat?:number
} | {

        /**
         *
         **/
         //clouds
         clouds?:string


        /**
         *
         **/
         //weatherCondition
         weatherCondition?:string


        /**
         *
         **/
         //observation
         observation?:string


        /**
         *
         **/
         //windDirection
         windDirection?:number


        /**
         *
         **/
         //ICAO
         ICAO?:string


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //temperature
         temperature?:string


        /**
         *
         **/
         //dewPoint
         dewPoint?:string


        /**
         *
         **/
         //windSpeed
         windSpeed?:string


        /**
         *
         **/
         //humidity
         humidity?:number


        /**
         *
         **/
         //stationName
         stationName?:string


        /**
         *
         **/
         //datetime
         datetime?:string


        /**
         *
         **/
         //lat
         lat?:number
} | {

        /**
         *
         **/
         //clouds
         clouds?:string


        /**
         *
         **/
         //weatherCondition
         weatherCondition?:string


        /**
         *
         **/
         //observation
         observation?:string


        /**
         *
         **/
         //windDirection
         windDirection?:number


        /**
         *
         **/
         //ICAO
         ICAO?:string


        /**
         *
         **/
         //seaLevelPressure
         seaLevelPressure?:number


        /**
         *
         **/
         //cloudsCode
         cloudsCode?:string


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //temperature
         temperature?:string


        /**
         *
         **/
         //dewPoint
         dewPoint?:string


        /**
         *
         **/
         //windSpeed
         windSpeed?:string


        /**
         *
         **/
         //humidity
         humidity?:number


        /**
         *
         **/
         //stationName
         stationName?:string


        /**
         *
         **/
         //datetime
         datetime?:string


        /**
         *
         **/
         //lat
         lat?:number
}[]


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface Weather_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //observation
         observation?:{

        /**
         *
         **/
         //observation
         observation?:string


        /**
         *
         **/
         //observationTime
         observationTime?:string


        /**
         *
         **/
         //stationName
         stationName?:string


        /**
         *
         **/
         //ICAO
         ICAO?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //elevation
         elevation?:number


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //temperature
         temperature?:number


        /**
         *
         **/
         //dewPoint
         dewPoint?:number


        /**
         *
         **/
         //humidity
         humidity?:number


        /**
         *
         **/
         //clouds
         clouds?:string


        /**
         *
         **/
         //weatherCondition
         weatherCondition?:string


        /**
         *
         **/
         //windDirection
         windDirection?:number


        /**
         *
         **/
         //windSpeed
         windSpeed?:number
}[]
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface WeatherIcaoResource{

        /**
         *  Result : returns the weather station and the most recent weather observation for the ICAO code
         * @ramlpath /weatherIcao{mediaTypeSuffix}  get
         **/
         get( options?:WeatherIcaoResourceGetOptions ):WeatherIcao | WeatherIcao_xml
}
export interface WeatherIcaoResourceGetOptions{

        /**
         *
         **/
         //ICAO 
         ICAO ?:string


        /**
         *
         **/
         //callback 
         callback ?:string
}
export interface WeatherIcao{

        /**
         *
         **/
         //weatherObservation
         weatherObservation?:{

        /**
         *
         **/
         //weatherCondition
         weatherCondition?:string


        /**
         *
         **/
         //clouds
         clouds?:string


        /**
         *
         **/
         //observation
         observation?:string


        /**
         *
         **/
         //windDirection
         windDirection?:number


        /**
         *
         **/
         //ICAO
         ICAO?:string


        /**
         *
         **/
         //elevation
         elevation?:number


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //cloudsCode
         cloudsCode?:string


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //temperature
         temperature?:string


        /**
         *
         **/
         //dewPoint
         dewPoint?:string


        /**
         *
         **/
         //windSpeed
         windSpeed?:string


        /**
         *
         **/
         //humidity
         humidity?:number


        /**
         *
         **/
         //stationName
         stationName?:string


        /**
         *
         **/
         //datetime
         datetime?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //hectoPascAltimeter
         hectoPascAltimeter?:number
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface WeatherIcao_xml{

        /**
         *
         **/
         //geonames
         geonames?:{

        /**
         *
         **/
         //observation
         observation?:{

        /**
         *
         **/
         //observation
         observation?:string


        /**
         *
         **/
         //observationTime
         observationTime?:string


        /**
         *
         **/
         //stationName
         stationName?:string


        /**
         *
         **/
         //ICAO
         ICAO?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //elevation
         elevation?:number


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //temperature
         temperature?:number


        /**
         *
         **/
         //dewPoint
         dewPoint?:number


        /**
         *
         **/
         //humidity
         humidity?:number


        /**
         *
         **/
         //clouds
         clouds?:{}


        /**
         *
         **/
         //weatherCondition
         weatherCondition?:{}


        /**
         *
         **/
         //windDirection
         windDirection?:number


        /**
         *
         **/
         //windSpeed
         windSpeed?:number
}[]
}


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface RssToGeoRSSResource{

        /**
         *  returns a RSS feed with latitude and longitude for each entry where the geonames search engine has found a relevant location. Already existant GeoRSS elements in the feed remain unchanged. There is an upper limit of 20 entries for performance reasons.
         * @ramlpath /rssToGeoRSS  get
         **/
         get( options:RssToGeoRSSResourceGetOptions ):GetRsstogeorssXmlResponse
}
export interface RssToGeoRSSResourceGetOptions{

        /**
         *
         **/
         //feedUrl
         feedUrl:string


        /**
         *
         **/
         //feedLanguage
         feedLanguage?:string


        /**
         *
         **/
         //type
         "type"?:string


        /**
         *
         **/
         //geoRSS
         geoRSS?:string


        /**
         *
         **/
         //addUngeocodedItems
         addUngeocodedItems?:boolean


        /**
         *
         **/
         //country
         country?:string
}
export interface GetRsstogeorssXmlResponse{}
export interface PostalCodeLookupResource{

        /**
         *  Result : returns a list of places for the given postalcode in JSON format, sorted by postalcode,placename 
         * @ramlpath /postalCodeLookup{mediaTypeSuffix}  get
         **/
         get( options?:PostalCodeLookupResourceGetOptions ):PostalCodeLookup | GetPostalcodelookup_mediatypesuffix_XmlResponse
}
export interface PostalCodeLookupResourceGetOptions{

        /**
         *
         **/
         //postalcode
         postalcode?:string


        /**
         *
         **/
         //country
         country?:string


        /**
         *
         **/
         //maxRows 
         maxRows ?:string


        /**
         *
         **/
         //callback
         callback?:string


        /**
         *
         **/
         //charset 
         charset ?:string
}
export interface PostalCodeLookup{

        /**
         *
         **/
         //postalcodes
         postalcodes?:{

        /**
         *
         **/
         //adminCode3
         adminCode3?:string


        /**
         *
         **/
         //adminName2
         adminName2?:string


        /**
         *
         **/
         //adminName3
         adminName3?:string


        /**
         *
         **/
         //adminCode2
         adminCode2?:string


        /**
         *
         **/
         //postalcode
         postalcode?:string


        /**
         *
         **/
         //adminCode1
         adminCode1?:string


        /**
         *
         **/
         //countryCode
         countryCode?:string


        /**
         *
         **/
         //lng
         lng?:number


        /**
         *
         **/
         //placeName
         placeName?:string


        /**
         *
         **/
         //lat
         lat?:number


        /**
         *
         **/
         //adminName1
         adminName1?:string
}[]


        /**
         *
         **/
         //__$harEntry__
         __$harEntry__?:HarEntry
}
export interface GetPostalcodelookup_mediatypesuffix_XmlResponse{}


export interface UnknownResponse{ __$harEntry__ : har.Entry }
export interface payloadType{}
export interface responseType{}
export interface invoker{ (url:String,method:string,options:any):any; }
export class ApiImpl implements Api 
{
private baseUrl:string='http://api.geonames.org'
private cfgEncoded=/*CONFIGENCODEDSTART*/{"numberIsString":true,"createTypesForResources":true,"queryParametersSecond":true,"collapseGet":false,"collapseOneMethod":false,"collapseMediaTypes":false,"methodNamesAsPrefixes":false,"storeHarEntry":true,"createTypesForParameters":true,"reuseTypeForParameters":true,"createTypesForSchemaElements":true,"reuseTypesForSchemaElements":true,"throwExceptionOnIncorrectStatus":false,"async":false,"debugOptions":{"generateImplementation":true,"generateSchemas":false,"generateInterface":true,"resourcePathFilter":null},"overwriteModules":true};/*CONFIGENCODEDEND*/

declaration():RamlWrapper.Api{
    var unit = new JsonModel.CompilationUnit(null,null,apiProvider.api(),null,true);
    var highLevelNode:highLevel.IHighLevelNode = new highLevelImpl.ASTNodeImpl(unit.ast(),null,<any>apiType,null);
    var api : RamlWrapper.Api = new RamlWrapper.ApiImpl(highLevelNode);
    endpoints.setApi(api);
    return api;
}

securityProvider():authManager.SecurityParametersProvider{
    var api:RamlWrapper.Api = this.declaration();
    return env.getSecurityProvider().getSubProvider(api.title()).getSubProvider(api.version());
}

authentificate(schemaName:string, options?:any){}
log(vName:string,val:any){this.inv.log(vName,val);return val;}baseUrlResolved():string{
        var burl=this.baseUrl;
        
        return burl;
        }
            private inv:executor.APIExecutor
            private options:any
            private canonicPath:string[] = []
            
        invoke(path:string,method:string,canonicPath:string[],obj:any){
            env.registerApi(this.declaration())
            env.getAuthManager().registerSchemes(this.declaration(),[])
            return this.inv.execute(path,method,obj,canonicPath)
        }

            authenticate(schemaName?:string,options?:any):any{return null;}

            constructor(){
                this.inv=new executor.APIExecutor(this.declaration(),this.baseUrlResolved(),<any>this.cfgEncoded);
                
            }
postalCodeSearch=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new PostalCodeSearchResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/postalCodeSearch{mediaTypeSuffix}'))
return res;/*d*postalCodeSearch*/}
extendedFindNearby=new ExtendedFindNearbyResourceImpl(this,this.canonicPath.concat('/extendedFindNearby'))
astergdem=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new AstergdemResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/astergdem{mediaTypeSuffix}'))
return res;/*d*astergdem*/}
children=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new ChildrenResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/children{mediaTypeSuffix}'))
return res;/*d*children*/}
cities=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new CitiesResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/cities{mediaTypeSuffix}'))
return res;/*d*cities*/}
findNearbyPostalCodes=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new FindNearbyPostalCodesResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/findNearbyPostalCodes{mediaTypeSuffix}'))
return res;/*d*findNearbyPostalCodes*/}
findNearbyPlaceName=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new FindNearbyPlaceNameResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/findNearbyPlaceName{mediaTypeSuffix}'))
return res;/*d*findNearbyPlaceName*/}
findNearby=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new FindNearbyResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/findNearby{mediaTypeSuffix}'))
return res;/*d*findNearby*/}
hierarchy=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new HierarchyResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/hierarchy{mediaTypeSuffix}'))
return res;/*d*hierarchy*/}
contains=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new ContainsResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/contains{mediaTypeSuffix}'))
return res;/*d*contains*/}
siblings=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new SiblingsResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/siblings{mediaTypeSuffix}'))
return res;/*d*siblings*/}
findNearbyWikipedia=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new FindNearbyWikipediaResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/findNearbyWikipedia{mediaTypeSuffix}'))
return res;/*d*findNearbyWikipedia*/}
wikipediaSearch=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new WikipediaSearchResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/wikipediaSearch{mediaTypeSuffix}'))
return res;/*d*wikipediaSearch*/}
wikipediaBoundingBox=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new WikipediaBoundingBoxResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/wikipediaBoundingBox{mediaTypeSuffix}'))
return res;/*d*wikipediaBoundingBox*/}
findNearByWeather=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new FindNearByWeatherResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/findNearByWeather{mediaTypeSuffix}'))
return res;/*d*findNearByWeather*/}
findNearestAddress=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new FindNearestAddressResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/findNearestAddress{mediaTypeSuffix}'))
return res;/*d*findNearestAddress*/}
findNearestIntersection=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new FindNearestIntersectionResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/findNearestIntersection{mediaTypeSuffix}'))
return res;/*d*findNearestIntersection*/}
findNearbyStreets=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new FindNearbyStreetsResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/findNearbyStreets{mediaTypeSuffix}'))
return res;/*d*findNearbyStreets*/}
findNearestIntersectionOSM=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new FindNearestIntersectionOSMResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/findNearestIntersectionOSM{mediaTypeSuffix}'))
return res;/*d*findNearestIntersectionOSM*/}
findNearbyStreetsOSM=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new FindNearbyStreetsOSMResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/findNearbyStreetsOSM{mediaTypeSuffix}'))
return res;/*d*findNearbyStreetsOSM*/}
findNearbyPOIsOSM=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new FindNearbyPOIsOSMResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/findNearbyPOIsOSM{mediaTypeSuffix}'))
return res;/*d*findNearbyPOIsOSM*/}
countrySubdivision=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new CountrySubdivisionResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/countrySubdivision{mediaTypeSuffix}'))
return res;/*d*countrySubdivision*/}
ocean=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new OceanResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/ocean{mediaTypeSuffix}'))
return res;/*d*ocean*/}
neighbourhood=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new NeighbourhoodResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/neighbourhood{mediaTypeSuffix}'))
return res;/*d*neighbourhood*/}
timezone=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new TimezoneResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/timezone{mediaTypeSuffix}'))
return res;/*d*timezone*/}
gtopo30=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new Gtopo30ResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/gtopo30{mediaTypeSuffix}'))
return res;/*d*gtopo30*/}
countryInfo=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new CountryInfoResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/countryInfo{mediaTypeSuffix}'))
return res;/*d*countryInfo*/}
search=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new SearchResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/search{mediaTypeSuffix}'))
return res;/*d*search*/}
countryCode=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new CountryCodeResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/countryCode{mediaTypeSuffix}'))
return res;/*d*countryCode*/}
earthquakes=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new EarthquakesResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/earthquakes{mediaTypeSuffix}'))
return res;/*d*earthquakes*/}
get=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new GetResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/get{mediaTypeSuffix}'))
return res;/*d*get*/}
neighbours=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new NeighboursResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/neighbours{mediaTypeSuffix}'))
return res;/*d*neighbours*/}
postalCodeCountryInfo=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new PostalCodeCountryInfoResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/postalCodeCountryInfo{mediaTypeSuffix}'))
return res;/*d*postalCodeCountryInfo*/}
srtm3=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new Srtm3ResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/srtm3{mediaTypeSuffix}'))
return res;/*d*srtm3*/}
weather=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new WeatherResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/weather{mediaTypeSuffix}'))
return res;/*d*weather*/}
weatherIcao=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new WeatherIcaoResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/weatherIcao{mediaTypeSuffix}'))
return res;/*d*weatherIcao*/}
rssToGeoRSS=new RssToGeoRSSResourceImpl(this,this.canonicPath.concat('/rssToGeoRSS'))
postalCodeLookup=( mediaTypeSuffix:string="JSON" )=>{
var res=<any> 
new PostalCodeLookupResourceImpl(mediaTypeSuffix, this,this.canonicPath.concat('/postalCodeLookup{mediaTypeSuffix}'))
return res;/*d*postalCodeLookup*/}

 /* type ending */ }

            
 var meta={}
