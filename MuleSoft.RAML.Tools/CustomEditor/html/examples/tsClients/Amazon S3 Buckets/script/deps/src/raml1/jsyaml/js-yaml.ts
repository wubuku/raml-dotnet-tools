/// <reference path="../../../typings/tsd.d.ts" />

'use strict';


import loader = require('./js-yaml/loader');
import dumper = require('./js-yaml/dumper');


function deprecated(name) {
  return function () {
    throw new Error('Function ' + name + ' is deprecated and cannot be used.');
  };
}

export var Type                = require('./js-yaml/type');
export var Schema              = require('./js-yaml/schema');
export var FAILSAFE_SCHEMA     = require('./js-yaml/schema/failsafe');
export var JSON_SCHEMA         = require('./js-yaml/schema/json');
export var CORE_SCHEMA         = require('./js-yaml/schema/core');
export var DEFAULT_SAFE_SCHEMA = require('./js-yaml/schema/default_safe');
export var DEFAULT_FULL_SCHEMA = require('./js-yaml/schema/default_full');
export var load= loader.load;
export var loadAll             = loader.loadAll;
export var safeLoad            = loader.safeLoad;
export var safeLoadAll         = loader.safeLoadAll;
export var dump                = dumper.dump;
export var safeDump            = dumper.safeDump;
export var YAMLException       = require('./js-yaml/exception');

// Deprecared schema names from JS-YAML 2.0.x
export var MINIMAL_SCHEMA = require('./js-yaml/schema/failsafe');
export var SAFE_SCHEMA    = require('./js-yaml/schema/default_safe');
export var DEFAULT_SCHEMA = require('./js-yaml/schema/default_full');

// Deprecated functions from JS-YAML 1.x.x
export var scan           = deprecated('scan');
export var parse          = deprecated('parse');
export var compose        = deprecated('compose');
export var addConstructor = deprecated('addConstructor');
