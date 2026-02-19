namespace WibboEmulator.Communication.Packets.Incoming.Rooms.Engine;

using Games.GameClients;
using Games.Quests;
using Games.Rooms;
using WibboEmulator.Games.Items;

internal sealed class UseFurnitureEvent : IPacketEvent
{
    public double Delay => 100;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (!RoomManager.TryGetRoom(session.User.RoomId, out var room))
        {
            return;
        }

        var id = packet.PopInt();

        var roomItem = room.RoomItemHandling.GetItem(id);
        if (roomItem == null)
        {
            return;
        }

        if (roomItem.ItemData.ItemName == "bw_lgchair")
        {
            QuestManager.ProgressUserQuest(session, QuestType.ExploreFindItem, 1936);
        }
        else if (roomItem.ItemData.ItemName.Contains("bw_sboard"))
        {
            QuestManager.ProgressUserQuest(session, QuestType.ExploreFindItem, 1969);
        }
        else if (roomItem.ItemData.ItemName.Contains("bw_van"))
        {
            QuestManager.ProgressUserQuest(session, QuestType.ExploreFindItem, 1956);
        }
        else if (roomItem.ItemData.ItemName.Contains("party_floor"))
        {
            QuestManager.ProgressUserQuest(session, QuestType.ExploreFindItem, 1369);
        }
        else if (roomItem.ItemData.ItemName.Contains("party_ball"))
        {
            QuestManager.ProgressUserQuest(session, QuestType.ExploreFindItem, 1375);
        }
        else if (roomItem.ItemData.ItemName.Contains("jukebox"))
        {
            QuestManager.ProgressUserQuest(session, QuestType.ExploreFindItem, 1019);
        }
        else if (roomItem.ItemData.ItemName.Contains("bb_gate"))
        {
            QuestManager.ProgressUserQuest(session, QuestType.ExploreFindItem, 2050);
        }
        else if (roomItem.ItemData.ItemName == "bb_patch1")
        {
            QuestManager.ProgressUserQuest(session, QuestType.ExploreFindItem, 2040);
        }
        else if (roomItem.ItemData.ItemName == "bb_rnd_tele")
        {
            QuestManager.ProgressUserQuest(session, QuestType.ExploreFindItem, 2049);
        }
        else if (roomItem.ItemData.ItemName.Contains("es_gate_"))
        {
            QuestManager.ProgressUserQuest(session, QuestType.ExploreFindItem, 2167);
        }
        else if (roomItem.ItemData.ItemName.Contains("es_score_"))
        {
            QuestManager.ProgressUserQuest(session, QuestType.ExploreFindItem, 2172);
        }
        else if (roomItem.ItemData.ItemName.Contains("es_exit"))
        {
            QuestManager.ProgressUserQuest(session, QuestType.ExploreFindItem, 2166);
        }
        else if (roomItem.ItemData.ItemName == "es_tagging")
        {
            QuestManager.ProgressUserQuest(session, QuestType.ExploreFindItem, 2148);
        }

        var roomUser = room.RoomUserManager.GetRoomUserByUserId(session.User.Id);

        if (roomUser.ColourEnable)
        {
            if (roomUser.Colours == null || roomUser.Colours.Length <= 1)
            {
                roomUser.SendWhisperChat("Houve um problema para pintar esta mobília. Utilize outra cor.", false);
                return;
            }

            if (!ItemBehaviourUtility.TypeIsColorable(roomItem.ItemData.ItemName))
            {
                roomUser.SendWhisperChat("Apenas mobílias da seção de coloríveis é possível definir suas cores.", false);
                return;
            }

            roomItem.Colour1 = roomUser.Colours[0];
            roomItem.Colour2 = roomUser.Colours[1];

            roomItem.UpdateState();
            return;
        }

        var request = packet.PopInt();

        roomItem.Interactor.OnTrigger(session, roomItem, request, room.CheckRights(session), false);
        roomItem.OnTrigger(room.RoomUserManager.GetRoomUserByUserId(session.User.Id));
    }
}
