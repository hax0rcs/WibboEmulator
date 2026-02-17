namespace WibboEmulator.Communication.Packets;

using Games.GameClients;
using Incoming;

public interface IPacketEvent
{
    void Parse(GameClient session, ClientPacket packet);

    double Delay { get; }
}
