namespace WibboEmulator.Communication.Packets.Incoming.Messenger;

using Games.GameClients;
using Outgoing.Rooms.Session;

internal sealed class FindNewFriendsEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet) => session.SendPacket(new RoomForwardComposer(447654));
}
