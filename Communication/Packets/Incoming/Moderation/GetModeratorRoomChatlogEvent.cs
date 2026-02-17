namespace WibboEmulator.Communication.Packets.Incoming.Moderation;

using Games.Chats.Logs;
using Games.GameClients;
using Games.Rooms;
using Outgoing.Moderation;

internal sealed class GetModeratorRoomChatlogEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (!session.User.HasPermission("mod"))
        {
            return;
        }

        _ = packet.PopInt(); //useless
        var roomId = packet.PopInt();

        if (!RoomManager.TryGetRoom(roomId, out var room))
        {
            return;
        }

        var listReverse = new List<ChatlogEntry>();
        listReverse.AddRange(room.ChatlogManager.ListOfMessages);
        listReverse.Reverse();

        session.SendPacket(new ModeratorRoomChatlogComposer(room, listReverse));
    }
}
