namespace WibboEmulator.Communication.Packets.Incoming.Inventory.Bots;

using Games.GameClients;
using Outgoing.Inventory.Bots;

internal sealed class GetBotInventoryEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (session.User == null)
        {
            return;
        }

        session.SendPacket(new BotInventoryComposer(session.User.InventoryComponent.Bots));
    }
}
