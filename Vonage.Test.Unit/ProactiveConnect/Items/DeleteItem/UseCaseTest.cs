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
using Vonage.ProactiveConnect;
using Vonage.ProactiveConnect.Items;
using Vonage.ProactiveConnect.Items.DeleteItem;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Items.DeleteItem;

public class UseCaseTest : BaseUseCase
{
    private Func<VonageHttpClientConfiguration, Task<Result<ListItem>>> Operation =>
        configuration => new ProactiveConnectClient(configuration).DeleteItemAsync(this.request);

    private readonly Result<DeleteItemRequest> request;

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
            .VerifyReturnsFailureGivenRequestIsFailure<DeleteItemRequest, ListItem>(
                (configuration, failureRequest) =>
                    new ProactiveConnectClient(configuration).DeleteItemAsync(failureRequest));

    [Fact]
    public async Task ShouldReturnFailure_GivenTokenGenerationFailed() =>
        await this.helper.VerifyReturnsFailureGivenTokenGenerationFails(this.Operation);

    [Fact]
    public async Task ShouldReturnSuccess_GivenApiResponseIsSuccess() =>
        await this.helper.VerifyReturnsExpectedValueGivenApiResponseIsSuccess(this.BuildExpectedRequest(),
            this.Operation);

    private ExpectedRequest BuildExpectedRequest() =>
        new()
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri(UseCaseHelper.GetPathFromRequest(this.request), UriKind.Relative),
            Content = this.request
                .Map(value => this.helper.Serializer.SerializeObject(value))
                .IfFailure(string.Empty),
        };

    private static Result<DeleteItemRequest> BuildRequest(ISpecimenBuilder fixture) =>
        DeleteItemRequest.Build().WithListId(fixture.Create<Guid>()).WithItemId(fixture.Create<Guid>()).Create();
}