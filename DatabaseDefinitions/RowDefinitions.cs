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
    public TableLine? Line { get; init; }
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

public class RowUpdateArgs : EventArgs
{
    public Row Row { get; init; }
}

public delegate void RowUpdatedHandler(object sender, RowUpdateArgs args);

public delegate bool RowSelector(Row row);
