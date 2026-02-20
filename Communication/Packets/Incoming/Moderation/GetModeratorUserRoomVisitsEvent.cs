namespace WibboEmulator.Communication.Packets.Incoming.Moderation;

using Games.GameClients;
using Outgoing.Moderation;

internal sealed class GetModeratorUserRoomVisitsEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (!session.User.HasPermission("mod"))
        {
            return;
        }

        var userId = packet.PopInt();

        var clientTarget = GameClientManager.GetClientByUserId(userId);

        if (clientTarget == null)
        {
            return;
        }

        session.SendPacket(new ModeratorUserRoomVisitsComposer(clientTarget.User, clientTarget.User.Visits));
    }
}
