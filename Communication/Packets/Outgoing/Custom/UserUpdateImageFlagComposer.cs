namespace WibboEmulator.Communication.Packets.Outgoing.Custom;

internal sealed class UserUpdateImageFlagComposer : ServerPacket
{
    public UserUpdateImageFlagComposer(int flag, string imageName, int userId) : base(ServerPacketHeader.UPDATE_USER_IMAGE_FLAG)
    {
        this.WriteInteger(flag);
        this.WriteString(imageName);
        this.WriteInteger(userId);
    }
}
