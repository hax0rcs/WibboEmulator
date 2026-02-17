namespace WibboEmulator.Games.Chats.Commands.User.Several;

using GameClients;
using Rooms;

internal sealed class ForceOpenGift : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        session.User.ForceOpenGift = !session.User.ForceOpenGift;

        if (session.User.ForceOpenGift)
        {
            session.SendWhisper("ForceOpenGift activé");
        }
        else
        {
            session.SendWhisper("ForceOpenGift désactivé");
        }
    }
}
