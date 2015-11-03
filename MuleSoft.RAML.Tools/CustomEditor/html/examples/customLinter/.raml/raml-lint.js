function lintResource(r,errorFactory){
  var relativeUri=r.relativeUri().value();
  if (relativeUri.indexOf("?")!=-1){
    errorFactory.error(r,"Relative url should not contain '?'");
  }
}
this.registerRule("Resource",lintResource)
