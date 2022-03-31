namespace LogicalDatabaseLibrary.Attrs;

public class PrimaryAttr : IdAttr
{
    public PrimaryAttr() : base("id")
    {
    }

    public object? FromLine(string line)
    {
        return FromLine(line, 0);
    }
}
