using Microsoft.AspNetCore.Mvc;
using Nexmo.Api;
using Nexmo.Samples.Web.Models;
using NumberInsight = Nexmo.Api.NumberInsight;

namespace Nexmo.Samples.Web.Controllers
{
    [Route("/NumberInsight")]
    public class NumberInsightController : Controller
    {
        [HttpPost]
        public ActionResult NumberInsight(Actions act)
        {
            return Json(Nexmo.Api.NumberInsight.RequestAsync(new NumberInsight.NumberInsightAsyncRequest
            {
                number = act.NI.Number,
                callback = $"https://{Configuration.Instance.Settings["callback_host"]}/api/NI"
            }));
        }
    }
}