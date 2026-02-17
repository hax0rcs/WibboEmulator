namespace WibboEmulator.Games.Items.Wired.Conditions;

using System.Data;
using Bases;
using Interfaces;
using Rooms;

public class FurniStuffIs(Item item, Room room) : WiredConditionBase(item, room, (int)WiredConditionType.STUFF_TYPE_MATCHES), IWiredCondition, IWired
{
    public bool AllowsExecution(RoomUser user, Item item)
    {
        if (item == null)
        {
            return false;
        }

        foreach (var roomItem in this.Items.ToList())
        {
            if (roomItem.BaseItemId == item.BaseItemId && roomItem.ExtraData == item.ExtraData)
            {
                return true;
            }
        }

        return false;
    }

    public void SaveToDatabase(IDbConnection dbClient) => WiredUtillity.SaveInDatabase(dbClient, this.Id, string.Empty, string.Empty, false, this.Items);

    public void LoadFromDatabase(string wiredTriggerData, string wiredTriggerData2, string wiredTriggersItem, bool wiredAllUserTriggerable, int wiredDelay) => this.LoadStuffIds(wiredTriggersItem);
}
