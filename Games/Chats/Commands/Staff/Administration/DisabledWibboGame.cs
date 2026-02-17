namespace WibboEmulator.Games.Chats.Commands.Staff.Administration;

using Animations;
using Core.Language;
using GameClients;
using Rooms;

internal sealed class DisabledAutoGame : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (!AnimationManager.ToggleForceDisabled)
        {
            session.SendWhisper(LanguageManager.TryGetValue("cmd.autogame.false", session.Language));
        }
        else
        {
            session.SendWhisper(LanguageManager.TryGetValue("cmd.autogame.true", session.Language));
        }
        return;
    }
}
