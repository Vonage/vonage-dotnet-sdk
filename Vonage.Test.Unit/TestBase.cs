using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Vonage.Common;
using Vonage.Common.Test.TestHelpers;
using Vonage.Request;
using Xunit;

namespace Vonage.Test.Unit
{
    public class TestBase
    {
        private static readonly Regex TokenReplacementRegEx = new Regex(@"\$(\w+)\$", RegexOptions.Compiled);
        private const string MockedMethod = "SendAsync";
        private const string JsonRegexPattern = "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+";
        protected string ApiUrl => this.configuration.Settings["vonage:Vonage.Url.Api"];
        protected string RestUrl => this.configuration.Settings["vonage:Vonage.Url.Rest"];
        protected readonly string ApiKey = Environment.GetEnvironmentVariable("VONAGE_API_KEY") ?? "testkey";
        protected readonly string ApiSecret = Environment.GetEnvironmentVariable("VONAGE_API_Secret") ?? "testSecret";

        protected readonly string AppId = Environment.GetEnvironmentVariable("APPLICATION_ID") ??
                                          "afed99d2-ae38-487c-bb5a-fe2518febd44";

        protected readonly string PrivateKey = Environment.GetEnvironmentVariable("PRIVATE_KEY") ??
                                               TokenHelper.GetKey();

        protected TestBase() => this.configuration = new Configuration();

#if NETCOREAPP2_0_OR_GREATER
        private static readonly Assembly ThisAssembly = typeof(TestBase).GetTypeInfo().Assembly;
#else
        private static readonly Assembly ThisAssembly = typeof(TestBase).Assembly;
#endif

        private static readonly string TestAssemblyName = ThisAssembly.GetName().Name;
        protected readonly Configuration configuration;

        protected VonageClient BuildVonageClient(Credentials credentials) =>
            new VonageClient(credentials, this.configuration, new TimeProvider());

        protected VonageClient BuildVonageClient(Credentials credentials, ITimeProvider timeProvider) =>
            new VonageClient(credentials, this.configuration, timeProvider);

        protected Credentials BuildCredentialsForBasicAuthentication() =>
            Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);

        protected Credentials BuildCredentialsForBearerAuthentication() =>
            Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);

        private static string GetAssemblyDirectory()
        {
            var location = ThisAssembly.CodeBase;
            var uri = new UriBuilder(location);
            var path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        protected void Setup(string uri, string responseContent, string requestContent = null,
            HttpStatusCode expectedCode = HttpStatusCode.OK) =>
            this.Setup(uri, new StringContent(responseContent, Encoding.UTF8, "application/json"), expectedCode,
                requestContent);

        protected void Setup(string uri, byte[] responseContent, HttpStatusCode expectedCode = HttpStatusCode.OK) =>
            this.Setup(uri, new StreamContent(new MemoryStream(responseContent)), expectedCode);

        private void Setup(string uri, HttpContent httpContent, HttpStatusCode expectedCode,
            string requestContent = null)
        {
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(MockedMethod,
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((actualHttpRequestMessage, cancellationToken) =>
                {
                    Assert.Equal(uri, actualHttpRequestMessage.RequestUri.AbsoluteUri,
                        StringComparer.OrdinalIgnoreCase);
                    if (requestContent == null)
                        return;
                    var actualContent = actualHttpRequestMessage.Content.ReadAsStringAsync().Result;
                    Assert.Equal(requestContent, actualContent, StringComparer.OrdinalIgnoreCase);
                })
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = expectedCode,
                    Content = httpContent,
                })
                .Verifiable();
            this.configuration.ClientHandler = mockHandler.Object;
        }

        protected string GetResponseJson([CallerMemberName] string name = null) => this.ReadJsonFile(name, "response");

        protected string GetRequestJson([CallerMemberName] string name = null) => this.ReadJsonFile(name, "request");

        private string ReadJsonFile(string name, string fileType)
        {
            var typeNamespace = this.GetType().Namespace;
            if (typeNamespace is null)
            {
                return string.Empty;
            }

            var path = Path.Combine(
                GetAssemblyDirectory(),
                typeNamespace.Substring(TestAssemblyName.Length),
                "Data",
                this.GetType().Name,
                $"{name}-{fileType}.json");
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File not found at {path}.");
            }

            return Regex.Replace(File.ReadAllText(path), JsonRegexPattern, "$1");
        }

        protected string GetResponseJson(Dictionary<string, string> parameters,
            [CallerMemberName] string name = null) =>
            TokenReplacementRegEx.Replace(this.GetResponseJson(name), match => parameters[match.Groups[1].Value]);

        protected string GetRequestJson(Dictionary<string, string> parameters, [CallerMemberName] string name = null) =>
            TokenReplacementRegEx.Replace(this.GetRequestJson(name), match => parameters[match.Groups[1].Value]);
    }
}