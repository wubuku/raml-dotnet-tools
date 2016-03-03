using ChinookClientSample.ChinookV1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookClientSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Test().Wait();
        }

        private static async Task Test()
        {
            var client = new ChinookV1Client("http://localhost:21911/");
            ///client.SchemaValidation.RaiseExceptions = false;
            var resp = await client.Artists.Get();
            var cont = resp.Content;
        }
    }
}
