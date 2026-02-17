namespace WibboEmulator.Games.Items.Wired.Actions;

using System.Data;
using Bases;
using Interfaces;
using Rooms;
using Database;

public class ControlChrono : WiredActionBase, IWiredEffect, IWired
{
    public ControlChrono(Item item, Room room) : base(item, room, (int)WiredActionType.CONTROL_CHRONO) => this.DefaultIntParams(0); // start chrono
    public override bool OnCycle(RoomUser user, Item item)
    {
        if (this.Items != null)
        {
            foreach (var chronoItem in this.Items.ToList())
            {
                if (chronoItem.ItemData.InteractionType != InteractionType.CHRONO_TIMER_UP)
                {
                    continue;
                }

                if (chronoItem.Interactor is InteractorUpChrono chronoInteractor)
                {
                    var option = this.GetIntParam(0);
                    switch (option)
                    {
                        case 0:
                            chronoInteractor.Start();
                            chronoItem.ReqUpdate(1);
                            break;
                        case 1:
                            chronoInteractor.Stop();
                            chronoItem.ReqUpdate(1);
                            break;
                        case 2:
                            chronoInteractor.Reset(chronoItem);
                            chronoItem.ReqUpdate(1);
                            break;
                        case 3:
                            chronoInteractor.Continue();
                            chronoItem.ReqUpdate(1);
                            break;
                    }
                }
            }
        }

        return false;
    }

    public void SaveToDatabase(IDbConnection dbClient) => WiredUtillity.SaveInDatabase(DatabaseManager.Connection, this.Item.Id, this.GetIntParam(0).ToString(), String.Empty, false, this.Items);

    public void LoadFromDatabase(string wiredTriggerData, string wiredTriggerData2, string wiredTriggersItem,
        bool wiredAllUserTriggerable, int wiredDelay)
    {
        if (int.TryParse(wiredTriggerData2, out var option))
        {
            this.SetIntParam(0, option);
        }

        this.LoadStuffIds(wiredTriggersItem);
        this.Delay = wiredDelay;
    }
}
