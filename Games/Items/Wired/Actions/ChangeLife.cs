namespace WibboEmulator.Games.Items.Wired.Actions;

using System;
using System.Data;
using Bases;
using Interfaces;
using Rooms;
using Utilities.User;

public class ChangeLife : WiredActionBase, IWired, IWiredEffect, IWiredCycleable
{
    private const int MAX_LIFE = 7;

    public ChangeLife(Item item, Room room) : base(item, room, (int)WiredActionType.CHANGE_LIFE)
    {
        this.DefaultIntParams(0);
        this.StringParam = "0";
    }
    public override bool OnCycle(RoomUser user, Item item)
    {
        if (user == null || user.IsBot || user.Client == null)
        {
            return false;
        }

        if (int.TryParse(this.StringParam, out var life) && life is >= 0 and <= MAX_LIFE)
        {
            var lifeOperator = (LifeOperatorAction)this.GetIntParam(0);
            switch (lifeOperator)
            {
                case LifeOperatorAction.Addition:
                    this.AdjustLife(user, life, (current, value) => Math.Min(current + value, MAX_LIFE));
                    break;
                case LifeOperatorAction.Subtraction:
                    this.AdjustLife(user, life, (current, value) => Math.Max(current - value, 0));
                    break;
                case LifeOperatorAction.Multiplication:
                    this.AdjustLife(user, life, (current, value) => Math.Min(current * value, MAX_LIFE));
                    break;
                case LifeOperatorAction.Division:
                    if (life != 0)
                    {
                        this.AdjustLife(user, life, (current, value) => current / value);
                    }
                    break;
                case LifeOperatorAction.Modulo:
                    if (life != 0)
                    {
                        this.AdjustLife(user, life, (current, value) => current % value);
                    }
                    break;
                case LifeOperatorAction.Replace:
                    this.ApplyLife(user, life);
                    break;
                case LifeOperatorAction.Delete:
                    this.RemoveLifes(user);
                    break;
                default:
                {
                    return false;
                }
            }
        }

        return false;
    }

    public void SaveToDatabase(IDbConnection dbClient)
    {
        var data = this.GetIntParam(0) + ";" + this.StringParam;
        WiredUtillity.SaveInDatabase(dbClient, this.Id, data, string.Empty, false, this.Items, this.Delay);
    }

    public void LoadFromDatabase(string wiredTriggerData, string wiredTriggerData2, string wiredTriggersItem, bool wiredAllUserTriggerable, int wiredDelay)
    {
        if (!string.IsNullOrEmpty(wiredTriggerData2))
        {
            var data = wiredTriggerData2.Split(';');
            if (data.Length > 0)
            {
                if (int.TryParse(data[0], out var option))
                {
                    this.SetIntParam(0, option);
                }
                else
                {
                    this.SetIntParam(0, 0);
                }
            }

            if (data.Length > 1)
            {
                this.StringParam = data[1];
            }
            else
            {
                this.StringParam = string.Empty;
            }

            return;
        }

        this.SetIntParam(0, 0);
        this.StringParam = string.Empty;
    }

    private void AdjustLife(RoomUser user, int life, Func<int, int, int> lifeAdjustment)
    {
        var newLife = lifeAdjustment(user.WiredLifes, life);
        if (newLife != user.WiredLifes)
        {
            this.ApplyLife(user, newLife);
        }
    }

    private void ApplyLife(RoomUser user, int life)
    {
        this.RemoveLifes(user);
        for (var i = 1; i <= life; i++)
        {
            user.ApplyEffect(Lifes.GetEnableIdByLifeValue(i));
        }
        user.WiredLifes = life;
    }

    private void RemoveLifes(RoomUser user)
    {
        user.ApplyEffect(Lifes.GetEnableIdByLifeValue(0));
        user.WiredLifes = 0;
    }

    public enum LifeOperatorAction
    {
        Addition,
        Subtraction,
        Multiplication,
        Division,
        Modulo,
        Replace,
        Delete
    }
}
