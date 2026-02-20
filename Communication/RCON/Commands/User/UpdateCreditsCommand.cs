namespace WibboEmulator.Communication.RCON.Commands.User;

using Database;
using Database.Daos.User;
using Games.GameClients;
using Packets.Outgoing.Inventory.Purse;

internal sealed class UpdateCreditsCommand : IRCONCommand
{
    public bool TryExecute(string[] parameters)
    {
        if (parameters.Length != 2)
        {
            return false;
        }

        if (!int.TryParse(parameters[1], out var userId))
        {
            return false;
        }

        if (userId == 0)
        {
            return false;
        }

        var client = GameClientManager.GetClientByUserId(userId);
        if (client == null)
        {
            return true;
        }

        using var dbClient = DatabaseManager.Connection;
        var credits = UserDao.GetCredits(dbClient, client.User.Id);

        client.User.Credits = credits;
        client.SendPacket(new CreditBalanceComposer(client.User.Credits));

        return true;
    }
}
