namespace WibboEmulator.Games.Chats.Commands.User.Build;

using Communication.Packets.Outgoing.Rooms.Notifications;
using Database;
using Database.Daos.Room;
using GameClients;
using Rooms;

public class TileHeight : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser roomUser, string[] parameters)
    {
        if (session == null)
        {
            return;
        }

        if (room == null)
        {
            return;
        }

        if (!room.CheckRights(session, true))
        {
            session.SendPacket(RoomNotificationComposer.SendBubble(
                "default",
                "Apenas o proprietário do quarto pode alterar o carregamento de ladrilhos!"));
            return;
        }

        room.RoomData.TileFlow = !room.RoomData.TileFlow;
        using (var dbClient = DatabaseManager.Connection)
        {
            RoomDao.UpdateTileFlow(dbClient, room.Id, room.RoomData.TileFlow);
        }

        var mode = room.RoomData.TileFlow ? "Dinâmico" : "Clássico";
        session.SendWhisper($"O carregamento de ladrilhos foi alterado para {mode}. Recarregue o quarto para fazer as atualizações!");
    }
}
