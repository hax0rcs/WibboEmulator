namespace WibboEmulator.Games.Chats.Commands.Staff.Moderation;

using Communication.Packets.Outgoing.Rooms.Chat;
using Core.Language;
using GameClients;
using Rooms;

internal sealed class Mute : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (parameters.Length != 2)
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
            var user = targetUser.User;

            user.SpamProtectionTime = 300;
            user.SpamEnable = true;
            targetUser.SendPacket(new FloodControlComposer(user.SpamProtectionTime));
        }
    }
}
