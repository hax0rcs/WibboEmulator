namespace WibboEmulator.Games.Items.Wired.Triggers;

using System.Data;
using Bases;
using Interfaces;
using Rooms;
using Rooms.Events;
using Rooms.Games.Teams;

public class ScoreAchieved : WiredTriggerBase, IWired
{
    public ScoreAchieved(Item item, Room room) : base(item, room, (int)WiredTriggerType.SCORE_ACHIEVED)
    {
        this.Room.GameManager.OnScoreChanged += this.OnScoreChanged;
        this.DefaultIntParams(0, 0);
    }

    private void OnScoreChanged(object sender, TeamScoreChangedEventArgs e)
    {
        var teamFlag = (TeamType)this.GetIntParam(0);
        var scoreLevel = this.GetIntParam(1);

        if (teamFlag == TeamType.None || e.Team == teamFlag)
        {
            if (e.Points >= scoreLevel)
            {
                this.Room.WiredHandler.ExecutePile(this.Item.Coordinate, e.User, null);
            }
        }
    }

    public override void Dispose()
    {
        this.Room.GameManager.OnScoreChanged -= this.OnScoreChanged;
        base.Dispose();
    }

    public void SaveToDatabase(IDbConnection dbClient)
    {
        var teamFlag = this.GetIntParam(0);
        var scoreLevel = this.GetIntParam(1);
        WiredUtillity.SaveInDatabase(dbClient, this.Id, string.Empty, $"{teamFlag};{scoreLevel}");
    }

    public void LoadFromDatabase(string wiredTriggerData, string wiredTriggerData2, string wiredTriggersItem, bool wiredAllUserTriggerable, int wiredDelay)
    {
        var data = wiredTriggerData.Split(';');
        if (data.Length == 2 && int.TryParse(data[0], out var teamFlag) && int.TryParse(data[1], out var scoreLevel))
        {
            this.SetIntParam(0, teamFlag);
            this.SetIntParam(1, scoreLevel);
        }
    }
}
