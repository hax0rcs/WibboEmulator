namespace WibboEmulator.Communication.Packets.Incoming.Moderation;

using Games.GameClients;
using Games.Moderations;

internal sealed class ReleaseTicketEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (!session.User.HasPermission("mod"))
        {
            return;
        }

        var num = packet.PopInt();
        for (var index = 0; index < num; ++index)
        {
            ModerationManager.ReleaseTicket(session, packet.PopInt());
        }
    }
}
