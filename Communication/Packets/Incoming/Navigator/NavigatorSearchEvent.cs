namespace WibboEmulator.Communication.Packets.Incoming.Navigator;

using Games.GameClients;
using Games.Navigators;
using Outgoing.Navigator.New;

internal sealed class NavigatorSearchEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        var category = packet.PopString();
        var search = packet.PopString();

        ICollection<SearchResultList> categories = [];

        if (!string.IsNullOrEmpty(search))
        {
            if (NavigatorManager.TryGetSearchResultList(0, out var queryResult))
            {
                categories.Add(queryResult);
            }
        }
        else
        {
            categories = NavigatorManager.GetCategorysForSearch(category);
            if (categories.Count == 0)
            {
                //Are we going in deep?!
                categories = NavigatorManager.GetResultByIdentifier(category);
                if (categories.Count > 0)
                {
                    session.SendPacket(new NavigatorSearchResultSetComposer(category, search, categories, session, 2, 50));
                    return;
                }
            }
        }

        session.SendPacket(new NavigatorSearchResultSetComposer(category, search, categories, session));
    }
}
