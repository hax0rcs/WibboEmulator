namespace WibboEmulator.Games.Items.Wired.Conditions;

using System.Data;
using Bases;
using Database;
using Interfaces;
using Rooms;
using Rooms.Games.Teams;

public class TeamHasRank : WiredConditionBase, IWiredCondition, IWired
{
    public TeamHasRank(Item item, Room room) : base(item, room, (int)WiredConditionType.TEAM_HAS_RANK) => this.DefaultIntParams(0, 0);

    public bool AllowsExecution(RoomUser user, Item item)
    {
        var team = this.GetIntParam(0);
        var position = this.GetIntParam(1);

        TeamType teamToCheck;
        switch (team)
        {
            case 0://time do ativador
                if (user == null)
                {
                    return false;
                }

                if (user.Team == TeamType.None)
                {
                    return false;
                }

                teamToCheck = user.Team;
                break;
            case 1:
                teamToCheck = TeamType.Red;
                break;
            case 2:
                teamToCheck = TeamType.Green;
                break;
            case 3:
                teamToCheck = TeamType.Blue;
                break;
            case 4:
                teamToCheck = TeamType.Yellow;
                break;
            default:
                return false;
        }

        return this.Room.GameManager.GetTeamPosition(teamToCheck) == position && this.Room.GameManager.GetTeamPosition(teamToCheck) != 0;
    }

    public void SaveToDatabase(IDbConnection dbClient)
    {
        var data = this.GetIntParam(0) + ";" + this.GetIntParam(1);
        WiredUtillity.SaveInDatabase(DatabaseManager.Connection, this.Item.Id, string.Empty, data);
    }

    public void LoadFromDatabase(string wiredTriggerData, string wiredTriggerData2, string wiredTriggersItem,
        bool wiredAllUserTriggerable, int wiredDelay)
    {
        if (wiredTriggerData.Split(';').Length == 2)
        {
            if (int.TryParse(wiredTriggerData.Split(';')[0], out var val))
            {
                this.SetIntParam(0, val);
            }

            if (int.TryParse(wiredTriggerData.Split(';')[1], out var position))
            {
                this.SetIntParam(1, position);
            }
        }
    }
}
