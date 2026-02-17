namespace WibboEmulator.Games.Chats.Commands.User.Several;

using Communication.Packets.Outgoing.Inventory.AvatarEffects;
using GameClients;
using Rooms;

internal sealed class Emblem : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        var emblemId = session.User.BadgeComponent.EmblemId;

        if (emblemId > 0)
        {
            userRoom.CurrentEffect = emblemId;
            room.SendPacket(new AvatarEffectComposer(userRoom.VirtualId, userRoom.CurrentEffect));
        }
    }
}
