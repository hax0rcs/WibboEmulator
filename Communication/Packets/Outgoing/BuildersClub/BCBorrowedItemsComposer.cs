namespace WibboEmulator.Communication.Packets.Outgoing.BuildersClub;

internal sealed class BCBorrowedItemsComposer : ServerPacket
{
    public BCBorrowedItemsComposer(int furnisCount)
        : base(ServerPacketHeader.BUILDERS_CLUB_FURNI_COUNT) => this.WriteInteger(furnisCount);
}
