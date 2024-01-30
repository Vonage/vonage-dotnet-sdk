using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using FluentAssertions;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Vonage.Utility;
using Xunit;

namespace Vonage.Test;

[Trait("Category", "Legacy")]
public class WebhookParserTests
{
    [Fact]
    public void TestParseHttpRequestContentWithBadlyEscapedUrl()
    {
        var contentType = "application/x-www-form-urlencoded";
        var contentString = "foo-bar=foo bar";
        var contentToBytes = Encoding.UTF8.GetBytes(contentString);
        var request = new HttpRequestMessage();
        request.Content = new ByteArrayContent(contentToBytes);
        request.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        var output = WebhookParser.ParseWebhook<Foo>(request);
        Assert.Equal("foo bar", output.FooBar);
    }

    [Theory]
    [InlineData("application/x-www-form-urlencoded")]
    [InlineData("application/json")]
    [InlineData("application/trash")]
    public void TestParseHttpRequestMessage(string contentType)
    {
        var contentString = "";
        if (contentType == "application/x-www-form-urlencoded")
        {
            contentString = "foo-bar=foo%20bar";
        }
        else
        {
            contentString = "{\"foo-bar\":\"foo bar\"}";
        }

        var contentToBytes = Encoding.UTF8.GetBytes(contentString);
        var request = new HttpRequestMessage();
        request.Content = new ByteArrayContent(contentToBytes);
        request.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        try
        {
            var output = WebhookParser.ParseWebhook<Foo>(request);
            Assert.Equal("foo bar", output.FooBar);
        }
        catch (Exception)
        {
            if (contentType != "application/trash")
                throw;
        }
    }

    [Fact]
    public void TestParseQueryArgsCore()
    {
        var queryArgs = new List<KeyValuePair<string, StringValues>>();
        queryArgs.Add(new KeyValuePair<string, StringValues>("foo-bar", "foo"));
        var value = WebhookParser.ParseQuery<Foo>(queryArgs);
        value.FooBar.Should().Be("foo");
    }

    [Fact]
    public void TestParseQueryArgsMvcLegacy()
    {
        var queryArgs = new List<KeyValuePair<string, string>>();
        queryArgs.Add(new KeyValuePair<string, string>("foo-bar", "foo"));
        var output = WebhookParser.ParseQueryNameValuePairs<Foo>(queryArgs);
        Assert.Equal("foo", output.FooBar);
    }

    [Theory]
    [InlineData("application/x-www-form-urlencoded; charset=UTF-8")]
    [InlineData("application/json; charset=UTF-8")]
    [InlineData("application/trash")]
    public void TestParseStream(string contentType)
    {
        var contentString = "";
        if (contentType == "application/x-www-form-urlencoded; charset=UTF-8")
        {
            contentString = "foo-bar=foo%20bar";
        }
        else
        {
            contentString = "{\"foo-bar\":\"foo bar\"}";
        }

        var contentToBytes = Encoding.UTF8.GetBytes(contentString);
        var stream = new MemoryStream(contentToBytes);
        try
        {
            var output = WebhookParser.ParseWebhook<Foo>(stream, contentType);
            Assert.Equal("foo bar", output.FooBar);
        }
        catch (Exception)
        {
            if (contentType != "application/trash")
                throw;
        }
    }

    public class Foo
    {
        [JsonProperty("foo-bar")] public string FooBar { get; set; }
    }
}