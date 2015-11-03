import hl=require("./highLevelAST");
import core=require("./parserCore");
import factory10 = require("./artifacts/raml003factory")
import factory08 = require("./artifacts/raml08factory")

export function buildWrapperNode(node:hl.IHighLevelNode):core.BasicSuperNode{

    var ramlVersion = node.definition().universe().version();
    if(ramlVersion=='RAML10'){
        return factory10.buildWrapperNode(node);
    }
    else if(ramlVersion=='RAML08'){
        return factory08.buildWrapperNode(node);
    }
    return null;

}