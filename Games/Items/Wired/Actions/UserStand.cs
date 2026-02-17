namespace WibboEmulator.Games.Items.Wired.Actions;

using System.Data;
using Bases;
using Database;
using Interfaces;
using Rooms;

public class UserStand : WiredActionBase, IWiredEffect, IWired
{
    public UserStand(Item item, Room room) : base(item, room, (int)WiredActionType.RESET) => this.DefaultIntParams(0);

    public override bool OnCycle(RoomUser user, Item item)
    {
        if (user != null)
        {

            if (user.ContainStatus("lay"))
            {
                user.RemoveStatus("lay");
            }

            if (user.ContainStatus("sit"))
            {
                user.RemoveStatus("sit");
            }

            if (user.ContainStatus("sign"))
            {
                user.RemoveStatus("sign");
            }

            user.UpdateNeeded = true;
        }

        return false;
    }
    public void SaveToDatabase(IDbConnection dbClient) => WiredUtillity.SaveInDatabase(DatabaseManager.Connection, this.Item.Id, string.Empty, string.Empty, false, null, this.Delay);

    public void LoadFromDatabase(string wiredTriggerData, string wiredTriggerData2, string wiredTriggersItem,
        bool wiredAllUserTriggerable, int wiredDelay) => this.Delay = wiredDelay;
}
