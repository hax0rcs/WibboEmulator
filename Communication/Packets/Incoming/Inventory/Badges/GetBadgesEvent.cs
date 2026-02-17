namespace WibboEmulator.Communication.Packets.Incoming.Inventory.Badges;

using Games.GameClients;
using Outgoing.Inventory.Badges;

internal sealed class GetBadgesEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (session == null || session.User == null)
        {
            return;
        }

        session.SendPacket(new BadgesComposer(session.User.BadgeComponent.BadgeList));
    }
}
