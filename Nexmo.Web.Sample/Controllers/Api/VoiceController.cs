using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Caching;
using System.Web.Http;
using System.Web.Mvc;
using Nexmo.Api;

namespace Nexmo.Web.Sample.Controllers.Api
{
    public class VoiceController : ApiController
    {
        readonly object _cacheLock = new object();

        public ActionResult Get([FromUri]Voice.CallReturn response)
        {
            lock (_cacheLock)
            {
                var callReturns = new List<Voice.CallReturn>();
                const string cachekey = "voice_call_returns";
                if (MemoryCache.Default.Contains(cachekey))
                {
                    callReturns = (List<Voice.CallReturn>)MemoryCache.Default.Get(cachekey);
                }
                callReturns.Add(response);
                MemoryCache.Default.Set(cachekey, callReturns, DateTimeOffset.MaxValue);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}