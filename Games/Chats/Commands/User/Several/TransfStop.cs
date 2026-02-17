namespace WibboEmulator.Games.Chats.Commands.User.Several;

using Communication.Packets.Outgoing.Rooms.Engine;
using GameClients;
using Rooms;
using Rooms.Games.Teams;

internal sealed class TransfStop : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (userRoom.Team != TeamType.None || userRoom.InGame || room.IsGameMode)
        {
            return;
        }

        if (userRoom.IsTransf && !userRoom.IsSpectator && !userRoom.InGame)
        {
            userRoom.IsTransf = false;

            room.SendPacket(new UserRemoveComposer(userRoom.VirtualId));
            room.SendPacket(new UsersComposer(userRoom));
        }
    }
}
