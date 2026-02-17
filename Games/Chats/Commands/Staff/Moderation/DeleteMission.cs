namespace WibboEmulator.Games.Chats.Commands.Staff.Moderation;

using Communication.Packets.Outgoing.Rooms.Engine;
using Core.Language;
using Database;
using Database.Daos.User;
using GameClients;
using Rooms;

internal sealed class DeleteMission : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (parameters.Length != 2)
        {
            return;
        }

        var username = parameters[1];
        var targetUser = GameClientManager.GetClientByUsername(username);
        if (targetUser == null)
        {
            userRoom.SendWhisperChat(LanguageManager.TryGetValue("input.usernotfound", session.Language));
        }
        else if (session.User.Rank <= targetUser.User.Rank)
        {
            userRoom.SendWhisperChat(LanguageManager.TryGetValue("user.notpermitted", session.Language));
        }
        else
        {
            targetUser.User.Motto = LanguageManager.TryGetValue("user.unacceptable_motto", targetUser.Language);
            using (var dbClient = DatabaseManager.Connection)
            {
                UserDao.UpdateMotto(dbClient, targetUser.User.Id, targetUser.User.Motto);
            }

            var currentRoom2 = targetUser.User.Room;
            if (currentRoom2 == null)
            {
                return;
            }

            var roomUserByUserId = currentRoom2.RoomUserManager.GetRoomUserByUserId(targetUser.User.Id);
            if (roomUserByUserId == null)
            {
                return;
            }

            currentRoom2.SendPacket(new UserChangeComposer(roomUserByUserId, false));
        }
    }
}
