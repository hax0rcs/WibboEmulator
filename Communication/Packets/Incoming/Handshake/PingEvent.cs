namespace WibboEmulator.Communication.Packets.Incoming.Handshake;

using Games.GameClients;

internal sealed class PingEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {

    }
}
