namespace WibboEmulator.Communication.Packets.Incoming.Navigator;

using Games.GameClients;
using Games.Rooms;
using Outgoing.Rooms.Session;

internal sealed class GoToHotelViewEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        session.SendPacket(new CloseConnectionComposer());
        session.User.LoadingRoomId = 0;

        if (session.User == null || !session.User.InRoom)
        {
            return;
        }

        if (!RoomManager.TryGetRoom(session.User.RoomId, out var room))
        {
            return;
        }

        room.RoomUserManager.RemoveUserFromRoom(session, false, false);
    }
}
