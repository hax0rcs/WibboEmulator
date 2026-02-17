namespace WibboEmulator.Games.Items;

using System.Drawing;

public class ItemCopy(int virtualId, int baseItemId, Point point, string extraData, double z, int rotation)
{
    public int VirtualId { get; set; } = virtualId;

    public int BaseItem { get; set; } = baseItemId;

    public Point Point { get; set; } = point;

    public int X { get; set; } = point.X;

    public int Y { get; set; } = point.Y;

    public string ExtraData { get; set; } = extraData;

    public double Z { get; set; } = z;

    public int Rotation { get; set; } = rotation;
}
