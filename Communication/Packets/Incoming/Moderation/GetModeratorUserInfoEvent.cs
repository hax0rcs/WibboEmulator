namespace WibboEmulator.Communication.Packets.Incoming.Moderation;

using Core.Language;
using Games.GameClients;
using Games.Users;
using Outgoing.Moderation;

internal sealed class GetModeratorUserInfoEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (!session.User.HasPermission("mod"))
        {
            return;
        }

        var userId = packet.PopInt();

        var user = UserManager.GetUserById(userId);

        if (user == null)
        {
            session.SendNotification(LanguageManager.TryGetValue("user.loadusererror", session.Language));
            return;
        }

        session.SendPacket(new ModeratorUserInfoComposer(user));
    }
}
