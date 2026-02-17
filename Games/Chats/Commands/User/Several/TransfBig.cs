namespace WibboEmulator.Games.Chats.Commands.User.Several;

using Communication.Packets.Outgoing.Rooms.Engine;
using Core.Language;
using GameClients;
using Rooms;
using Rooms.Games.Teams;

internal sealed class TransfBig : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (parameters.Length != 2)
        {
            return;
        }

        if (userRoom.Team != TeamType.None || userRoom.InGame || room.IsGameMode)
        {
            return;
        }

        if (session.User.IsSpectator)
        {
            return;
        }

        if (!userRoom.SetPetTransformation("big" + parameters[1], 0))
        {
            session.SendHugeNotification(LanguageManager.TryGetValue("cmd.littleorbig.help", session.Language));
            return;
        }

        userRoom.IsTransf = true;

        room.SendPacket(new UserRemoveComposer(userRoom.VirtualId));
        room.SendPacket(new UsersComposer(userRoom));
    }
}
