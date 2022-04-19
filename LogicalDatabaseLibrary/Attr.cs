using MiscLibrary.Sanitizing;

namespace LogicalDatabaseLibrary;

public class Attr : IEquatable<Attr>, IInputSanitizable<object?>
{
	private string name;
	public virtual string Name { get => name; }

	private Type type;
	public virtual Type Type { get => type; }

	public Attr(string name) : this(name, typeof(string))
    {
    }

	public Attr(string name, Type type)
    {
		this.name = name;
		this.type = type;
    }

	public virtual string TypeToString()
	{
		return type.Name;
	}

	public virtual object? Default()
    {
		return (type.IsValueType) ? Activator.CreateInstance(type): null;
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

	public virtual bool Check(object? o) => o?.GetType() == type || (o is null && Nullable.GetUnderlyingType(type) is not null);

	public virtual string? ToString(object? o)
    {
		if (!Check(o))
        {
            throw new UnsanitizedInputException();
        }
		return o is null ? "" : o.ToString();
    }

	public virtual object? FromString(string str)
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

    public override bool Equals(object? obj)
    {
        return obj is Attr attr && this.Equals(attr);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(type, name);
    }
}
