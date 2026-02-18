namespace ButterflyEmulator.HabboHotel.Chats.Commands.User.Premium;

using WibboEmulator.Communication.Packets.Outgoing.Messenger;
using WibboEmulator.Database;
using WibboEmulator.Database.Daos.Guild;
using WibboEmulator.Games.Chats.Commands;
using WibboEmulator.Games.GameClients;
using WibboEmulator.Games.Groups;
using WibboEmulator.Games.Rooms;

internal class RemoveGroupChat : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser roomUser, string[] parameters)
    {
        if (!int.TryParse(parameters[1], out var groupId))
        {
            session.SendWhisper("ID fornecido é inválido!", false);
            return;
        }

        if (!GroupManager.TryGetGroup(groupId, out var group))
        {
            session.SendWhisper("Não foi possível obter o grupo fornecido!", false);
            return;
        }

        if (group.CreatorId != session.User.Id)
        {
            session.SendWhisper("Apenas o proprietário do Grupo pode deletar o chat de grupo!", false);
            return;
        }

        using var dbClient = DatabaseManager.Connection;

        group.HasChat = false;
        GuildDao.UpdateHasChat(dbClient, groupId, false);

        group.SendPacket(FriendListUpdateComposer.WriteGroupChat(group, -1));
        session.SendWhisper("Chat de Grupo desabilitado com sucesso!", false);
    }
}
