﻿using FluentAssertions;
using Vonage.Common.Exceptions;
using Vonage.Common.Failures;
using Vonage.Test.Unit.Common.Extensions;
using Xunit;

namespace Vonage.Test.Unit.Common.Failures
{
    public class ResultFailureTest
    {
        [Fact]
        public void FromErrorMessage_ShouldReturnFailure() =>
            ResultFailure.FromErrorMessage("Some error.").GetFailureMessage().Should().Be("Some error.");

        [Fact]
        public void ToException_ShouldReturnVonageException() =>
            ResultFailure.FromErrorMessage("Some error.").ToException()
                .Should()
                .BeOfType<VonageException>()
                .Which.Message.Should().Be("Some error.");

        [Fact]
        public void ToResult_ShouldReturnFailure() =>
            ResultFailure.FromErrorMessage("Some error.").ToResult<int>().Should()
                .BeFailure(ResultFailure.FromErrorMessage("Some error."));

        [Fact]
        public void Type_ShouldReturnResultFailure() => ResultFailure.FromErrorMessage("Some error.")
            .Type
            .Should()
            .Be(typeof(ResultFailure));
    }
}