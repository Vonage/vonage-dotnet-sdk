using System.Web.Mvc;
using Nexmo.Web.Sample.Models;
using NumberInsight = Nexmo.Api.NumberInsight;
using NumberVerify = Nexmo.Api.NumberVerify;

namespace Nexmo.Web.Sample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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