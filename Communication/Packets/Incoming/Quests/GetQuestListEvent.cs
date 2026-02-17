namespace WibboEmulator.Communication.Packets.Incoming.Quests;

using Games.GameClients;
using Games.Quests;

internal sealed class GetQuestListEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet) => QuestManager.SendQuestList(session, false);
}
