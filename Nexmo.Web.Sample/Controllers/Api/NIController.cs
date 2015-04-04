using System.Web.Http;
using Nexmo.Api;

namespace Nexmo.Web.Sample.Controllers.Api
{
    public class NIController : ApiController
    {
        public void get([FromUri]NumberInsight.NumberInsightResponse response)
        {
            // TODO: do something with this response
        }
    }
}