function lintMethod(r,errorFactory){
  var method=r.method();
  if (method=="post"){
      var qp=r.queryParameters();
      if (qp&&qp.length>0){
        errorFactory.warning(r,"POST methods should not have query parameters");
      }

  }
}
this.registerRule("Method",lintMethod)
