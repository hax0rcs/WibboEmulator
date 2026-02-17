namespace WibboEmulator.Games.Chats.Commands.Staff.Animation;

using Animations;
using GameClients;
using Rooms;

internal sealed class EventAlert : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (!AnimationManager.AllowAnimation)
        {
            return;
        }

        var str = CommandManager.MergeParams(parameters, 1);
        str = "<b>[ANIMATION]</b>\r\n" + str + "\r\n- " + session.User.Username;
        GameClientManager.SendSuperNotification("Animation des Staffs", str, "game_promo_small", "event:navigator/goto/" + userRoom.RoomId, "Rejoindre!");
    }
}
