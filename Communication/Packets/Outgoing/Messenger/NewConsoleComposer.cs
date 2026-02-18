namespace WibboEmulator.Communication.Packets.Outgoing.Messenger;

internal sealed class NewConsoleComposer : ServerPacket
{
    public NewConsoleComposer(int sender, string message, int time = 0)
        : base(ServerPacketHeader.MESSENGER_CHAT)
    {
        this.WriteInteger(sender);
        this.WriteString(message);
        this.WriteInteger(time);
    }

    public static ServerPacket WriteMessageGroupChat(int sender, string message, int time, string username, string look, int userId)
    {

        var newConsoleComposer = new ServerPacket(ServerPacketHeader.MESSENGER_CHAT);

        newConsoleComposer.WriteInteger(sender);
        newConsoleComposer.WriteString(message);
        newConsoleComposer.WriteInteger(time);

        newConsoleComposer.WriteString(username + "/" + look + "/" + userId);
        return newConsoleComposer;
    }
}
