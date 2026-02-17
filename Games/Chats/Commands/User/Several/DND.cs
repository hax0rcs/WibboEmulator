namespace WibboEmulator.Games.Chats.Commands.User.Several;

using GameClients;
using Rooms;

internal sealed class DND : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        session.User.IgnoreRoomInvites = !session.User.IgnoreRoomInvites;
        session.SendWhisper("Tu " + (session.User.IgnoreRoomInvites ? "acceptes" : "refuses") + " les messages dans ta console d'amis");
    }
}
