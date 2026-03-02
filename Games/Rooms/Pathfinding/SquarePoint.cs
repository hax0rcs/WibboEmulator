namespace WibboEmulator.Games.Rooms.PathFinding;

using System.Drawing;
using WibboEmulator.Games.Items;

public readonly struct SquarePoint(RoomUser user, int x, int y, int targetX, int targetY, byte squareData, bool isOverride, bool allowWalkthrough, byte squareDataUser)
{
    public readonly bool _lastStep = x == targetX && y == targetY;

    public int X { get; } = x;

    public int Y { get; } = y;

    public double Distance { get; } = GetDistance(x, y, targetX, targetY);

    public bool CanWalk(double sqHeight)
    {
        foreach (var sqItem in user.Room.GameMap.GetCoordinatedItems(new Point(x, y)))
        {
            if (sqItem.ItemData.InteractionType == InteractionType.PILE_WALK)
            {
                return true;
            }
        }

        if (sqHeight > 4.0)
        {
            return false;
        }

        if (this._lastStep)
        {
            return isOverride || squareData == 3 || squareData == 1;
        }

        return isOverride || squareData == 1;
    }

    public bool AllowWalkthrough => allowWalkthrough || squareDataUser == 0;

    public static double GetDistance(int x1, int y1, int x2, int y2)
    {
        var dx = x1 - x2;
        var dy = y1 - y2;
        return (dx * dx) + (dy * dy);
    }
}
