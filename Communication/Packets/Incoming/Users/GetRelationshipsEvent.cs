namespace WibboEmulator.Communication.Packets.Incoming.Users;

using Games.GameClients;
using Games.Users;
using Outgoing.Users;

internal sealed class GetRelationshipsEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        var userId = packet.PopInt();

        var user = UserManager.GetUserById(userId);
        if (user == null)
        {
            return;
        }

        if (user.Messenger == null)
        {
            session.SendPacket(new GetRelationshipsComposer(user.Id, []));
            return;
        }

        session.SendPacket(new GetRelationshipsComposer(user.Id, user.Messenger.Relationships));
    }
}
