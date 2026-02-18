namespace WibboEmulator.Communication.Packets.Incoming.Groups;

using Games.GameClients;
using Games.Groups;
using Games.Users;
using Outgoing.Groups;
using WibboEmulator.Communication.Packets.Outgoing.Messenger;

internal sealed class AcceptGroupMembershipEvent : IPacketEvent
{
    public double Delay => 100;

    public void Parse(GameClient session, ClientPacket packet)
    {
        var groupId = packet.PopInt();
        var userId = packet.PopInt();

        if (!GroupManager.TryGetGroup(groupId, out var group))
        {
            return;
        }

        if (session.User.Id != group.CreatorId && !group.IsAdmin(session.User.Id) && !session.User.HasPermission("delete_group_limit"))
        {
            return;
        }

        if (!group.HasRequest(userId))
        {
            return;
        }

        var user = UserManager.GetUserById(userId);
        if (user == null)
        {
            return;
        }

        if (group.HasChat && user.Client != null)
        {
            user.Client.SendPacket(FriendListUpdateComposer.WriteGroupChat(group, 1));
        }

        group.HandleRequest(userId, true);

        user.MyGroups.Add(group.Id);

        session.SendPacket(new GroupMemberUpdatedComposer(groupId, user, 4));
    }
}
