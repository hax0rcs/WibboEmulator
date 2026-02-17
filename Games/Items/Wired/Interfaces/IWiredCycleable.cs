namespace WibboEmulator.Games.Items.Wired.Interfaces;

using Rooms;

public interface IWiredCycleable
{
    bool OnCycle(RoomUser user, Item item);

    bool Disposed();

    int DelayCycle { get; }

    bool IsTeleport { get; }
}
