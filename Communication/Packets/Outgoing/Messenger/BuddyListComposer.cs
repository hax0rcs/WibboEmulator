namespace WibboEmulator.Communication.Packets.Outgoing.Messenger;

using Games.Users.Messenger;
using WibboEmulator.Games.Groups;

internal sealed class BuddyListComposer : ServerPacket
{
    public BuddyListComposer(Dictionary<int, MessengerBuddy> friends, List<int> groupsIds)
        : base(ServerPacketHeader.MESSENGER_FRIENDS)
    {
        this.WriteInteger(1);
        this.WriteInteger(0);

        var groups = GroupManager.GetGroupsChatForUser(groupsIds);
        this.WriteInteger(friends.Count + groups.Count);

        foreach (var friend in friends.Values)
        {
            this.WriteInteger(friend.UserId);
            this.WriteString(friend.Username);
            this.WriteInteger(1);

            var isOnline = friend.IsOnline;
            this.WriteBoolean(isOnline);

            if (isOnline)
            {
                this.WriteBoolean(!friend.HideInRoom);
            }
            else
            {
                this.WriteBoolean(false);
            }

            this.WriteString(isOnline ? friend.Look : "");
            this.WriteInteger(0);
            this.WriteString(""); //Motto ?
            this.WriteString(string.Empty);
            this.WriteString(string.Empty);
            this.WriteBoolean(true); // Allows offline messaging
            this.WriteBoolean(false);
            this.WriteBoolean(false);
            this.WriteShort(friend.Relation);
        }

        foreach (var group in groups)
        {
            if (group == null || !group.HasChat)
            {
                continue;
            }

            this.WriteInteger(-group.Id);
            this.WriteString($"{group.Name} ({group.Id})");
            this.WriteInteger(1); // unknown
            this.WriteBoolean(true);
            this.WriteBoolean(false);
            this.WriteString(group == null ? "" : group.Badge);
            this.WriteInteger(0);
            this.WriteString(string.Empty);
            this.WriteString(string.Empty);
            this.WriteString(string.Empty);
            this.WriteBoolean(false);
            this.WriteBoolean(false);
            this.WriteBoolean(false);

            this.WriteShort(0);
        }
    }
}
