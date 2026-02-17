namespace WibboEmulator.Games.Items.Wired.Actions;

using System.Data;
using Bases;
using Database;
using Interfaces;
using Rooms;

public class UserRotation : WiredActionBase, IWiredEffect, IWired
{
    public UserRotation(Item item, Room room) : base(item, room, (int)WiredActionType.USER_ROTATION) => this.DefaultIntParams(0, 0);

    public override bool OnCycle(RoomUser user, Item item)
    {
        if (user != null)
        {
            var rot = this.GetIntParam(0);
            var headOnly = this.GetIntParam(1) == 1;

            user.ForceRot(rot, headOnly, true);
            user.UpdateNeeded = true;
        }

        return false;
    }

    public void SaveToDatabase(IDbConnection dbClient)
    {
        var data = this.GetIntParam(0) + ";" + this.GetIntParam(1);
        WiredUtillity.SaveInDatabase(DatabaseManager.Connection, this.Item.Id, string.Empty, data, false, null, this.Delay);
    }

    public void LoadFromDatabase(string wiredTriggerData, string wiredTriggerData2, string wiredTriggersItem,
        bool wiredAllUserTriggerable, int wiredDelay)
    {
        if (wiredTriggerData.Split(';').Length > 1)
        {
            _ = int.TryParse(wiredTriggerData.Split(';')[0], out var rot);
            this.SetIntParam(0, rot);
            _ = int.TryParse(wiredTriggerData.Split(';')[1], out var headOnly);
            this.SetIntParam(1, headOnly);
        }

        this.Delay = wiredDelay;
    }
}
