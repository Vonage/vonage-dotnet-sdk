#region
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads;

public class MaybeExtensionsTest
{
    public class Bind
    {
        [Fact]
        public async Task BindAsync_ShouldReturnNone_GivenValueIsNone()
        {
            var result = await MaybeBehaviors.CreateNoneAsync<int>().BindAsync(MaybeBehaviors.IncrementBindAsync);
            result.Should().BeNone();
        }

        [Fact]
        public async Task BindAsync_ShouldReturnSome_GivenValueIsSome()
        {
            var result = await MaybeBehaviors.CreateSomeAsync(10).BindAsync(MaybeBehaviors.IncrementBindAsync);
            result.Should().BeSome(11);
        }

        [Fact]
        public async Task Bind_ShouldReturnNone_GivenValueIsNone()
        {
            var result = await MaybeBehaviors.CreateNoneAsync<int>().Bind(MaybeBehaviors.IncrementBind);
            result.Should().BeNone();
        }

        [Fact]
        public async Task Bind_ShouldReturnSome_GivenValueIsSome()
        {
            var result = await MaybeBehaviors.CreateSomeAsync(10).Bind(MaybeBehaviors.IncrementBind);
            result.Should().BeSome(11);
        }
    }

    public class Map
    {
        [Fact]
        public async Task Map_ShouldReturnNone_GivenValueIsNone()
        {
            var result = await MaybeBehaviors.CreateNoneAsync<int>().Map(MaybeBehaviors.Increment);
            result.Should().BeNone();
        }

        [Fact]
        public async Task Map_ShouldReturnSome_GivenValueIsSome()
        {
            var result = await MaybeBehaviors.CreateSomeAsync(10).Map(MaybeBehaviors.Increment);
            result.Should().BeSome(11);
        }

        [Fact]
        public async Task MapAsync_ShouldReturnNone_GivenValueIsNone()
        {
            var result = await MaybeBehaviors.CreateNoneAsync<int>().MapAsync(MaybeBehaviors.IncrementAsync);
            result.Should().BeNone();
        }

        [Fact]
        public async Task MapAsync_ShouldReturnSome_GivenValueIsSome()
        {
            var result = await MaybeBehaviors.CreateSomeAsync(10).MapAsync(MaybeBehaviors.IncrementAsync);
            result.Should().BeSome(11);
        }
    }

    public class Match
    {
        [Fact]
        public async Task Match_ShouldReturnNone_GivenValueIsNone()
        {
            var result = await MaybeBehaviors.CreateNoneAsync<int>().Match(_ => "some", () => "none");
            result.Should().Be("none");
        }

        [Fact]
        public async Task Match_ShouldReturnSome_GivenValueIsSome()
        {
            var result = await MaybeBehaviors.CreateSomeAsync(10).Match(_ => "some", () => "none");
            result.Should().Be("some");
        }
    }

    public class IfNone
    {
        [Fact]
        public async Task IfNone_Value_ShouldReturnSpecifiedValue_GivenValueIsNone()
        {
            var result = await MaybeBehaviors.CreateNoneAsync<int>().IfNone(5);
            result.Should().Be(5);
        }

        [Fact]
        public async Task IfNone_Value_ShouldReturnValue_GivenValueIsSome()
        {
            var result = await MaybeBehaviors.CreateSomeAsync(10).IfNone(5);
            result.Should().Be(10);
        }
    }

    public class Do
    {
        [Fact]
        public async Task Do_ShouldExecuteSomeAction_GivenStateIsSome()
        {
            var value = 0;
            await MaybeBehaviors.CreateSomeAsync(5).Do(success => value += success, () => { });
            value.Should().Be(5);
        }

        [Fact]
        public async Task Do_ShouldExecuteNoneAction_GivenStateIsNone()
        {
            var value = 0;
            await MaybeBehaviors.CreateNoneAsync<int>().Do(_ => { }, () => value = 10);
            value.Should().Be(10);
        }

        [Fact]
        public async Task Do_ShouldReturnInstance()
        {
            var result = await MaybeBehaviors.CreateSomeAsync(5).Do(_ => { }, () => { });
            result.Should().BeSome(5);
        }

        [Fact]
        public async Task DoWhenSome_ShouldExecuteAction_GivenStateIsSome()
        {
            var value = 0;
            await MaybeBehaviors.CreateSomeAsync(5).DoWhenSome(success => value += success);
            value.Should().Be(5);
        }

        [Fact]
        public async Task DoWhenSome_ShouldNotExecuteAction_GivenStateIsNone()
        {
            var value = 0;
            await MaybeBehaviors.CreateNoneAsync<int>().DoWhenSome(_ => value = 10);
            value.Should().Be(0);
        }

        [Fact]
        public async Task DoWhenSome_ShouldReturnInstance()
        {
            var result = await MaybeBehaviors.CreateSomeAsync(5).DoWhenSome(_ => { });
            result.Should().BeSome(5);
        }

        [Fact]
        public async Task DoWhenNone_ShouldExecuteAction_GivenStateIsNone()
        {
            var value = 0;
            await MaybeBehaviors.CreateNoneAsync<int>().DoWhenNone(() => value = 10);
            value.Should().Be(10);
        }

        [Fact]
        public async Task DoWhenNone_ShouldNotExecuteAction_GivenStateIsNone()
        {
            var value = 0;
            await MaybeBehaviors.CreateSomeAsync(5).DoWhenNone(() => value = 10);
            value.Should().Be(0);
        }

        [Fact]
        public async Task DoWhenNone_ShouldReturnInstance()
        {
            var result = await MaybeBehaviors.CreateSomeAsync(5).DoWhenNone(() => { });
            result.Should().BeSome(5);
        }
    }
}