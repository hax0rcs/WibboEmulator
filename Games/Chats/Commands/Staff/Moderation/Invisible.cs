namespace WibboEmulator.Games.Chats.Commands.Staff.Moderation;

using Communication.Packets.Outgoing.Navigator;
using GameClients;
using Rooms;

internal sealed class Invisible : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (session.User.IsSpectator)
        {
            session.User.IsSpectator = false;
            session.User.HideInRoom = false;
        }
        else
        {
            session.User.IsSpectator = true;
            session.User.HideInRoom = true;
        }

        session.SendPacket(new GetGuestRoomResultComposer(session, room.RoomData, false, true));
    }
}
