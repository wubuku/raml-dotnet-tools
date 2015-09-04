using System;
using System.IO;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using OrdersClientSample.OrdersXml.Models;

namespace OrdersClientSample
{
	public class FakeResponseHandler : DelegatingHandler
	{
	    private readonly IOrdersRepository repository;

	    public FakeResponseHandler(IOrdersRepository repository)
        {
            this.repository = repository;
        }

	    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	    {
	        var orderSerializer = new XmlSerializer(typeof(PurchaseOrderType));
            var ordersSerializer = new XmlSerializer(typeof(PurchaseOrdersType));

			var responseMessage = new HttpResponseMessage();
			var stream = new MemoryStream();
			var localPath = request.RequestUri.LocalPath;

            // GET /orders/notshipped
			if (localPath.EndsWith("/orders/notshipped"))
                ordersSerializer.Serialize(stream, repository.GetNotShipped());

            // GET /orders/shipped
            if (localPath.EndsWith("/orders/shipped"))
                ordersSerializer.Serialize(stream, repository.GetShipped());

            // POST /orders/{id}/ship
            if (localPath.EndsWith("/ship") && request.Method == HttpMethod.Post)
	        {
                var shipUrl = localPath;
                var shipId = shipUrl.Replace("/api/orders/", string.Empty).Replace("/ship", string.Empty);
	            var shipOrder = repository.Get(shipId);
	            shipOrder.shipped = true;
                repository.Update(shipOrder);
	        }

            // POST /orders
            if (localPath.EndsWith("/orders") && request.Method == HttpMethod.Post)
            {
                var newOrder = (PurchaseOrderType)orderSerializer.Deserialize(request.Content.ReadAsStreamAsync().GetAwaiter().GetResult());
                repository.Add(newOrder);
	        }

            // PUT /orders
	        if (request.Method == HttpMethod.Put)
	        {
                var orderToUpdate = (PurchaseOrderType)orderSerializer.Deserialize(request.Content.ReadAsStreamAsync().GetAwaiter().GetResult());
                repository.Update(orderToUpdate);            
	        }

	        if (request.Method == HttpMethod.Get && !localPath.EndsWith("/orders") && !localPath.EndsWith("/orders/") && !localPath.EndsWith("shipped"))
	        {
	            // GET /orders/{id}
	            var url = localPath;
	            var id = url.Replace("/api/orders/", string.Empty);
	            var order = repository.Get(id);
	            orderSerializer.Serialize(stream, order);
	        }

            var content = Encoding.UTF8.GetString(stream.GetBuffer());
	        content = content.Substring(content.IndexOf(Environment.NewLine) + 1);
	        responseMessage.Content = new StringContent(content, Encoding.UTF8, "application/xml");
			
            return Task.FromResult(responseMessage);
		}
	}
}