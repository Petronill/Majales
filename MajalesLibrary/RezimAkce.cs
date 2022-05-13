namespace MajalesLibrary;

public enum RezimAkce
{
    ONLINE,
    OFFLINE,
    NIL
}

public static class RezimAkceInfo
{
    public static IEnumerable<RezimAkce> Items { get => (IEnumerable<RezimAkce>)Enum.GetValues(typeof(RezimAkce)); }

    public static string ToString(RezimAkce rezim)
    {
        switch (rezim)
        {
            case RezimAkce.ONLINE:
                return "online";
            case RezimAkce.OFFLINE:
                return "offline";
            default:
                return "nil";
        }
    }

    public static string ToString(int index)
    {
        switch (index)
        {
            case 0:
                return "online";
            case 1:
                return "offline";
            default:
                return "nil";
        }
    }

    public static int ToIndex(RezimAkce rezim)
    {
        switch (rezim)
        {
            case RezimAkce.ONLINE:
                return 0;
            case RezimAkce.OFFLINE:
                return 1;
            default:
                return 2;
        }
    }

    public static int ToIndex(string name)
    {
        if (name == "online")
        {
            return 0;
        }
        if (name == "offline")
        {
            return 1;
        }
        return 2;
    }

    public static RezimAkce ToEnum(int index)
    {
        switch (index)
        {
            case 0:
                return RezimAkce.ONLINE;
            case 1:
                return RezimAkce.OFFLINE;
            default:
                return RezimAkce.NIL;
        }
    }

    public static RezimAkce ToEnum(string name)
    {
        if (name == "online")
        {
            return RezimAkce.ONLINE;
        }
        if (name == "offline")
        {
            return RezimAkce.OFFLINE;
        }
        return RezimAkce.NIL;
    }
}
