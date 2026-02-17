namespace WibboEmulator.Communication.Packets.Incoming.Catalog;

using Games.GameClients;
using Games.Groups;
using Outgoing.Catalog;

internal sealed class GetGroupFurniConfigEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet) => session.SendPacket(new GroupFurniConfigComposer(GroupManager.GetGroupsForUser(session.User.MyGroups)));
}
