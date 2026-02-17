namespace WibboEmulator.Games.Chats.Commands.Staff.Administration;

using Communication.Packets.Outgoing.Rooms.Engine;
using GameClients;
using Rooms;

internal sealed class ForceTransfStop : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (parameters.Length != 2)
        {
            return;
        }

        var username = parameters[1];

        var roomUserByUserId = room.RoomUserManager.GetRoomUserByName(username);
        if (roomUserByUserId == null)
        {
            return;
        }

        if (roomUserByUserId.IsTransf && !roomUserByUserId.IsSpectator)
        {
            roomUserByUserId.IsTransf = false;

            room.SendPacket(new UserRemoveComposer(roomUserByUserId.VirtualId));
            room.SendPacket(new UsersComposer(roomUserByUserId));
        }
    }
}
