namespace WibboEmulator.Communication.Packets.Incoming.Rooms.Engine;

using Core.Language;
using Database;
using Games.GameClients;
using Games.Quests;
using Games.Rooms;
using Outgoing.BuildersClub;

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

        if (!room.CheckRights(session, true))
        {
            return;
        }

        var item = room.RoomItemHandling.GetItem(itemId);
        if (item == null)
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

        if (!item.IsBuilderClub)
        {
            session.User.InventoryComponent.AddItem(dbClient, item);
        }
        else
        {
            session.User.BCItemsUsed--;
            session.SendPacket(new BCBorrowedItemsComposer(session.User.BCItemsUsed));
        }

        QuestManager.ProgressUserQuest(session, QuestType.FurniPick, 0);
    }
}
