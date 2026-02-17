namespace WibboEmulator.Communication.Packets.Incoming.Catalog;

using Games.Catalogs;
using Games.GameClients;
using Outgoing.Catalog;

internal sealed class GetCatalogPageEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        var pageId = packet.PopInt();

        var offerId = packet.PopInt();
        var cataMode = packet.PopString();


        if (cataMode == "NORMAL")
        {
            _ = CatalogManager.TryGetPage(pageId, out var page);

            if (page == null || !page.HavePermission(session.User))
            {
                return;
            }

            if (page.Template == "club_gifts")
            {
                session.SendPacket(new ClubGiftInfoComposer([.. page.Items.Values]));
            }

            session.SendPacket(new CatalogPageComposer(page, cataMode, session.Language, offerId));
        }

        if (cataMode == "BUILDERS_CLUB")
        {
            _ = CatalogManager.TryGetBCPage(pageId, out var page);

            if (page == null || !page.HavePermission(session.User))
            {
                return;
            }

            // need fix
            //if (page.Template == "club_gifts")
            //{
            //    session.SendPacket(new ClubGiftInfoComposer([.. page.Items.Values]));
            //}

            session.SendPacket(new CatalogBuildersPageComposer(page, cataMode, session.Language, offerId));
        }
    }
}
