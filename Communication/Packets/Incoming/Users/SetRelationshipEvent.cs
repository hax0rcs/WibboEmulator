namespace WibboEmulator.Communication.Packets.Incoming.Users;

using Database;
using Database.Daos.Messenger;
using Games.GameClients;
using Games.Users.Messenger;

internal sealed class SetRelationshipEvent : IPacketEvent
{
    public double Delay => 250;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (session.User == null || session.User.Messenger == null)
        {
            return;
        }

        var user = packet.PopInt();
        var type = packet.PopInt();

        if (type is < 0 or > 4)
        {
            return;
        }

        if (!session.User.Messenger.FriendshipExists(user))
        {
            return;
        }

        if (type == 0)
        {
            if (session.User.Messenger.Relation.ContainsKey(user))
            {
                _ = session.User.Messenger.Relation.Remove(user);
            }
        }
        else
        {
            if (session.User.Messenger.Relation.TryGetValue(user, out var value))
            {
                value.Type = type;
            }
            else
            {
                session.User.Messenger.Relation.Add(user, new Relationship(user, type));
            }
        }

        using (var dbClient = DatabaseManager.Connection)
        {
            MessengerFriendshipDao.UpdateRelation(dbClient, type, session.User.Id, user);
        }

        session.User.Messenger.RelationChanged(user, type);
        session.User.Messenger.UpdateFriend(user, true);
    }
}
