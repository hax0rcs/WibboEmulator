namespace WibboEmulator.Communication.Packets.Incoming.Catalog;

using Games.GameClients;
using Outgoing.Catalog;

internal sealed class GetMarketplaceConfigurationEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet) => session.SendPacket(new MarketplaceConfigurationComposer());
}
