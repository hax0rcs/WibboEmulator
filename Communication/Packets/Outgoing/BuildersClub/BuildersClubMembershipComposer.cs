namespace WibboEmulator.Communication.Packets.Outgoing.BuildersClub;

internal sealed class BuildersClubMembershipComposer : ServerPacket
{
    public BuildersClubMembershipComposer(int secondsLeft, int furnisLeft, int maxFurnis, int secondsGrace)
        : base(ServerPacketHeader.BUILDERS_CLUB_EXPIRED)
    {
        this.WriteInteger(secondsLeft);//secondes left
        this.WriteInteger(furnisLeft);//furnis left
        this.WriteInteger(maxFurnis);//furnis max
        this.WriteInteger(secondsGrace);//seconds left with grace?
    }
}
