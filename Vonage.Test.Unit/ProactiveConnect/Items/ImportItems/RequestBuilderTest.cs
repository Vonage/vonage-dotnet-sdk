using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using AutoFixture;
using FluentAssertions;
using Vonage.ProactiveConnect.Items.ImportItems;
using Vonage.Test.Unit.Common.Extensions;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Items.ImportItems
{
    public class RequestBuilderTest
    {
        private readonly Fixture fixture;
        private readonly Guid listId;
        private readonly MockFileData mockData;
        private readonly MockFileSystem mockFileSystem;
        private readonly string filePath;

        public RequestBuilderTest()
        {
            this.fixture = new Fixture();
            this.listId = this.fixture.Create<Guid>();
            this.filePath = @"C:\ThisIsATest.txt";
            this.mockData = new MockFileData("Bla bla bla.");
            this.mockFileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {this.filePath, this.mockData},
            });
        }

        [Fact]
        public void Build_ShouldReturnFailure_GivenFileIsEmpty() =>
            this.Build()
                .WithListId(this.listId)
                .WithFileData(Array.Empty<byte>())
                .Create()
                .Should()
                .BeParsingFailure("File cannot be empty.");

        [Fact]
        public void Build_ShouldReturnFailure_GivenFilePathIsIncorrect() =>
            this.Build()
                .WithListId(this.listId)
                .WithFilePath(this.fixture.Create<string>())
                .Create()
                .Should()
                .BeParsingFailure("File cannot be empty.");

        [Fact]
        public void Build_ShouldReturnFailure_GivenListIdIsEmpty() =>
            this.Build()
                .WithListId(Guid.Empty)
                .WithFilePath(this.filePath)
                .Create()
                .Should()
                .BeParsingFailure("ListId cannot be empty.");

        [Fact]
        public void Build_ShouldSetFile_GivenFileIsProvided() =>
            this.Build()
                .WithListId(this.listId)
                .WithFileData(this.mockData.Contents)
                .Create()
                .Map(request => request.File)
                .Should()
                .BeSuccess(file => file.Should().BeEquivalentTo(this.mockData.Contents));

        [Fact]
        public void Build_ShouldSetFile_GivenFilePathIsProvided() =>
            this.Build()
                .WithListId(this.listId)
                .WithFilePath(this.filePath)
                .Create()
                .Map(request => request.File)
                .Should()
                .BeSuccess(file => file.Should().BeEquivalentTo(this.mockData.Contents));

        [Fact]
        public void Build_ShouldSetListId() =>
            this.Build()
                .WithListId(this.listId)
                .WithFilePath(this.filePath)
                .Create()
                .Map(request => request.ListId)
                .Should()
                .BeSuccess(this.listId);

        private IBuilderForListId Build() => new ImportItemsRequestBuilder(this.mockFileSystem.File);
    }
}