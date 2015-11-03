import hl=require("./highLevelAST");
import hlImpl=require("./highLevelImpl");
import jsyaml=require("./jsyaml/jsyaml2lowLevel");
import def=require("./definitionSystem");

export interface AbstractWrapperNode{

    wrapperClassName():string
}

export interface BasicSuperNode extends AbstractWrapperNode{

    parent():BasicSuperNode

    highLevel():hl.IHighLevelNode
}

export class BasicSuperNodeImpl implements BasicSuperNode{

    constructor(protected _node:hl.IHighLevelNode){
        _node.setWrapperNode(this);
    }

    wrapperClassName():string{
        return 'BasicSuperNodeImpl';
    }

    parent():BasicSuperNode{
        var parent = this._node.parent()
        return parent ? parent.wrapperNode() : null;
    }

    highLevel():hl.IHighLevelNode{
        return this._node;
    }

    attributes(name:string,constr?:(attr:hl.IAttribute)=>any):any[]{
        var attrs:hl.IAttribute[] = this._node.attributes(name);
        if(!attrs){
            return null;
        }
        if(constr){
            return attrs.map(x=>constr(x));
        }
        else{
            return attrs.map(x=>x.value());
        }
    }

    attribute(name:string,constr?:(attr:hl.IAttribute)=>any):any{
        var attr:hl.IAttribute = this._node.attr(name);
        if(!attr){
            return null;
        }
        if(constr){
            return constr(attr);
        }
        else{
            return attr.value();
        }
    }

    elements(name:string):any[]{
        var elements:hl.IHighLevelNode[] = this._node.elementsOfKind(name);
        if(!elements){
            return null;
        }
        return elements.map(x=>x.wrapperNode());
    }

    element(name:string):any{
        var element:hl.IHighLevelNode = this._node.element(name);
        if(!element){
            return null;
        }
        return element.wrapperNode();
    }

    add(node:BasicSuperNodeImpl){
        this.highLevel().add(node.highLevel());
    }
    addToProp(node:BasicSuperNodeImpl,prop:string){
        var hl=<any>node.highLevel();
        var pr=(<def.NodeClass>this.highLevel().definition()).property(prop);
        (<any>hl)._prop=pr;
        this.highLevel().add(hl);
    }

    remove(node:BasicSuperNodeImpl){
        this.highLevel().remove(node.highLevel());
    }

    dump(){
        return this.highLevel().dump("yaml");
    }
}

export function toStructuredValue(node:hl.IAttribute):hlImpl.StructuredValue{
    var value = node.value();
    if(typeof value ==='string'){
        var mockNode=jsyaml.createNode(value.toString());
        mockNode._actualNode().startPosition=node.lowLevel().valueStart();
        mockNode._actualNode().endPosition=node.lowLevel().valueEnd();
        var stv=new hlImpl.StructuredValue(mockNode,node.parent(),node.property());
        return stv;
    }
    else{
        return <hlImpl.StructuredValue>value;
    }
}
