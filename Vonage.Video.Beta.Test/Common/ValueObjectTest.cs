using System.Collections.Generic;
using FluentAssertions;
using Vonage.Video.Beta.Common;
using Xunit;

namespace Vonage.Video.Beta.Test.Common
{
    public class ValueObjectTest
    {
        [Fact]
        public void Equals_ShouldReturnFalse_GivenValueAreDifferent() =>
            FakeValueObject.Hello.Equals(FakeValueObject.World).Should().BeFalse();

        [Fact]
        public void Equals_ShouldReturnTrue_GivenValueAreSame() =>
            FakeValueObject.Hello.Equals(FakeValueObject.Hello).Should().BeTrue();

        [Fact]
        public void EqualsOperator_ShouldReturnFalse_GivenValueAreDifferent() =>
            (FakeValueObject.Hello == FakeValueObject.World).Should().BeFalse();

        [Fact]
        public void EqualsOperator_ShouldReturnTrue_GivenValueAreSame() =>
            (FakeValueObject.Hello == FakeValueObject.Hello).Should().BeTrue();

        [Fact]
        public void DifferentOperator_ShouldReturnFalse_GivenValueAreSame() =>
            (FakeValueObject.Hello != FakeValueObject.Hello).Should().BeFalse();

        [Fact]
        public void DifferentOperator_ShouldReturnTrue_GivenValueAreDifferent() =>
            (FakeValueObject.Hello != FakeValueObject.World).Should().BeTrue();

        [Fact]
        public void GetHashCode_ShouldReturnSameValue_GivenValueAreSame() =>
            FakeValueObject.Hello.GetHashCode().Should()
                .Be(FakeValueObject.Hello.GetHashCode());

        [Fact]
        public void GetHashCode_ShouldReturnDifferent_GivenValueAreDifferent() =>
            FakeValueObject.Hello.GetHashCode().Should()
                .NotBe(FakeValueObject.World.GetHashCode());

        private class FakeValueObject : ValueObject<string>
        {
            private readonly string value;

            private FakeValueObject(string value)
            {
                this.value = value;
            }

            public static FakeValueObject Hello => new FakeValueObject("Hello");
            public static FakeValueObject World => new FakeValueObject("World");

            protected override IEnumerable<object> GetEqualityComponents() => new[] {this.value};
        }
    }
}