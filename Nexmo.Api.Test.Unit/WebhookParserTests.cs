using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xunit;
namespace Nexmo.Api.Test.Unit
{
    public class WebhookParserTests
    {
        [Theory]
        [InlineData("application/x-www-form-urlencoded; charset=UTF-8")]
        [InlineData("application/json; charset=UTF-8")]
        [InlineData("application/trash")]
        public void TestParseStream(string contentType)
        {
            string contentString = "";
            if(contentType == "application/x-www-form-urlencoded; charset=UTF-8")
            {
                contentString = "foo-bar=foo%20bar";
            }
            else
            {
                contentString = "{\"foo-bar\":\"foo bar\"}";
            }
            byte[] contentToBytes = Encoding.UTF8.GetBytes(contentString);
            MemoryStream stream = new MemoryStream(contentToBytes);
            try
            {
                var output = Utility.WebhookParser.ParseWebhook<Foo>(stream, contentType);
                Assert.Equal("foo bar", output.FooBar);
            }
            catch (Exception)
            {
                if (contentType != "application/trash")
                    throw;
            }
        }

        [Theory]
        [InlineData("application/x-www-form-urlencoded")]
        [InlineData("application/json")]
        [InlineData("application/trash")]
        public void TestParseHttpRequestMessage(string contentType)
        {
            string contentString = "";
            if (contentType == "application/x-www-form-urlencoded")
            {
                contentString = "foo-bar=foo%20bar";
            }
            else
            {
                contentString = "{\"foo-bar\":\"foo bar\"}";
            }
            byte[] contentToBytes = Encoding.UTF8.GetBytes(contentString);            
            var request = new HttpRequestMessage();
            request.Content = new ByteArrayContent(contentToBytes);
            request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);       
            try
            {
                var output = Utility.WebhookParser.ParseWebhook<Foo>(request);
                Assert.Equal("foo bar", output.FooBar);
            }
            catch (Exception)
            {
                if (contentType != "application/trash")
                    throw;
            }
        }

        [Fact]
        public void TestParseHttpRequestContentWithBadlyEscapedUrl()
        {
            var contentType = "application/x-www-form-urlencoded";
            var contentString = "foo-bar=foo bar";
            byte[] contentToBytes = Encoding.UTF8.GetBytes(contentString);
            var request = new HttpRequestMessage();
            request.Content = new ByteArrayContent(contentToBytes);
            request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
            var output = Utility.WebhookParser.ParseWebhook<Foo>(request);
            Assert.Equal("foo bar", output.FooBar);
        }

        [Fact]
        public void TestParseQueryArgsMvcLegacy()
        {
            var queryArgs = new List<KeyValuePair<string, string>>();
            queryArgs.Add(new KeyValuePair<string, string>("foo-bar", "foo" ));
            var output = Utility.WebhookParser.ParseQueryNameValuePairs<Foo>(queryArgs);
            Assert.Equal("foo", output.FooBar);
        }

        [Fact]
        public void TestParseQueryArgsCore()
        {
            var queryArgs = new List<KeyValuePair<string, StringValues>>();
            queryArgs.Add(new KeyValuePair<string, StringValues>("foo-bar", "foo"));
            var output = Utility.WebhookParser.ParseQuery<Foo>(queryArgs);
        }

        public class Foo
        {
            [JsonProperty("foo-bar")]
            public string FooBar { get; set; }
        }
    }
}
