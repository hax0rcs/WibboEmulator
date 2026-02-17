namespace WibboEmulator.Games.Items.Wired.Interfaces;

using Rooms;

public interface IWiredCondition : IWired
{
    bool AllowsExecution(RoomUser user, Item item);
}
