namespace WibboEmulator.Communication.Packets.Incoming.Catalog;

using Games.Catalogs;
using Games.GameClients;
using Outgoing.BuildersClub;
using Outgoing.Catalog;
using Utilities;

internal sealed class GetCatalogIndexEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        var mode = packet.PopString();
        var packetList = new ServerPacketList();

        if (mode == "NORMAL")
        {
            packetList.Add(new CatalogIndexComposer(session, CatalogManager.Pages, packet.PopString()));
        }

        if (mode == "BUILDERS_CLUB")
        {
            packetList.Add(new CatalogBCIndexComposer(session, CatalogManager.BCPages, packet.PopString()));
        }

        packetList.Add(new CatalogItemDiscountComposer());
        packetList.Add(new BCBorrowedItemsComposer(session.User.BCItemsUsed));

        session.SendPacket(packetList);
    }
}
