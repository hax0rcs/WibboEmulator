namespace WibboEmulator.Games.Chats.Commands.User.Several;

using Core.Language;
using GameClients;
using Rooms;
using Rooms.Games.Teams;

internal sealed class FaceWalk : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (userRoom.Team != TeamType.None || userRoom.InGame || room.IsGameMode)
        {
            return;
        }

        userRoom.FacewalkEnabled = !userRoom.FacewalkEnabled;
        if (userRoom.FacewalkEnabled)
        {
            session.SendWhisper(LanguageManager.TryGetValue("cmd.facewalk.true", session.Language));
        }
        else
        {
            session.SendWhisper(LanguageManager.TryGetValue("cmd.facewalk.false", session.Language));
        }
    }
}
