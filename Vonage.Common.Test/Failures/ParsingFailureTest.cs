﻿using FluentAssertions;
using Vonage.Common.Exceptions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;

namespace Vonage.Common.Test.Failures
{
    public class ParsingFailureTest
    {
        [Fact]
        public void GetFailureMessage_ShouldReturnFailureMessage() =>
            InitializeParsingFailure()
                .GetFailureMessage()
                .Should()
                .Be("Parsing failed with the following errors: First failure, Second failure.");

        [Fact]
        public void ToException_ShouldReturnVonageException() =>
            InitializeParsingFailure()
                .ToException()
                .Should()
                .BeOfType<VonageException>()
                .Which.Message.Should().Be("Parsing failed with the following errors: First failure, Second failure.");

        [Fact]
        public void ToResult_ShouldReturnFailure() =>
            InitializeParsingFailure()
                .ToResult<int>()
                .Should()
                .BeFailure(InitializeParsingFailure());

        [Fact]
        public void Type_ShouldReturnParsingFailure() =>
            InitializeParsingFailure()
                .Type
                .Should()
                .Be(typeof(ParsingFailure));

        private static ParsingFailure InitializeParsingFailure() =>
            ParsingFailure.FromFailures(ResultFailure.FromErrorMessage("First failure"),
                ResultFailure.FromErrorMessage("Second failure"));
    }
}