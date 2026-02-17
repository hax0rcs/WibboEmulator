namespace WibboEmulator.Communication.Packets.Incoming.Messenger;

using Games.GameClients;
using Outgoing.Messenger;

internal sealed class GetBuddyRequestsEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet) => session.SendPacket(new BuddyRequestsComposer(session.User.Messenger.Requests));
}
