namespace WibboEmulator.Games.Chats.Commands.Staff.Moderation;

using Core.Language;
using GameClients;
using Rooms;

internal sealed class RemoveBadge : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        var targetUser = GameClientManager.GetClientByUsername(parameters[1]);
        if (targetUser != null && targetUser.User != null)
        {
            targetUser.User.BadgeComponent.RemoveBadge(parameters[2]);
        }
        else
        {
            userRoom.SendWhisperChat(LanguageManager.TryGetValue("input.usernotfound", session.Language));
        }
    }
}
