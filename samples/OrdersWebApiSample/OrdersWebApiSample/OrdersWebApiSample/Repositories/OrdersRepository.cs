using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OrdersWebApiSample.OrdersXml.Models;

namespace OrdersWebApiSample.Repositories
{
    public interface IOrdersRepository
    {
        void Add(PurchaseOrderType order);
        void Update(PurchaseOrderType order);
        PurchaseOrderType Get(string id);
        PurchaseOrdersType GetShipped();
        PurchaseOrdersType GetNotShipped();
    }

    public class OrdersRepository : IOrdersRepository
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

        public PurchaseOrderType Get(string id)
        {
            return orders[id];
        }

        public PurchaseOrdersType GetShipped()
        {
            var collection = new PurchaseOrderTypeCollection();
            var notShippedOrders = orders.Values.Where(o => !o.shipped);
            foreach (var order in notShippedOrders)
            {
                collection.Add(order);
            }
            
            return new PurchaseOrdersType { orders = collection };
        }

        public PurchaseOrdersType GetNotShipped()
        {
            var collection = new PurchaseOrderTypeCollection();
            var notShippedOrders = orders.Values.Where(o => o.shipped);
            foreach (var order in notShippedOrders)
            {
                collection.Add(order);
            }

            return new PurchaseOrdersType { orders = collection };
        }
    }
}