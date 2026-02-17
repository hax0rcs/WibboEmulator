namespace WibboEmulator.Communication.Packets.Incoming.Messenger;

using Games.GameClients;
using Outgoing;
using Outgoing.Messenger;

internal sealed class SendRoomInviteEvent : IPacketEvent
{
    public double Delay => 1000;

    public void Parse(GameClient session, ClientPacket packet)
    {
        var timeSpan = DateTime.Now - session.User.FloodTime;
        if (timeSpan.Seconds > 4)
        {
            session.User.FloodCount = 0;
        }

        if (timeSpan.Seconds < 4 && session.User.FloodCount > 5 && session.User.Rank < 5)
        {
            return;
        }

        session.User.FloodTime = DateTime.Now;
        session.User.FloodCount++;

        var inviteCount = packet.PopInt();
        if (inviteCount > 100)
        {
            return;
        }

        var targets = new List<int>();
        for (var i = 0; i < inviteCount; ++i)
        {
            var id = packet.PopInt();
            if (i < 100)
            {
                targets.Add(id);
            }
        }

        var textMessage = packet.PopString(121);

        if (session.User.CheckChatMessage(textMessage, "<RM>"))
        {
            return;
        }

        if (session.User.IgnoreAll)
        {
            return;
        }


        ServerPacket roomInvitePacket = new RoomInviteComposer(session.User.Id, textMessage);

        foreach (var userId in targets)
        {
            if (session.User.Messenger.FriendshipExists(userId))
            {
                var clientByUserId = GameClientManager.GetClientByUserID(userId);
                if (clientByUserId == null || clientByUserId.User.IgnoreRoomInvites)
                {
                    break;
                }

                if (clientByUserId.User.Messenger.FriendshipExists(session.User.Id))
                {
                    clientByUserId.SendPacket(roomInvitePacket);
                }
            }
        }
    }
}
