namespace WibboEmulator.Communication.Packets.Incoming.Moderation;

using Games.GameClients;
using Outgoing.Help;

internal sealed class OpenHelpToolEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (session.User == null || session.User.HasPermission("helptool"))
        {
            return;
        }

        session.SendPacket(new OpenHelpToolComposer(0));
    }
}
