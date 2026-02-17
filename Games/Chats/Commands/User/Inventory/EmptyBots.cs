namespace WibboEmulator.Games.Chats.Commands.User.Inventory;

using Communication.Packets.Outgoing.Inventory.Bots;
using Core.Language;
using GameClients;
using Rooms;

internal sealed class EmptyBots : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        session.User.InventoryComponent.ClearBots();
        session.SendPacket(new BotInventoryComposer(session.User.InventoryComponent.Bots));
        userRoom.SendWhisperChat(LanguageManager.TryGetValue("empty.cleared", session.Language));
    }
}
