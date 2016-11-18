using Microsoft.AspNetCore.Mvc;
using Nexmo.Samples.Web.Models;
using NumberVerify = Nexmo.Api.NumberVerify;

namespace Nexmo.Samples.Web.Controllers
{
    [Route("/NumberVerify")]
    public class NumberVerifyController : Controller
    {
        [HttpPost]
        public ActionResult Verify(Actions act)
        {
            return Json(Nexmo.Api.NumberVerify.Verify(new NumberVerify.VerifyRequest
            {
                number = act.NV_V.Number,
                brand = act.NV_V.Brand
            }));
        }

        [HttpPost("Check")]
        public ActionResult Check(Actions act)
        {
            return Json(Nexmo.Api.NumberVerify.Check(new NumberVerify.CheckRequest
            {
                request_id = act.NV_C.RequestId,
                code = act.NV_C.Code
            }));
        }

        [HttpPost("Search")]
        public ActionResult Search(Actions act)
        {
            return Json(Nexmo.Api.NumberVerify.Search(new NumberVerify.SearchRequest
            {
                request_id = act.NV_S.RequestId
            }));
        }
    }
}