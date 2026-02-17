namespace WibboEmulator.Communication.Packets.Incoming.Quests;

using Games.GameClients;
using Games.Quests;

internal sealed class StartQuestEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        var questId = packet.PopInt();

        QuestManager.ActivateQuest(session, questId);
    }
}
