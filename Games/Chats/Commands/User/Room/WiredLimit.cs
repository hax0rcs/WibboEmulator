namespace WibboEmulator.Games.Chats.Commands.User.Room;

using Core.Language;
using Database;
using Database.Daos.Room;
using GameClients;
using Rooms;

internal sealed class WiredLimit : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        room.WiredHandler.SecurityEnabled = !room.WiredHandler.SecurityEnabled;

        if (room.WiredHandler.SecurityEnabled)
        {
            userRoom.SendWhisperChat(LanguageManager.TryGetValue("cmd.wiredlimit.true", session.Language));
        }
        else
        {
            userRoom.SendWhisperChat(LanguageManager.TryGetValue("cmd.wiredlimit.false", session.Language));
        }

        room.RoomData.WiredSecurity = room.WiredHandler.SecurityEnabled;

        using (var dbClient = DatabaseManager.Connection)
        {
            RoomDao.UpdateWiredSecurity(dbClient, room.Id, room.RoomData.WiredSecurity);
        }
    }
}
