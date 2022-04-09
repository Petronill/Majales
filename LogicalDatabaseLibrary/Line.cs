using System.Collections;

namespace LogicalDatabaseLibrary;

public class Line : IEnumerable<object?>, IEquatable<Line>
{
    protected object?[] content;

    protected Line()
    {
    }

    public Line(params object?[] content)
    {
        this.content = content;
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
                content[i] = value;
            }
        }
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
