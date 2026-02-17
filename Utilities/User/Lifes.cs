namespace WibboEmulator.Utilities.User;

public static class Lifes
{
    private static readonly Dictionary<LifesEnum, (int Id, int LifeValue)> values = new()
    {
        { LifesEnum.LifeGaugeNML0, (0, 0) },
        { LifesEnum.LifeGaugeNML1, (917, 1) },
        { LifesEnum.LifeGaugeNML2, (918, 2) },
        { LifesEnum.LifeGaugeNML3, (919, 3) },
        { LifesEnum.LifeGaugeNML4, (920, 4) },
        { LifesEnum.LifeGaugeNML5, (921, 5) },
        { LifesEnum.LifeGaugeNML6, (922, 6) },
        { LifesEnum.LifeGaugeNML7, (923, 7) },
    };

    public static string GetEnableName(this LifesEnum playerLife)
    {
        return playerLife.ToString();
    }

    public static int GetEnableId(this LifesEnum playerLife)
    {
        return values[playerLife].Id;
    }

    public static int GetValueLife(this LifesEnum playerLife)
    {
        return values[playerLife].LifeValue;
    }

    public static int GetEnableIdByLifeValue(int lifeValue)
    {
        foreach (var kvp in values)
        {
            if (kvp.Value.LifeValue == lifeValue)
            {
                return kvp.Value.Id;
            }
        }
        return 0;
    }

    public static int GetLifeValueByEnableName(string enableName)
    {
        if (Enum.TryParse(enableName, true, out LifesEnum result))
        {
            return values[result].LifeValue;
        }
        return 0;
    }

    public enum LifesEnum
    {
        LifeGaugeNML0 = 0,
        LifeGaugeNML1 = 1,
        LifeGaugeNML2 = 2,
        LifeGaugeNML3 = 3,
        LifeGaugeNML4 = 4,
        LifeGaugeNML5 = 5,
        LifeGaugeNML6 = 6,
        LifeGaugeNML7 = 7
    }
}
