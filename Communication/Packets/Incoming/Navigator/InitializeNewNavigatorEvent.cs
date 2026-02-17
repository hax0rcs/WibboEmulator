namespace WibboEmulator.Communication.Packets.Incoming.Navigator;

using Games.GameClients;
using Games.Navigators;
using Outgoing.Navigator.New;
using Utilities;

internal sealed class InitializeNewNavigatorEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        var topLevelItems = NavigatorManager.TopLevelItems;

        var packetList = new ServerPacketList();
        packetList.Add(new NavigatorMetaDataParserComposer(topLevelItems));
        packetList.Add(new NavigatorLiftedRoomsComposer());
        packetList.Add(new NavigatorCollapsedCategoriesComposer());

        session.SendPacket(packetList);
    }
}
