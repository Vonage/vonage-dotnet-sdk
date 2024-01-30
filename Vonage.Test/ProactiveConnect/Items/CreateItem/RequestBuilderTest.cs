using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Vonage.ProactiveConnect.Items.CreateItem;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.ProactiveConnect.Items.CreateItem;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    private readonly KeyValuePair<string, object> element;
    private readonly Guid listId;

    public RequestBuilderTest()
    {
        var fixture = new Fixture();
        this.listId = fixture.Create<Guid>();
        this.element = fixture.Create<KeyValuePair<string, object>>();
    }

    [Fact]
    public void Build_ShouldReturnFailure_GivenDataIsEmpty() =>
        CreateItemRequest
            .Build()
            .WithListId(this.listId)
            .Create()
            .Should()
            .BeParsingFailure("Data cannot be empty.");

    [Fact]
    public void Build_ShouldReturnFailure_GivenListIdIsEmpty() =>
        CreateItemRequest
            .Build()
            .WithListId(Guid.Empty)
            .WithCustomData(this.element)
            .Create()
            .Should()
            .BeParsingFailure("ListId cannot be empty.");

    [Fact]
    public void Build_ShouldSetData() =>
        CreateItemRequest
            .Build()
            .WithListId(this.listId)
            .WithCustomData(new KeyValuePair<string, object>("value1", "value"))
            .WithCustomData(new KeyValuePair<string, object>("value2", 0))
            .WithCustomData(new KeyValuePair<string, object>("value3", true))
            .Create()
            .Map(request => request.Data)
            .Should()
            .BeSuccess(data => data.ToList().Should().BeEquivalentTo(new[]
            {
                new KeyValuePair<string, object>("value1", "value"),
                new KeyValuePair<string, object>("value2", 0),
                new KeyValuePair<string, object>("value3", true),
            }));

    [Fact]
    public void Build_ShouldSetListId() =>
        CreateItemRequest
            .Build()
            .WithListId(this.listId)
            .WithCustomData(this.element)
            .Create()
            .Map(request => request.ListId)
            .Should()
            .BeSuccess(this.listId);
}