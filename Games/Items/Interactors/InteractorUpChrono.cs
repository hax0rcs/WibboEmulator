using WibboEmulator.Games.GameClients;
using WibboEmulator.Games.Items;
using WibboEmulator.Games.Items.Interactors;

public class InteractorUpChrono : FurniInteractor
{
    private bool _chronoStarter;
    private int _currentTime;

    public override void OnPlace(GameClient session, Item item)
    {
    }

    public override void OnRemove(GameClient session, Item item) => item.ExtraData = "0";

    public override void OnTrigger(GameClient session, Item item, int request, bool userHasRights, bool reverse)
    {
        if (!userHasRights)
        {
            return;
        }

        this._currentTime = 0;
        if (!string.IsNullOrEmpty(item.ExtraData))
        {
            _ = int.TryParse(item.ExtraData, out this._currentTime);
        }

        if (request == 1)
        {
            if (this._chronoStarter)
            {
                this._chronoStarter = false;
                //item.Room.GameManager.StopGame();
            }
            else
            {
                this._chronoStarter = true;
                item.ReqUpdate(1);
                item.Room.GameManager.StartGame();
            }
        }
        else if (request == 2)
        {
            if (!this._chronoStarter)
            {
                this._currentTime = 0;
                this._chronoStarter = false;
                item.ReqUpdate(1);
                item.Room.GameManager.StopGame();
            }
        }

        item.ExtraData = this._currentTime.ToString();
        item.UpdateState();
    }

    public override void OnTick(Item item)
    {
        if (item == null)
        {
            return;
        }

        if (string.IsNullOrEmpty(item.ExtraData))
        {
            return;
        }

        if (!int.TryParse(item.ExtraData, out this._currentTime))
        {
            return;
        }

        if (!this._chronoStarter)
        {
            return;
        }

        if (this._currentTime >= 0 && this._currentTime < 3600)
        {
            if (item.InteractionCountHelper == 1)
            {
                this._currentTime++;
                item.ChronoUp(item, this._currentTime);

                item.InteractionCountHelper = 0;
                item.ExtraData = this._currentTime.ToString();
                item.UpdateState();
            }
            else
            {
                item.InteractionCountHelper++;
            }

            item.UpdateCounter = 1;
            return;
        }
        else if (this._currentTime >= 3600)
        {
            this._chronoStarter = false;
            //item.Room.GameManager.StopGame();
            return;
        }
    }

    public void Stop()
    {
        if (this._chronoStarter)
        {
            this._chronoStarter = false;
        }
    }

    public void Continue()
    {
        if (!this._chronoStarter)
        {
            this._chronoStarter = true;
        }
    }

    public void Start()
    {
        if (!this._chronoStarter)
        {
            this._chronoStarter = true;
        }
    }

    public void Reset(Item item)
    {
        this._currentTime = 0;
        this._chronoStarter = false;
        item.ExtraData = "0";
        item.UpdateState();
    }

    public int getCurrentTime() => this._currentTime;
}
