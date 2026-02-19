namespace WibboEmulator.Games.Chats.Commands.User.Several;

using System.Text.RegularExpressions;
using WibboEmulator.Games.GameClients;
using WibboEmulator.Games.Rooms;

internal class Colour : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser roomUser, string[] parameters)
    {
        if (session == null || roomUser == null || parameters == null)
        {
            return;
        }

        if (parameters.Length > 1 && parameters[1].Equals("stop", StringComparison.OrdinalIgnoreCase))
        {
            roomUser.ColourEnable = false;
            roomUser.SendWhisperChat("Modo de colorir foi desativado!", false);
            return;
        }

        if (parameters.Length < 3)
        {
            roomUser.SendWhisperChat("Use: :setcor #cor1 #cor2 (ex: :setcor #006FFF #7500FF). Não utilize código de cores resumidas, como #000 ou #FFF.", false);
            return;
        }

        roomUser.Colours = new string[2];

        for (var i = 0; i < 2; i++)
        {
            var colorInput = parameters[i + 1];

            if (IsHexColor(colorInput))
            {
                roomUser.Colours[i] = colorInput.TrimStart('#').ToUpper();
            }
            else
            {
                roomUser.Colours[i] = "FFFFFF";
            }
        }

        roomUser.ColourEnable = true;
        roomUser.SendWhisperChat($"Cores selecionadas: #{roomUser.Colours[0]} e #{roomUser.Colours[1]}", false);
        roomUser.SendWhisperChat("Clique duplo no mobi para pintar. Use ':setcor stop' para sair.", false);
    }

    public static bool IsHexColor(string hex) =>
        Regex.IsMatch(hex, @"^#?[0-9A-Fa-f]{6}$");
}
