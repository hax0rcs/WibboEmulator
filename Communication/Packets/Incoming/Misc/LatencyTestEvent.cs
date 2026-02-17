namespace WibboEmulator.Communication.Packets.Incoming.Misc;

using Games.GameClients;
using Outgoing.Misc;

internal sealed class LatencyTestEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet) => session.SendPacket(new LatencyResponseComposer(packet.PopInt()));
}
