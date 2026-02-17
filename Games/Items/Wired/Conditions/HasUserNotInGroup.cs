namespace WibboEmulator.Games.Items.Wired.Conditions;

using System.Data;
using Bases;
using Database;
using Groups;
using Interfaces;
using Rooms;

public class HasUserNotInGroup : WiredConditionBase, IWiredCondition, IWired
{

    public HasUserNotInGroup(Item item, Room room) : base(item, room, (int)WiredConditionType.ACTOR_IS_GROUP_MEMBER) => this.DefaultIntParams(1, 0, 0, 0);

    public bool AllowsExecution(RoomUser user, Item item)
    {
        var groupId = this.GetIntParam(0);
        var verifyIsMember = this.GetIntParam(1) == 1;
        var verifyIsAdmin = this.GetIntParam(2) == 1;
        var verifyIsOwn = this.GetIntParam(3) == 1;

        //var allMatch = this.GetIntParam(3) == 1;
        //var anyoneMatch = this.GetIntParam(4) == 1;

        if (user == null || user.IsBot || user.Client == null || user.Client.User == null)
        {
            return false;
        }

        if (!GroupManager.TryGetGroup(groupId, out var group))
        {
            return true;
        }

        if (verifyIsMember && !group.IsMember(user.UserId))
        {
            return true;
        }

        if (verifyIsAdmin && !group.IsAdmin(user.UserId))
        {
            return true;
        }

        if (verifyIsOwn && group.CreatorId != user.UserId)
        {
            return true;
        }

        return false;
    }

    public void SaveToDatabase(IDbConnection dbClient)
    {
        var data = "" + this.GetIntParam(0) + ";" + this.GetIntParam(1) + ";" + this.GetIntParam(2);
        WiredUtillity.SaveInDatabase(DatabaseManager.Connection, this.Item.Id, string.Empty, data);
    }

    public void LoadFromDatabase(string wiredTriggerData, string wiredTriggerData2, string wiredTriggersItem, bool wiredAllUserTriggerable, int wiredDelay)
    {
        var data = wiredTriggerData.Split(';');

        if (data.Length == 3)
        {
            if (int.TryParse(data[0], out var groupId))
            {
                this.SetIntParam(0, groupId);
            }

            if (int.TryParse(data[1], out var verifyAdmin))
            {
                this.SetIntParam(1, verifyAdmin);
            }

            if (int.TryParse(data[2], out var verifyOwn))
            {
                this.SetIntParam(2, verifyOwn);
            }
        }
    }
}
