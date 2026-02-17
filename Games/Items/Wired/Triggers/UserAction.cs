namespace WibboEmulator.Games.Items.Wired.Triggers
{
    using System.Data;
    using Bases;
    using Database;
    using Interfaces;
    using Rooms;
    using Rooms.Events;

    public class UserAction : WiredTriggerBase, IWired
    {
        public UserAction(Item item, Room room) : base(item, room, (int)WiredTriggerType.AVATAR_ACTION)
        {
            this.DefaultIntParams(0);
            this.StringParam = "0;false;0";
            room.RoomUserManager.OnUserAction += this.OnUserAction;
        }

        private void OnUserAction(object sender, UserActionEventArgs args)
        {
            if (sender is not RoomUser user)
            {
                return;
            }

            if (string.IsNullOrEmpty(this.StringParam))
            {
                return;
            }

            var parts = this.StringParam.Split(';');
            if (parts.Length < 3)
            {
                return;
            }

            _ = int.TryParse(parts[0], out var option);
            _ = int.TryParse(parts[2], out var value);
            var filter = parts[1].Equals("true");

            Console.WriteLine("entrou no user action " + option);

            if (option != args.Action)
            {
                return;
            }

            if (this.IsValidAction(args, option, value, filter))
            {
                this.ExecutePile(user);
            }
        }

        private bool IsValidAction(UserActionEventArgs args, int option, int value, bool filter)
        {
            switch (option)
            {
                case 1://acenar
                case 2://manda beijo
                case 3://rir
                case 4://curtir (_b)
                case 13://despertou
                case 5://dormiu
                case 7://sentar
                case 8://levantar
                case 9://deitar
                    return args.Action == option;
                case 10://sinais
                case 11://dançar
                    return !filter || args.Value == value;
                default:
                    return false;
            }
        }

        public override void Dispose()
        {
            this.Room.RoomUserManager.OnUserAction -= this.OnUserAction;
            base.Dispose();
        }

        public void ExecutePile(RoomUser user) => this.Room.WiredHandler.ExecutePile(this.Item.Coordinate, user, null);

        public void SaveToDatabase(IDbConnection dbClient)
        {
            var data = this.StringParam.Split(';');
            if (!int.TryParse(data[0], out var option))
            {
                data[0] = "0";
            }

            if (!bool.TryParse(data[1], out var filter) && !data[1].Equals("true"))
            {
                data[1] = "false";
            }

            if (!int.TryParse(data[2], out var value))
            {
                data[2] = "1";
            }

            var dataSave = "" + option + ";" + (filter ? "true" : "false") + ";" + value;

            WiredUtillity.SaveInDatabase(DatabaseManager.Connection, this.Item.Id, dataSave, string.Empty);
        }

        public void LoadFromDatabase(string wiredTriggerData, string wiredTriggerData2, string wiredTriggersItem,
            bool wiredAllUserTriggerable, int wiredDelay)
        {
            var data = wiredTriggerData2.Split(';');

            if (data.Length == 3)
            {
                this.StringParam = wiredTriggerData;
            }

            this.Delay = wiredDelay;
        }
    }
}
