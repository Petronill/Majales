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
}

public struct Row
{
    public string? Line { get; init; }
    public RowMeta Meta { get; init; }
    public static Row Empty { get => new() { Line = null, Meta = RowMeta.Empty }; }

    public override bool Equals(object? obj)
    {
        return obj is Row row &&
               Line == row.Line &&
               Meta.Equals(row.Meta);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Line, Meta);
    }
}

public static class LineFormat
{
    public static int IdStringToInt(string id)
    {
        return int.Parse(id);
    }

    public static string IdIntToString(int id)
    {
        return id.ToString("D10");
    }

    public static int GetId(string line)
    {
        return IdStringToInt(GetIdAsString(line));
    }

    public static int GetId(Row row)
    {
        return IdStringToInt(GetIdAsString(row));
    }

    public static string GetIdAsString(string line)
    {
        return line.Substring(0, 10);
    }

    public static string GetIdAsString(Row row)
    {
        return row.Line != null ? GetIdAsString(row.Line) : string.Empty;
    }
}

public struct TableHead
{
    public string Separator { get; set; }
    public int LineLimit { get; set; }
    public int StartPage { get; set; }

    public static TableHead Empty()
    {
        return new TableHead { Separator = string.Empty, LineLimit = 0, StartPage = 0};
    }

    public override bool Equals(object? obj)
    {
        return obj is TableHead head &&
               Separator == head.Separator &&
               LineLimit == head.LineLimit &&
               StartPage == head.StartPage;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Separator, LineLimit, StartPage);
    }
}
