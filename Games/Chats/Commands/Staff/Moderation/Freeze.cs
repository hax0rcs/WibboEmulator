namespace WibboEmulator.Games.Chats.Commands.Staff.Moderation;

using GameClients;
using Rooms;

internal sealed class Freeze : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (parameters.Length != 2)
        {
            return;
        }

        var targetUser = userRoom.Room.RoomUserManager.GetRoomUserByName(parameters[1]);
        if (targetUser == null)
        {
            return;
        }

        targetUser.Freeze = !targetUser.Freeze;
        targetUser.FreezeEndCounter = 0;
    }
}
