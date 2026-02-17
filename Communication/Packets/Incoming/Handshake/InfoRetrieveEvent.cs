namespace WibboEmulator.Communication.Packets.Incoming.Handshake;

using Games.GameClients;
using Outgoing.Handshake;

internal sealed class InfoRetrieveEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        session.SendPacket(new UserObjectComposer(session.User));
        session.SendPacket(new UserPerksComposer(session.User));
    }
}
