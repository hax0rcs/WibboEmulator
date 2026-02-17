namespace WibboEmulator.Communication.Packets.Incoming.Quests;

using Games.GameClients;
using Games.Quests;

internal sealed class GetCurrentQuestEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet) => QuestManager.GetCurrentQuest(session);
}
