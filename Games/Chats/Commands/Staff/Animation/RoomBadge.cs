namespace WibboEmulator.Games.Chats.Commands.Staff.Animation;

using Database;
using Database.Daos.User;
using GameClients;
using Rooms;

internal sealed class RoomBadge : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (parameters.Length != 2)
        {
            return;
        }

        var badgeId = parameters[1];

        var userIds = new List<int>();

        foreach (var user in room.RoomUserManager.UserList.ToList())
        {
            if (!user.IsBot && user.Client != null && user.Client.User != null && user.Client.User.BadgeComponent != null)
            {
                user.Client.User.BadgeComponent.GiveBadge(badgeId, false);

                userIds.Add(user.Client.User.Id);
            }
        }

        using var dbClient = DatabaseManager.Connection;
        UserBadgeDao.InsertAll(dbClient, userIds, badgeId);
    }
}
