namespace WibboEmulator.Communication.Packets.Outgoing.Rooms.Furni.MysteryBox;

public class MysteryBoxDataComposer : ServerPacket
{
    public MysteryBoxDataComposer(string boxColour, string keyColour) : base(ServerPacketHeader.MYSTERY_BOX_KEYS)
    {
        this.WriteString(boxColour);
        this.WriteString(keyColour);
    }
}
