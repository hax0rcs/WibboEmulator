namespace WibboEmulator.Communication.Packets.Incoming.Rooms.Furni;

using Games.GameClients;
using Games.Items;
using Games.Rooms;

internal sealed class UpdateMagicTileEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (session != null && session.User != null)
        {
            var itemId = packet.PopInt();
            var heightToSet = packet.PopInt();

            if (!RoomManager.TryGetRoom(session.User.RoomId, out var room))
            {
                return;
            }

            if (!room.CheckRights(session))
            {
                return;
            }

            var item = room.RoomItemHandling.GetItem(itemId);
            switch (item.ItemData.InteractionType)
            {
                case InteractionType.PILE_MAGIC:
                    if (heightToSet > 5000)
                    {
                        heightToSet = 5000;
                    }

                    if (heightToSet < 0)
                    {
                        heightToSet = 0;
                    }

                    var totalZ = (heightToSet / 100.00);

                    item.SetState(item.X, item.Y, totalZ);

                    item.UpdateState(false);
                    break;
                case InteractionType.PILE_WALK:
                    if (heightToSet > 5000)
                    {
                        heightToSet = 5000;
                    }

                    if (heightToSet < 0)
                    {
                        heightToSet = 0;
                    }

                    var walkZ = (heightToSet / 100.00);

                    item.SetState(item.X, item.Y, walkZ);
                    item.UpdateState();
                    session.User.Room.RoomUserManager.UpdateUserStatusses();
                    break;
            }
        }
    }
}
