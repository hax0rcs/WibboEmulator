namespace WibboEmulator.Communication.Packets.Incoming.BuildersClub;

using Games.GameClients;
using Outgoing.BuildersClub;

public class BuildersClubQueryFurniCountEvent : IPacketEvent
{
    public double Delay { get; }

    public void Parse(GameClient session, ClientPacket packet) => session.SendPacket(new BCBorrowedItemsComposer(session.User.BCItemsUsed));

}
