namespace WibboEmulator.Communication.Packets.Incoming.Guide;

using Games.GameClients;
using Outgoing.Help;

internal sealed class RecomendHelpersEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet) => session.SendPacket(new OnGuideSessionDetachedComposer());
}
