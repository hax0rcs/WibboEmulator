namespace WibboEmulator.Games.Chats.Commands.Staff.Administration;

using Filter;
using GameClients;
using Rooms;

internal sealed class AddFilter : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (parameters.Length != 2)
        {
            return;
        }

        WordFilterManager.AddFilterPub(parameters[1].ToLower());
        session.SendWhisper("Le mot" + parameters[1] + " vient d'être ajouté au filtre des mots interdits.");
    }
}
