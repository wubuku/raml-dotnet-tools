// Template: Controller Implementation (ApiControllerImplementation.t4) version 3.0

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using OrdersWebApiSample.OrdersXml.Models;

namespace OrdersWebApiSample.OrdersXml
{
    public partial class OrdersController : IOrdersController
    {

		/// <summary>
		/// Create a new purchase order
		/// </summary>
		/// <param name="purchaseordertype"></param>
        public IHttpActionResult Post(Models.PurchaseOrderType purchaseordertype)
        {
            // TODO: implement Post - route: orders/
			return Ok();
        }

		/// <summary>
		/// gets an order by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>PurchaseOrderType</returns>
        public IHttpActionResult Get([FromUri] string id)
        {
            // TODO: implement Get - route: orders/{id}
			// var result = new PurchaseOrderType();
			// return Ok(result);
			return Ok();
        }

		/// <summary>
		/// updates an order
		/// </summary>
		/// <param name="purchaseordertype"></param>
		/// <param name="id"></param>
        public IHttpActionResult Put(Models.PurchaseOrderType purchaseordertype,[FromUri] string id)
        {
            // TODO: implement Put - route: orders/{id}
			return Ok();
        }

		/// <summary>
		/// ship one or more order items
		/// </summary>
		/// <param name="itemstype"></param>
		/// <param name="id"></param>
        public IHttpActionResult PutShip(Models.ItemsType itemstype,[FromUri] string id)
        {
            // TODO: implement PutShip - route: orders/{id}/ship
			return Ok();
        }

		/// <summary>
		/// gets the not shipped items of an order
		/// </summary>
		/// <param name="id"></param>
		/// <returns>ItemsType</returns>
        public IHttpActionResult GetNotshipped([FromUri] string id)
        {
            // TODO: implement GetNotshipped - route: orders/{id}/notshipped
			// var result = new ItemsType();
			// return Ok(result);
			return Ok();
        }

		/// <summary>
		/// gets the already shipped items of an order
		/// </summary>
		/// <param name="id"></param>
		/// <returns>ItemsType</returns>
        public IHttpActionResult GetShipped([FromUri] string id)
        {
            // TODO: implement GetShipped - route: orders/{id}/shipped
			// var result = new ItemsType();
			// return Ok(result);
			return Ok();
        }

    }
}
