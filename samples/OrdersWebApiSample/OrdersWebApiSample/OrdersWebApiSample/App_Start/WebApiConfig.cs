using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml.Serialization;

namespace OrdersWebApiSample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Add(new XmlSerializerFormatter());

            // Web API routes
            config.MapHttpAttributeRoutes();
        }

        public class XmlSerializerFormatter : MediaTypeFormatter
        {
            public XmlSerializerFormatter()
            {
                SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xml"));
                SupportedEncodings.Add(new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            }

            public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content,
                TransportContext transportContext)
            {
                var taskSource = new TaskCompletionSource<object>();
                try
                {
                    new XmlSerializer(type).Serialize(writeStream, content);
                    taskSource.SetResult(null);
                }
                catch (Exception e)
                {
                    taskSource.SetException(e);
                }
                return taskSource.Task;
            }

            public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
            {
                var taskSource = new TaskCompletionSource<object>();
                try
                {
                    var obj = new XmlSerializer(type).Deserialize(readStream);
                    taskSource.SetResult(obj);
                }
                catch (Exception e)
                {
                    taskSource.SetException(e);
                }
                return taskSource.Task;
            }

            public override bool CanReadType(Type type)
            {
                return true;
            }

            public override bool CanWriteType(Type type)
            {
                return true;
            }
        }
    }
}
