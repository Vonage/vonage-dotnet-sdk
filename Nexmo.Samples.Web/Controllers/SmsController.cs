using Microsoft.AspNetCore.Mvc;
using Nexmo.Api;
using Nexmo.Samples.Web.Models;

namespace Nexmo.Samples.Web.Controllers
{
    [Route("/Sms")]
    public class SmsController : Controller
    {
        [HttpPost]
        public ActionResult Sms(Actions act)
        {
            act.SMS.callback = $"https://{Configuration.Instance.Settings["callback_host"]}/api/SmsDelivery";
            return Json(SMS.Send(act.SMS));
        }
    }
}