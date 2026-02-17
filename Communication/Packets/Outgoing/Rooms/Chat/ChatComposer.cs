namespace WibboEmulator.Communication.Packets.Outgoing.Rooms.Chat;

using Games.Chats.Emotions;

internal sealed class ChatComposer : ServerPacket
{
    public ChatComposer(int virtualId, string message, int chatIcon, int color)
        : base(ServerPacketHeader.UNIT_CHAT)
    {
        this.WriteInteger(virtualId);
        this.WriteString(message);
        this.WriteInteger(chatIcon);
        this.WriteInteger(ChatEmotionsManager.GetEmotionsForText(message));
        this.WriteInteger(color);
        this.WriteInteger(0);
        this.WriteInteger(-1);
    }
}
