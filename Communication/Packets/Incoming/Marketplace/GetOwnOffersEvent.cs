namespace WibboEmulator.Communication.Packets.Incoming.Marketplace;

using Games.GameClients;
using Outgoing.MarketPlace;

internal sealed class GetOwnOffersEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet) => session.SendPacket(new MarketPlaceOwnOffersComposer(session.User.Id));
}
