namespace WibboEmulator.Communication.Packets.Incoming.Navigator;

using Database;
using Database.Daos.User;
using Games.GameClients;
using Games.Rooms;
using Outgoing.Navigator;

internal sealed class NavigatorHomeRoomEvent : IPacketEvent
{
    public double Delay => 250;

    public void Parse(GameClient session, ClientPacket packet)
    {
        var roomId = packet.PopInt();

        var roomData = RoomManager.GenerateRoomData(roomId);
        if (roomId != 0 && (roomData == null || !roomData.OwnerName.ToLower().Equals(session.User.Username, StringComparison.CurrentCultureIgnoreCase)))
        {
            return;
        }

        session.User.HomeRoom = roomId;
        using (var dbClient = DatabaseManager.Connection)
        {
            UserDao.UpdateHomeRoom(dbClient, session.User.Id, roomId);
        }

        session.SendPacket(new NavigatorHomeRoomComposer(roomId, 0));
    }
}
