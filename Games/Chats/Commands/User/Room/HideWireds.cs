namespace WibboEmulator.Games.Chats.Commands.User.Room;

using Core.Language;
using GameClients;
using Rooms;
using WibboEmulator.Communication.Packets.Outgoing.Rooms.Engine;
using WibboEmulator.Games.Items.Wired;

internal sealed class HideWireds : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        room.RoomData.HideWireds = !room.RoomData.HideWireds;

        foreach (var roomItem in room.RoomItemHandling.FloorItems)
        {
            if (room.RoomData.HideWireds)
            {
                if (WiredUtillity.TypeIsWired(roomItem.ItemData.InteractionType) || WiredUtillity.AllowHideWiredType(roomItem.ItemData.InteractionType))
                {
                    room.SendPacket(new ObjectRemoveComposer(roomItem.Id, 0));
                }
                else
                {
                    room.SendPacket(new ObjectAddComposer(roomItem, roomItem.Username, roomItem.UserId));
                }
            }
            else
            {
                if (WiredUtillity.TypeIsWired(roomItem.ItemData.InteractionType) || WiredUtillity.AllowHideWiredType(roomItem.ItemData.InteractionType))
                {
                    room.SendPacket(new ObjectAddComposer(roomItem, roomItem.Username, roomItem.UserId));
                }
            }
        }

        room.GameMap.GenerateMaps();
        room.RoomUserManager.UpdateUserStatusses();

        session.SendWhisper(room.RoomData.HideWireds ? LanguageManager.TryGetValue("cmd.hidewireds.true", session.Language) : LanguageManager.TryGetValue("cmd.hidewireds.false", session.Language));
    }
}
