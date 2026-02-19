namespace WibboEmulator.Communication.Packets.Outgoing.Rooms.Engine;

using Games.Items;
using Games.Rooms;

internal sealed class ItemWallComposer : ServerPacket
{
    public ItemWallComposer(Item[] items, Room room)
        : base(ServerPacketHeader.ITEM_WALL)
    {
        this.WriteInteger(room.RoomItemHandling.OwnersItems.Count);//total Owners

        foreach (var ownerItem in room.RoomItemHandling.OwnersItems)
        {
            this.WriteInteger(ownerItem.Key);
            this.WriteString(ownerItem.Value);
        }

        this.WriteInteger(items.Length);

        foreach (var item in items)
        {
            this.WriteWallItem(item, room.RoomData.OwnerId);
        }
    }

    private void WriteWallItem(Item item, int userId)
    {
        this.WriteString(item.Id.ToString());
        this.WriteInteger(item.ItemData.SpriteId);
        this.WriteString(item.WallCoord ?? string.Empty);

        ItemBehaviourUtility.GenerateWallExtradata(item, this);

        this.WriteInteger(-1);
        this.WriteInteger((item.ItemData.Modes > 1) ? 1 : 0);
        this.WriteInteger(userId);
    }
}
