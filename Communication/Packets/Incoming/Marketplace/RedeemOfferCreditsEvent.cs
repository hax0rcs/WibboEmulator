namespace WibboEmulator.Communication.Packets.Incoming.Marketplace;

using Database;
using Database.Daos.Catalog;
using Database.Daos.User;
using Games.GameClients;
using Outgoing.Inventory.Purse;

internal sealed class RedeemOfferCreditsEvent : IPacketEvent
{
    public double Delay => 500;

    public void Parse(GameClient session, ClientPacket packet)
    {
        using var dbClient = DatabaseManager.Connection;

        var creditsOwed = CatalogMarketplaceOfferDao.GetSunPrice(dbClient, session.User.Id);

        if (creditsOwed >= 1)
        {
            CatalogMarketplaceOfferDao.Delete(dbClient, session.User.Id);
            UserDao.UpdateAddPoints(dbClient, session.User.Id, creditsOwed);

            session.User.WibboPoints += creditsOwed;
            session.SendPacket(new ActivityPointNotificationComposer(session.User.WibboPoints, 0, 105));
        }
    }
}
