#region
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads.Maybe;

public class DoTest
{
    [Fact]
    public void Do_ShouldExecuteSomeAction_GivenStateIsSome()
    {
        var value = 0;
        MaybeBehaviors.CreateSome(5).Do(success => value += success, () => { });
        value.Should().Be(5);
    }

    [Fact]
    public void Do_ShouldExecuteNoneAction_GivenStateIsNone()
    {
        var value = 0;
        MaybeBehaviors.CreateNone<int>().Do(_ => { }, () => value = 10);
        value.Should().Be(10);
    }

    [Fact]
    public void Do_ShouldReturnInstance() =>
        MaybeBehaviors.CreateSome(5).Do(_ => { }, () => { }).Should().BeSome(5);

    [Fact]
    public void DoWhenSome_ShouldExecuteAction_GivenStateIsSome()
    {
        var value = 0;
        MaybeBehaviors.CreateSome(5).DoWhenSome(success => value += success);
        value.Should().Be(5);
    }

    [Fact]
    public void DoWhenSome_ShouldNotExecuteAction_GivenStateIsNone()
    {
        var value = 0;
        MaybeBehaviors.CreateNone<int>().DoWhenSome(_ => value = 10);
        value.Should().Be(0);
    }

    [Fact]
    public void DoWhenSome_ShouldReturnInstance() =>
        MaybeBehaviors.CreateSome(5).DoWhenSome(_ => { }).Should().BeSome(5);

    [Fact]
    public void DoWhenNone_ShouldExecuteAction_GivenStateIsNone()
    {
        var value = 0;
        MaybeBehaviors.CreateNone<int>().DoWhenNone(() => value = 10);
        value.Should().Be(10);
    }

    [Fact]
    public void DoWhenNone_ShouldNotExecuteAction_GivenStateIsSome()
    {
        var value = 0;
        MaybeBehaviors.CreateSome(5).DoWhenNone(() => value = 10);
        value.Should().Be(0);
    }

    [Fact]
    public void DoWhenFailure_ShouldReturnInstance() =>
        MaybeBehaviors.CreateSome(5).DoWhenNone(() => { }).Should().BeSome(5);

    [Fact]
    public async Task DoExtension_ShouldExecuteSomeAction_GivenStateIsSome()
    {
        var value = 0;
        await MaybeBehaviors.CreateSomeAsync(5).Do(success => value += success, () => { });
        value.Should().Be(5);
    }

    [Fact]
    public async Task DoExtension_ShouldExecuteNoneAction_GivenStateIsNone()
    {
        var value = 0;
        await MaybeBehaviors.CreateNoneAsync<int>().Do(_ => { }, () => value = 10);
        value.Should().Be(10);
    }

    [Fact]
    public async Task DoExtension_ShouldReturnInstance()
    {
        var result = await MaybeBehaviors.CreateSomeAsync(5).Do(_ => { }, () => { });
        result.Should().BeSome(5);
    }

    [Fact]
    public async Task DoWhenSomeExtension_ShouldExecuteAction_GivenStateIsSome()
    {
        var value = 0;
        await MaybeBehaviors.CreateSomeAsync(5).DoWhenSome(success => value += success);
        value.Should().Be(5);
    }

    [Fact]
    public async Task DoWhenSomeExtension_ShouldNotExecuteAction_GivenStateIsNone()
    {
        var value = 0;
        await MaybeBehaviors.CreateNoneAsync<int>().DoWhenSome(_ => value = 10);
        value.Should().Be(0);
    }

    [Fact]
    public async Task DoWhenSomeExtension_ShouldReturnInstance()
    {
        var result = await MaybeBehaviors.CreateSomeAsync(5).DoWhenSome(_ => { });
        result.Should().BeSome(5);
    }

    [Fact]
    public async Task DoWhenNoneExtension_ShouldExecuteAction_GivenStateIsNone()
    {
        var value = 0;
        await MaybeBehaviors.CreateNoneAsync<int>().DoWhenNone(() => value = 10);
        value.Should().Be(10);
    }

    [Fact]
    public async Task DoWhenNoneExtension_ShouldNotExecuteAction_GivenStateIsNone()
    {
        var value = 0;
        await MaybeBehaviors.CreateSomeAsync(5).DoWhenNone(() => value = 10);
        value.Should().Be(0);
    }

    [Fact]
    public async Task DoWhenNoneExtension_ShouldReturnInstance()
    {
        var result = await MaybeBehaviors.CreateSomeAsync(5).DoWhenNone(() => { });
        result.Should().BeSome(5);
    }
}