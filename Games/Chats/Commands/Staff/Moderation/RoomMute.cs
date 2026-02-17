namespace WibboEmulator.Games.Chats.Commands.Staff.Moderation;

using GameClients;
using Rooms;

internal sealed class RoomMute : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        room.RoomMuted = !room.RoomMuted;

        foreach (var user in room.RoomUserManager.RoomUsers)
        {
            if (user == null)
            {
                continue;
            }

            user.SendWhisperChat(room.RoomMuted ? "Vous ne pouvez plus parler" : "Vous pouvez parler");
        }
    }
}
