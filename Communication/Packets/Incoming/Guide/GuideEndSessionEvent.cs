namespace WibboEmulator.Communication.Packets.Incoming.Guide;

using Games.GameClients;
using Games.Helps;
using Outgoing.Help;

internal sealed class GuideEndSessionEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        var requester = GameClientManager.GetClientByUserId(session.User.GuideOtherUserId);

        session.SendPacket(new OnGuideSessionEndedComposer(1));

        session.User.GuideOtherUserId = 0;
        if (session.User.OnDuty)
        {
            HelpManager.MarkAsOffDuty(session.User.Id);
        }

        if (requester != null)
        {
            requester.SendPacket(new OnGuideSessionEndedComposer(1));
            requester.User.GuideOtherUserId = 0;

            if (requester.User.OnDuty)
            {
                HelpManager.MarkAsOffDuty(requester.User.Id);
            }
        }
    }
}
