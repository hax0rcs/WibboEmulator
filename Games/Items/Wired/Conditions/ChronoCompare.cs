namespace WibboEmulator.Games.Items.Wired.Conditions;

using System.Data;
using Bases;
using Interfaces;
using Rooms;
using Database;

public class ChronoCompare : WiredConditionBase, IWiredCondition, IWired
{
    public ChronoCompare(Item item, Room room) : base(item, room, (int)WiredConditionType.CHRONO_COMPARE) => this.DefaultIntParams(0, 0, 0);
    public bool AllowsExecution(RoomUser user, Item item)
    {
        if (this.Items == null)
        {
            return false;
        }

        var option = this.GetIntParam(0);
        var targetMinutes = this.GetIntParam(1);
        var targetSeconds = this.GetIntParam(2);

        var targetTime = (targetMinutes * 60) + targetSeconds;
        foreach (var chronoItem in this.Items.ToList())
        {
            if (chronoItem.ItemData.InteractionType != InteractionType.CHRONO_TIMER_UP)
            {
                continue;
            }

            if (chronoItem.Interactor is InteractorUpChrono chronoInteractor)
            {
                var currentTime = chronoInteractor.getCurrentTime();
                switch (option)
                {
                    case 0:
                        if (currentTime < targetTime)
                        {
                            return true;
                        }

                        break;
                    case 1:
                        if (currentTime > targetTime)
                        {
                            return true;
                        }

                        break;
                    case 2:
                        if (currentTime == targetTime)
                        {
                            return true;
                        }

                        break;
                    case 3:
                        if (currentTime != targetTime)
                        {
                            return true;
                        }

                        break;
                    case 4:
                        if (currentTime <= targetTime)
                        {
                            return true;
                        }

                        break;
                    case 5:
                        if (currentTime >= targetTime)
                        {
                            return true;
                        }

                        break;
                }
            }
        }

        return false;
    }

    public void SaveToDatabase(IDbConnection dbClient)
    {
        var data = this.GetIntParam(0) + ";" + this.GetIntParam(1) + ";" + this.GetIntParam(2);
        WiredUtillity.SaveInDatabase(DatabaseManager.Connection, this.Item.Id, string.Empty, data, false, this.Items);
    }

    public void LoadFromDatabase(string wiredTriggerData, string wiredTriggerData2, string wiredTriggersItem,
        bool wiredAllUserTriggerable, int wiredDelay)
    {
        if (wiredTriggerData.Split(';').Length == 3)
        {
            if (int.TryParse(wiredTriggerData.Split(';')[0], out var option))
            {
                this.SetIntParam(0, option);
            }
        }

        if (int.TryParse(wiredTriggerData.Split(';')[1], out var minutes))
        {
            this.SetIntParam(1, minutes);
        }

        if (int.TryParse(wiredTriggerData.Split(';')[2], out var seconds))
        {
            this.SetIntParam(2, seconds);
        }

        this.LoadStuffIds(wiredTriggersItem);
    }
}
