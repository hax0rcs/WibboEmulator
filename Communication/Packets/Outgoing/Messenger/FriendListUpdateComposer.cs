namespace WibboEmulator.Communication.Packets.Outgoing.Messenger;

using Games.Users.Messenger;
using WibboEmulator.Games.Groups;

internal sealed class FriendListUpdateComposer : ServerPacket
{
    public FriendListUpdateComposer(MessengerBuddy friend, int friendId = 0)
        : base(ServerPacketHeader.MESSENGER_UPDATE)
    {
        this.WriteInteger(0);
        this.WriteInteger(1);
        this.WriteInteger(friend != null ? 0 : -1);

        if (friend != null)
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
            this.WriteBoolean(false);
        }
        else
        {
            this.WriteInteger(friendId);
        }
    }

    public static ServerPacket WriteGroupChat(Group group, int action)
    {
        var friendListGroupComposer = new ServerPacket(ServerPacketHeader.MESSENGER_UPDATE);

        friendListGroupComposer.WriteInteger(0);
        friendListGroupComposer.WriteInteger(1);

        friendListGroupComposer.WriteInteger(action);

        if (group != null)
        {
            friendListGroupComposer.WriteInteger(-group.Id);//truque nitro =v
            friendListGroupComposer.WriteString($"{group.Name} ({group.Id})");
            friendListGroupComposer.WriteInteger(77); // unknown
            friendListGroupComposer.WriteBoolean(true);
            friendListGroupComposer.WriteBoolean(false);
            friendListGroupComposer.WriteString(group == null ? "" : group.Badge);
            friendListGroupComposer.WriteInteger(1);
            friendListGroupComposer.WriteString(string.Empty);
            friendListGroupComposer.WriteString(string.Empty);
            friendListGroupComposer.WriteString(string.Empty);
            friendListGroupComposer.WriteBoolean(false);
            friendListGroupComposer.WriteBoolean(false);
            friendListGroupComposer.WriteBoolean(false);
            friendListGroupComposer.WriteShort(0);
            friendListGroupComposer.WriteInteger(1);
        }

        return friendListGroupComposer;
    }
}
