namespace WibboEmulator.Games.Chats.Commands.Staff.Administration;

using Effects;
using GameClients;
using Rooms;

internal sealed class RoomEnable : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (!int.TryParse(parameters[1], out var effectId))
        {
            return;
        }

        if (!EffectManager.HasEffect(effectId, session.User.HasPermission("god")))
        {
            return;
        }

        foreach (var user in room.RoomUserManager.UserList.ToList())
        {
            if (!user.IsBot)
            {
                user.ApplyEffect(effectId);
            }
        }
    }
}
