namespace WibboEmulator.Games.Chats;

using System.Data;
using Commands;
using Filter;
using Pets.Commands;
using Styles;

public static class ChatManager
{
    public static void Initialize(IDbConnection dbClient)
    {
        PetCommandManager.Initialize(dbClient);
        ChatStyleManager.Initialize(dbClient);
        CommandManager.Initialize(dbClient);
        WordFilterManager.Initialize(dbClient);
    }
}
