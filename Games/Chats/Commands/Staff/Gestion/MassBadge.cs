namespace WibboEmulator.Games.Chats.Commands.Staff.Administration;

using Database;
using Database.Daos.User;
using GameClients;
using Rooms;

internal sealed class MassBadge : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (parameters.Length != 2)
        {
            return;
        }

        var badge = parameters[1];

        if (string.IsNullOrEmpty(badge))
        {
            return;
        }

        var userIds = new List<int>();

        foreach (var client in GameClientManager.Clients.ToList())
        {
            if (client.User != null)
            {
                client.User.BadgeComponent.GiveBadge(badge, false);
                client.SendNotification($"Vous venez de recevoir le badge : {badge} !");

                userIds.Add(client.User.Id);
            }
        }

        using var dbClient = DatabaseManager.Connection;
        UserBadgeDao.InsertAll(dbClient, userIds, badge);
    }
}
