namespace WibboEmulator.Games.Chats.Commands.User.Several;

using Core.Language;
using GameClients;
using Rooms;
using Rooms.Games.Teams;

internal sealed class MoonWalk : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (userRoom.Team != TeamType.None || userRoom.InGame || room.IsGameMode)
        {
            return;
        }

        userRoom.MoonwalkEnabled = !userRoom.MoonwalkEnabled;
        if (userRoom.MoonwalkEnabled)
        {
            session.SendWhisper(LanguageManager.TryGetValue("cmd.moonwalk.true", session.Language));
        }
        else
        {
            session.SendWhisper(LanguageManager.TryGetValue("cmd.moonwalk.false", session.Language));
        }
    }
}
