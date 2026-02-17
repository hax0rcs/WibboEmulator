namespace WibboEmulator.Communication.Packets.Incoming.Rooms.Action;

using Games.GameClients;
using Games.Rooms;
using Outgoing.Rooms.Notifications;

internal sealed class WhisperGroupEvent : IPacketEvent
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

        var userId = packet.PopInt();

        var roomUserByUserTarget = room.RoomUserManager.GetRoomUserByUserId(userId);
        if (roomUserByUserTarget == null)
        {
            return;
        }

        if (!roomUserByUserId.WhisperGroupUsers.Contains(roomUserByUserTarget.Username))
        {
            if (roomUserByUserId.WhisperGroupUsers.Count >= 5)
            {
                roomUserByUserId.WhisperGroupUsers.RemoveAt(0);
            }

            roomUserByUserId.WhisperGroupUsers.Add(roomUserByUserTarget.Username);
            session.SendPacket(RoomNotificationComposer.SendBubble("whisper_group_add", $"{roomUserByUserTarget.Username} foi inserido ao seu Grupo de Sussurro!"));
        }
        else
        {
            _ = roomUserByUserId.WhisperGroupUsers.Remove(roomUserByUserTarget.Username);
            session.SendPacket(RoomNotificationComposer.SendBubble("whisper_group_add", $"{roomUserByUserTarget.Username} foi removido do seu Grupo de Sussurro!"));
        }
    }
}
