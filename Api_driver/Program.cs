using Nexmo.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_driver
{
    class Program
    {
        static void Main(string[] args)
        {
            const string API_KEY = "7428e935";
            const string API_SECRET = "3C1249A001ae";
            var id = "eb4b59be-57c0-4e8a-be48-f8d437aa721f";
            var client = new Client(creds: new Nexmo.Api.Request.Credentials(nexmoApiKey: API_KEY, nexmoApiSecret: API_SECRET));
            var response = client.ApplicationV2.Get(appId: id);
        }
    }
}
