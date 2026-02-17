namespace WibboEmulator.Games.Chats.Commands.Staff.Administration;

using GameClients;
using Rooms;

internal sealed class MakeSay : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (parameters.Length < 3)
        {
            return;
        }

        var username = parameters[1];
        var message = CommandManager.MergeParams(parameters, 2);

        var roomUserByUserId = session.User.Room.RoomUserManager.GetRoomUserByName(username);
        if (roomUserByUserId == null)
        {
            return;
        }

        roomUserByUserId.OnChat(message, 0, false);
    }
}
