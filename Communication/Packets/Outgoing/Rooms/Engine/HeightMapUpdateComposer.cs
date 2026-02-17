namespace WibboEmulator.Communication.Packets.Outgoing.Rooms.Engine;

using System.Drawing;
using WibboEmulator.Games.Rooms.Map;

internal sealed class HeightMapUpdateComposer : ServerPacket
{
    public HeightMapUpdateComposer(GameMap map, List<Point> position, double height = 0.0) : base(ServerPacketHeader.ROOM_HEIGHT_MAP_UPDATE)
    {
        var singlePoint = position.Count <= 1;
        if (singlePoint)
        {
            this.WriteByte(1);
            this.WriteByte((byte)position[0].X);
            this.WriteByte((byte)position[0].Y);
            this.WriteShort((int)Math.Floor((map.SqAbsoluteHeight(position[0].X, position[0].Y) + height) * 256.0));
            return;
        }

        this.WriteByte((byte)(singlePoint ? 1 : position.Count));

        foreach (var pos in position)
        {
            this.WriteByte((byte)pos.X);
            this.WriteByte((byte)pos.Y);
            this.WriteShort((int)Math.Floor((map.SqAbsoluteHeight(pos.X, pos.Y) + height) * 256.0));
        }
    }
}
