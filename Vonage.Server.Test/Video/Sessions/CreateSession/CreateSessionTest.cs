using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Common.Test.TestHelpers;
using Vonage.Server.Video.Sessions;
using Vonage.Server.Video.Sessions.CreateSession;
using Xunit;

namespace Vonage.Server.Test.Video.Sessions.CreateSession
{
    public class CreateSessionTest : BaseUseCase
    {
        private readonly CreateSessionRequest request = CreateSessionRequest.Default;
        private readonly CreateSessionResponse session;

        private Func<VonageHttpClientConfiguration, Task<Result<CreateSessionResponse>>> Operation =>
            configuration => new SessionClient(configuration).CreateSessionAsync(this.request);

        public CreateSessionTest() => this.session = this.Helper.Fixture.Create<CreateSessionResponse>();

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.Helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.Helper.VerifyReturnsFailureGivenRequestIsFailure<CreateSessionRequest, CreateSessionResponse>(
                (configuration, failureRequest) =>
                    new SessionClient(configuration).CreateSessionAsync(failureRequest));

        [Fact]
        public async Task ShouldReturnFailure_GivenResponseContainsNoSession()
        {
            var expectedResponse = this.Helper.Serializer.SerializeObject(Array.Empty<CreateSessionResponse>());
            var client = FakeHttpRequestHandler
                .Build(HttpStatusCode.OK)
                .WithExpectedRequest(this.BuildExpectedRequest())
                .WithResponseContent(expectedResponse)
                .ToHttpClient();
            var configuration = new VonageHttpClientConfiguration(client, () => this.Helper.Fixture.Create<string>(),
                this.Helper.Fixture.Create<string>());
            var result = await this.Operation(configuration);
            result.Should().BeFailure(ResultFailure.FromErrorMessage(CreateSessionResponse.NoSessionCreated));
        }

        [Property]
        public Property ShouldReturnFailure_GivenStatusCodeIsFailure() =>
            this.Helper.VerifyReturnsFailureGivenApiResponseIsError(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnSuccess_GivenMultipleSessionsAreCreated()
        {
            var expectedResponse = this.Helper.Serializer.SerializeObject(new[]
            {
                this.session, this.Helper.Fixture.Create<CreateSessionResponse>(),
                this.Helper.Fixture.Create<CreateSessionResponse>(),
            });
            var client = FakeHttpRequestHandler
                .Build(HttpStatusCode.OK)
                .WithExpectedRequest(this.BuildExpectedRequest())
                .WithResponseContent(expectedResponse)
                .ToHttpClient();
            var configuration = new VonageHttpClientConfiguration(client, () => this.Helper.Fixture.Create<string>(),
                this.Helper.Fixture.Create<string>());
            var result = await this.Operation(configuration);
            result.Should().BeSuccess(this.session);
        }

        [Fact]
        public async Task ShouldReturnSuccess_GivenSessionIsCreated()
        {
            var expectedResponse = this.Helper.Serializer.SerializeObject(new[] {this.session});
            var client = FakeHttpRequestHandler
                .Build(HttpStatusCode.OK)
                .WithExpectedRequest(this.BuildExpectedRequest())
                .WithResponseContent(expectedResponse)
                .ToHttpClient();
            var configuration = new VonageHttpClientConfiguration(client, () => this.Helper.Fixture.Create<string>(),
                this.Helper.Fixture.Create<string>());
            var result = await this.Operation(configuration);
            result.Should().BeSuccess(this.session);
        }

        private ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(this.request.GetEndpointPath(), UriKind.Relative),
                Content = this.request.GetUrlEncoded(),
            };
    }
}