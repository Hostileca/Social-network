namespace Infrastructure.SignalR.Hubs;

public static class ClientEvents
{
    public const string ChatCreated = "ChatCreated";
    
    public const string ChatDeleted = "ChatDeleted";
    
    public const string ChatMemberAdded = "ChatMemberAdded";
    
    public const string ChatMemberRemoved = "ChatMemberRemoved";
    
    public const string ChatMemberUpdated = "ChatMemberRemoved";
    
    public const string ChatMemberLeft = "ChatMemberLeft";
    
    public const string MessageSent = "MessageSent";
    
    public const string ReactionSent = "ReactionSent";
    
    public const string ReactionRemoved = "ReactionSent";
}