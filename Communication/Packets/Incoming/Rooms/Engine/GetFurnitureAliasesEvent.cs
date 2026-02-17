namespace WibboEmulator.Communication.Packets.Incoming.Rooms.Engine;

using Games.GameClients;
using Outgoing.Rooms.Engine;

internal sealed class GetFurnitureAliasesMessageEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet) => session.SendPacket(new FurnitureAliasesComposer());
}
