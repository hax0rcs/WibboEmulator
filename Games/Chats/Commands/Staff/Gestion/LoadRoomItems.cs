namespace WibboEmulator.Games.Chats.Commands.Staff.Gestion;

using Communication.Packets.Outgoing.Navigator;
using Database;
using GameClients;
using Rooms;

internal sealed class LoadRoomItems : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (parameters.Length != 2)
        {
            return;
        }

        if (!int.TryParse(parameters[1], out var roomId))
        {
            return;
        }

        if (room.Id == roomId)
        {
            return;
        }

        using var dbClient = DatabaseManager.Connection;

        room.RoomItemHandling.LoadFurniture(dbClient, roomId);
        room.GameMap.GenerateMaps(true);
        session.SendWhisper("Mobi de l'appart n° " + roomId + " chargé!");
        session.SendPacket(new GetGuestRoomResultComposer(session, room.RoomData, false, true));
    }
}
