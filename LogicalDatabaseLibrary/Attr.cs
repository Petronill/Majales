namespace LogicalDatabaseLibrary;

public class Attr : IEquatable<Attr>
{
	private string name;
	public string Name { get => name; }

	private Type type;
	public Type Type { get => type; }

	public Attr(string name) : this(name, typeof(string))
    {
    }

	public Attr(string name, Type type)
    {
		this.name = name;
		this.type = type;
    }

	public string TypeToString()
	{
		return type.Name;
	}

	public override string ToString()
    {
		return type.ToString() + " " + name;
    }

	public static Attr? NewFromString(string str)
	{
		string[] tokens = str.Split(" ", 2);
		Type? t = Type.GetType(tokens[0]);
		return (t is not null) ? new Attr(tokens[1], t) : null;
	}

	public bool Check(object o) => o.GetType() == type;

	public string? ToString(object o)
    {
		return (o is null || !Check(o)) ? "" : o.ToString();
    }

	public object? FromString(string str)
    {
		if (type == typeof(string))
        {
			return str;
        }
		return type.GetMethod("Parse", new[] { typeof(string) })?.Invoke(null, new[] { str });
    }

    public bool Equals(Attr? obj)
    {
        return obj is not null &&
               name == obj.name &&
               type == obj.type;
    }
}
