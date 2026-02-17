namespace WibboEmulator.Communication.Packets.Incoming.Rooms.Connection;

using Games.GameClients;
using Outgoing.Rooms.Session;

internal sealed class GoToFlatEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (!session.User.InRoom)
        {
            return;
        }

        var room = session.User.Room;

        if (room == null)
        {
            return;
        }

        if (!session.User.EnterRoom(room))
        {
            session.SendPacket(new CloseConnectionComposer());
        }
    }
}
