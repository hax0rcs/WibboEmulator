namespace WibboEmulator.Games.Items.Wired.Triggers;

using System.Data;
using Bases;
using Interfaces;
using Rooms;
using Rooms.Events;

public class WalksOffFurni(Item item, Room room) : WiredTriggerBase(item, room, (int)WiredTriggerType.AVATAR_WALKS_OFF_FURNI), IWired
{
    private void OnUserWalksOffFurni(object obj, ItemTriggeredEventArgs args) => this.Room.WiredHandler.ExecutePile(this.Item.Coordinate, args.User, args.Item);

    public override void LoadItems(bool inDatabase = false)
    {
        base.LoadItems();

        if (this.Items != null)
        {
            foreach (var roomItem in this.Items.ToList())
            {
                roomItem.OnUserWalksOffFurni += this.OnUserWalksOffFurni;
            }
        }
    }

    public override void Dispose()
    {
        if (this.Items != null)
        {
            foreach (var roomItem in this.Items.ToList())
            {
                roomItem.OnUserWalksOffFurni -= this.OnUserWalksOffFurni;
            }
        }

        base.Dispose();
    }

    public void SaveToDatabase(IDbConnection dbClient) => WiredUtillity.SaveInDatabase(dbClient, this.Item.Id, string.Empty, string.Empty, false, this.Items);

    public void LoadFromDatabase(string wiredTriggerData, string wiredTriggerData2, string wiredTriggersItem, bool wiredAllUserTriggerable, int wiredDelay) => this.LoadStuffIds(wiredTriggersItem);
}
