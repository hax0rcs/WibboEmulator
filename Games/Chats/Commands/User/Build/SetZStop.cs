namespace WibboEmulator.Games.Chats.Commands.User.Build;

using Core.Language;
using GameClients;
using Rooms;

internal sealed class SetZStop : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        userRoom.BuildToolStackHeight = false;
        session.SendPacket(room.GameMap.Model.SerializeRelativeHeightmap());

        session.SendWhisper(LanguageManager.TryGetValue("cmd.setz.disabled", session.Language));
    }
}
