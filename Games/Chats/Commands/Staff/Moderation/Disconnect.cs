namespace WibboEmulator.Games.Chats.Commands.Staff.Moderation;

using Core.Language;
using GameClients;
using Rooms;

internal sealed class Disconnect : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (parameters.Length < 2)
        {
            return;
        }

        var targetUser = GameClientManager.GetClientByUsername(parameters[1]);
        if (targetUser == null || targetUser.User == null)
        {
            userRoom.SendWhisperChat(LanguageManager.TryGetValue("input.usernotfound", session.Language));
        }
        else if (targetUser.User.Rank >= session.User.Rank)
        {
            userRoom.SendWhisperChat(LanguageManager.TryGetValue("action.notallowed", session.Language));
        }
        else
        {
            targetUser.Disconnect();
        }
    }
}
