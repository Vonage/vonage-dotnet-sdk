using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Nexmo.Api;

namespace Nexmo.Samples.Web.Controllers.Api
{
    [Route("/api/SmsDelivery")]
    public class SmsDeliveryController : Controller
    {
        static IMemoryCache _cache;

        public SmsDeliveryController(IMemoryCache cache) {
            _cache = cache;
        }

        readonly object _cacheLock = new object();

        [HttpGet]
        public ActionResult Get([FromQuery]SMS.SMSDeliveryReceipt response)
        {
            // Upon initial setup with Nexmo, this action will be tested up to 5 times. No response data will be included. Just accept the empty request with a 200.
            if (null == response.to && null == response.msisdn)
                return new OkResult();

            lock (_cacheLock)
            {
                List<SMS.SMSDeliveryReceipt> receipts;
                const string cachekey = "sms_receipts";
                _cache.TryGetValue(cachekey, out receipts);
                if (null == receipts) {
                    receipts = new List<SMS.SMSDeliveryReceipt>();
                }
                receipts.Add(response);
                _cache.Set(cachekey, receipts, DateTimeOffset.MaxValue);
            }

            return new OkResult();
        }
    }
}