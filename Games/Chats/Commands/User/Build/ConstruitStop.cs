namespace WibboEmulator.Games.Chats.Commands.User.Build;

using Core.Language;
using GameClients;
using Rooms;

internal sealed class ConstruitStop : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        userRoom.BuildToolEnable = false;

        session.SendWhisper(LanguageManager.TryGetValue("cmd.construit.disabled", session.Language));
    }
}
