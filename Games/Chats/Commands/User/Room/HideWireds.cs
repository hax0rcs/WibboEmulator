namespace WibboEmulator.Games.Chats.Commands.User.Room;

using Core.Language;
using Database;
using Database.Daos.Room;
using GameClients;
using Rooms;

internal sealed class HideWireds : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        room.RoomData.HideWireds = !room.RoomData.HideWireds;

        using (var dbClient = DatabaseManager.Connection)
        {
            RoomDao.UpdateHideWireds(dbClient, room.Id, room.RoomData.HideWireds);
        }

        if (room.RoomData.HideWireds)
        {
            session.SendWhisper(LanguageManager.TryGetValue("cmd.hidewireds.true", session.Language));
        }
        else
        {
            session.SendWhisper(LanguageManager.TryGetValue("cmd.hidewireds.false", session.Language));
        }
    }
}
