namespace WibboEmulator.Games.Items.Wired.Triggers;

using System.Data;
using Bases;
using Interfaces;
using Rooms;
using Rooms.Events;

public class UserIdle : WiredTriggerBase, IWired
{
    public UserIdle(Item item, Room room) : base(item, room, (int)WiredTriggerType.GAME_ENDS)
    {
        this.DefaultIntParams(1);

        this.Room.RoomUserManager.OnUserIdle += this.OnUserIdle;
    }

    private void OnUserIdle(object sender, UserIdleEventArgs e)
    {
        var user = e.User;
        if (user == null || user.IsBot || user.Client == null)
        {
            return;
        }

        var sleeping = e.IdleTicks > 600;
        if (sleeping)
        {
            this.Room.WiredHandler.ExecutePile(this.Item.Coordinate, user, null);
        }
    }

    public override void Dispose()
    {
        base.Dispose();

        this.Room.RoomUserManager.OnUserIdle -= this.OnUserIdle;
    }

    public void SaveToDatabase(IDbConnection dbClient)
    {
    }

    public void LoadFromDatabase(string wiredTriggerData, string wiredTriggerData2, string wiredTriggersItem, bool wiredAllUserTriggerable, int wiredDelay)
    {
    }
}
