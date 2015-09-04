using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using OrdersClientSample.Models;
using OrdersClientSample.OrdersXml;
using OrdersClientSample.OrdersXml.Models;

namespace OrdersClientSample.Controllers
{
    [RoutePrefix("orders")]
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository ordersRepository;
        private readonly OrdersXmlClient client;

        public OrdersController(IOrdersRepository ordersRepository)
        {
            this.ordersRepository = ordersRepository;
            var fakeResponseHandler = new FakeResponseHandler(ordersRepository);
            var httpClient = new HttpClient(fakeResponseHandler) { BaseAddress = new Uri("http://test.com/api/") };
            client = new OrdersXmlClient(httpClient);
        }

        [Route("")]
        public async Task<ActionResult> Index()
        {
            ViewBag.Title = "Not Shipped Orders";
            var resp = await client.Orders.Notshipped.Get();
            var orders = resp.Content;
            var viewModels = Map(orders);
            return View(viewModels);
        }

        [Route("{id}")]
        public async Task<ActionResult> Index(string id)
        {
            var resp = await client.Orders.Get(id);
            var order = resp.Content;
            var viewModel = Map(order);
            return View("View", viewModel);
        }

        [Route("shipped")]
        public async Task<ActionResult> Shipped()
        {
            ViewBag.Title = "Shipped Orders";
            var resp = await client.Orders.Shipped.Get();
            var orders = resp.Content;
            var viewModels = Map(orders);
            return View("Index", viewModels);
        }

        private IEnumerable<OrderViewModel> Map(PurchaseOrdersType orders)
        {
            var viewModels = new Collection<OrderViewModel>();
            foreach (var order in orders.orders)
            {
                viewModels.Add(Map(order));
            }
            return viewModels;
        }

        private OrderViewModel Map(PurchaseOrderType order)
        {
            var vm = new OrderViewModel
            {
                Id = order.id,
                Date = order.orderDate,
                Shipped = order.shipped,
                Addresses = Map(order.Items),
                Products = Map(order.items.item)
            };
            return vm;
        }

        private IEnumerable<ProductViewModel> Map(ItemsTypeItem[] items)
        {
            var viewModels = new Collection<ProductViewModel>();
            if (items == null)
                return viewModels;

            foreach (var item in items)
            {
                var vm = new ProductViewModel
                {
                    Name = item.productName,
                    Quantity = item.quantity,
                    PartNumber = item.partNum,
                    Price = item.USPrice
                };
                viewModels.Add(vm);
            }
            return viewModels;
        }

        private IEnumerable<string> Map(AddressType[] addresses)
        {
            var viewModels = new Collection<string>();
            if (addresses == null)
                return viewModels;

            foreach (var item in addresses)
            {
                var vm = "";
                if (!string.IsNullOrWhiteSpace(item.name))
                    vm += " " + item.name;

                if (!string.IsNullOrWhiteSpace(item.street))
                    vm += " " + item.street;

                if (!string.IsNullOrWhiteSpace(item.city))
                    vm += " " + item.city;

                viewModels.Add(vm);
            }
            return viewModels;
        }

    }
}