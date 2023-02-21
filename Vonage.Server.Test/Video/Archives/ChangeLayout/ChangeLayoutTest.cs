using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Kernel;
using FsCheck;
using FsCheck.Xunit;
using Vonage.Common.Monads;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Common;
using Vonage.Server.Serialization;
using Vonage.Server.Video.Archives;
using Vonage.Server.Video.Archives.ChangeLayout;
using WireMock.RequestBuilders;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.ChangeLayout
{
    public class ChangeLayoutTest
    {
        private readonly ArchiveClient client;

        private Func<Task<Result<Unit>>> Operation => () => this.client.ChangeLayoutAsync(this.request);
        private readonly Result<ChangeLayoutRequest> request;
        private readonly UseCaseHelper helper;

        public ChangeLayoutTest()
        {
            this.helper = new UseCaseHelper(JsonSerializerBuilder.Build());
            this.client = new ArchiveClient(this.helper.Server.CreateClient(), () => this.helper.Token,
                this.helper.Fixture.Create<string>());
            this.request = BuildRequest(this.helper.Fixture);
        }

        [Property]
        public Property ShouldReturnFailure_GivenApiErrorCannotBeParsed() =>
            this.helper.VerifyReturnsFailureGivenErrorCannotBeParsed(this.CreateRequest(), this.Operation);

        [Property]
        public Property ShouldReturnFailure_GivenApiResponseIsError() =>
            this.helper.VerifyReturnsFailureGivenApiResponseIsError(this.CreateRequest(), this.Operation);

        [Fact]
        public async Task ShouldReturnFailure_GivenRequestIsFailure() =>
            await this.helper.VerifyReturnsFailureGivenRequestIsFailure<ChangeLayoutRequest, Unit>(this.client
                .ChangeLayoutAsync);

        [Fact]
        public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
            await this.helper.VerifyReturnsUnitGivenApiResponseIsSuccess(this.CreateRequest(), this.Operation);

        private static Result<ChangeLayoutRequest> BuildRequest(ISpecimenBuilder fixture) =>
            ChangeLayoutRequest.Parse(fixture.Create<Guid>(), fixture.Create<Guid>(),
                fixture.Create<ArchiveLayout>());

        private IRequestBuilder CreateRequest() =>
            WireMockExtensions
                .CreateRequest(this.helper.Token, UseCaseHelper.GetPathFromRequest(this.request))
                .UsingPut();
    }
}