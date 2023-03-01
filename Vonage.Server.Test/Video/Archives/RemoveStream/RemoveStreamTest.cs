using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Server.Video.Archives;
using Vonage.Server.Video.Archives.RemoveStream;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.RemoveStream
{
    public class RemoveStreamTest : BaseUseCase
    {
        private Func<VonageHttpClientConfiguration, Task<Result<Unit>>> Operation =>
            configuration => new ArchiveClient(configuration).RemoveStreamAsync(this.request);

        private readonly Result<RemoveStreamRequest> request;

        public RemoveStreamTest() => this.request = BuildRequest(this.Helper.Fixture);

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.Helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.BuildExpectedRequest(), this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.Helper.VerifyReturnsFailureGivenApiResponseIsError(this.BuildExpectedRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.Helper.VerifyReturnsFailureGivenRequestIsFailure<RemoveStreamRequest, Unit>(
                (configuration, failureRequest) =>
                    new ArchiveClient(configuration).RemoveStreamAsync(failureRequest));

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.Helper.VerifyReturnsUnitGivenApiResponseIsSuccess(this.BuildExpectedRequest(), this.Operation);

        private ExpectedRequest BuildExpectedRequest() =>
            new ExpectedRequest
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri(UseCaseHelper.GetPathFromRequest(this.request), UriKind.Relative),
                Content = this.request
                    .Map(value => this.Helper.Serializer.SerializeObject(new {RemoveStream = value.StreamId}))
                    .IfFailure(string.Empty),
            };

        private static Result<RemoveStreamRequest> BuildRequest(ISpecimenBuilder fixture) =>
            RemoveStreamRequest.Parse(fixture.Create<Guid>(), fixture.Create<Guid>(), fixture.Create<Guid>());
    }
}