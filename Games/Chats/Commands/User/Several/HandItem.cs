namespace WibboEmulator.Games.Chats.Commands.User.Several;

using GameClients;
using Rooms;
using Rooms.Games.Teams;

internal sealed class HandItem : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (parameters.Length != 2)
        {
            return;
        }

        if (userRoom.Team != TeamType.None || userRoom.InGame || room.IsGameMode)
        {
            return;
        }

        _ = int.TryParse(parameters[1], out var handitemid);
        if (handitemid < 0)
        {
            return;
        }

        userRoom.CarryItem(handitemid);
    }
}
