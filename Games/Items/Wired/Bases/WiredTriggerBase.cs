namespace WibboEmulator.Games.Items.Wired.Bases;

using Communication.Packets.Outgoing.Rooms.Wireds;
using GameClients;
using Rooms;

public class WiredTriggerBase : WiredBase
{
    internal WiredTriggerBase(Item item, Room room, int type) : base(item, room, type)
    {

    }

    public override void OnTrigger(GameClient session) => session.SendPacket(new WiredFurniTriggerComposer(this.StuffTypeSelectionEnabled, this.FurniLimit, this.StuffIds, this.StuffTypeId, this.Id,
            this.StringParam, this.IntParams, this.StuffTypeSelectionCode, this.Type, this.Conflicting));
}
