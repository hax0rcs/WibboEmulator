namespace WibboEmulator.Games.Chats.Commands.User.Several;

using GameClients;
using Rooms;

internal sealed class Teleport : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters) => userRoom.TeleportEnabled = !userRoom.TeleportEnabled;
}
