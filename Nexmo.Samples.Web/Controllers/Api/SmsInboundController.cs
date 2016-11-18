using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Nexmo.Api;

namespace Nexmo.Samples.Web.Controllers.Api
{
    [Route("/api/SmsInbound")]
    public class SmsInboundController : Controller
    {
        static IMemoryCache _cache;

        public SmsInboundController(IMemoryCache cache) {
            _cache = cache;
        }

        readonly object _cacheLock = new object();

        [HttpGet]
        public ActionResult Get([FromQuery]SMS.SMSInbound response)
        {
            // Upon initial setup with Nexmo, this action will be tested up to 5 times. No response data will be included. Just accept the empty request with a 200.
            if (null == response.to && null == response.msisdn)
                return new OkResult();

            lock (_cacheLock)
            {
                List<SMS.SMSInbound> receipts;
                const string cachekey = "sms_inbounds";
                _cache.TryGetValue(cachekey, out receipts);
                if (null == receipts) {
                    receipts = new List<SMS.SMSInbound>();
                }
                receipts.Add(response);
                _cache.Set(cachekey, receipts, DateTimeOffset.MaxValue);
            }

            return new OkResult();
        }
    }
}