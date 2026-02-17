namespace WibboEmulator.Communication.Packets.Incoming.Rooms.Avatar;

using Games.GameClients;
using Games.Quests;
using Games.Rooms;
using Outgoing.Rooms.Avatar;

internal sealed class ActionEvent : IPacketEvent
{
    public double Delay => 250;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (!RoomManager.TryGetRoom(session.User.RoomId, out var room))
        {
            return;
        }

        var roomUserByUserId = room.RoomUserManager.GetRoomUserByUserId(session.User.Id);
        if (roomUserByUserId == null)
        {
            return;
        }

        roomUserByUserId.Unidle();
        var actionId = packet.PopInt();

        room.SendPacket(new ActionComposer(roomUserByUserId.VirtualId, actionId));

        if (actionId == 5)
        {
            roomUserByUserId.IsAsleep = true;
            room.SendPacket(new SleepComposer(roomUserByUserId.VirtualId, true));
        }

        room.RoomUserManager.UserAction(roomUserByUserId, actionId);
        QuestManager.ProgressUserQuest(session, QuestType.SocialWave, 0);
    }
}
