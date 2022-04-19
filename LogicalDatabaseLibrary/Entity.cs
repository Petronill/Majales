using System.Collections;
using System.Text;

namespace LogicalDatabaseLibrary;

public class Entity : IEnumerable<Attr>, IEquatable<Entity>
{
    protected Attr[] attrs;

    public Entity(params Attr[] attributes)
    {
        attrs = attributes;
    }

    public virtual int Count()
    {
        return attrs.Length;
    }

    public virtual Attr? this[int i]
    {
        get => (i >= 0 && i < attrs.Length) ? attrs[i] : null;
    }

    public virtual Attr? this[string name]
    {
        get => this[GetIndex(name)];
    }

    public virtual int GetIndex(string name)
    {
        for (int i = 0; i < attrs.Length; i++)
        {
            if (attrs[i].Name == name)
            {
                return i;
            }
        }
        return -1;
    }

    public virtual Line? FromTokens(string[] tokens)
    {
        if (attrs.Length > tokens.Length)
        {
            return null;
        }

        object?[] tmp = new object?[attrs.Length];
        for (int i = 0; i < tmp.Length; i++)
        {
            object? tmpObj = this[i]?.FromString(tokens[i]);
            if (!this[i].Check(tmpObj))
            {
                return null;
            }
            tmp[i] = tmpObj;
        }

        return new Line(tmp);
    }

    public virtual string ToString(Line line, string separator)
    {
        StringBuilder sb = new();
        for (int i = 0; i < attrs.Length; i++)
        {
            string? str = this[i]?.ToString(line[i]);
            sb.Append(str is not null ? str : "").Append(separator);
        }
        return sb.Remove(sb.Length - 1, 1).ToString();
    }

    public virtual string ToString(string separator)
    {
        StringBuilder sb = new();
        foreach (Attr attr in attrs)
        {
            sb.Append(attr.ToString()).Append(separator);
        }
        return sb.Remove(sb.Length - separator.Length, separator.Length).ToString();
    }

    protected static Attr[] AttrsFromTokens(string[] tokens)
    {
        List<Attr> ats = new();
        foreach (string token in tokens)
        {
            Attr? tmp = Attr.NewFromString(token);
            if (tmp is null)
            {
                return Array.Empty<Attr>();
            }
            ats.Add(tmp);
        }
        return ats.ToArray();
    }

    public static Entity? EntityFromTokens(string[] tokens)
    {
        return new Entity(AttrsFromTokens(tokens));
    }

    public bool Equals(Entity? other)
    {
        if (other == null || this.Count() == other.Count())
        {
            return false;
        }

        for (int i = 0; i < this.Count(); i++)
        {
            if (!attrs[i].Equals(other[i]))
            {
                return false;
            }
        }
        return true;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Entity);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(attrs);
    }

    public IEnumerator<Attr> GetEnumerator()
    {
        return ((IEnumerable<Attr>)attrs).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return attrs.GetEnumerator();
    }
}
