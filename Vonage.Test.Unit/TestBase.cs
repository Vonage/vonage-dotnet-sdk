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
using Vonage.Request;
using Xunit;

namespace Vonage.Test.Unit
{
    public class TestBase
    {
        private static readonly Regex TokenReplacementRegEx = new Regex(@"\$(\w+)\$", RegexOptions.Compiled);
        private const string MockedMethod = "SendAsync";
        protected readonly string ApiUrl = Configuration.Instance.Settings["appSettings:Vonage.Url.Api"];
        protected readonly string RestUrl = Configuration.Instance.Settings["appSettings:Vonage.Url.Rest"];
        protected readonly string ApiKey = Environment.GetEnvironmentVariable("VONAGE_API_KEY") ?? "testkey";
        protected readonly string ApiSecret = Environment.GetEnvironmentVariable("VONAGE_API_Secret") ?? "testSecret";
        protected readonly string AppId = Environment.GetEnvironmentVariable("APPLICATION_ID") ??
                                          "afed99d2-ae38-487c-bb5a-fe2518febd44";
        protected readonly string PrivateKey = Environment.GetEnvironmentVariable("PRIVATE_KEY") ??
                                               Environment.GetEnvironmentVariable("Vonage.Test.RsaPrivateKey");

#if NETCOREAPP2_0_OR_GREATER
        private static readonly Assembly ThisAssembly = typeof(TestBase).GetTypeInfo().Assembly;
#else
        private static readonly Assembly ThisAssembly = typeof(TestBase).Assembly;
#endif

        private static readonly string TestAssemblyName = ThisAssembly.GetName().Name;

        protected Credentials BuildCredentialsForBasicAuthentication() =>
            Credentials.FromApiKeyAndSecret(this.ApiKey, this.ApiSecret);

        protected Credentials BuildCredentialsForBearerAuthentication() =>
            Credentials.FromAppIdAndPrivateKey(this.AppId, this.PrivateKey);

        private static string AssemblyDirectory
        {
            get
            {
                var codeBase = ThisAssembly.CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
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
            typeof(Configuration).GetField("_client", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.SetValue(Configuration.Instance, null);
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
            Configuration.Instance.ClientHandler = mockHandler.Object;
        }

        protected string GetResponseJson([CallerMemberName] string name = null)
        {
            var type = this.GetType().Name;
            var ns = this.GetType().Namespace;
            if (ns != null)
            {
                var projectFolder = ns.Substring(TestAssemblyName.Length);
                var path = Path.Combine(AssemblyDirectory, projectFolder, "Data", type, $"{name}-response.json");
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException($"File not found at {path}.");
                }

                var jsonContent = File.ReadAllText(path);
                jsonContent = Regex.Replace(jsonContent, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");
                return jsonContent;
            }

            return string.Empty;
        }

        protected string GetResponseJson(Dictionary<string, string> parameters, [CallerMemberName] string name = null) =>
            TokenReplacementRegEx.Replace(this.GetResponseJson(name), match => parameters[match.Groups[1].Value]);

        protected string GetRequestJson([CallerMemberName] string name = null)
        {
            var type = this.GetType().Name;
            var ns = this.GetType().Namespace;
            if (ns != null)
            {
                var projectFolder = ns.Substring(TestAssemblyName.Length);
                var path = Path.Combine(AssemblyDirectory, projectFolder, "Data", type, $"{name}-request.json");
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException($"File not found at {path}.");
                }

                var jsonContent = File.ReadAllText(path);
                jsonContent = Regex.Replace(jsonContent, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");
                return jsonContent;
            }

            return string.Empty;
        }

        protected string GetRequestJson(Dictionary<string, string> parameters, [CallerMemberName] string name = null)
        {
            var response = this.GetRequestJson(name);
            response = TokenReplacementRegEx.Replace(response, match => parameters[match.Groups[1].Value]);
            return response;
        }
    }
}