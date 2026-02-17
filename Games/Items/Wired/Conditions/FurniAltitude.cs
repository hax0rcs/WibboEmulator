namespace WibboEmulator.Games.Items.Wired.Conditions;

using System.Data;
using Bases;
using Database;
using Interfaces;
using Rooms;

public class FurniAltitude : WiredConditionBase, IWiredCondition, IWired
{
    public FurniAltitude(Item item, Room room) : base(item, room, (int)WiredConditionType.FURNI_ALTITUDE) => this.DefaultIntParams(0, 0);

    public bool AllowsExecution(RoomUser user, Item item)
    {
        if (this.Items == null)
        {
            return false;
        }

        var mode = this.GetIntParam(0);
        var height = this.GetIntParam(1) / 100.0;
        var roomItem = this.Items.FirstOrDefault(floor => floor != null);

        return mode switch
        {
            0 => //>
                roomItem != null && roomItem.Z > height,
            1 => //<
                roomItem != null && roomItem.Z < height,
            2 => //>=
                roomItem != null && roomItem.Z >= height,
            3 => //<=
                roomItem != null && roomItem.Z <= height,
            4 => //==
                roomItem != null && roomItem.Z == height,
            5 => //!=
                roomItem != null && roomItem.Z != height,
            _ => false
        };
    }

    public void SaveToDatabase(IDbConnection dbClient)
    {
        var data = this.GetIntParam(0) + ";" + this.GetIntParam(1);
        WiredUtillity.SaveInDatabase(DatabaseManager.Connection, this.Item.Id, string.Empty, data, false, this.Items);
    }

    public void LoadFromDatabase(string wiredTriggerData, string wiredTriggerData2, string wiredTriggersItem,
        bool wiredAllUserTriggerable, int wiredDelay)
    {
        if (wiredTriggerData.Split(';').Length == 2)
        {
            if (int.TryParse(wiredTriggerData.Split(';')[0], out var mode))
            {
                this.SetIntParam(0, mode);
            }

            if (int.TryParse(wiredTriggerData.Split(';')[1], out var height))
            {
                this.SetIntParam(1, height);
            }
        }

        this.LoadStuffIds(wiredTriggersItem);
    }
}
