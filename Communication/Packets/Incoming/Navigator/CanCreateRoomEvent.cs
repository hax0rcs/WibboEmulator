namespace WibboEmulator.Communication.Packets.Incoming.Navigator;

using Games.GameClients;
using Outgoing.Navigator;

internal sealed class CanCreateRoomEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet) => session.SendPacket(new CanCreateRoomComposer(false, 400));
}
