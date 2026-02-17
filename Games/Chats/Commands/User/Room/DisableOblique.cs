namespace WibboEmulator.Games.Chats.Commands.User.Room;

using GameClients;
using Rooms;

internal sealed class DisableOblique : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters) => room.GameMap.ObliqueDisable = !room.GameMap.ObliqueDisable;
}
