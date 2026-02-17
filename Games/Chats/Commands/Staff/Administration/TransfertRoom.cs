namespace WibboEmulator.Games.Chats.Commands.Staff.Administration;

using Communication.Packets.Outgoing.Navigator;
using Communication.Packets.Outgoing.Rooms.Session;
using Database;
using Database.Daos.Room;
using Database.Daos.User;
using GameClients;
using Rooms;
using Users;

internal sealed class TransfertRoom : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (parameters.Length != 2)
        {
            return;
        }

        if (!room.CheckRights(session, true) && !session.User.HasPermission("transfert_all_room"))
        {
            return;
        }

        var username = parameters[1];

        using var dbClient = DatabaseManager.Connection;

        var userId = UserDao.GetIdByName(dbClient, username);
        if (userId == 0)
        {
            return;
        }

        var userTarget = UserManager.GetUserById(userId);

        RoomDao.UpdateOwnerByRoomId(dbClient, userTarget.Username, room.Id);

        room.RoomData.OwnerName = userTarget.Username;
        room.SendPacket(new RoomInfoUpdatedComposer(room.Id));

        var usersToReturn = room.RoomUserManager.RoomUsers.ToList();

        RoomManager.UnloadRoom(room);

        foreach (var user in usersToReturn)
        {
            if (user == null || user.Client == null)
            {
                continue;
            }

            user.Client.SendPacket(new RoomForwardComposer(room.Id));
        }
    }
}
