namespace WibboEmulator.Games.Chats.Commands.User.Build;

using Core.Language;
using GameClients;
using Rooms;

internal sealed class UseStop : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        session.User.ForceUse = -1;

        session.SendWhisper(LanguageManager.TryGetValue("cmd.use.disabled", session.Language));
    }
}
