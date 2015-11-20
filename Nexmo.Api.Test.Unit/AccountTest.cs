using System;
using System.IO;
using System.Text;
using Moq;
using Nexmo.Api.Request;
using NUnit.Framework;

namespace Nexmo.Api.Test.Unit
{
    [TestFixture]
    public class AccountTest
    {
        private Mock<IHttpWebRequestFactory> _mock;
        private Mock<IHttpWebRequest> _request;

        [SetUp]
        public void Setup()
        {
            _mock = new Mock<IHttpWebRequestFactory>();
            _request = new Mock<IHttpWebRequest>();
            _mock.Setup(x => x.CreateHttp(It.IsAny<Uri>()))
                .Returns<Uri>(r => _request.Object);
            ApiRequest.WebRequestFactory = _mock.Object;
        }

        [Test]
        public void should_get_account_balance()
        {
            var resp = new Mock<IWebResponse>();
            resp.Setup(e => e.GetResponseStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes("{\"value\":0.43}")));
            _request.Setup(e => e.GetResponse()).Returns(resp.Object);
            var balance = Account.GetBalance();

            _mock.Verify(h => h.CreateHttp(new Uri("https://rest-sandbox.nexmo.com/account/get-balance/SD_81218/PS_90451")), Times.Once);
            Assert.AreEqual(.43d, balance);
        }

