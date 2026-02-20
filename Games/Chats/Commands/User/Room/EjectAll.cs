namespace ButterflyEmulator.HabboHotel.Chats.Commands.User.Room;

using WibboEmulator.Database;
using WibboEmulator.Database.Daos.Item;
using WibboEmulator.Games.Chats.Commands;
using WibboEmulator.Games.GameClients;
using WibboEmulator.Games.Rooms;

internal class EjectAll : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser roomUser, string[] parameters)
    {
        if (session == null)
        {
            return;
        }

        if (room == null)
        {
            return;
        }

        if (!room.CheckRights(session, true))
        {
            return;
        }

        var roomItems = room.RoomItemHandling.RemoveAllFurnitureToInventory(session, false, true);

        if (roomItems.Count <= 0)
        {
            return;
        }


        using var dbClient = DatabaseManager.Connection;
        foreach (var roomItem in roomItems)
        {
            GameClient itemOwner;

            if (roomItem == null)
            {
                continue;
            }

            itemOwner = GameClientManager.GetClientByUserId(roomItem.UserId);

            if (itemOwner == null)
            {
                ItemDao.UpdateRoomIdForItemIdAndUser(dbClient, roomItem.Id, roomItem.RoomId, roomItem.UserId, roomItem.Username);
            }
            else
            {
                itemOwner.User.InventoryComponent.AddItem(DatabaseManager.Connection, roomItem);
            }
        }
    }
}
