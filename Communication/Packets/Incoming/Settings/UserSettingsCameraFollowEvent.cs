namespace WibboEmulator.Communication.Packets.Incoming.Settings;

using Database;
using Database.Daos.User;
using Games.GameClients;

internal sealed class UserSettingsCameraFollowEvent : IPacketEvent
{
    public double Delay => 0;

    public void Parse(GameClient session, ClientPacket packet)
    {
        var flag = packet.PopBoolean();

        if (session == null || session.User == null)
        {
            return;
        }

        using (var dbClient = DatabaseManager.Connection)
        {
            UserDao.UpdateCameraFollowDisabled(dbClient, session.User.Id, flag);
        }

        session.User.CameraFollowDisabled = flag;
    }
}
