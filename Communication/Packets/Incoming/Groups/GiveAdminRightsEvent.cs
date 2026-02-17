namespace WibboEmulator.Communication.Packets.Incoming.Groups;

using Games.GameClients;
using Games.Groups;
using Games.Rooms;
using Games.Users;
using Outgoing.Groups;
using Outgoing.Rooms.Permissions;

internal sealed class GiveAdminRightsEvent : IPacketEvent
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

        if (session.User.Id != group.CreatorId || !group.IsMember(userId))
        {
            return;
        }

        var user = UserManager.GetUserById(userId);
        if (user == null)
        {
            return;
        }

        group.MakeAdmin(userId);

        if (RoomManager.TryGetRoom(group.RoomId, out var room))
        {
            var userRooom = room.RoomUserManager.GetRoomUserByUserId(userId);
            if (userRooom != null)
            {
                if (!userRooom.ContainStatus("flatctrl"))
                {
                    userRooom.SetStatus("flatctrl", "1");
                }

                userRooom.UpdateNeeded = true;

                userRooom.Client?.SendPacket(new YouAreControllerComposer(1));
            }
        }

        session.SendPacket(new GroupMemberUpdatedComposer(groupId, user, 1));
    }
}
