/**
 * Created by kor on 26/06/15.
 */

//////////////////////////////////////////
// System classes

export interface SpecPartMetaData{
    title:string
}


export enum RAMLVersion{
    RAML08,RAML10
}

/////////////////////////////////////////////
//   Property semantic constraints         //

/**
 * declares that annotated property may only
 * have values that are members of args array
 * @param args
 */
export function oneOf(args:string[]) {};

/**
 * declares default value for a simple property
 * (works only for string typed properties)
 * @param v
 */
export function defaultValue(v:string){}

/**
 * declares that values of annotated property
 * should start from 'value' argument value
 * @param value
 */
export function startFrom(value:string) {}

export function facetId(value:string){}
/**
 * marks that annotated field represent object key
 * in yaml parsing values for annotated field will be taken
 * from mapping key
 *
 * For example when parsing folloeing yaml mapping
 * offset:
 *   type:string
 *   default:10
 *
 * to object model value of property annotated with key will be offset
 *
 */
export function key() {}

/**
 * marks that annotated field represent object key
 * in yaml parsing values for annotated field will be taken
 * from mapping key
 *
 * For example when parsing folloeing yaml mapping
 * offset:
 *   type:string
 *   default:10
 *
 * to object model value of property annotated with key will be offset
 *
 */
export function value() {}

/**
 * marks that annotated field represent object key
 * in yaml parsing values for annotated field will be taken
 * from mapping key
 *
 * For example when parsing folloeing yaml mapping
 * offset:
 *   type:string
 *   default:10
 *
 * to object model value of property annotated with key will be offset
 *
 */
export function canBeValue() {}

export function allowMultiple(){}

export function oftenKeys(args:string[]){}

/**
 * this annotation marks annotated property
 * as a property whose values contains field declarations
 * for the type defined by property domain instance
 * may be only applied to members of classes which are annotated
 * with 'declaresSubTypeOf' and contribute to 'open typesystem'
 */
export function declaringFields(){}


/**
 * This annotation marks annotated property as
 * descriminator property for owning type subclasses
 * it also means that annotated property can have only values
 * coming from constraints applied in subclasses to it or
 * names of subclasses classes defined in open part of typesystem
 */
export function descriminatingProperty(){}

/**
 * marks annotated property as required
 */
export function required(){}

/**
 * This annotation sets context value for annotated field
 * values and for their children (recursively)
 * @param field - name of context value
 * @param value - value of context value
 */
export function setsContextValue(field:string,value:any){}

/**
 * marks annotated property as a system property
 * this means that property value will be inherited
 * from context value with same name as annotated property has
 * also user will not be able to change value of annotated property
 */
export function system(){}
/**
 * marks that value of key property may be inherited from context value
 * in this case node may be embededed in parent ast node
 * @param contextValue
 */
export function canInherit(contextValue:string){}

/**
 * very custom case only needed for multiple parameter types
 */
export function canBeDuplicator(){}
/**
 * marks that annotated property is equivalent to placing
 * annotation with name equal to value of 'name' argument
 * and the argument equal to property value
 * can only be used to mark properties of the range of
 * the property annotated with 'declaringFields' annotation
 * @param name
 */
export function describesAnnotation(name:string){}

/**
 * this annotation allows configure label for new value action
 * which is associated with this property
 * TODO move it to class level annotations
 * @param n
 */
export function newInstanceName(n:string){}


/**
 * This annotation is persistence related and means
 * that property values are stored in map
 * only can be aplied to  properties with multiple values
 *
 *
 */
export function embeddedInMaps() {}

export function valueRestriction(exp:string,message:string){}

export function description(text:string){}

export function needsClarification(reason:string){};

export function issue(reason:string){};
/**
 * specifies that this property sets its value into context
 */
export function inherited(){};

export function thisFeatureCovers(issue:string){}


export function semanticTag(tag:string){};

export function grammarTokenKind(token:string){};

export function selector(selector:string){};

export function selfNode(){};

/////////////////////////////////////////////



/////////////////////////////////////////////////////////
//   Class annotations

/**
 * marks the class as class whose instances contribute
 * types to the open type system. Contributed types
 * will be subclasses of class with name equal to value of 'superTypeName' argument
 * properties of contributed types will be discovered from the values of property
 * annotated with 'declaringFields' annotation
 * @param superTypeName
 */
export function declaresSubTypeOf(superTypeName:string){}

/**
 * marks the class as class whose instances contribute types to the 'open type system'
 * Contributed types will not have any super types. Their properties will be calculated
 * based on <<propertyName>> template usage in values of properties of
 * instances who contribute types to 'open type system'
 */
export function inlinedTemplates(){};


/**
 * this annotation should be applied to value types and means that this value type
 * has internal structured content which may be parsed from value string.
 * TODO More doc
 * @param n
 */
export function innerType(n:string) {};

/**
 * This annotation means that instances of class may only placed
 * in the places of hierarchy where context value with a name equal
 * to value of 'field' argument is available and equal to value
 * of 'value' argument
 * @param field
 * @param value
 */
export function requireValue(field:string,value:any){}


export function extraMetaKey(name:string){}
/**
 * this annotation means that this class is already defined at runtime
 * level with name equal to 'name' argument value
 * @param name
 */
export function nameAtRuntime(name:string){}


export function alias(name:string){}
/**
 * This annotation may be placed on types which implements Referancable
 * interface. It means that instantiating this class exports
 * value of the field whose name is passed in 'name' argument to the global context instead
 * of exporting the object
 * @param name
 */
export function actuallyExports(name:string){};

export function version(v:RAMLVersion){};

export function functionalDescriminator(expr:string){};

export function allowNull(){}

export function referenceIs(fieldName:string){}
/**
 * This annotations means that given node can have any nodes as its children
 */
export function allowAny(){}
/**
 * This annotations means that given node can have question at the end of keys of child not primitive nodes
 */
export function allowQuestion(){}

export function convertsToGlobalOfType(name:string){};
/**
 *
 * @param interfaceName
 * @param path
 */
export function providesInterface(interfaceName:string,path:string){}
export function scriptingScope(scope:string){}
export function consumesRefs(){}
export function definingPropertyIsEnough(v:string){}

/////////////////////////////////////////////////////////