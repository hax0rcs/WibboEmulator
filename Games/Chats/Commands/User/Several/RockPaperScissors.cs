namespace WibboEmulator.Games.Chats.Commands.User.Several;

using Core.Language;
using GameClients;
using Rooms;
using Rooms.Games.Teams;

internal sealed class RockPaperScissors : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (parameters.Length != 2)
        {
            return;
        }

        if (userRoom.Team != TeamType.None || userRoom.InGame || room.IsGameMode)
        {
            return;
        }

        if (session.User.IsSpectator)
        {
            return;
        }

        var username = parameters[1];

        if (string.IsNullOrWhiteSpace(username))
        {
            return;
        }

        var roomUserTarget = room.RoomUserManager.GetRoomUserByName(username);
        if (roomUserTarget == null)
        {
            session.SendWhisper(LanguageManager.TryGetValue("input.usernotfound", session.Language));
            return;
        }

        if (roomUserTarget.UserId == userRoom.UserId)
        {
            return;
        }

        room.JankenManager.Start(userRoom, roomUserTarget);
    }
}
