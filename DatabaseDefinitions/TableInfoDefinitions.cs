using LogicalDatabaseLibrary;

namespace DatabaseDefinitions;

[Serializable]
public struct TableHead
{
    public string Separator { get; set; }
    public int LineLimit { get; set; }
    public int StartPage { get; set; }
    public TableEntity Entity { get; set; }

    public int MaxId { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is TableHead head &&
               Separator == head.Separator &&
               LineLimit == head.LineLimit &&
               StartPage == head.StartPage &&
               Entity.Equals(head.Entity) &&
               MaxId == head.MaxId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Separator, LineLimit, StartPage, Entity, MaxId);
    }

    public static bool operator ==(TableHead left, TableHead right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(TableHead left, TableHead right)
    {
        return !(left == right);
    }
}

public struct TableMeta
{
    public string TableName { get; init; }
    public TableHead Head { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is TableMeta meta &&
               TableName == meta.TableName &&
               Head == meta.Head;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(TableName, Head);
    }

    public static bool operator ==(TableMeta left, TableMeta right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(TableMeta left, TableMeta right)
    {
        return !(left == right);
    }
}

public class TableUpdateArgs : EventArgs
{
    public string Name { get; init; }
}

public delegate void TableReorganizationHandler(object sender, EventArgs args);

public delegate void TableUpdatedHandler(object sender, TableUpdateArgs e);
