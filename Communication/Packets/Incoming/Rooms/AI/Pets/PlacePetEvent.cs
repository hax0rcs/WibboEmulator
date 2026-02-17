namespace WibboEmulator.Communication.Packets.Incoming.Rooms.AI.Pets;

using Core.Language;
using Database;
using Database.Daos.Bot;
using Games.GameClients;
using Games.Rooms;
using Games.Rooms.AI;
using Outgoing.Inventory.Pets;

internal sealed class PlacePetEvent : IPacketEvent
{
    public double Delay => 250;

    public void Parse(GameClient session, ClientPacket packet)
    {
        if (!RoomManager.TryGetRoom(session.User.RoomId, out var room))
        {
            return;
        }

        if (!room.CheckRights(session, true))
        {
            //session.SendPacket(new RoomErrorNotifComposer(1));
            return;
        }

        if (room.RoomUserManager.BotPetCount >= 30)
        {
            session.SendNotification(LanguageManager.TryGetValue("notif.placepet.error", session.Language));
            return;
        }

        if (!session.User.InventoryComponent.TryGetPet(packet.PopInt(), out var pet))
        {
            return;
        }

        if (pet == null)
        {
            return;
        }

        if (pet.PlacedInRoom)
        {
            return;
        }

        var x = packet.PopInt();
        var y = packet.PopInt();

        if (!room.GameMap.CanWalk(x, y, false))
        {
            return;
        }

        if (room.RoomUserManager.TryGetPet(pet.PetId, out var oldPet))
        {
            room.RoomUserManager.RemoveBot(oldPet.VirtualId, false);
        }

        pet.X = x;
        pet.Y = y;

        pet.PlacedInRoom = true;
        pet.RoomId = room.Id;

        _ = room.RoomUserManager.DeployBot(new RoomBot(pet.PetId, pet.OwnerId, pet.RoomId, BotAIType.Pet, true, pet.Name, "", "", pet.Look, x, y, 0, 0, false, "", 0, false, 0, 0, 0), pet);

        using (var dbClient = DatabaseManager.Connection)
        {
            BotPetDao.UpdateRoomId(dbClient, pet.PetId, pet.RoomId);
        }

        if (!session.User.InventoryComponent.TryRemovePet(pet.PetId, out _))
        {
            return;
        }

        session.SendPacket(new PetInventoryComposer(session.User.InventoryComponent.Pets));
    }
}
