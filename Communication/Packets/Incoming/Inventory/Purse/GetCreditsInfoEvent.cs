namespace WibboEmulator.Communication.Packets.Incoming.Inventory.Purse;

using Games.GameClients;
using Outgoing.Inventory.Purse;

internal sealed class GetCreditsInfoEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        session.SendPacket(new CreditBalanceComposer(session.User.Credits));
        session.SendPacket(new ActivityPointsComposer(session.User.Duckets, session.User.WibboPoints));
    }
}
