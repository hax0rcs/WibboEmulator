namespace WibboEmulator.Games.Items.Wired.Triggers;

using System.Data;
using Bases;
using Database;
using Interfaces;
using Rooms;
using Rooms.Events;
using WebSocketSharp;

public class ChronoIs : WiredTriggerBase, IWired
{
    public ChronoIs(Item item, Room room) : base(item, room, (int)WiredTriggerType.CHRONO_IS)
    {
        this.DefaultIntParams(0, 0);

        item.OnChronoUp += this.OnChronoIs;
    }
    private void OnChronoIs(object sender, ItemTriggeredEventArgs args)
    {
        var chrono = args.Item;
        if (chrono == null)
        {
            return;
        }

        if (!int.TryParse(args.Value, out var chronoTime))
        {
            return;
        }

        var wiredMinute = this.GetIntParam(0);
        var wiredSeconds = this.GetIntParam(1);
        var totalWiredSeconds = (wiredMinute * 60) + wiredSeconds;

        if (chronoTime == totalWiredSeconds)
        {
            this.Room.WiredHandler.ExecutePile(this.Item.Coordinate, null, null);
        }
    }

    public override void LoadItems(bool inDatabase = false)
    {
        base.LoadItems();

        if (this.Items != null)
        {
            foreach (var choronoItem in this.Items.ToList())
            {
                choronoItem.OnChronoUp += this.OnChronoIs;
            }
        }
    }

    public override void Dispose()
    {
        if (this.Items != null)
        {
            foreach (var roomItem in this.Items.ToList())
            {
                roomItem.OnChronoUp -= this.OnChronoIs;
            }
        }

        base.Dispose();
    }

    public void SaveToDatabase(IDbConnection dbClient)
    {
        var minute = this.GetIntParam(0);
        var secounds = this.GetIntParam(1);

        var data = minute + ";" + secounds;
        WiredUtillity.SaveInDatabase(DatabaseManager.Connection, this.Item.Id, data, String.Empty, false, this.Items);
    }

    public void LoadFromDatabase(string wiredTriggerData, string wiredTriggerData2, string wiredTriggersItem,
        bool wiredAllUserTriggerable, int wiredDelay)
    {
        if (wiredTriggerData2.Contains(';'))
        {
            if (int.TryParse(wiredTriggerData2.Split(';')[0], out var minute))
            {
                this.SetIntParam(0, minute);
            }

            if (int.TryParse(wiredTriggerData2.Split(';')[1], out var secounds))
            {
                this.SetIntParam(1, secounds);
            }
        }

        this.LoadStuffIds(wiredTriggersItem);
    }
}
