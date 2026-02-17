namespace WibboEmulator.Games.Chats.Commands.Staff.Gestion;

using Core.Settings;
using Database;
using Database.Daos.Bot;
using GameClients;
using Rooms;
using Rooms.AI;

internal sealed class ChatGPT : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (parameters.Length != 2)
        {
            return;
        }

        var bot = room.RoomUserManager.GetBotOrPetByName(parameters[1]);
        if (bot == null || bot.BotData.AiType == BotAIType.ChatGPT)
        {
            return;
        }

        bot.BotData.AiType = BotAIType.ChatGPT;
        bot.BotData.ChatText = SettingsManager.GetData<string>("openia.prompt");
        bot.BotData.LoadRandomSpeech(bot.BotData.ChatText);

        bot.BotAI = bot.BotData.GenerateBotAI(bot.VirtualId);
        bot.BotAI.Initialize(bot.BotData.Id, bot, room);

        var dbClient = DatabaseManager.Connection;
        BotUserDao.UpdateChatGPT(dbClient, bot.BotData.Id, bot.BotData.ChatText);

        userRoom.SendWhisperChat("ChatGPT vient d'être activé !");
    }
}
