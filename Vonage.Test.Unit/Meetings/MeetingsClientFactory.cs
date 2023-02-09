using System.IO.Abstractions.TestingHelpers;
using AutoFixture;
using Vonage.Common.Test;
using Vonage.Meetings;

namespace Vonage.Test.Unit.Meetings
{
    public static class MeetingsClientFactory
    {
        public static MeetingsClient Create(UseCaseHelper helper) =>
            new MeetingsClient(helper.Server.CreateClient(),
                () => helper.Token,
                helper.Fixture.Create<string>(),
                new MockFileSystem());

        public static MeetingsClient Create(UseCaseHelper helper, MockFileSystem mockFileSystem) =>
            new MeetingsClient(helper.Server.CreateClient(),
                () => helper.Token,
                helper.Fixture.Create<string>(),
                mockFileSystem);
    }
}