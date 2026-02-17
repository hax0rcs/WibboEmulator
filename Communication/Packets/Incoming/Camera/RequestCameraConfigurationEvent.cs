namespace WibboEmulator.Communication.Packets.Incoming.Camera;

using Games.GameClients;
using Outgoing.Camera;

internal sealed class RequestCameraConfigurationEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet) => session.SendPacket(new CameraPriceComposer(0, 0, 0));
}
