namespace WibboEmulator.Communication.Packets.Outgoing.Rooms.Engine;

using Games.Rooms;
using Games.Rooms.Map;

internal sealed class HeightMapComposer : ServerPacket
{
    public HeightMapComposer(RoomModelDynamic map, double height = 0.0)
        : base(ServerPacketHeader.ROOM_HEIGHT_MAP)
    {
        this.WriteInteger(map.MapSizeX);
        this.WriteInteger(map.MapSizeX * map.MapSizeY);
        for (var i = 0; i < map.MapSizeY; i++)
        {
            for (var j = 0; j < map.MapSizeX; j++)
            {
                if (map.SqState[j, i] == SquareStateType.Bloked)
                {
                    this.WriteShort(-1);
                }
                else
                {
                    this.WriteShort((int)Math.Floor((map.SqFloorHeight[j, i] + height) * 256.0));
                }
            }
        }
    }

    public static ServerPacket WriteFlowHeightMap(Room room, double height)
    {
        var heightMapComposer = new ServerPacket(ServerPacketHeader.ROOM_HEIGHT_MAP);

        heightMapComposer.WriteInteger(room.GameMap.Model.MapSizeX);
        heightMapComposer.WriteInteger(room.GameMap.Model.MapSizeX * room.GameMap.Model.MapSizeY);
        for (var i = 0; i < room.GameMap.Model.MapSizeY; i++)
        {
            for (var j = 0; j < room.GameMap.Model.MapSizeX; j++)
            {
                if (room.GameMap.Model.SqState[j, i] == SquareStateType.Bloked)
                {
                    heightMapComposer.WriteShort(-1);
                }
                else
                {
                    heightMapComposer.WriteShort((int)Math.Floor((room.GameMap.SqAbsoluteHeight(j, i) + height) * 256.0));
                }
            }
        }

        return heightMapComposer;
    }
}
