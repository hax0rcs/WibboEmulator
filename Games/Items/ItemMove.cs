namespace WibboEmulator.Games.Items;

public class ItemMove(int virtualId, int baseItemId, int x, int y, double z, int rotation)
{
    public int VirtualId { get; set; } = virtualId;

    public int BaseItem { get; set; } = baseItemId;
    public int X { get; set; } = x;

    public int Y { get; set; } = y;
    public double Z { get; set; } = z;

    public int Rotation { get; set; } = rotation;
}
