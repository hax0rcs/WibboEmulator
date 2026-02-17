namespace WibboEmulator.Games.Items.Wired.Interfaces;

using Rooms;

public interface IWiredEffect
{
    void Handle(RoomUser user, Item item);
}
