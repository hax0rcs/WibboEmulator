namespace WibboEmulator.Communication.Packets.Incoming.LandingView;

using Games.GameClients;
using Games.LandingView;
using Outgoing.LandingView;

internal sealed class GetCommunityGoalHallOfFameEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet) => session.SendPacket(new CommunityGoalHallOfFameComposer(HallOfFameManager.UserRanking));
}
