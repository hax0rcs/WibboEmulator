namespace WibboEmulator.Communication.Packets.Incoming.Marketplace;

using Database;
using Database.Daos.Catalog;
using Games.GameClients;
using Outgoing.MarketPlace;

internal sealed class GetMarketplaceItemStatsEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        var itemId = packet.PopInt();
        var spriteId = packet.PopInt();

        var avgprice = 0;
        using (var dbClient = DatabaseManager.Connection)
        {
            avgprice = CatalogMarketplaceDataDao.GetPriceBySprite(dbClient, spriteId);
        }

        session.SendPacket(new MarketplaceItemStatsComposer(itemId, spriteId, avgprice));
    }
}
