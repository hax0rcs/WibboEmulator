namespace WibboEmulator.Communication.Packets.Incoming.Groups;

using Games.GameClients;
using Games.Groups;
using Games.Rooms;
using Games.Users;
using Outgoing.Groups;
using Outgoing.Rooms.Permissions;

internal sealed class TakeAdminRightsEvent : IPacketEvent
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

        group.TakeAdmin(userId);

        if (RoomManager.TryGetRoom(group.RoomId, out var room))
        {
            var userRoom = room.RoomUserManager.GetRoomUserByUserId(userId);
            if (userRoom != null)
            {
                if (userRoom.ContainStatus("flatctrl"))
                {
                    userRoom.RemoveStatus("flatctrl");
                }

                userRoom.UpdateNeeded = true;
                userRoom.Client?.SendPacket(new YouAreControllerComposer(0));
            }
        }

        session.SendPacket(new GroupMemberUpdatedComposer(groupId, user, 2));
    }
}
