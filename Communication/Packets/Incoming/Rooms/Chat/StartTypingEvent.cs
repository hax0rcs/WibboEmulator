namespace WibboEmulator.Communication.Packets.Incoming.Rooms.Chat;

using Games.GameClients;
using Games.Rooms;
using Outgoing.Rooms.Chat;

internal sealed class StartTypingEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (!RoomManager.TryGetRoom(session.User.RoomId, out var room))
        {
            return;
        }

        var roomUserByUserId = room.RoomUserManager.GetRoomUserByUserId(session.User.Id);
        if (roomUserByUserId == null)
        {
            return;
        }

        room.SendPacket(new UserTypingComposer(roomUserByUserId.VirtualId, true));
    }
}
