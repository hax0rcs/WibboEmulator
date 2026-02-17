namespace WibboEmulator.Games.Rooms.Events;

public class UserIdleEventArgs(RoomUser user, int idleTicks) : EventArgs
{
    public RoomUser User { get; private set; } = user;
    public int IdleTicks { get; private set; } = idleTicks;
}
