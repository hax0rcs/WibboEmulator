namespace WibboEmulator.Games.Chats.Commands.Staff.Animation;

using Communication.Packets.Outgoing.Moderation;
using GameClients;
using Rooms;

internal sealed class HotelAlert : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        var message = CommandManager.MergeParams(parameters, 1);
        if (session.User.CheckChatMessage(message, "<CMD>", room.Id))
        {
            return;
        }
        GameClientManager.SendMessage(new BroadcastMessageAlertComposer(message + "\r\n" + "- " + session.User.Username));
    }
}
