namespace WibboEmulator.Communication.Packets.Incoming.Moderation;

using Games.GameClients;
using Games.Moderations;

internal sealed class PickTicketEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (!session.User.HasPermission("mod"))
        {
            return;
        }

        _ = packet.PopInt();
        ModerationManager.PickTicket(session, packet.PopInt());
    }
}
