namespace WibboEmulator.Communication.Packets.Incoming.Rooms.Engine;

using Core.Language;
using Database;
using Games.GameClients;
using Games.Quests;
using Games.Rooms;
using Outgoing.BuildersClub;
using WibboEmulator.Database.Daos.Item;

internal sealed class PickupObjectEvent : IPacketEvent
{
    public double Delay => 100;

    public void Parse(GameClient session, ClientPacket packet)
    {
        _ = packet.PopInt();
        var itemId = packet.PopInt();

        if (!RoomManager.TryGetRoom(session.User.RoomId, out var room))
        {
            return;
        }

        var item = room.RoomItemHandling.GetItem(itemId);
        if (item == null)
        {
            return;
        }

        if (!room.CheckRights(session, true) && item.UserId != session.User.Id)
        {
            return;
        }


        if (room.RoomData.SellPrice > 0)
        {
            session.SendNotification(LanguageManager.TryGetValue("roomsell.error.7", session.Language));
            return;
        }

        using var dbClient = DatabaseManager.Connection;

        room.RoomItemHandling.RemoveFurniture(session, item.Id);

        if (item.IsBuilderClub)
        {
            session.User.BCItemsUsed--;
            room.RoomItemHandling.RemoveFurniture(session, item.Id, false);

            ItemDao.Delete(dbClient, item.Id);

            session.SendPacket(new BCBorrowedItemsComposer(session.User.BCItemsUsed));
        }
        else
        {
            var ownerItem = GameClientManager.GetClientByUserId(item.UserId);
            room.RoomItemHandling.RemoveFurniture(session, item.Id, true);

            if (ownerItem != null && ownerItem.User.Username != session.User.Username)
            {
                ownerItem.User.InventoryComponent.AddItem(dbClient, item);
            }
            else if (session.User.Id == item.UserId)
            {
                session.User.InventoryComponent.AddItem(dbClient, item);
            }
            else
            {
                ItemDao.UpdateRoomIdForItemIdAndUser(dbClient, item.Id, 0, item.UserId, item.Username);
            }

        }

        QuestManager.ProgressUserQuest(session, QuestType.FurniPick, 0);
    }
}
