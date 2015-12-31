using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Caching;
using System.Web.Http;
using System.Web.Mvc;
using Nexmo.Api;

namespace Nexmo.Web.Sample.Controllers.Api
{
    public class SmsInboundController : ApiController
    {
        readonly object _cacheLock = new object();

        public ActionResult Get([FromUri]SMS.SMSInbound response)
        {
            // Upon initial setup with Nexmo, this action will be tested up to 5 times. No response data will be included. Just accept the empty request with a 200.
            if (null == response.to && null == response.msisdn)
                return new HttpStatusCodeResult(HttpStatusCode.OK);

            lock (_cacheLock)
            {
                var receipts = new List<SMS.SMSInbound>();
                const string cachekey = "sms_inbounds";
                if (MemoryCache.Default.Contains(cachekey))
                {
                    receipts = (List<SMS.SMSInbound>)MemoryCache.Default.Get(cachekey);
                }
                receipts.Add(response);
                MemoryCache.Default.Set(cachekey, receipts, DateTimeOffset.MaxValue);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}