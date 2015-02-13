
using System;
using System.Web.Http;
using System.Web.Http.Description;
using MoviesWebApi2Sample.Movies.Models;

namespace MoviesWebApi2Sample.Movies
{
    public partial class SearchController : ISearchController
    {


        public IHttpActionResult Get([FromUri] string name = null,[FromUri] string director = null)
        {
            // put your code here
			var movies = new[]
	                     {
		                     new SearchGetOKResponseContent {Director = "Tim Burton", Name = "Big Fish "},
		                     new SearchGetOKResponseContent {Director = "Woody Allen", Name = "Midnight in Paris"}
	                     };
            return Ok(movies);
        }

    }
}