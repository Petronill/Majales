namespace LogicalDatabaseLibrary.Attrs;

public delegate object? FromStringCallback(string str);
public delegate string? ToStringCallback(object? o);

public class GenericAttr<T> : IAttr where T : class?
{
    protected string name;
    protected FromStringCallback fromStringCallback;
    protected ToStringCallback toStringCallback;

    public GenericAttr(string name, FromStringCallback fromString)
    {
        Init(name, fromString, (o) => (o == null) ? "" : o.ToString());
    }
    
    public GenericAttr(string name, FromStringCallback fromString, ToStringCallback toString)
    {
        Init(name, fromString, toString);
    }

    private void Init(string name, FromStringCallback fromString, ToStringCallback toString)
    {
        this.name = name;
        fromStringCallback = fromString;
        toStringCallback = toString;
    }

    public string GetName()
    {
        return name;
    }
    
    public Type GetAttrType()
    {
        return typeof(T);
    }

    public bool Check(object? o)
    {
        return o == null || o is T;
    }

    public object? FromString(string str)
    {
        return fromStringCallback(str);
    }

    public string? ToString(object? o)
    {
        return Check(o) ? toStringCallback(o) : null;
    }
}
