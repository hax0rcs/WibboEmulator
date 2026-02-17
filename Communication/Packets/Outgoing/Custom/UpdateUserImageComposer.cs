namespace WibboEmulator.Communication.Packets.Outgoing.Custom;

internal sealed class UpdateUserImageComposer : ServerPacket
{
    public UpdateUserImageComposer(string imageKey, string imageFormat, string imageType = "", int flag = 1) : base(ServerPacketHeader.UPDATE_USER_IMAGE)
    {
        this.WriteString(imageKey);
        this.WriteString(imageType);
        this.WriteString(imageFormat);
        this.WriteInteger(flag);
    }
}
