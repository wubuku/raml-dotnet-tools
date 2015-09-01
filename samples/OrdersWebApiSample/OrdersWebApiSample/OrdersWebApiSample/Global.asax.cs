using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using OrdersWebApiSample.OrdersXml.Models;
using OrdersWebApiSample.Repositories;

namespace OrdersWebApiSample
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();

            var ordersRepository = new OrdersRepository();

            var order = new PurchaseOrderType();
            order.id = "113";
            order.orderDate = DateTime.Now;
            order.items = new ItemsType();
            order.items.item = new ItemsTypeItemCollection();
            order.items.item.Add(new ItemsTypeItem
            {
                productName = "XBOX One",
                partNum = "5465653",
                quantity = "1",
                USPrice = 400
            });

            ordersRepository.Add(order);

            var order2 = new PurchaseOrderType();
            order2.id = "117";
            order2.orderDate = DateTime.Now.Subtract(new TimeSpan(1,0,0,0));
            order2.items = new ItemsType();
            order2.items.item = new ItemsTypeItemCollection();
            order2.items.item.Add(new ItemsTypeItem
            {
                productName = "iPod Nano",
                partNum = "32312312",
                quantity = "2",
                USPrice = 200
            });

            ordersRepository.Add(order2);

            var order3 = new PurchaseOrderType();
            order3.id = "111";
            order3.orderDate = DateTime.Now.Subtract(new TimeSpan(5, 0, 0, 0));
            order3.items = new ItemsType();
            order3.items.item = new ItemsTypeItemCollection();
            order3.items.item.Add(new ItemsTypeItem
            {
                productName = "iPod Nano Touch",
                partNum = "32312888",
                quantity = "1",
                USPrice = 250,
                shipDate = DateTime.Now,
                weightKg = 1
            });

            ordersRepository.Add(order3);

        }
    }
}
