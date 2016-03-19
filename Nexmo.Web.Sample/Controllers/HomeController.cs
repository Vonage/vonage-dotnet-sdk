using System.Collections.Generic;
using System.Runtime.Caching;
using System.Web.Mvc;
using Nexmo.Api;
using Nexmo.Web.Sample.Models;
using NumberInsight = Nexmo.Api.NumberInsight;
using NumberVerify = Nexmo.Api.NumberVerify;

namespace Nexmo.Web.Sample.Controllers
{
    public class HomeController : Controller
    {
        private static List<T> GetFromCacheAndClear<T>(string key)
        {
            var values = new List<T>();
            if (!MemoryCache.Default.Contains(key)) return values;
            values = (List<T>)MemoryCache.Default.Get(key);
            MemoryCache.Default.Remove(key);
            return values;
        }

        public ActionResult Index()
        {
            return View(new Actions
            {
                Receipts = GetFromCacheAndClear<SMS.SMSDeliveryReceipt>("sms_receipts"),
                Inbounds = GetFromCacheAndClear<SMS.SMSInbound>("sms_inbounds"),
                VoiceReturns = GetFromCacheAndClear<Voice.CallReturn>("voice_call_returns"),
                TTSReturns = GetFromCacheAndClear<Voice.TextToSpeechReturn>("tts_call_returns")
            });
        }

        [HttpPost]
        public ActionResult Sms(Actions act)
        {
            return Json(SMS.Send(act.SMS));
        }


        [HttpPost]
        public ActionResult NumberInsight(Actions act)
        {
            return Json(Nexmo.Api.NumberInsight.Request(new NumberInsight.NumberInsightRequest
            {
                Number = act.NI.Number,
                Callback = "https://nmotest.ngrok.io/api/NI"
            }));
        }

        [HttpPost]
        public ActionResult NumberVerify(Actions act)
        {
            return Json(Nexmo.Api.NumberVerify.Verify(new NumberVerify.VerifyRequest
            {
                number = act.NV_V.Number,
                brand = act.NV_V.Brand
            }));
        }

        [HttpPost]
        public ActionResult NumberVerifyCheck(Actions act)
        {
            return Json(Nexmo.Api.NumberVerify.Check(new NumberVerify.CheckRequest
            {
                request_id = act.NV_C.RequestId,
                code = act.NV_C.Code
            }));
        }

        [HttpPost]
        public ActionResult NumberVerifySearch(Actions act)
        {
            return Json(Nexmo.Api.NumberVerify.Search(new NumberVerify.SearchRequest
            {
                request_id = act.NV_S.RequestId
            }));
        }
    }
}