using System.Collections.Generic;
using System.ComponentModel;
using Nexmo.Api;

namespace Nexmo.Web.Sample.Models
{
    public class Actions
    {
        public SMS.SMSRequest SMS { get; set; }
        public List<SMS.SMSDeliveryReceipt> Receipts { get; set; }
        public List<SMS.SMSInbound> Inbounds { get; set; }

        public NumberInsight NI { get; set; }
        public NumberVerify NV_V { get; set; }
        public NumberVerifyCheck NV_C { get; set; }
        public NumberVerifySearch NV_S { get; set; }
    }

    public class NumberInsight
    {
        [Description("Number Insight")]
        public string Number { get; set; }
    }

    public class NumberVerify
    {
        [Description("Number")]
        public string Number { get; set; }
        [Description("Brand")]
        public string Brand { get; set; }
    }

    public class NumberVerifyCheck
    {
        [Description("Request ID")]
        public string RequestId { get; set; }
        [Description("Code")]
        public string Code { get; set; }
    }

    public class NumberVerifySearch
    {
        [Description("Request ID")]
        public string RequestId { get; set; }
    }
}