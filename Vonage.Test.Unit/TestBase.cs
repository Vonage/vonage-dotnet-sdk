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
using Xunit;

namespace Vonage.Test.Unit
{
    public class TestBase
    {
        private static readonly Regex TokenReplacementRegEx = new Regex(@"\$(\w+)\$", RegexOptions.Compiled);
        private const string MockedMethod = "SendAsync";
        protected string ApiUrl = Configuration.Instance.Settings["appSettings:Vonage.Url.Api"];
        protected string RestUrl = Configuration.Instance.Settings["appSettings:Vonage.Url.Rest"];
        protected string ApiKey = Environment.GetEnvironmentVariable("VONAGE_API_KEY") ?? "testkey";
        protected string ApiSecret = Environment.GetEnvironmentVariable("VONAGE_API_Secret") ?? "testSecret";

        protected string AppId = Environment.GetEnvironmentVariable("APPLICATION_ID") ??
                                 "afed99d2-ae38-487c-bb5a-fe2518febd44";

        protected string PrivateKey = Environment.GetEnvironmentVariable("PRIVATE_KEY") ??
                                      Environment.GetEnvironmentVariable("Vonage.Test.RsaPrivateKey");

#if NETCOREAPP2_0_OR_GREATER
        private static readonly Assembly ThisAssembly = typeof(TestBase).GetTypeInfo().Assembly;
#else
        private static readonly Assembly ThisAssembly = typeof(TestBase).Assembly;
#endif

        private static readonly string TestAssemblyName = ThisAssembly.GetName().Name;

        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = ThisAssembly.CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        protected void Setup(string uri, string responseContent, string requestContent = null,
            HttpStatusCode expectedCode = HttpStatusCode.OK)
        {
            this.Setup(uri, new StringContent(responseContent, Encoding.UTF8, "application/json"), expectedCode,
                requestContent);
        }

        protected void Setup(string uri, byte[] responseContent, HttpStatusCode expectedCode = HttpStatusCode.OK)
        {
            this.Setup(uri, new StreamContent(new MemoryStream(responseContent)), expectedCode);
        }

        private void Setup(string uri, HttpContent httpContent, HttpStatusCode expectedCode,
            string requestContent = null)
        {
            typeof(Configuration).GetField("_client", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.SetValue(Configuration.Instance, null);
            Mock<HttpMessageHandler> mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
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
                    Content = httpContent
                })
                .Verifiable();
            Configuration.Instance.ClientHandler = mockHandler.Object;
        }

        [Obsolete("Use GetResponseJson")]
        protected string GetExpectedJson([CallerMemberName] string name = null)
        {
            var type = this.GetType().Name;
            var projectFolder = this.GetType().Namespace.Substring(TestAssemblyName.Length);
            var path = Path.Combine(AssemblyDirectory, projectFolder, "Data", type, name + ".json");
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File not found at {path}.");
            }

            var jsonContent = File.ReadAllText(path);
            jsonContent = Regex.Replace(jsonContent, "(\"(?:[^\"\\\\]|\\\\.)*\")|\\s+", "$1");
            return jsonContent;
        }

        protected string GetResponseJson([CallerMemberName] string name = null)
        {
            string type = this.GetType().Name;
            string ns = this.GetType().Namespace;
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

        protected string GetResponseJson(Dictionary<string, string> parameters, [CallerMemberName] string name = null)
        {
            var response = this.GetResponseJson(name);
            response = TokenReplacementRegEx.Replace(response, match => parameters[match.Groups[1].Value]);
            return response;
        }

        protected string GetRequestJson([CallerMemberName] string name = null)
        {
            string type = this.GetType().Name;
            string ns = this.GetType().Namespace;
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