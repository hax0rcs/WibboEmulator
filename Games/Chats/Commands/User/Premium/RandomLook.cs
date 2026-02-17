namespace WibboEmulator.Games.Chats.Commands.User.Premium;

using Communication.Packets.Outgoing.Avatar;
using Communication.Packets.Outgoing.Rooms.Engine;
using Database;
using Database.Daos.User;
using GameClients;
using Rooms;
using Rooms.Games.Teams;

internal sealed class RandomLook : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (userRoom.Team != TeamType.None || userRoom.InGame || room.IsGameMode)
        {
            return;
        }

        if (session.User == null)
        {
            return;
        }

        if (userRoom.IsTransf || userRoom.IsSpectator)
        {
            return;
        }

        using (var dbClient = DatabaseManager.Connection)
        {
            session.User.Look = UserWardrobeDao.GetOneRandomLook(dbClient);
        }

        session.SendPacket(new FigureUpdateComposer(session.User.Look, session.User.Gender));
        session.SendPacket(new UserChangeComposer(userRoom, true));
        room.SendPacket(new UserChangeComposer(userRoom, false));
    }
}
