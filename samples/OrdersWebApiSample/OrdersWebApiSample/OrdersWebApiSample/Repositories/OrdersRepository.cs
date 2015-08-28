using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OrdersWebApiSample.OrdersXml.Models;

namespace OrdersWebApiSample.Repositories
{
    public class OrdersRepository
    {
        public static IDictionary<string, PurchaseOrderType> orders = new Dictionary<string, PurchaseOrderType>();

        public void Add(PurchaseOrderType order)
        {
            var id = orders.Count + 1;
            order.id = id.ToString();
            orders.Add(order.id, order);
        }

        public void Update(PurchaseOrderType order)
        {
            orders[order.id] = order;
        }


    }
}