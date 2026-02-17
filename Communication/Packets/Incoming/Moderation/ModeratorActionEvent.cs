namespace WibboEmulator.Communication.Packets.Incoming.Moderation;

using Games.GameClients;
using Games.Moderations;
using Outgoing.Moderation;

internal sealed class ModeratorActionEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (!session.User.HasPermission("alert"))
        {
            return;
        }

        var alertMode = packet.PopInt();
        var alertMessage = packet.PopString();

        _ = alertMode != 3;

        if (session.User.CheckChatMessage(alertMessage, "<MT>"))
        {
            return;
        }

        ModerationManager.LogStaffEntry(session.User.Id, session.User.Username, 0, string.Empty, alertMessage.Split(' ')[0].Replace(":", ""), string.Format("Modtool Roomalert: {0}", alertMessage));

        session.User.Room.SendPacket(new BroadcastMessageAlertComposer(alertMessage));
    }
}
