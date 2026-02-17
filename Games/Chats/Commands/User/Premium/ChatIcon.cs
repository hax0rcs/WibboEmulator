namespace WibboEmulator.Games.Chats.Commands.User.Premium;

using WibboEmulator.Core.Language;
using WibboEmulator.Database;
using WibboEmulator.Database.Daos.User;
using WibboEmulator.Games.GameClients;
using WibboEmulator.Games.Rooms;

internal class ChatIcon : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser roomUser, string[] parameters)
    {
        if (session == null || roomUser == null)
        {
            return;
        }

        if (!int.TryParse(CommandManager.MergeParams(parameters, 1), out var chatIcon))
        {
            roomUser.SendWhisperChat(LanguageManager.TryGetValue("chaticon.error.parsing", session.Language));
            return;
        }

        session.User.ChatIcon = chatIcon;

        using var dbClient = DatabaseManager.Connection;
        UserDao.UpdateChatIcon(dbClient, chatIcon, session.User.Id);
    }
}
