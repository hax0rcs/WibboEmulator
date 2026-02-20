namespace WibboEmulator.Communication.Packets.Incoming.Rooms.Settings;

using Database;
using Database.Daos.Bot;
using Database.Daos.Item;
using Database.Daos.Room;
using Database.Daos.User;
using Games.GameClients;
using Games.Rooms;
using WibboEmulator.Core.Language;

internal sealed class DeleteRoomEvent : IPacketEvent
{
    public double Delay => 5000;

    public void Parse(GameClient session, ClientPacket packet)
    {
        var roomId = packet.PopInt();

        if (session == null || session.User == null || session.User.UsersRooms == null)
        {
            return;
        }

        if (!RoomManager.TryGetRoom(roomId, out var room))
        {
            return;
        }

        if (!(room.RoomData.OwnerName == session.User.Username))
        {
            return;
        }

        if (room.RoomData.Group != null)
        {
            session.SendHugeNotification(LanguageManager.TryGetValue("room.guild.delete.first", session.User.Langue));

        }

        var roomItems = room.RoomItemHandling.RemoveAllFurnitureToInventory(session, true);
        if (roomItems.Count <= 0)
        {
            return;
        }

        foreach (var roomItem in roomItems)
        {
            GameClient itemOwner;

            if (roomItem == null)
            {
                continue;
            }


            using var dbClient = DatabaseManager.Connection;

            itemOwner = GameClientManager.GetClientByUserId(roomItem.UserId);
            if (itemOwner == null)
            {
                ItemDao.UpdateRoomIdForItemIdAndUser(dbClient, roomItem.Id, roomId, roomItem.UserId, roomItem.Username);
            }
            else
            {
                if (itemOwner.User.Id == roomItem.UserId)
                {

                itemOwner.User.InventoryComponent.AddItem(dbClient, roomItem);
                }
            }
        }

        RoomManager.UnloadRoom(room);

        using (var dbClient = DatabaseManager.Connection)
        {
            RoomDao.Delete(dbClient, roomId);
            UserFavoriteDao.Delete(dbClient, roomId);
            RoomRightDao.Delete(dbClient, roomId);
            ItemDao.DeleteAllByRoomId(dbClient, roomId);
            UserDao.UpdateHomeRoom(dbClient, session.User.Id, 0);
            BotUserDao.UpdateRoomBot(dbClient, roomId);
            BotPetDao.UpdateRoomIdByRoomId(dbClient, roomId);
        }

        if (session.User.UsersRooms.Contains(roomId))
        {
            _ = session.User.UsersRooms.Remove(roomId);
        }

        if (session.User.FavoriteRooms != null)
        {
            if (session.User.FavoriteRooms.Contains(roomId))
            {
                _ = session.User.FavoriteRooms.Remove(roomId);
            }
        }
    }
}
