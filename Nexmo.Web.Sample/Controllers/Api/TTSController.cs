using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Caching;
using System.Web.Http;
using System.Web.Mvc;
using Nexmo.Api;

namespace Nexmo.Web.Sample.Controllers.Api
{
    public class TTSController : ApiController
    {
        readonly object _cacheLock = new object();

        public ActionResult Get([FromUri]Voice.TextToSpeechReturn response)
        {
            lock (_cacheLock)
            {
                var callReturns = new List<Voice.TextToSpeechReturn>();
                const string cachekey = "tts_call_returns";
                if (MemoryCache.Default.Contains(cachekey))
                {
                    callReturns = (List<Voice.TextToSpeechReturn>)MemoryCache.Default.Get(cachekey);
                }
                callReturns.Add(response);
                MemoryCache.Default.Set(cachekey, callReturns, DateTimeOffset.MaxValue);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}