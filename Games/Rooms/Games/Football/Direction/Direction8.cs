namespace WibboEmulator.Games.Rooms.Games.Football.Direction;

//cappostrike
public class Direction8
{
    public static readonly Direction8[] DIRECTIONS = new Direction8[8];

    public static readonly Direction8 N = new(0, "N", 0, -1);
    public static readonly Direction8 NE = new(1, "NE", 1, -1);
    public static readonly Direction8 E = new(2, "E", 1, 0);
    public static readonly Direction8 SE = new(3, "SE", 1, 1);
    public static readonly Direction8 S = new(4, "S", 0, 1);
    public static readonly Direction8 SW = new(5, "SW", -1, 1);
    public static readonly Direction8 W = new(6, "W", -1, 0);
    public static readonly Direction8 NW = new(7, "NW", -1, -1);

    private readonly int _rot;
    private readonly int _xDiff;
    private readonly int _yDiff;
    private readonly string _rotName;

    public Direction8(int r, string rName, int diffx, int diffy)
    {
        this._rot = r;
        this._rotName = rName;
        this._xDiff = diffx;
        this._yDiff = diffy;
        DIRECTIONS[r] = this;
    }

    public static Direction8 GetDirection(int dir)
    {
        if (dir < 0 || dir > 7)
        {
            return N;
        }

        return DIRECTIONS[dir];
    }

    public static int ValidateDirection8Value(int dir) => dir & 0b111;

    public static Direction8 GetRot(int curX, int curY, int targetX, int targetY)
    {
        var deltaX = targetX - curX;
        var deltaY = targetY - curY;

        if (deltaX == 0)
        {
            if (deltaY < 0)
            {
                return N;
            }

            if (deltaY > 0)
            {
                return S;
            }
        }

        if (deltaX > 0)
        {
            if (deltaY < 0)
            {
                return NE;
            }

            if (deltaY == 0)
            {
                return E;
            }

            if (deltaY > 0)
            {
                return SE;
            }
        }

        if (deltaX < 0)
        {
            if (deltaY < 0)
            {
                return NW;
            }

            if (deltaY == 0)
            {
                return W;
            }

            if (deltaY > 0)
            {
                return SW;
            }
        }

        Console.WriteLine("ERROR: Direction8.GetRot == NULL");

        return null;
    }

    public int GetRot() => this._rot;

    public Direction8 RotateDirection180Degrees() => this.GetDirectionAtRot(4);

    public Direction8 RotateDirection45Degrees(bool clockwise) => this.GetDirectionAtRot(clockwise ? 1 : -1);

    public Direction8 RotateDirection90Degrees(bool clockwise) => this.GetDirectionAtRot(clockwise ? 2 : -2);

    public bool IsEven() => this._rot % 2 == 0;

    public int GetDirectionValue() => this._rot;

    public Direction8 GetDirectionAtRot(int diff) => DIRECTIONS[ValidateDirection8Value(this._rot + diff)];

    public override string ToString() => $"{this._rotName}({this._rot})";

    public string GetRotName() => this._rotName;

    public int GetDiffX() => this._xDiff;

    public int GetDiffY() => this._yDiff;

    public static bool HaveDirection(Direction8 find, params Direction8[] directions) => directions.Any(val => find == val);
}
