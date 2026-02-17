namespace WibboEmulator.Games.Chats.Commands.User.Info;

using Communication.Packets.Outgoing.Moderation;
using GameClients;
using Rooms;

internal sealed class About : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters) =>
        session.SendPacket(new BroadcastMessageAlertComposer("<b>Butterfly Edition Wibbo</b>\n\n" +
            "   <b>Credits</b>:\n" +
            "   Meth0d, maritnmine, Carlos, Super0ca,\n" +
            "   Mike, Sledmore, Joopie, Tweeny, \n" +
            "   JasonDhose, Leenster, sarkazm (hax0r) Moogly, Niels, AKllX, rbi0s\n\n"));
}
