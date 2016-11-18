using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Nexmo.Api;
using Nexmo.Samples.Web.Models;

namespace Nexmo.Samples.Web.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        static IMemoryCache _cache;

        public HomeController(IMemoryCache cache) {
            _cache = cache;
        }

        private static List<T> GetFromCacheAndClear<T>(string key)
        {
            // IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
            List<T> values;
            if (!_cache.TryGetValue(key, out values)) return new List<T>();
            // values = (List<T>)_cache.Get(key);
            _cache.Remove(key);
            return values;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new Actions
            {
                Receipts = GetFromCacheAndClear<SMS.SMSDeliveryReceipt>("sms_receipts"),
                Inbounds = GetFromCacheAndClear<SMS.SMSInbound>("sms_inbounds"),
            });
        }
    }
}