import fs=require("fs")
import path=require("path")
import tsstruct=require("./tsStructureParser")
import ts2def=require("./tsStrut2Def")
import def=require("./definitionSystem")

var universes:{[key:string]:def.Universe}={}

var locations = {

    "RAML10" : "./spec-1.0/api.ts",

    "RAML08" : "./spec-0.8/api.ts",

    "Swagger2" : "./spec-swagger-2.0/swagger.ts"
};

var jsonDefinitions = {

    "RAML10" : require("./artifacts/RAML10"),

    "RAML08" : require("./artifacts/RAML08"),

    "Swagger2" : require("./artifacts/Swagger2")
};


interface UniverseProvider{

    (key:string):def.Universe

    availableUniverses():string[]

    clean()
}

var getUniverse:UniverseProvider = (()=>{

    var x:any = (key:string)=>{

        if(universes[key]){
            return universes[key];
        }
        var src = getDecl(key);
        var universe = ts2def.toDefSystem(src);
        if(universe) {
            universe.setUniverseVersion(key);
            universes[key] = universe;
        }
        var mediaTypeParser=require("media-typer")
        global.mediaTypeParser=mediaTypeParser;
        return universe;
    }
    x.availableUniverses = function(){return Object.keys(locations)}
    x.clean = function(){
        Object.keys(jsonDefinitions).forEach(x=>{
            jsonDefinitions[x] = null;
            universes[x] = null;
            fs.writeFileSync(path.resolve(__dirname,`./artifacts/${x}.json`),'null');
        });
    }
    return x;
})();

function getDecl(key:string){

    if(jsonDefinitions[key]){
        return toModule(jsonDefinitions[key]);
    }

    var tsPath=path.resolve(__dirname,locations[key]);
    var decls=fs.readFileSync(tsPath).toString();
    var src=tsstruct.parseStruct(decls,{},tsPath);
    var arr = toModulesCollection(src);

    var jsonPath = path.resolve(path.resolve(path.dirname(tsPath),'../artifacts'),key+'.json');
    fs.writeFileSync(jsonPath, JSON.stringify(arr, null, 2));

    src = toModule(arr);
    return src;
}

function toModulesCollection(mod:any, map:any={},arr:any[]=[]):any[]{

    var name = mod['name'];
    if(map[name]){
        return;
    }
    map[name] = mod;
    arr.push(mod);
    var imports = mod['imports'];
    Object.keys(imports).forEach(x=>{

        var submod = imports[x];
        var n = submod['name'];
        imports[x] = n;
        toModulesCollection(submod,map,arr);
    });
    return arr;
}

function toModule(arr:any[]):any{

    var main = arr[0];
    var map = {}
    arr.forEach(x=>map[x['name']]=x);
    arr.forEach(x=>{
        var imports = x['imports'];
        Object.keys(imports).forEach(y=>{
            var name = imports[y];
            imports[y] = map[name];
        });
    });
    return main;
}

export = getUniverse;