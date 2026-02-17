namespace WibboEmulator.Communication.Packets.Incoming.Rooms.FloorPlan;

using Games.GameClients;
using Games.Rooms;
using Outgoing.Rooms.FloorPlan;

internal sealed class GetOccupiedTilesEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (!RoomManager.TryGetRoom(session.User.RoomId, out var room))
        {
            return;
        }

        session.SendPacket(new FloorPlanFloorMapComposer(room.GameMap.CoordinatedItems));
    }
}
