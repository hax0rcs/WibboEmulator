namespace WibboEmulator.Communication.Packets.Incoming.Rooms.Furni.Moodlight;

using Games.GameClients;
using Games.Rooms;
using Outgoing.Rooms.Furni.Moodlight;

internal sealed class GetMoodlightConfigEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (!RoomManager.TryGetRoom(session.User.RoomId, out var room))
        {
            return;
        }

        if (!room.CheckRights(session, true))
        {
            return;
        }

        if (room.MoodlightData == null || room.MoodlightData.Presets == null)
        {
            return;
        }

        session.SendPacket(new MoodlightConfigComposer(room.MoodlightData));
    }
}
