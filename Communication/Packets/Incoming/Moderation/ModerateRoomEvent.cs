namespace WibboEmulator.Communication.Packets.Incoming.Moderation;

using Games.GameClients;
using Games.Moderations;

internal sealed class ModerateRoomEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (!session.User.HasPermission("mod"))
        {
            return;
        }

        var roomId = packet.PopInt();
        var lockRoom = packet.PopInt() == 1;
        var inappropriateRoom = packet.PopInt() == 1;
        var kickUsers = packet.PopInt() == 1;

        ModerationManager.PerformRoomAction(session, roomId, kickUsers, lockRoom, inappropriateRoom);
    }
}
