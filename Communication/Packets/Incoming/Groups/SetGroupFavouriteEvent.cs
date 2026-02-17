namespace WibboEmulator.Communication.Packets.Incoming.Groups;

using Database;
using Database.Daos.User;
using Games.GameClients;
using Games.Groups;
using Outgoing.Groups;
using Outgoing.Users;

internal sealed class SetGroupFavouriteEvent : IPacketEvent
{
    public double Delay => 100;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (session == null)
        {
            return;
        }

        var groupId = packet.PopInt();
        if (groupId == 0)
        {
            return;
        }

        if (!GroupManager.TryGetGroup(groupId, out var group))
        {
            return;
        }

        session.User.FavouriteGroupId = group.Id;
        using (var dbClient = DatabaseManager.Connection)
        {
            UserStatsDao.UpdateGroupId(dbClient, session.User.FavouriteGroupId, session.User.Id);
        }

        if (session.User.InRoom && session.User.Room != null)
        {
            session.User.Room.SendPacket(new RefreshFavouriteGroupComposer(session.User.Id));
            if (group != null)
            {
                session.User.Room.SendPacket(new UserGroupBadgesComposer(group));

                var user = session.User.Room.RoomUserManager.GetRoomUserByUserId(session.User.Id);
                if (user != null)
                {
                    session.User.Room.SendPacket(new UpdateFavouriteGroupComposer(group, user.VirtualId));
                }
            }
        }
        else
        {
            session.SendPacket(new RefreshFavouriteGroupComposer(session.User.Id));
        }
    }
}
