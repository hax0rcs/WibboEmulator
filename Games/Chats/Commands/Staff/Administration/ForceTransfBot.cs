namespace WibboEmulator.Games.Chats.Commands.Staff.Administration;

using Communication.Packets.Outgoing.Rooms.Engine;
using Core.Language;
using GameClients;
using Rooms;

internal sealed class ForceTransfBot : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (parameters.Length != 2)
        {
            return;
        }

        var username = parameters[1];

        var roomUserByUserId = room.RoomUserManager.GetRoomUserByName(username);
        if (roomUserByUserId == null || roomUserByUserId.Client == null)
        {
            return;
        }

        if (session.Language != roomUserByUserId.Client.Language)
        {
            session.SendWhisper(string.Format(LanguageManager.TryGetValue("cmd.authorized.langue.user", session.Language), roomUserByUserId.Client.Language));
            return;
        }

        if (!roomUserByUserId.IsTransf && !roomUserByUserId.IsSpectator)
        {
            roomUserByUserId.TransfBot = !roomUserByUserId.TransfBot;

            room.SendPacket(new UserRemoveComposer(roomUserByUserId.VirtualId));
            room.SendPacket(new UsersComposer(roomUserByUserId));
        }

    }
}
