using LogicalDatabaseLibrary;

namespace DatabaseDefinitions;

public struct RowMeta
{
    public int PageNumber { get; init; }
    public int LineNumber { get; init; }
    public static RowMeta Empty { get => new() { PageNumber = -1, LineNumber = -1 }; }

    public override bool Equals(object? obj)
    {
        return obj is RowMeta meta &&
               PageNumber == meta.PageNumber &&
               LineNumber == meta.LineNumber;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(PageNumber, LineNumber);
    }

    public static bool operator ==(RowMeta left, RowMeta right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(RowMeta left, RowMeta right)
    {
        return !(left == right);
    }
}

public struct Row
{
    public Line? Line { get; init; }
    public RowMeta Meta { get; init; }
    public static Row Empty { get => new() { Line = null, Meta = RowMeta.Empty }; }

    public override bool Equals(object? obj)
    {
        return obj is Row row &&
               Line.Equals(row.Line) &&
               Meta.Equals(row.Meta);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Line, Meta);
    }

    public static bool operator ==(Row left, Row right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Row left, Row right)
    {
        return !(left == right);
    }
}

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
