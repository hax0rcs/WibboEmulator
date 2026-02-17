namespace WibboEmulator.Games.Chats.Commands.Staff.Administration;

using GameClients;
using Rooms;

internal sealed class RegenMap : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        room.GameMap.GenerateMaps();
        session.SendWhisper("Rafraichissement de la map d'appartement");
    }
}
