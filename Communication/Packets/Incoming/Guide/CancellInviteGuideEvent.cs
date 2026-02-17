namespace WibboEmulator.Communication.Packets.Incoming.Guide;

using Games.GameClients;
using Outgoing.Help;

internal sealed class CancellInviteGuideEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        var requester = GameClientManager.GetClientByUserID(session.User.GuideOtherUserId);

        session.SendPacket(new OnGuideSessionDetachedComposer());

        requester?.SendPacket(new OnGuideSessionDetachedComposer());
    }
}
