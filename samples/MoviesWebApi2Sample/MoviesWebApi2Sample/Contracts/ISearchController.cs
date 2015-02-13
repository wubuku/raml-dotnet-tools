
using System;
using System.Web.Http;
using System.Web.Http.Description;
using MoviesWebApi2Sample.Movies.Models;

namespace MoviesWebApi2Sample.Movies
{
    public interface ISearchController
    {

        IHttpActionResult Get([FromUri] string name = null,[FromUri] string director = null);

    }
}