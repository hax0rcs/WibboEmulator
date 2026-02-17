namespace WibboEmulator.Communication.Packets.Incoming.Rooms.Settings;

using Games.GameClients;
using Outgoing.Rooms.Settings;

internal sealed class GetRoomBannedUsersEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (!session.User.InRoom)
        {
            return;
        }

        var room = session.User.Room;
        if (room == null || !room.CheckRights(session, true))
        {
            return;
        }

        if (room.Bans.Count > 0)
        {
            session.SendPacket(new GetRoomBannedUsersComposer(room));
        }
    }
}
