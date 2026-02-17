namespace WibboEmulator.Games.Items.Wired.Actions;

using System.Data;
using Bases;
using Communication.Packets.Outgoing.Rooms.Engine;
using Database;
using Interfaces;
using Rooms;

public class Altitude : WiredActionBase, IWiredEffect, IWired
{
    public Altitude(Item item, Room room) : base(item, room, (int)WiredActionType.ALTITUDE) => this.DefaultIntParams(0, 0);

    public override bool OnCycle(RoomUser user, Item item)
    {
        if (this.Items == null)
        {
            return false;
        }

        var mode = this.GetIntParam(0);
        var targetHeight = this.GetIntParam(1) / 100.0;

        if (targetHeight > 50.0)
        {
            targetHeight = 50.0;
        }

        if (targetHeight < 0.0)
        {
            targetHeight = 0.0;
        }

        switch (mode)
        {
            case 0: //incremento
                foreach (var roomItem in this.Items)
                {
                    var difference = roomItem.Z;
                    if (difference >= 0)
                    {
                        this.AdjustHeight(roomItem, difference, targetHeight);
                    }
                }
                break;
            case 1: //decremento
                foreach (var roomItem in this.Items)
                {
                    var difference = roomItem.Z;
                    if (difference >= 0)
                    {
                        var newHeight = roomItem.Z - targetHeight;
                        if (newHeight < 0.0)
                        {
                            newHeight = 0.0;
                        }
                        this.AdjustHeight(roomItem, 0, newHeight);
                    }
                }
                break;
            case 2: //absoluto
                foreach (var roomItem in this.Items)
                {
                    this.AdjustHeight(roomItem, targetHeight, 0.0);
                }
                break;
        }

        return false;
    }

    private void AdjustHeight(Item roomItem, double difference, double targetHeight)
    {
        var newZ = difference + targetHeight;
        if (newZ < 0.0)
        {
            newZ = 0.0;
        }
        this.Room.SendPacket(new SlideObjectBundleComposer(roomItem.X, roomItem.Y, roomItem.Z, roomItem.X, roomItem.Y, newZ, roomItem.Id, 0, true));
        roomItem.Z = newZ;
    }


    public void SaveToDatabase(IDbConnection dbClient)
    {
        var data = this.GetIntParam(0) + ";" + this.GetIntParam(1);
        WiredUtillity.SaveInDatabase(DatabaseManager.Connection, this.Item.Id, string.Empty, data, false, this.Items, this.Delay);
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

        this.Delay = wiredDelay;
        this.LoadStuffIds(wiredTriggersItem);
    }
}
