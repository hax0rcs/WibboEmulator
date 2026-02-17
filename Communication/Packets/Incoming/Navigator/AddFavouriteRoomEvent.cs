namespace WibboEmulator.Communication.Packets.Incoming.Navigator;

using Database;
using Database.Daos.User;
using Games.GameClients;
using Games.Rooms;
using Outgoing.Navigator;

internal sealed class AddFavouriteRoomEvent : IPacketEvent
{
    public double Delay => 250;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (session.User == null)
        {
            return;
        }

        var roomId = packet.PopInt();

        var roomData = RoomManager.GenerateRoomData(roomId);
        if (roomData == null || session.User.FavoriteRooms.Count >= 30 || session.User.FavoriteRooms.Contains(roomId))
        {
            return;
        }
        else
        {
            session.SendPacket(new UpdateFavouriteRoomComposer(roomId, true));

            session.User.FavoriteRooms.Add(roomId);

            using var dbClient = DatabaseManager.Connection;
            UserFavoriteDao.Insert(dbClient, session.User.Id, roomId);
        }
    }
}
