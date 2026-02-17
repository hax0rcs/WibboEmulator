namespace WibboEmulator.Games.Chats.Commands;

using GameClients;
using Rooms;

public interface IChatCommand
{
    void Execute(GameClient session, Room room, RoomUser roomUser, string[] parameters);
}
