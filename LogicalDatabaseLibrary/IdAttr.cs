namespace LogicalDatabaseLibrary.Attrs;

public class IdAttr : IAttr
{
    protected const int idDigits = 10;
    protected string name;

    public IdAttr(string name)
    {
        this.name = name;
    }

    public bool Check(object? o)
    {
        return o is int;
    }

    public object? FromString(string str)
    {
        if (int.TryParse(str.AsSpan(0, idDigits), out int i))
        {
            return i;
        }
        return null;
    }

    public object? FromLine(string line, int index)
    {
        int offset = (idDigits + 1) * index;
        if (int.TryParse(line.AsSpan(offset, offset+idDigits), out int i))
        {
            return i;
        }
        return null;
    }

    public string GetName()
    {
        return name;
    }

    public Type GetAttrType()
    {
        return typeof(int);
    }

    public string? ToString(object? o)
    {
        return Check(o) ? ((int?) o)?.ToString("D"+idDigits) : null ;
    }
}
