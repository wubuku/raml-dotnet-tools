using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OrdersClientSample.Models;
using OrdersClientSample.OrdersXml.Models;

namespace OrdersClientSample.App
{
    public static class Mappers
    {
        public static PurchaseOrderType Map(OrderEditModel model)
        {
            return new PurchaseOrderType
            {
                id = model.Id,
                orderDate = model.Date,
                orderDateSpecified = true,
                shipped = model.Shipped,
                shippedSpecified = true,
                Items = new []{ model.BillTo, model.ShipTo },
                ItemsElementName = new[] { ItemsChoiceType.billTo, ItemsChoiceType.shipTo },
                items = Map(model.Products)
            };
        }

        private static ItemsType Map(IEnumerable<ProductViewModel> products)
        {
            return new ItemsType
            {
                item = products.Select(Map).ToArray()
            };
        }

        private static ItemsTypeItem Map(ProductViewModel product)
        {
            return new ItemsTypeItem
            {
                productName = product.Name,
                USPrice = product.Price,
                partNum = product.PartNumber,
                quantity = product.Quantity
            };
        }

        public static IEnumerable<OrderViewModel> Map(PurchaseOrdersType orders)
        {
            var viewModels = new Collection<OrderViewModel>();
            foreach (var order in orders.orders)
            {
                viewModels.Add(Map(order));
            }
            return viewModels;
        }

        public static OrderViewModel Map(PurchaseOrderType order)
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

        private static IEnumerable<ProductViewModel> Map(ItemsTypeItem[] items)
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

        private static IEnumerable<string> Map(AddressType[] addresses)
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