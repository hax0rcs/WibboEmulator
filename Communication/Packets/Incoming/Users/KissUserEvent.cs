namespace WibboEmulator.Communication.Packets.Incoming.Users;

using Games.GameClients;
using Games.Rooms;

public class KissUserEvent : IPacketEvent
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

        if (session.User.Kisses <= 0)
        {
            return;
        }

        var roomUserByUserIdTarget = room.RoomUserManager.GetRoomUserByUserId(packet.PopInt());
        if (roomUserByUserIdTarget == null || roomUserByUserIdTarget.Client == null || roomUserByUserIdTarget.Client.User.Id == session.User.Id || roomUserByUserIdTarget.IsBot)
        {
            return;
        }

        session.User.Kisses--;
        roomUserByUserIdTarget.Client.User.RecievedKisses++;
        var roomUserByUserId = room.RoomUserManager.GetRoomUserByUserId(session.User.Id);
        roomUserByUserId.OnChat($"Beijando {roomUserByUserIdTarget.Username}");
        roomUserByUserIdTarget.OnChat($"Recebo um beijo de {roomUserByUserId.Username}");
    }
}
