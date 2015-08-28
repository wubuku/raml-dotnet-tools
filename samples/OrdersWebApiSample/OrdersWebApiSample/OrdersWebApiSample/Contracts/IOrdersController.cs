// Template: Controller Interface (ApiControllerInterface.t4) version 3.0

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using OrdersWebApiSample.OrdersXml.Models;


namespace OrdersWebApiSample.OrdersXml
{
    public interface IOrdersController
    {

        IHttpActionResult Post(Models.PurchaseOrderType purchaseordertype);
        IHttpActionResult Get([FromUri] string id);
        IHttpActionResult Put(Models.PurchaseOrderType purchaseordertype,[FromUri] string id);
        IHttpActionResult PutShip(Models.ItemsType itemstype,[FromUri] string id);
        IHttpActionResult GetNotshipped([FromUri] string id);
        IHttpActionResult GetShipped([FromUri] string id);
    }
}
