namespace WibboEmulator.Games.Chats.Commands.Staff.Administration;

using Communication.Packets.Outgoing.Rooms.Engine;
using Core.Language;
using GameClients;
using Rooms;

internal sealed class ForceTransf : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (parameters.Length < 2)
        {
            return;
        }

        var username = parameters[1];

        var roomUserByUserId = room.RoomUserManager.GetRoomUserByName(username);
        if (roomUserByUserId == null)
        {
            return;
        }

        var clientByUsername = roomUserByUserId.Client;
        if (clientByUsername == null)
        {
            return;
        }

        if (clientByUsername.User.IsSpectator)
        {
            return;
        }

        if (parameters.Length is not 4 and not 3)
        {
            return;
        }

        var raceId = 0;
        if (parameters.Length == 4)
        {
            var x = parameters[3];
            if (int.TryParse(x, out _))
            {
                _ = int.TryParse(parameters[2], out raceId);
                if (raceId is < 1 or > 50)
                {
                    raceId = 0;
                }
            }
        }
        else
        {
            raceId = 0;
        }

        if (!roomUserByUserId.SetPetTransformation(parameters[2], raceId))
        {
            session.SendHugeNotification(LanguageManager.TryGetValue("cmd.transf.help", session.Language));
            return;
        }

        roomUserByUserId.SendWhisperChat(LanguageManager.TryGetValue("cmd.transf.helpstop", session.Language));

        roomUserByUserId.IsTransf = true;

        room.SendPacket(new UserRemoveComposer(roomUserByUserId.VirtualId));
        room.SendPacket(new UsersComposer(roomUserByUserId));
    }
}
