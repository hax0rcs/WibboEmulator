namespace WibboEmulator.Communication.Packets.Incoming.Rooms.Furni.Stickys;

using Games.GameClients;
using Games.Items;
using Games.Rooms;
using Outgoing.Rooms.Furni.Stickys;

internal sealed class GetStickyNoteEvent : IPacketEvent
{
    public double Delay => 250;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (!RoomManager.TryGetRoom(session.User.RoomId, out var room))
        {
            return;
        }

        var itemId = packet.PopInt();

        var roomItem = room.RoomItemHandling.GetItem(itemId);
        if (roomItem == null || roomItem.ItemData.InteractionType != InteractionType.POSTIT)
        {
            return;
        }

        session.SendPacket(new StickyNoteComposer(roomItem.Id, roomItem.ExtraData));
    }
}
