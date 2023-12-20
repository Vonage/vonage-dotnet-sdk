﻿using FluentAssertions;
using Vonage.Common.Failures;
using Xunit;

namespace Vonage.Test.Unit.Common.Failures
{
    public class AuthenticationFailureTest
    {
        [Fact]
        public void Type_ShouldReturnAuthenticationFailure() => new AuthenticationFailure()
            .Type
            .Should()
            .Be(typeof(AuthenticationFailure));
    }
}