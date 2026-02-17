namespace WibboEmulator.Communication.Packets.Incoming.Navigator;

using Games.GameClients;
using Games.Navigators;
using Outgoing.Navigator;

internal sealed class GetUserFlatCatsEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        var categories = NavigatorManager.FlatCategories;

        session.SendPacket(new UserFlatCatsComposer(categories, session.User.Rank));
    }
}
