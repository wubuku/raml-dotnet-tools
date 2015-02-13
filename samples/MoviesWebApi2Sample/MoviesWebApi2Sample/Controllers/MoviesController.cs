
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using MoviesWebApi2Sample.Movies.Models;

namespace MoviesWebApi2Sample.Movies
{
    public partial class MoviesController : IMoviesController
    {


        public IHttpActionResult Get()
        {
            // put your code here
	        var movies = new[]
	                     {
		                     new MoviesGetOKResponseContent {Director = "Tim Burton", Name = "Big Fish "},
		                     new MoviesGetOKResponseContent {Director = "Woody Allen", Name = "Midnight in Paris"}
	                     };
            return Ok(movies);
        }


        public IHttpActionResult Post(Models.MoviesPostRequestContent moviespostrequestcontent,[FromUri] string access_token = null)
        {
            // put your code here
            return Ok();
        }


        public IHttpActionResult GetById([FromUri] string id)
        {
            // put your code here
	        var movie = new IdGetOKResponseContent
	                    {
		                    Director = "Tim Burton",
		                    Name = "Big Fish"
	                    };
            return Ok(movie);
        }


        public IHttpActionResult Put(Models.IdPutRequestContent idputrequestcontent,[FromUri] string id)
        {
            // put your code here
            return Ok();
        }


        public IHttpActionResult Delete([FromUri] string id)
        {
            // put your code here
            return Ok();
        }


        public IHttpActionResult PutRent(string json,[FromUri] string id,[FromUri] string access_token = null)
        {
            // put your code here
            return Ok();
        }


        public IHttpActionResult PutReturn(string json,[FromUri] string id,[FromUri] string access_token = null)
        {
            // put your code here
            return Ok();
        }


        public IHttpActionResult GetWishlist([FromUri] string access_token = null)
        {
            // put your code here
            return Ok();
        }


        public IHttpActionResult PostById(string json,[FromUri] string id,[FromUri] string access_token = null)
        {
            // put your code here
            return Ok();
        }


        public IHttpActionResult DeleteById([FromUri] string id,[FromUri] string access_token = null)
        {
            // put your code here
            return Ok();
        }


        public IHttpActionResult GetRented()
        {
            // put your code here
            return Ok();
        }


        public IHttpActionResult GetAvailable()
        {
            // put your code here
            return Ok();
        }

    }
}