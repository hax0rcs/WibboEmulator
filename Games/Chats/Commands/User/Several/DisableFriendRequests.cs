namespace WibboEmulator.Games.Chats.Commands.User.Several;

using Core.Language;
using GameClients;
using Rooms;

internal sealed class DisableFriendRequests : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (session.User.HasFriendRequestsDisabled)
        {
            session.User.HasFriendRequestsDisabled = false;
            session.SendWhisper(LanguageManager.TryGetValue("cmd.textamigo.true", session.Language));
        }
        else
        {
            session.User.HasFriendRequestsDisabled = true;
            session.SendWhisper(LanguageManager.TryGetValue("cmd.textamigo.false", session.Language));
        }
    }
}
