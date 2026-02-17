namespace WibboEmulator.Database.Daos.User;

using System.Data;
using Dapper;

internal sealed class UserStatsDao
{
    internal static void UpdateRemoveAllGroupId(IDbConnection dbClient, int groupId) => dbClient.Execute(
        "UPDATE `user_stats` SET `group_id` = '0' WHERE `group_id` = '" + groupId + "' LIMIT 1");

    internal static void UpdateRemoveGroupId(IDbConnection dbClient, int userId) => dbClient.Execute(
        "UPDATE user_stats SET group_id = '0' WHERE id = @UserId LIMIT 1", new { UserId = userId });

    internal static void UpdateGroupId(IDbConnection dbClient, int groupId, int userId) => dbClient.Execute(
        "UPDATE user_stats SET group_id = @GroupId WHERE id = @UserId LIMIT 1",
        new { GroupId = groupId, UserId = userId });

    internal static void UpdateAchievementScore(IDbConnection dbClient, int userId, int score) => dbClient.Execute(
        "UPDATE `user_stats` SET `achievement_score` = `achievement_score` + '" + score + "' WHERE `id` = '" + userId + "'");

    internal static void UpdateKisses(IDbConnection dbClient, int userId, int kisses) => dbClient.Execute(
        "UPDATE `user_stats` SET `kisses` = `kisses` + '" + kisses + "' WHERE `id` = '" + userId + "'");

    internal static void UpdateBCItemsUsed(IDbConnection dbClient, int userId, int itemsUsed) => dbClient.Execute(
        "UPDATE `user_stats` SET `bc_items_used` = + '" + itemsUsed + "' WHERE `id` = '" + userId + "'");

    internal static void UpdateBCItemsMax(IDbConnection dbClient, int userId, int itemsMax) => dbClient.Execute(
        "UPDATE `user_stats` SET `bc_items_max` = + '" + itemsMax + "' WHERE `id` = '" + userId + "'");

    internal static void UpdateReceivedKisses(IDbConnection dbClient, int userId, int receiveKisses) => dbClient.Execute(
        "UPDATE `user_stats` SET `received_kisses` = `received_kisses` + '" + receiveKisses + "' WHERE `id` = '" + userId + "'");

    internal static void UpdateReceivedDuckets(IDbConnection dbClient, int userId, int receiveDuckets) => dbClient.Execute(
        "UPDATE `user_stats` SET `received_duckets` = `received_duckets` + '" + receiveDuckets + "' WHERE `id` = '" + userId + "'");

    internal static void UpdateLevel(IDbConnection dbClient, int userId, int level) => dbClient.Execute(
        "UPDATE `user_stats` SET `level` = `level` + '" + level + "' WHERE `id` = '" + userId + "'");

    internal static void UpdateAll(IDbConnection dbClient, int userId, int groupId, int onlineTime, int questId, int respect, int dailyRespectPoints, int kisses, int receivedKisses, int receivedDuckets, int level, int bcItemsUsed, int bcItemsMax, int dailyPetRespectPoints) => dbClient.Execute(
        @"UPDATE user_stats
        SET group_id = @GroupId,
        online_time = online_time + @OnlineTime,
        quest_id = @QuestId,
        respect = @Respect,
        daily_respect_points = @DailyRespectPoints,
        kisses = @Kisses,
        received_kisses = @ReceivedKisses,
        received_duckets = @ReceivedDuckets,
        level = @Level,
        bc_items_used = @BcItemsUsed,
        bc_items_max = @BcItemsMax,
        daily_pet_respect_points = @DailyPetRespectPoints
        WHERE id = @Id",
        new
        {
            Id = userId,
            GroupId = groupId,
            OnlineTime = onlineTime,
            QuestId = questId,
            Respect = respect,
            DailyRespectPoints = dailyRespectPoints,
            Kisses = kisses,
            ReceivedKisses = receivedKisses,
            ReceivedDuckets = receivedDuckets,
            Level = level,
            BcItemsUsed = bcItemsUsed,
            BcItemsMax = bcItemsMax,
            DailyPetRespectPoints = dailyPetRespectPoints
        });

    internal static void UpdateRespectPoint(IDbConnection dbClient, int userId, int count) => dbClient.Execute(
        "UPDATE `user_stats` SET `daily_respect_points` = '" + count + "', `daily_pet_respect_points` = '" + count + "' WHERE `id` = '" + userId + "' LIMIT 1");

    internal static void Insert(IDbConnection dbClient, int userId) => dbClient.Execute(
        "INSERT INTO `user_stats` (`id`) VALUES ('" + userId + "')");

    internal static UserStatsEntity GetOne(IDbConnection dbClient, int userId) => dbClient.QuerySingleOrDefault<UserStatsEntity>(
        "SELECT `id`, `online_time`, `respect`, `respect_given`, `gifts_given`, `gifts_received`, `daily_respect_points`, `kisses`, `received_kisses`, `received_duckets`, `level`, `bc_items_used`, `bc_items_max`, `daily_pet_respect_points`, `achievement_score`, `quest_id`, `quest_progress`, `lev_builder`, `lev_social`, `lev_identity`, `lev_explore`, `group_id` FROM `user_stats` WHERE id = @Id", new { Id = userId });
}

public class UserStatsEntity
{
    public int Id { get; set; }
    public int OnlineTime { get; set; }
    public int Respect { get; set; }
    public int RespectGiven { get; set; }
    public int GiftsGiven { get; set; }
    public int GiftsReceived { get; set; }
    public int DailyRespectPoints { get; set; }
    public int DailyPetRespectPoints { get; set; }

    public int Kisses { get; set; }

    public int ReceivedKisses { get; set; }

    public int ReceivedDuckets { get; set; }

    public int Level { get; set; }

    public int BcItemsUseds { get; set; }
    public int BcItemsMax { get; set; }
    public int AchievementScore { get; set; }
    public int QuestId { get; set; }
    public int QuestProgress { get; set; }
    public int LevBuilder { get; set; }
    public int LevSocial { get; set; }
    public int LevIdentity { get; set; }
    public int LevExplore { get; set; }
    public int GroupId { get; set; }
}
