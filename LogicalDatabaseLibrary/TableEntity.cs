using ArrayUtilsLibrary;

namespace LogicalDatabaseLibrary;

public class TableEntity : Entity
{
    public TableEntity(params Attr[] attributes) : base(attributes)
    {
        ArrayUtils.Prepend(ref attrs, new Attr("rowId", typeof(int)));
    }

    private TableEntity(bool done, params Attr[] attributes) : base(attributes)
    {
    }

    public new TableLine? FromTokens(string[] tokens)
    {
        if (attrs.Length > tokens.Length)
        {
            return null;
        }

        object?[] tmp = new object?[attrs.Length];
        for (int i = 0; i < attrs.Length; i++)
        {
            object? tmpObj = this[i]?.FromString(tokens[i]);
            if (!this[i].Check(tmpObj))
            {
                return null;
            }
            tmp[i] = tmpObj;
        }

        return new TableLine(tmp);
    }

    public TableLine? FromTokens(int id, string[] lineTokens)
    {
        if (attrs.Length > lineTokens.Length+1)
        {
            return null;
        }

        object?[] tmp = new object?[attrs.Length+1];
        tmp[0] = id;
        for (int i = 1; i < tmp.Length; i++)
        {
            object? tmpObj = this[i]?.FromString(lineTokens[i]);
            if (!this[i].Check(tmpObj))
            {
                return null;
            }
            tmp[i] = tmpObj;
        }

        return new TableLine(tmp);
    }

    public TableLine? FromLine(int id, Line line)
    {
        if (attrs.Length > line.Count())
        {
            return null;
        }

        object?[] tmp = new object?[attrs.Length];
        tmp[0] = id;
        for (int i = 1; i < tmp.Length; i++)
        {
            if (!this[i].Check(line[i]))
            {
                return null;
            }
            tmp[i] = line[i];
        }

        return new TableLine(tmp);
    }

    public static new TableEntity? EntityFromTokens(string[] tokens)
    {
        return new TableEntity(true, AttrsFromTokens(tokens));
    }
}