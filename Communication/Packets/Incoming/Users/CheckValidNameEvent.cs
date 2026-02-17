namespace WibboEmulator.Communication.Packets.Incoming.Users;

using Games.GameClients;
using Outgoing.Users;

internal sealed class CheckValidNameEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (session.User == null || session == null)
        {
            return;
        }

        var name = packet.PopString(16);

        session.SendPacket(new NameChangeUpdateComposer(name));
    }
}
