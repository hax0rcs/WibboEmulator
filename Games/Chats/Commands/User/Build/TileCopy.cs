namespace WibboEmulator.Games.Chats.Commands.User.Build;

using GameClients;
using Rooms;

public class TileCopy : IChatCommand
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

        if (!session.User.InventoryLoaded)
        {
            roomUser.SendWhisperChat("Abra o use inventário para carregar suas mobílias e feche-o!");
            return;
        }

        if (roomUser.TileMove)
        {
            session.SendWhisper("Desative o Tile Move para usar este comando!");
            return;
        }

        roomUser.TileCopy = !roomUser.TileCopy;
        var mode = roomUser.TileCopy ? "Ativado" : "Desativado";
        if (mode.Contains("Desativado"))
        {
            roomUser.ItemsCopy = [];
        }

        roomUser.SendWhisperChat($"O Copiador de Pilhas foi {mode}!");
    }
}
