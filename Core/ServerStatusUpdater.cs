namespace WibboEmulator.Core;

using System.Diagnostics;
using System.Data;
using Database;
using Database.Daos.Emulator;
using Games.Animations;
using Games.GameClients;
using Games.Rooms;

public static class ServerStatusUpdater
{
    private static int _userPeak;
    private static bool _isExecuted;

    public static void Initialize(IDbConnection dbClient)
    {
        _userPeak = EmulatorStatusDao.GetUserpeak(dbClient);

        _lowPriorityProcessWatch = new Stopwatch();
        _lowPriorityProcessWatch.Start();

        Console.WriteLine("Server Status Updater has been started.");
    }


    private static Stopwatch _lowPriorityProcessWatch;
    public static void Process()
    {
        if (_lowPriorityProcessWatch.ElapsedMilliseconds >= 60000 || !_isExecuted)
        {
            _isExecuted = true;
            _lowPriorityProcessWatch.Restart();
            try
            {
                var usersOnline = GameClientManager.Count;

                AnimationManager.OnUpdateUsersOnline(usersOnline);

                if (usersOnline > _userPeak)
                {
                    _userPeak = usersOnline;
                }

                var roomsLoaded = RoomManager.Count;

                var uptime = DateTime.Now - WibboEnvironment.ServerStarted;

                using var dbClient = DatabaseManager.Connection;
                EmulatorStatsDao.Insert(dbClient, usersOnline, roomsLoaded);
                EmulatorStatusDao.UpdateScore(dbClient, usersOnline, roomsLoaded, _userPeak);
            }
            catch (Exception e)
            {
                ExceptionLogger.LogThreadException(e.ToString(), "ServerStatusUpdate");
            }
        }
    }
}
