namespace WibboEmulator.Communication.Packets.Incoming.Users;

using Database;
using Database.Daos.User;
using Games.GameClients;
using Games.Rooms;

public class UserGiveStarEvent : IPacketEvent
{
    public double Delay => 1000;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (session == null || session.User == null)
        {
            return;
        }

        if (!RoomManager.TryGetRoom(session.User.RoomId, out var room))
        {
            return;
        }

        if (session.User.Duckets <= 0)
        {
            session.SendWhisper("Você não tem Duckets suficientes! Consiga mais ficando online ou ganhando de outros usuários!");
            return;
        }

        var roomUserByUserIdTarget = room.RoomUserManager.GetRoomUserByUserId(packet.PopInt());
        if (roomUserByUserIdTarget == null || roomUserByUserIdTarget.Client == null || roomUserByUserIdTarget.Client.User.Id == session.User.Id || roomUserByUserIdTarget.IsBot)
        {
            return;
        }

        session.User.Duckets--;
        roomUserByUserIdTarget.Client.User.ReceivedDuckets++;
        UserStatsDao.UpdateReceivedDuckets(DatabaseManager.Connection, roomUserByUserIdTarget.UserId, 1);
        var roomUserByUserId = room.RoomUserManager.GetRoomUserByUserId(session.User.Id);
        roomUserByUserId.OnChat($"Dou-lhe +1 Ducket para {roomUserByUserIdTarget.Username}");
        roomUserByUserIdTarget.OnChat($"Recebo +1 um Ducket de {roomUserByUserId.Username}");
    }


}
