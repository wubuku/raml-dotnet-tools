
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Movies
{
    [RoutePrefix("movies")]
    public partial class MoviesController : ApiController
    {


        /// gets all movies in the catalogue
        [HttpGet]
        [Route("")]
        public virtual async Task<IHttpActionResult> Get()
        {
            return Ok();
        }


        /// adds a movie to the catalogue
        [HttpPost]
        [Route("")]
        public virtual async Task<IHttpActionResult> Post(Models.MoviesPostRequestContent moviespostrequestcontent,[FromUri] string access_token = null)
        {
            return Ok();
        }


        /// get the info of a movie
        [HttpGet]
        [Route("{id}")]
        public virtual async Task<IHttpActionResult> GetById([FromUri] string id)
        {
            return Ok();
        }


        /// update the info of a movie
        [HttpPut]
        [Route("{id}")]
        public virtual async Task<IHttpActionResult> Put(Models.IdPutRequestContent idputrequestcontent,[FromUri] string id)
        {
            return Ok();
        }


        /// remove a movie from the catalogue
        [HttpDelete]
        [Route("{id}")]
        public virtual async Task<IHttpActionResult> Delete([FromUri] string id)
        {
            return Ok();
        }


        /// rent a movie
        [HttpPut]
        [Route("{id}/rent")]
        public virtual async Task<IHttpActionResult> PutRent(string json,[FromUri] string id,[FromUri] string access_token = null)
        {
            return Ok();
        }


        /// return a movie
        [HttpPut]
        [Route("{id}/return")]
        public virtual async Task<IHttpActionResult> PutReturn(string json,[FromUri] string id,[FromUri] string access_token = null)
        {
            return Ok();
        }


        /// gets the current user movies wishlist
        [HttpGet]
        [Route("wishlist")]
        public virtual async Task<IHttpActionResult> GetWishlist([FromUri] string access_token = null)
        {
            return Ok();
        }


        /// add a movie to the current user movies wishlist
        [HttpPost]
        [Route("wishlist/{id}")]
        public virtual async Task<IHttpActionResult> PostById(string json,[FromUri] string id,[FromUri] string access_token = null)
        {
            return Ok();
        }


        /// removes a movie from the current user movies wishlist
        [HttpDelete]
        [Route("wishlist/{id}")]
        public virtual async Task<IHttpActionResult> DeleteById([FromUri] string id,[FromUri] string access_token = null)
        {
            return Ok();
        }


        /// gets the user rented movies
        [HttpGet]
        [Route("rented")]
        public virtual async Task<IHttpActionResult> GetRented()
        {
            return Ok();
        }


        /// get all movies that are not currently rented
        [HttpGet]
        [Route("available")]
        public virtual async Task<IHttpActionResult> GetAvailable()
        {
            return Ok();
        }

    }
}