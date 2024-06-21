using System;
using Vonage.Test.Common.Extensions;
using Vonage.Video.ExperienceComposer.GetSessions;
using Xunit;

namespace Vonage.Test.Video.ExperienceComposer.GetSessions;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    private const int ValidOffset = 100;
    private const int ValidCount = 100;
    private readonly Guid validApplicationId = Guid.NewGuid();

    [Fact]
    public void Build_ShouldHaveDefaultOffset() =>
        GetSessionsRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .Create()
            .Map(request => request.Offset)
            .Should()
            .BeSuccess(0);

    [Fact]
    public void Build_ShouldHaveDefaultCount() =>
        GetSessionsRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .Create()
            .Map(request => request.Count)
            .Should()
            .BeSuccess(50);

    [Fact]
    public void Build_ShouldReturnFailure_GivenCountIsHigherThanOneThousand() =>
        GetSessionsRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .WithCount(1001)
            .Create()
            .Should()
            .BeParsingFailure("Count cannot be higher than 1000.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenCountIsLowerThanFifty() =>
        GetSessionsRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .WithCount(49)
            .Create()
            .Should()
            .BeParsingFailure("Count cannot be lower than 50.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
        GetSessionsRequest
            .Build()
            .WithApplicationId(Guid.Empty)
            .Create()
            .Should()
            .BeParsingFailure("ApplicationId cannot be empty.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenOffsetIsLowerThanZero() =>
        GetSessionsRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .WithOffset(-1)
            .Create()
            .Should()
            .BeParsingFailure("Offset cannot be lower than 0.");

    [Fact]
    public void Build_ShouldSetApplicationId() =>
        GetSessionsRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .Create()
            .Map(request => request.ApplicationId)
            .Should()
            .BeSuccess(this.validApplicationId);

    [Fact]
    public void Build_ShouldSetCount() =>
        GetSessionsRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .WithCount(ValidCount)
            .Create()
            .Map(request => request.Count)
            .Should()
            .BeSuccess(ValidCount);

    [Fact]
    public void Build_ShouldSetOffset() =>
        GetSessionsRequest
            .Build()
            .WithApplicationId(this.validApplicationId)
            .WithOffset(ValidOffset)
            .Create()
            .Map(request => request.Offset)
            .Should()
            .BeSuccess(ValidOffset);
}