namespace WibboEmulator.Games.Roleplays;

using System.Collections.Concurrent;
using System.Data;
using Database.Daos.Roleplay;
using Enemy;
using Item;
using Player;
using Weapon;

public static class RoleplayManager
{
    private static readonly ConcurrentDictionary<int, RolePlayerManager> RolePlays = new();

    public static RolePlayerManager GetRolePlay(int ownerId)
    {
        if (!RolePlays.ContainsKey(ownerId))
        {
            return null;
        }

        _ = RolePlays.TryGetValue(ownerId, out var rp);
        return rp;
    }

    public static void Initialize(IDbConnection dbClient)
    {
        RolePlays.Clear();

        RPItemManager.Initialize(dbClient);
        RPWeaponManager.Initialize(dbClient);
        RPEnemyManager.Initialize(dbClient);

        var roleplayList = RoleplayDao.GetAll(dbClient);
        if (roleplayList.Count != 0)
        {
            foreach (var roleplay in roleplayList)
            {
                if (!RolePlays.ContainsKey(roleplay.OwnerId))
                {
                    _ = RolePlays.TryAdd(roleplay.OwnerId, new RolePlayerManager(roleplay.OwnerId, roleplay.HopitalId, roleplay.PrisonId));
                }
            }
        }
    }
}
