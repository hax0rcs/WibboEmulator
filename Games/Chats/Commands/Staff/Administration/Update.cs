namespace WibboEmulator.Games.Chats.Commands.Staff.Administration;

using Animations;
using Banners;
using Catalogs;
using Communication.Packets.Outgoing.Catalog;
using Communication.Packets.Outgoing.Rooms.Chat;
using Communication.WebSocket;
using Core.FigureData;
using Core.Language;
using Core.Settings;
using Database;
using Effects;
using Filter;
using GameClients;
using Items;
using LandingView;
using Loots;
using Moderations;
using Navigators;
using Permissions;
using Roleplays;
using Roleplays.Enemy;
using Roleplays.Item;
using Roleplays.Weapon;
using Rooms;
using Styles;

internal sealed class Update : IChatCommand
{
    public void Execute(GameClient session, Room room, RoomUser userRoom, string[] parameters)
    {
        if (parameters.Length != 2)
        {
            return;
        }

        var cmd = parameters[1];

        if (string.IsNullOrEmpty(cmd))
        {
            return;
        }

        using var dbClient = DatabaseManager.Connection;
        switch (cmd.ToLower())
        {
            case "emuban":
            {
                WebSocketManager.ResetBan();
                session.SendWhisper("Réinitialisation des bannissements de l'émulateur");
                break;
            }
            case "landingview":
            case "view":
            case "lv":
            case "vue":
            {
                LandingViewManager.Initialize(dbClient);
                session.SendWhisper("Vue et promotion mises à jour");
                break;
            }
            case "hof":
            {
                HallOfFameManager.Initialize(dbClient);
                session.SendWhisper("Hof mises à jour");
                break;
            }
            case "text":
            case "texte":
            case "locale":
            {
                LanguageManager.Initialize(dbClient);
                session.SendWhisper("Local mis à jour");
                break;
            }
            case "autogame":
            {
                AnimationManager.Initialize(dbClient);
                session.SendWhisper("Jeux automatique mis à jour");
                break;
            }
            case "lootbox":
            {
                LootManager.Initialize(dbClient);
                session.SendWhisper("Lootbox mis à jour");
                break;
            }
            case "rpitems":
            {
                RPItemManager.Initialize(dbClient);
                session.SendWhisper("RP Items mis à jour");
                break;
            }
            case "rpweapon":
            {
                RPWeaponManager.Initialize(dbClient);
                session.SendWhisper("RP Weapon mis à jour");
                break;
            }
            case "rpenemy":
            {
                RPEnemyManager.Initialize(dbClient);
                session.SendWhisper("RP Enemy mis à jour");
                break;
            }
            case "cmd":
            case "commands":
            {
                CommandManager.Initialize(dbClient);
                session.SendWhisper("Commands mis à jour");
                break;
            }
            case "chat":
            {
                ChatStyleManager.Initialize(dbClient);
                session.SendWhisper("Chat mis à jour");
                break;
            }
            case "permission":
            {
                PermissionManager.Initialize(dbClient);
                session.SendWhisper("Permissions mises à jour !");
                break;
            }
            case "effet":
            case "enable":
            {
                EffectManager.Initialize(dbClient);
                session.SendWhisper("Effet mis à jour");
                break;
            }
            case "rp":
            case "roleplay":
            {
                RoleplayManager.Initialize(dbClient);
                session.SendWhisper("Role play mis à jour");
                break;
            }
            case "moderation":
            {
                ModerationManager.Initialize(dbClient);
                session.SendWhisper("Moderation mis à jour");
                GameClientManager.SendMessageStaff(new WhisperComposer(userRoom.VirtualId, "Les outils de modération viennent d'être mis à jour, reconnectez-vous!", 23));
                break;
            }
            case "catalogue":
            case "cata":
            {
                ItemManager.Initialize(dbClient);
                CatalogManager.Initialize(dbClient);
                GameClientManager.SendMessage(new CatalogUpdatedComposer());
                session.SendWhisper("Catalogue mis à jour");
                break;
            }
            case "navigateur":
            case "navi":
            case "navigator":
            {
                NavigatorManager.Initialize(dbClient);
                session.SendWhisper("Navigateur mis à jour");
                break;
            }
            case "filter":
            case "filtre":
            {
                WordFilterManager.Initialize(dbClient);
                session.SendWhisper("Filtre mis à jour");
                break;
            }
            case "items":
            case "furni":
            {
                ItemManager.Initialize(dbClient);
                session.SendWhisper("Items mis à jour");
                break;
            }
            case "model":
            {
                RoomManager.Initialize(dbClient);
                session.SendWhisper("Model mis à jour");
                break;
            }
            case "mutant":
            case "figure":
            {
                FigureDataManager.Initialize();
                session.SendWhisper("Mutant/Figure mises à jour");
                break;
            }
            case "setting":
            case "settings":
            {
                SettingsManager.Initialize(dbClient);
                session.SendWhisper("Paramètre mises à jour");
                break;
            }
            case "banner":
            {
                BannerManager.Initialize(dbClient);
                session.SendWhisper("Bannière mises à jour");
                break;
            }
            default:
            {
                session.SendWhisper(LanguageManager.TryGetValue("cmd.notfound", session.Language));
                return;
            }
        }
    }
}
