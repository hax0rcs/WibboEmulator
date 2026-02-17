namespace WibboEmulator.Games.Chats.Commands.User.Build;

using GameClients;
using Rooms;

public class TileMove : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser roomUser, string[] parameters)
    {
        if (session == null)
        {
            return;
        }

        if (roomUser.Room == null)
        {
            return;
        }

        if (!roomUser.Room.CheckRights(session, true))
        {
            session.SendWhisper("Apenas o proprietário do quarto pode usar este comando!");
            return;
        }

        if (roomUser.TileCopy)
        {
            session.SendWhisper("Desative o Tile Copy para usar este comando!");
            return;
        }

        roomUser.TileMove = !roomUser.TileMove;
        var mode = roomUser.TileMove ? "Ativado" : "Desativado";
        if (mode.Contains("Desativado"))
        {
            roomUser.ItemsMove = [];
        }

        roomUser.SendWhisperChat($"O Movimentador de Pilhas foi {mode}!");
    }
}
