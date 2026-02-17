namespace WibboEmulator.Communication.Packets.Incoming.Rooms.Settings;

using Games.GameClients;
using Games.Rooms;
using Outgoing.Rooms.Settings;

internal sealed class GetRoomSettingsEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        var roomId = packet.PopInt();

        if (!RoomManager.TryGetRoom(roomId, out var room))
        {
            return;
        }

        if (!room.CheckRights(session, true) && !session.User.HasPermission("settings_room"))
        {
            return;
        }

        session.SendPacket(new RoomSettingsDataComposer(room.RoomData));
    }
}
