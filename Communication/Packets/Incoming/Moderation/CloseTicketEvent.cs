namespace WibboEmulator.Communication.Packets.Incoming.Moderation;

using Games.GameClients;
using Games.Moderations;

internal sealed class CloseTicketEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (session == null || session.User == null || !session.User.HasPermission("mod"))
        {
            return;
        }

        var result = packet.PopInt();
        _ = packet.PopInt();
        var ticketId = packet.PopInt();

        ModerationManager.CloseTicket(session, ticketId, result);
    }
}
