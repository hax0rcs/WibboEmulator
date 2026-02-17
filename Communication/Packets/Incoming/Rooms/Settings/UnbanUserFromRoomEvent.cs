namespace WibboEmulator.Communication.Packets.Incoming.Rooms.Settings;

using Games.GameClients;
using Outgoing.Rooms.Settings;

internal sealed class UnbanUserFromRoomEvent : IPacketEvent
{
    public double Delay => 250;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (!session.User.InRoom)
        {
            return;
        }

        var instance = session.User.Room;
        if (instance == null || !instance.CheckRights(session, true))
        {
            return;
        }

        var userId = packet.PopInt();
        var roomId = packet.PopInt();

        if (!instance.HasBanExpired(userId))
        {
            instance.RemoveBan(userId);

            session.SendPacket(new UnbanUserFromRoomComposer(roomId, userId));
        }
    }
}
