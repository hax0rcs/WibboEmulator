namespace WibboEmulator.Games.Chats.Commands.Staff.Administration;

using GameClients;
using Rooms;

public class BuildersClub : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser roomUser, string[] parameters)
    {
        if (session == null)
        { return; }

        if (roomUser == null)
        { return; }

        var action = parameters[1];

        var valueInt = int.TryParse(parameters[2], out var value);

        switch (action)
        {
        }
    }
}
