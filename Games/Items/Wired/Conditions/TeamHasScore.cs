namespace WibboEmulator.Games.Items.Wired.Conditions
{
    using System.Data;
    using Bases;
    using Database;
    using Interfaces;
    using Rooms;
    using Rooms.Games.Teams;

    public class TeamHasScore : WiredConditionBase, IWiredCondition, IWired
    {
        internal TeamHasScore(Item item, Room room) : base(item, room, (int)WiredConditionType.TEAM_HAS_SCORE) => this.DefaultIntParams(0, 0, 0);

        public bool AllowsExecution(RoomUser user, Item item)
        {
            var team = this.GetIntParam(0);
            var mathOperator = this.GetIntParam(1);
            var scoreToCompare = this.GetIntParam(2);

            TeamType teamToCheck;
            switch (team)
            {
                case 0:
                    if (user == null || user.Team == TeamType.None)
                        return false;

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

            var teamScore = this.Room.GameManager.GetScoreForTeam(teamToCheck);
            return mathOperator switch
            {
                0 => teamScore > scoreToCompare,
                1 => teamScore < scoreToCompare,
                2 => teamScore >= scoreToCompare,
                3 => teamScore <= scoreToCompare,
                4 => teamScore == scoreToCompare,
                5 => teamScore != scoreToCompare,
                _ => false,
            };
        }

        public void SaveToDatabase(IDbConnection dbClient)
        {
            var data = this.GetIntParam(0) + ";" + this.GetIntParam(1) + ";" + this.GetIntParam(2);
            WiredUtillity.SaveInDatabase(DatabaseManager.Connection, this.Item.Id, string.Empty, data);
        }

        public void LoadFromDatabase(string wiredTriggerData, string wiredTriggerData2, string wiredTriggersItem,
            bool wiredAllUserTriggerable, int wiredDelay)
        {
            if (wiredTriggerData.Split(';').Length == 3)
            {
                if (int.TryParse(wiredTriggerData.Split(';')[0], out var team))
                {
                    this.SetIntParam(0, team);
                }

                if (int.TryParse(wiredTriggerData.Split(';')[1], out var mathOperator))
                {
                    this.SetIntParam(1, mathOperator);
                }

                if (int.TryParse(wiredTriggerData.Split(';')[2], out var scores))
                {
                    this.SetIntParam(2, scores);
                }
            }
        }
    }
}
