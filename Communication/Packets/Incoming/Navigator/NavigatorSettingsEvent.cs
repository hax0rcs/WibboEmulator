namespace WibboEmulator.Communication.Packets.Incoming.Navigator;

using Games.GameClients;
using Outgoing.Navigator;

internal sealed class NavigatorSettingsEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet) => session.SendPacket(new NavigatorSettingsComposer(0, 0, 0, 0, false, 0));
}
