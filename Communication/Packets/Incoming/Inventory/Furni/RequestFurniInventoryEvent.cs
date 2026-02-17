namespace WibboEmulator.Communication.Packets.Incoming.Inventory.Furni;

using Games.GameClients;
using Outgoing.Inventory.Furni;

internal sealed class RequestFurniInventoryEvent : IPacketEvent
{
    public double Delay => 5000;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (session.User == null)
        {
            return;
        }

        if (session.User.InventoryComponent == null)
        {
            return;
        }

        session.User.InventoryComponent.LoadInventory();

        var items = session.User.InventoryComponent.GetWallAndFloor;
        session.SendPacket(new FurniListComposer(items.ToList(), 1, 0));
    }
}
