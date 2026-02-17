namespace WibboEmulator.Games.Chats.Commands.User.Several;

using Core.Language;
using GameClients;
using Rooms;

internal sealed class DisableFollow : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (session.User.HideInRoom)
        {
            session.User.HideInRoom = false;
            session.SendWhisper(LanguageManager.TryGetValue("cmd.followme.true", session.Language));
        }
        else
        {
            session.User.HideInRoom = true;
            session.SendWhisper(LanguageManager.TryGetValue("cmd.followme.false", session.Language));
        }
    }
}
