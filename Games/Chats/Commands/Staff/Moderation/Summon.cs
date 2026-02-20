namespace WibboEmulator.Games.Chats.Commands.Staff.Moderation;

using Communication.Packets.Outgoing.Navigator;
using Core.Language;
using GameClients;
using Rooms;

internal sealed class Summon : IChatCommand
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
            session.SendWhisper(LanguageManager.TryGetValue("input.useroffline", session.Language));
            return;
        }
        else if (targetUser.User.Room != null && targetUser.User.Room.Id == session.User.Room.Id)
        {
            return;
        }

        if (!targetUser.User.IsTeleporting)
        {
            targetUser.User.IsTeleporting = true;
            targetUser.User.TeleportingRoomId = room.RoomData.Id;
            targetUser.User.TeleporterId = 0;

            targetUser.SendPacket(new GetGuestRoomResultComposer(targetUser, room.RoomData, false, true));
        }
    }
}
