/// <reference path="../../typings/tsd.d.ts" />
import tsutil = require("../util/tsutil");
import util   = require("../util/index");
import config   = require("./config");
import _ = require("underscore");
import assert = require("assert")

import Opt=require("../Opt");
import RamlWrapper= require("../Raml08Wrapper")
import TS =require("./TSDeclModel")

export class JSONSchematToTS{

    protected name:string

    protected cfg:config.IConfig

    protected module:TS.TSAPIModule

    constructor(name:string,module:TS.TSAPIModule = new TS.TSAPIModule(),cfg?:config.IConfig) {
        //console.log(name);
        this.cfg = cfg
        this.name=name.charAt(0).toUpperCase()+name.substr(1);
        this.name=this.name.replace("-","")
        this.module=module
    }

    protected generateHarEntry():boolean {
        return this.cfg ? this.cfg.storeHarEntry : false
    }

    parse(schema:string,appendHar:boolean=false):TS.TSTypeReference<any>{

        var obj=JSON.parse(schema);
        if(obj['type']=='array'){
            var items = obj['items'];
            if(items){
                var itemsArray:any[]
                if(items instanceof Array){
                    itemsArray = items;
                }
                else{
                    itemsArray = [ items ];
                }
                var componentType:TS.TSTypeReference<any> = new TS.AnyType();
                var i = 0;
                itemsArray.forEach(x=>
                    componentType = componentType.union(this.createInterface(x,appendHar,itemsArray.length>1?'' + i++:''))
                );
                componentType.locked = true;
                var ref:TS.TSArrayReference = new TS.TSArrayReference();
                ref.componentType = componentType;
                return ref;
            }
        }
        return this.createInterface(obj,appendHar);
    }

    private createInterface(obj, appendHar:boolean=false,index:string=''):TS.TSTypeReference<any> {

        //if(obj['patternProperties']&&obj['patternProperties'].length>0){
        //    return new TS.AnyType();
        //}

        var tsi = new TS.TSInterface(this.module ? this.module:TS.Universe, this.name+index);

        this.processNode(obj, tsi);

        if (this.generateHarEntry() && appendHar) {
            this.appendHarEntry(tsi);
        }
        return tsi.toReference();
    }

    appendHarEntry(tsi:TS.TSTypeDeclaration):void{
        var iHarEntryRef;
        var iHarEntry = this.module.getInterface('HarEntry');
        if(iHarEntry.isDefined()){
            iHarEntryRef = iHarEntry.getOrThrow().toReference();
        }
        else{
            this.name = 'HarEntry';
            iHarEntryRef = this.createInterface(HAR_ENTRY_SCHEMA,false);
        }
        var prop = new TS.TSAPIElementDeclaration(tsi,'__$harEntry__');
        prop.rangeType = iHarEntryRef;
        prop.optional = true;
    }

    protected  replace(original:TS.TSTypeDeclaration):TS.TSTypeDeclaration{
        return original;
    }

    protected createTypeDeclaration(pd:TS.TSAPIElementDeclaration):TS.TSTypeDeclaration{
        var td = new TS.TSStructuralTypeReference(pd);
        td.locked=true;
        return td;
    }

    private processNode(obj:any, tsi:TS.TSTypeDeclaration):void {
        for (var prop in obj.properties) {

            var pd = new TS.TSAPIElementDeclaration(tsi, prop);
            var propObj=obj.properties[prop];
            var tv = propObj["type"];
            var rv = propObj["required"];
            if (!rv){
                pd.optional=true;
            }
            if (tv == "array") {
                var componentType:TS.TSTypeReference<any> = new TS.AnyType();
                var items:any[]=[]
                if(propObj.items){
                    if (propObj.items instanceof Array) {
                        items=propObj.items
                    }
                    else{
                        items = items.concat(propObj.items)
                    }
                }

                    items.filter(x=>x["type"]).forEach(x=> {
                        var tp = x["type"];
                        var st:TS.TSTypeReference<any> = null;
                        //if(tp=='object' && x.patternProperties && Object.keys(x.patternProperties).length>0){
                        //    st = new TS.AnyType();
                        //}
                        //else {
                            if (this.isPrimitive(tp)) {
                                st = new TS.TSSimpleTypeReference(pd, tp);
                            }
                            else {
                                var td = this.createTypeDeclaration(pd);
                                this.processNode(x, td);
                                st = this.replace(td).toReference();
                            }
                        //}
                        st.locked = true;
                        componentType = componentType.union(st);
                    })
                var ref:TS.TSArrayReference = new TS.TSArrayReference();
                ref.componentType = componentType;
                pd.rangeType = ref;
            }
            else if (tv === "object") {
                var st:TS.TSTypeReference<any> = null;
                //if(propObj.patternProperties && Object.keys(propObj.patternProperties).length>0){
                //    st = new TS.AnyType();
                //}
                //else {
                    var q = this.createTypeDeclaration(pd);
                    this.processNode(propObj, q);
                    st = this.replace(q).toReference();
                //}
                pd.rangeType = st;
            } else {
                tv=this.cleanUpTypeDecl(tv);
                pd.rangeType = new TS.TSSimpleTypeReference(pd, tv);
            }
        }
    }

    private cleanUpTypeDecl(tv):string {
        if (tv == null) {
            tv = "string"
        }
        tv = tv.toString();
        if (tv.indexOf(",") != -1) {
            tv = "string"
        }
        if (tv.indexOf(" ") != -1) {
            tv = "string"
        }
        if (tv == "integer") {
            tv = "number";
        }
        if (tv == "null") {
            tv = "string"
        }
        if (tv != "string" && tv != "number" && tv != "boolean" && tv != "any[]") {
            tv = "string";
        }
        return tv;
    }

    private isPrimitive(tp) {
        return tp === "string" || tp === "number";
    }
}

export var HAR_ENTRY_SCHEMA = {
            "properties": {
                "response": {
                    "type": "object",
                    "required": true,
                    "properties": {
                        "content": {
                            "type": "object",
                            "required": true,
                            "properties": {
                                "text": {
                                    "type": "string",
                                    "required": true
                                }
                            }
                        },
                        "status": {
                            "type": "number",
                            "required": true
                        }
                    }
                },
                "request": {
                    "type": "object",
                    "required": true
                }
            }
};
