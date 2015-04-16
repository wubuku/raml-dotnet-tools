// Template: Controller Interface (ApiControllerInterface.t4) version 0.1

using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using MoviesWebApi2Sample.Movies.Models;

namespace MoviesWebApi2Sample.Movies
{
    public interface IMoviesController
    {

        IHttpActionResult Get();

        IHttpActionResult Post(Models.MoviesPostRequestContent moviespostrequestcontent,[FromUri] string access_token = null);

        IHttpActionResult GetById([FromUri] string id);

        IHttpActionResult Put(Models.IdPutRequestContent idputrequestcontent,[FromUri] string id);

        IHttpActionResult Delete([FromUri] string id);

        IHttpActionResult PutRent(string json,[FromUri] string id,[FromUri] string access_token = null);

        IHttpActionResult PutReturn(string json,[FromUri] string id,[FromUri] string access_token = null);

        IHttpActionResult GetWishlist([FromUri] string access_token = null);

        IHttpActionResult PostById(string json,[FromUri] string id,[FromUri] string access_token = null);

        IHttpActionResult DeleteById([FromUri] string id,[FromUri] string access_token = null);

        IHttpActionResult GetRented();

        IHttpActionResult GetAvailable();

    }
}
