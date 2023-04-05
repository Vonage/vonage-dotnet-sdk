using System;
using System.IO.Abstractions.TestingHelpers;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Meetings;
using Vonage.Meetings.UpdateApplication;
using Xunit;

namespace Vonage.Test.Unit.Meetings.UpdateApplication
{
    public class UseCaseTest : BaseUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<UpdateApplicationResponse>>> Operation =>
            configuration => MeetingsClientFactory.Create(configuration).UpdateApplicationAsync(this.request);

        private readonly Result<UpdateApplicationRequest> request;

        public UseCaseTest() => this.request = BuildRequest(this.helper.Fixture);

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenApiResponseCannotBeParsed() =>
            await this.helper.VerifyReturnsFailureGivenApiResponseCannotBeParsed(this.BuildExpectedRequest(),
                this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper
                .VerifyReturnsFailureGivenRequestIsFailure<UpdateApplicationRequest, UpdateApplicationResponse>(
                    (configuration, failureRequest) =>
                        new MeetingsClient(configuration, new MockFileSystem()).UpdateApplicationAsync(failureRequest));

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(this.BuildExpectedRequest(),
                this.Operation);

        private ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri(UseCaseHelper.GetPathFromRequest(this.request), UriKind.Relative),
                Content = this.request
                    .Map(value => this.helper.Serializer.SerializeObject(new {UpdateDetails = value}))
                    .IfFailure(string.Empty),
            };

        private static Result<UpdateApplicationRequest> BuildRequest(ISpecimenBuilder fixture) =>
            UpdateApplicationRequest.Parse(fixture.Create<Guid>());
    }
}