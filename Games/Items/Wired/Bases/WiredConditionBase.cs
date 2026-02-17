namespace WibboEmulator.Games.Items.Wired.Bases;

using Communication.Packets.Outgoing.Rooms.Wireds;
using GameClients;
using Rooms;

public class WiredConditionBase : WiredBase
{
    internal WiredConditionBase(Item item, Room room, int type) : base(item, room, type)
    {

    }

    public override void OnTrigger(GameClient session) => session.SendPacket(new WiredFurniConditionComposer(this.StuffTypeSelectionEnabled, this.FurniLimit, this.StuffIds, this.StuffTypeId, this.Id,
            this.StringParam, this.IntParams, this.StuffTypeSelectionCode, this.Type));
}
