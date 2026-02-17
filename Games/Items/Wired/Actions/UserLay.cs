namespace WibboEmulator.Games.Items.Wired.Actions;

using System.Data;
using Bases;
using Database;
using Interfaces;
using Rooms;

public class UserLay : WiredActionBase, IWiredEffect, IWired
{
    public UserLay(Item item, Room room) : base(item, room, (int)WiredActionType.RESET) => this.DefaultIntParams(0);

    public override bool OnCycle(RoomUser user, Item item)
    {
        if (user != null)
        {
            if (user.RotBody % 2 == 0)
            {
                user.SetStatus("lay", "0.7");

                user.IsLay = true;
                user.UpdateNeeded = true;
            }
        }

        return false;
    }
    public void SaveToDatabase(IDbConnection dbClient) => WiredUtillity.SaveInDatabase(DatabaseManager.Connection, this.Item.Id, string.Empty, string.Empty, false, null, this.Delay);

    public void LoadFromDatabase(string wiredTriggerData, string wiredTriggerData2, string wiredTriggersItem,
        bool wiredAllUserTriggerable, int wiredDelay) => this.Delay = wiredDelay;
}
