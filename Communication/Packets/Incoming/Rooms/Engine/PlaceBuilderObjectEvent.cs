namespace WibboEmulator.Communication.Packets.Incoming.Rooms.Engine;

using Core.Language;
using Database;
using Database.Daos.Item;
using Database.Daos.User;
using Games.Catalogs;
using Games.Catalogs.Builders;
using Games.GameClients;
using Games.Items;
using Games.Rooms;
using Outgoing.BuildersClub;
using Outgoing.Rooms.Notifications;

public class PlaceBuilderObjectEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (session == null || session.User == null || !session.User.InRoom)
        {
            return;
        }

        if (!RoomManager.TryGetRoom(session.User.RoomId, out var room))
        {
            return;
        }

        if (!room.CheckRights(session, true))
        {
            session.SendPacket(new RoomNotificationComposer("furni_placement_error", "message", "${room.error.cant_set_not_owner}"));
            return;
        }

        if (room.RoomData.SellPrice > 0)
        {
            session.SendNotification(LanguageManager.TryGetValue("roomsell.error.7", session.Language));
            return;
        }

        if (session.User.BCItemsUsed == session.User.BCItemsMax)
        {
            session.SendPacket(RoomNotificationComposer.SendBubble("bc_max_atingido", "Você atingiu o Limite máximo de Raros emprestados!"));
            return;
        }

        var pageId = packet.PopInt();
        var itemId = packet.PopInt();

        var extraParam = packet.PopString();
        var x = packet.PopInt();
        var y = packet.PopInt();
        var rot = packet.PopInt();

        _ = CatalogManager.TryGetBCPage(pageId, out var page);

        if (page == null)
        {
            return;
        }

        if (!page.Enabled)
        {
            return;
        }

        CatalogBuildersItem item = null;
        var bcItem = page.GetItem(itemId);

        if (bcItem != null)
        {
            item = bcItem;
        }

        if (item == null)
        {
            return;
        }

        var newItem = ItemFactory.CreateSingleItemNullable(DatabaseManager.Connection, item.Data, session.User, extraParam, 0, 0, true);

        if (room.RoomItemHandling.SetFloorItem(session, newItem, x, y, rot, true, false, true))
        {
            ItemDao.UpdateRoomIdAndUserId(DatabaseManager.Connection, newItem.Id, room.Id, newItem.UserId, newItem.Username);
        }
        else
        {
            session.SendPacket(new RoomNotificationComposer("furni_placement_error", "message", "${room.error.cant_set_item}"));
            return;
        }

        session.User.BCItemsUsed++;
        UserStatsDao.UpdateBCItemsUsed(DatabaseManager.Connection, session.User.Id, session.User.BCItemsUsed);
        session.SendPacket(new BCBorrowedItemsComposer(session.User.BCItemsUsed));
    }

}
