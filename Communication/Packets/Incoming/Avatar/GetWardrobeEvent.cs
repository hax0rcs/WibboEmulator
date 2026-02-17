namespace WibboEmulator.Communication.Packets.Incoming.Avatar;

using Games.GameClients;
using Outgoing.Avatar;

internal sealed class GetWardrobeEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet) => session.SendPacket(new WardrobeComposer(session.User.WardrobeComponent.Wardrobes));
}
