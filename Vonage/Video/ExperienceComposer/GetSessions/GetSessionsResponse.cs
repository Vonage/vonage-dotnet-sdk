namespace Vonage.Video.ExperienceComposer.GetSessions;

/// <summary>
/// </summary>
/// <param name="Count"></param>
/// <param name="Items"></param>
public record GetSessionsResponse(int Count, Session[] Items);