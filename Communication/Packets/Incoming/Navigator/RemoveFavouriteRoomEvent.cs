namespace WibboEmulator.Communication.Packets.Incoming.Navigator;

using Database;
using Database.Daos.User;
using Games.GameClients;
using Outgoing.Navigator;

internal sealed class RemoveFavouriteRoomEvent : IPacketEvent
{
    public double Delay => 250;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (session.User == null)
        {
            return;
        }

        var roomId = packet.PopInt();

        if (!session.User.FavoriteRooms.Contains(roomId))
        {
            return;
        }

        _ = session.User.FavoriteRooms.Remove(roomId);

        session.SendPacket(new UpdateFavouriteRoomComposer(roomId, false));

        using var dbClient = DatabaseManager.Connection;
        UserFavoriteDao.Delete(dbClient, session.User.Id, roomId);
    }
}
