namespace WibboEmulator.Communication.RCON.Commands.User;

using Core.Language;
using Games.GameClients;
using Games.Moderations;
using Packets.Outgoing.Moderation;

internal sealed class HaCommand : IRCONCommand
{
    public bool TryExecute(string[] parameters)
    {
        if (parameters.Length != 3)
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

        var message = parameters[2];

        ModerationManager.LogStaffEntry(client.User.Id, client.User.Username, 0, string.Empty, "ha", string.Format("WbTool ha: {0}", message));
        if (client.User.CheckChatMessage(message, "<alert>"))
        {
            return true;
        }

        GameClientManager.SendMessage(new BroadcastMessageAlertComposer(LanguageManager.TryGetValue("hotelallert.notice", client.Language) + "\r\n" + message + "\r\n- " + client.User.Username));
        return true;
    }
}
