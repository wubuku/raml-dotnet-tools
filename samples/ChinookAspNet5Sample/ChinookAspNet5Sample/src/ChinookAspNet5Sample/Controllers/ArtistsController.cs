// Template: Controller Implementation (ApiControllerImplementation.t4) version 3.0

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ChinookAspNet5Sample.ChinookV1.Models;

namespace ChinookAspNet5Sample.ChinookV1
{
    public partial class ArtistsController : IArtistsController
    {

		/// <returns>IList&lt;Artist&gt;</returns>
        public IActionResult Get()
        {
            // TODO: implement Get - route: artists/
			// var result = new IList<Artist>();
			// return new ObjectResult(result);
			return new ObjectResult("");
        }

		/// <param name="content"></param>
        public IActionResult Post([FromBody] Models.Artist content)
        {
            // TODO: implement Post - route: artists/
			return new ObjectResult("");
        }

		/// <param name="id"></param>
		/// <returns>Artist</returns>
        public IActionResult GetById(string id)
        {
            // TODO: implement GetById - route: artists/{id}
			// var result = new Artist();
			// return new ObjectResult(result);
			return new ObjectResult("");
        }

		/// <param name="content"></param>
		/// <param name="id"></param>
        public IActionResult Put([FromBody] Models.Artist content,string id)
        {
            // TODO: implement Put - route: artists/{id}
			return new ObjectResult("");
        }

		/// <param name="id"></param>
        public IActionResult Delete(string id)
        {
            // TODO: implement Delete - route: artists/{id}
			return new ObjectResult("");
        }

		/// <param name="id"></param>
		/// <returns>IDictionary&lt;string,Artist&gt;</returns>
        public IActionResult GetA(string id)
        {
            // TODO: implement GetA - route: artists/bytrack/{id}
			// var result = new IDictionary<string,Artist>();
			// return new ObjectResult(result);
			return new ObjectResult("");
        }

    }
}
