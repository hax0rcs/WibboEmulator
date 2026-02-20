namespace WibboEmulator.Communication.Packets.Incoming.Messenger;

using Games.GameClients;
using Games.Rooms;
using Outgoing.Rooms.Session;

internal sealed class FollowFriendEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        var userId = packet.PopInt();
        var clientByUserId = GameClientManager.GetClientByUserId(userId);
        if (clientByUserId == null || clientByUserId.User == null || !clientByUserId.User.InRoom || (clientByUserId.User.HideInRoom && !session.User.HasPermission("mod")))
        {
            return;
        }

        if (!RoomManager.TryGetRoom(clientByUserId.User.RoomId, out var room))
        {
            return;
        }

        session.SendPacket(new RoomForwardComposer(room.Id));
    }
}
