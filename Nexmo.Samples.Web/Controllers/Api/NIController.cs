using Microsoft.AspNetCore.Mvc;
using Nexmo.Api;

namespace Nexmo.Samples.Web.Controllers.Api
{
    [Route("/api/NI")]
    public class NIController : Controller
    {
        [HttpGet()]
        public void get([FromQuery]NumberInsight.NumberInsightResponse response)
        {
            // TODO: do something with this response
        }
    }
}