namespace WibboEmulator.Communication.Packets.Incoming.Rooms.Settings;

using Games.GameClients;
using Outgoing.Rooms.Settings;

internal sealed class GetRoomRightsEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (!session.User.InRoom)
        {
            return;
        }

        var rooom = session.User.Room;
        if (rooom == null)
        {
            return;
        }

        if (!rooom.CheckRights(session))
        {
            return;
        }

        session.SendPacket(new RoomRightsListComposer(rooom));
    }
}