        [Test]
        public void should_get_pricing()
        {
            var resp = new Mock<IWebResponse>();
            resp.Setup(e => e.GetResponseStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes(
@"{""country"":""US"",""name"":""United States"",""prefix"":""1"",""mt"":""0.00480000"",""networks"":[{""code"":""US-FIXED"",""network"":""United States of America Landline"",""mtPrice"":""0.00480000""},{""code"":""311340"",""network"":""Illinois Valley Cellular RSA 2-I Partnership"",""mtPrice"":""0.00480000""},{""code"":""311740"",""network"":""TelAlaska Cellular, Inc."",""mtPrice"":""0.00480000""},{""code"":""311910"",""network"":""SI WIRELESS, LLC"",""mtPrice"":""0.00480000""},{""code"":""310860"",""network"":""Five Star Wireless"",""mtPrice"":""0.00480000""},{""code"":""310760"",""network"":""Panhandle Telecommunications Systems, Inc."",""mtPrice"":""0.00480000""},{""code"":""310011"",""network"":""Northstar Technology, LLC"",""mtPrice"":""0.00480000""},{""code"":""311887"",""network"":""XO California, Inc."",""mtPrice"":""0.00480000""},{""code"":""311380"",""network"":""NEW DIMENSION WIRELESS LTD."",""mtPrice"":""0.00480000""},{""code"":""310300"",""network"":""Smart Call, LLC"",""mtPrice"":""0.00480000""},{""code"":""310004"",""network"":""Verizon Wireless"",""mtPrice"":""0.00480000""},{""code"":""US-VOIP"",""network"":""United States of America VoIP"",""mtPrice"":""0.00480000""},{""code"":""311580"",""network"":""United States Cellular Corp. - Maine"",""mtPrice"":""0.00480000""},{""code"":""311230"",""network"":""Cellular South, Inc."",""mtPrice"":""0.00480000""},{""code"":""310610"",""network"":""Epic Touch Co."",""mtPrice"":""0.00480000""},{""code"":""310060"",""network"":""New Cell, Inc. dba CeLLCom"",""mtPrice"":""0.00480000""},{""code"":""310870"",""network"":""Kaplan Telephone Company"",""mtPrice"":""0.00480000""},{""code"":""310750"",""network"":""East Kentucky Netwrk, LLC dba Appalachian Wireless"",""mtPrice"":""0.00480000""},{""code"":""311290"",""network"":""Pinpoint Wireless, Inc."",""mtPrice"":""0.00480000""},{""code"":""311330"",""network"":""Michigan Wireless, LLC dba Bug Tussel Wireless"",""mtPrice"":""0.00480000""},{""code"":""310710"",""network"":""ASTAC Wireless LLC"",""mtPrice"":""0.00480000""},{""code"":""311050"",""network"":""Wilkes Cellular, Inc."",""mtPrice"":""0.00480000""},{""code"":""310770"",""network"":""Iowa Wireless Services, Lp"",""mtPrice"":""0.00480000""},{""code"":""310100"",""network"":""Plateau Telecommunications, Inc."",""mtPrice"":""0.00480000""},{""code"":""311090"",""network"":""Flat Wireless, LLC"",""mtPrice"":""0.00480000""},{""code"":""311190"",""network"":""Cellular Properties, Inc."",""mtPrice"":""0.00480000""},{""code"":""311710"",""network"":""NORTHEAST WIRELESS NETWORKS, LLC"",""mtPrice"":""0.00480000""},{""code"":""311370"",""network"":""NACS Wireless, Inc."",""mtPrice"":""0.00480000""},{""code"":""311430"",""network"":""RSA 1 Limited Partnership dba Chat Mobility"",""mtPrice"":""0.00480000""},{""code"":""311100"",""network"":""Nex-Tech Wireless, LLC"",""mtPrice"":""0.00480000""},{""code"":""311240"",""network"":""Cordova Wireless Communications, Inc."",""mtPrice"":""0.00480000""},{""code"":""316011"",""network"":""Southern Communications Services"",""mtPrice"":""0.00480000""},{""code"":""310340"",""network"":""Westlink Communications, LLC"",""mtPrice"":""0.00480000""},{""code"":""311860"",""network"":""Uintah Basin Electronic Telecommunications"",""mtPrice"":""0.00480000""},{""code"":""311670"",""network"":""Pine Belt Cellular, Inc."",""mtPrice"":""0.00480000""},{""code"":""US-PREMIUM"",""network"":""United States Premium"",""mtPrice"":""0.00480000""},{""code"":""310570"",""network"":""MTPCS, LLC"",""mtPrice"":""0.00480000""},{""code"":""310180"",""network"":""CT Cube, L.P. dba West Central Cellular"",""mtPrice"":""0.00480000""},{""code"":""311080"",""network"":""Pine Telephone Co."",""mtPrice"":""0.00480000""},{""code"":""310130"",""network"":""Carolina West"",""mtPrice"":""0.00480000""},{""code"":""310580"",""network"":""Inland Cellular"",""mtPrice"":""0.00480000""},{""code"":""311040"",""network"":""Commnet Wireless, LLC"",""mtPrice"":""0.00480000""},{""code"":""316993"",""network"":""Cablevision Lightpath, Inc. - NY"",""mtPrice"":""0.00480000""},{""code"":""316995"",""network"":""Coral Wireless, LLC"",""mtPrice"":""0.00480000""},{""code"":""311410"",""network"":""Iowa RSA 2 Limited Partnership dba Chat Mobility"",""mtPrice"":""0.00480000""},{""code"":""310630"",""network"":""Choice Wireless LC"",""mtPrice"":""0.00480000""},{""code"":""311420"",""network"":""Northwest Missouri Cellular Limited Partnership"",""mtPrice"":""0.00480000""},{""code"":""310270"",""network"":""POWERTEL MEMPHIS LICENSES, INC."",""mtPrice"":""0.00480000""},{""code"":""31100"",""network"":""Mid-Tex Cellular Ltd."",""mtPrice"":""0.00480000""},{""code"":""311610"",""network"":""North Dakota Network Co dba SRT Wireless"",""mtPrice"":""0.00480000""},{""code"":""316884"",""network"":""Kentucky RSA 4 Cellular General Partnership"",""mtPrice"":""0.00480000""},{""code"":""310540"",""network"":""Oklahoma Western Telephone Company"",""mtPrice"":""0.00480000""},{""code"":""316885"",""network"":""Sagebrush Cellular, Inc."",""mtPrice"":""0.00480000""},{""code"":""316883"",""network"":""Virginia PCS Alliance, L.c."",""mtPrice"":""0.00480000""},{""code"":""312040"",""network"":""Custer Telephone Cooperative, Inc."",""mtPrice"":""0.00480000""},{""code"":""311650"",""network"":""United Wireless Communications, Inc."",""mtPrice"":""0.00480000""},{""code"":""310690"",""network"":""Keystone Wireless, LLC"",""mtPrice"":""0.00480000""},{""code"":""310120"",""network"":""SPRINT Spectrum L.P."",""mtPrice"":""0.00480000""},{""code"":""310020"",""network"":""Union Telephone Company"",""mtPrice"":""0.00480000""},{""code"":""311020"",""network"":""Chariton Valley Cellular"",""mtPrice"":""0.00480000""},{""code"":""311030"",""network"":""Indigo Wireless, Inc."",""mtPrice"":""0.00480000""},{""code"":""311310"",""network"":""New Mexico RSA 6-III Partnership dba Leaco Rural"",""mtPrice"":""0.00480000""},{""code"":""310023"",""network"":""Voicestream GSM I, LLC"",""mtPrice"":""0.00480000""},{""code"":""310320"",""network"":""Smith Bagley Inc. dba Cellular One of Ne Arizona"",""mtPrice"":""0.00480000""},{""code"":""311730"",""network"":""Proximity Mobility, LLC"",""mtPrice"":""0.00480000""},{""code"":""310260"",""network"":""T-mobile USA, Inc."",""mtPrice"":""0.00480000""},{""code"":""310450"",""network"":""N.E. Colorado Cellular, Inc."",""mtPrice"":""0.00480000""},{""code"":""310090"",""network"":""AT&T Mobility"",""mtPrice"":""0.00480000""},{""code"":""310740"",""network"":""Tracy Corporation Ii"",""mtPrice"":""0.00480000""},{""code"":""311690"",""network"":""Telebeeper of New Mexico"",""mtPrice"":""0.00480000""},{""code"":""310840"",""network"":""Telecom North America Mobile Inc"",""mtPrice"":""0.00480000""},{""code"":""310880"",""network"":""Advantage Cellular Systems, Inc."",""mtPrice"":""0.00480000""}]}")));
            _request.Setup(e => e.GetResponse()).Returns(resp.Object);
            var pricing = Account.GetPricing("US");

            _mock.Verify(h => h.CreateHttp(new Uri("https://rest-sandbox.nexmo.com/account/get-pricing/outbound/SD_81218/PS_90451/US")), Times.Once);
            Assert.AreEqual("US-FIXED", pricing.networks[0].code);
            Assert.AreEqual("United States of America Landline", pricing.networks[0].network);
        }
    }
}