using System;
using System.IO.Abstractions.TestingHelpers;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Common.Test.TestHelpers;
using Vonage.Meetings;
using Vonage.Meetings.Common;
using Vonage.Meetings.GetThemes;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetThemes
{
    public class GetThemesTest : BaseUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<Theme[]>>> Operation =>
            configuration => new MeetingsClient(configuration, new MockFileSystem()).GetThemesAsync();

        [Fact]
        public async Task ShouldReturnEmptyArray_GivenResponseIsEmpty()
        {
            var client = FakeHttpRequestHandler
                .Build(HttpStatusCode.OK)
                .WithExpectedRequest(this.BuildExpectedRequest())
                .ToHttpClient();
            var configuration = new VonageHttpClientConfiguration(client, () => this.helper.Fixture.Create<string>(),
                this.helper.Fixture.Create<string>());
            var result = await this.Operation(configuration);
            result.Should().BeSuccess(success => success.Should().BeEmpty());
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.BuildExpectedRequest(), this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenStatusCodeIsFailure() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(this.BuildExpectedRequest(),
                this.Operation);

        private ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(GetThemesRequest.Default.GetEndpointPath(), UriKind.Relative),
            };
    }
}