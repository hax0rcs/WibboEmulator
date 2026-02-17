namespace WibboEmulator.Communication.Packets.Incoming.Rooms.Action;

using Database;
using Database.Daos.Room;
using Games.GameClients;
using Games.Rooms;
using Outgoing.Rooms.Permissions;

internal sealed class RemoveMyRightsEvent : IPacketEvent
{
    public double Delay => 250;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (session.User == null)
        {
            return;
        }

        if (!session.User.InRoom)
        {
            return;
        }

        if (!RoomManager.TryGetRoom(session.User.RoomId, out var room))
        {
            return;
        }

        if (!room.CheckRights(session))
        {
            return;
        }

        if (room.UsersWithRights.Contains(session.User.Id))
        {
            var user = room.RoomUserManager.GetRoomUserByUserId(session.User.Id);
            if (user != null && !user.IsBot)
            {
                user.RemoveStatus("flatctrl");
                user.UpdateNeeded = true;

                user.Client.SendPacket(new YouAreNotControllerComposer());
            }

            using (var dbClient = DatabaseManager.Connection)
            {
                RoomRightDao.Delete(dbClient, room.Id, session.User.Id);
            }

            if (room.UsersWithRights.Contains(session.User.Id))
            {
                _ = room.UsersWithRights.Remove(session.User.Id);
            }
        }
    }
}
