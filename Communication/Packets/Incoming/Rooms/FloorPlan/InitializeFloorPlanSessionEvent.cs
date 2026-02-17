namespace WibboEmulator.Communication.Packets.Incoming.Rooms.FloorPlan;

using Games.GameClients;
using Games.Rooms;
using Outgoing.Rooms.FloorPlan;

internal sealed class InitializeFloorPlanSessionEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (!RoomManager.TryGetRoom(session.User.RoomId, out var room))
        {
            return;
        }

        session.SendPacket(new FloorPlanSendDoorComposer(room.GameMap.Model.DoorX, room.GameMap.Model.DoorY, room.GameMap.Model.DoorOrientation));
    }
}
