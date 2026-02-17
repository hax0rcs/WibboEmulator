namespace WibboEmulator.Games.Chats.Commands.User.Room;

using GameClients;
using Rooms;

internal sealed class DisableDiagonal : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters) => room.GameMap.DiagonalEnabled = !room.GameMap.DiagonalEnabled;
}
