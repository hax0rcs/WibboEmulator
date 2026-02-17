namespace WibboEmulator.Games.Chats.Commands.Staff.Gestion;

using GameClients;
using Rooms;

internal sealed class UnloadEmptyRooms : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters) => RoomManager.UnloadEmptyRooms();
}
