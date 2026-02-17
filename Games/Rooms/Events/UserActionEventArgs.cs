namespace WibboEmulator.Games.Rooms.Events;

public class UserActionEventArgs(int action, int value) : EventArgs
{
    public int Action { get; set; } = action;
    public int Value { get; set; } = value;
}
