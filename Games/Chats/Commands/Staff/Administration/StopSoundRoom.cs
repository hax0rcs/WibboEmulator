namespace WibboEmulator.Games.Chats.Commands.Staff.Administration;

using Communication.Packets.Outgoing.Sound;
using GameClients;
using Rooms;

internal sealed class StopSoundRoom : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters) => room.SendPacket(new StopSoundComposer(parameters.Length != 2 ? "" : parameters[1])); //Type = Trax
}
