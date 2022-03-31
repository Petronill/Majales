using System.Collections;
using System.Text;

namespace LogicalDatabaseLibrary;

public class Line : IEnumerable<object?>, IEquatable<Line>
{
    public Entity Entity { get; init; }
    protected object?[] content;

    protected Line()
    {
    }

    public Line(Entity entity)
    {
        Entity = entity;
        this.content = new object?[Entity.Count()];
    }

    public int Count()
    {
        return content.Length;
    }

    public object? this[int i]
    {
        get
        {
            if (i >= 0 && i < content.Length)
            {
                return this.content[i];
            }
            return null;
        }

        set
        {
            if (i >= 0 && i < content.Length)
            {
                if (!Entity[i].Check(content[i]))
                {
                    throw new ArgumentException("Line content does not correspond to an Attribute");
                }
                content[i] = value;
            }
        }
    }

    public object? this[string name]
    {
        get
        {
            return this[Entity.GetIndex(name)];
        }

        set
        {
            this[Entity.GetIndex(name)] = value;
        }
    }

    public bool FromTokens(string[] tokens)
    {
        object?[] tmp = new object?[content.Length];
        if (content.Length > tokens.Length)
        {
            return false;
        }
        for (int i = 0; i < content.Length; i++)
        {
            object? tmpObj = Entity[i].FromString(tokens[i]);
            if (!Entity[i].Check(tmpObj))
            {
                return false;
            }
            tmp[i] = tmpObj;
        }
        return true;
    }

    public string ToString(string separator)
    {
        StringBuilder sb = new();
        for (int i = 0; i < content.Length; i++)
        {
            sb.Append(Entity[i].ToString(content[i])).Append(separator);
        }
        return sb.Remove(sb.Length - 1, 1).ToString();
    }

    public bool Equals(Line? other)
    {
        if(other == null || this.Count() == other.Count())
        {
            return false;
        }

        for (int i = 0; i < this.Count(); i++)
        {
            if ((content[i] == null && other.content[i] != null ) || !content[i].Equals(other.content[i]))
            {
                return false;
            }
        }
        return true;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Line);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(content);
    }

    public IEnumerator<object?> GetEnumerator()
    {
        foreach (var o in content)
        {
            yield return o;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return content.GetEnumerator();
    }
}
