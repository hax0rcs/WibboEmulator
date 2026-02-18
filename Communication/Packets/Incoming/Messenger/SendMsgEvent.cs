namespace WibboEmulator.Communication.Packets.Incoming.Messenger;

using Games.Chats.Filter;
using Games.GameClients;
using WibboEmulator.Games.Groups;

internal sealed class SendMsgEvent : IPacketEvent
{
    public double Delay => 100;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (session == null || session.User == null || session.User.Messenger == null)
        {
            return;
        }

        var userId = packet.PopInt();

        if (userId == session.User.Id)
        {
            return;
        }
        Group group = null;
        if (userId < 0)
        {
            group = GroupManager.GetGroupsChatForUser(Math.Abs(userId));
        }

        var message = WordFilterManager.CheckMessage(packet.PopString());
        if (string.IsNullOrWhiteSpace(message))
        {
            return;
        }

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

        if (session.User.CheckChatMessage("<" + userId + "> " + message, "<MP>"))
        {
            return;
        }

        if (session.User.IgnoreAll)
        {
            return;
        }

        if (group != null)
        {
            if (!group.IsMember(session.User.Id))
            {
                return;
            }

            if (!group.HasChat)
            {
                return;
            }

            session.User.Messenger.SendInstantGroupMessage(group.Id, group.GetAllMembers, message);
        }
        else
        {
            session.User.Messenger.SendInstantMessage(userId, message);
        }
    }
}
