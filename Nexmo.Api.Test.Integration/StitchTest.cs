using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nexmo.Api.Stitch;

namespace Nexmo.Api.Test.Integration
{
	[TestClass]
	public class StitchTest
	{
		[TestMethod]
		public void should_create_then_delete_convo_user_member()
		{
			var ids = should_create_convo_user_member();
			should_delete_convo_user_member(ids.Item1, ids.Item2, ids.Item3);
		}

		private Tuple<string, string, string> should_create_convo_user_member()
		{
			var convo = Conversation.CreateConversation(new Conversation.ConversationRequest
			{
				Name = "nexmodotnet",
				DisplayName = "Nexmo dotnet Client"
			});

			Console.WriteLine(convo.Id);

			var user = User.CreateUser(new User.UserRequest
			{
				Name = "testuser",
				DisplayName = "Test User",
			});

			Console.WriteLine(user.Id);

			var member = Member.CreateMember(convo.Id, new Member.MemberRequest
			{
				UserId = user.Id,
				UserName = "testuser",
				Action = "join",
				Channel = new Member.MemberChannel
				{
					Type = "app"
				},
			});

			Console.WriteLine(member.Id);

			return new Tuple<string, string, string>(convo.Id, user.Id, member.Id);
		}

		private void should_delete_convo_user_member(string convoId, string userId, string memberId)
		{
			var memberResponse = Member.DeleteMember(convoId, memberId);
			var userResponse = User.DeleteUser(userId);
			var convoResponse = Conversation.DeleteConversation(convoId);
		}
	}
}