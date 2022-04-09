using ArrayUtilsLibrary;
using DatabaseDefinitions;
using FileSupportLibrary;
using LogicalDatabaseLibrary;

namespace DatabaseLibrary;


public delegate void BufferFlushedHandler(object sender, EventArgs args);

public class BufferedTable : Table, IComparable<BufferedTable>
{
    protected int bufferCount;
    protected int currentBuffer;
    protected bool changed = false;

    public event BufferFlushedHandler? BufferFlushed;
    protected virtual void OnBufferFlushed(EventArgs args) => BufferFlushed?.Invoke(this, args);

    public BufferedTable(string name, IFileSupport fileSupport, int bufferCount = 0, bool preload = false) : base(name, fileSupport, preload)
    {
        this.bufferCount = currentBuffer = bufferCount;
    }

    public static new TableProvider<BufferedTable> GetTableProvider(int bufferCount = 0, bool preload = false)
    {
        return (name, fileSupport) => {
            return new BufferedTable(name, fileSupport, bufferCount, preload);
        };
    }

    protected virtual void BufferDecrement()
    {
        currentBuffer--;
        changed = true;
        if (currentBuffer == 0)
        {
            Flush();
        }
    }

    public virtual void Flush()
    {
        if (changed)
        {
            if (!fileSupporter.UpdateInfo(Meta) || fileSupporter.WriteLines(Name, currentPage, ToStrings()) != rows.Length)
            {
                throw new IOException();
            }
            BufferFlushed?.Invoke(this, new EventArgs());
            currentBuffer = bufferCount;
            changed = false;
        }
    }

    protected override void LoadPage(int page)
    {
        Flush();
        base.LoadPage(page);
    }

    public override TableLine? this[int id]
    {
        get
        {
            return base[id];
        }

        set
        {
            if (value is not null && FindId(id, out int lineNumber))
            {
                rows[lineNumber] = value;
                BufferDecrement();
                OnRowUpdated(new RowUpdateArgs { Row = new Row { Line = this[lineNumber], Meta = new RowMeta { PageNumber = currentPage, LineNumber = lineNumber } } });
            }
        }
    }

    public override void Add(TableLine line)
    {
        line[0] = ++head.MaxId;
        int page = FindFreeSpace();
        int lineNumber = 0;
        if (page == currentPage)
        {
            lineNumber = rows.Length;
            ArrayUtils.Append(ref rows, line);
        }
        else
        {
            fileSupporter.AppendLine(Name, page, head.Entity.ToString(line, head.Separator));
        }
        BufferDecrement();
        OnRowUpdated(new RowUpdateArgs { Row = new Row { Line = this[lineNumber], Meta = new RowMeta { PageNumber = currentPage, LineNumber = lineNumber } } });
    }

    public virtual void Update(TableLine line)
    {
        if (FindId(line.GetId(), out int lineNumber))
        {
            OnRowRequested(new RowUpdateArgs { Row = new Row { Line = this[lineNumber], Meta = new RowMeta { PageNumber = currentPage, LineNumber = lineNumber } } });
            rows[lineNumber] = line;
            BufferDecrement();
            OnRowUpdated(new RowUpdateArgs { Row = new Row { Line = this[lineNumber], Meta = new RowMeta { PageNumber = currentPage, LineNumber = lineNumber } } });
        }
        else
        {
            Add(line);
        }
    }
    protected override void Remove(int id, int lineNumber)
    {
        ArrayUtils.Delete(ref rows, lineNumber);
        if(id == head.MaxId)
        {
            head.MaxId--;
        }
        BufferDecrement();
        OnRowDeleted(new RowUpdateArgs { Row = new Row { Line = this[lineNumber], Meta = new RowMeta { PageNumber = currentPage, LineNumber = lineNumber } } });
    }

    public override void Clear()
    {
        Flush();
        base.Clear();
    }

    public override bool Equals(object oth)
    {
        return oth is BufferedTable other &&base.Equals(other) && bufferCount == other.bufferCount;
    }

    public int CompareTo(BufferedTable? other)
    {
        return base.CompareTo(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), head, pages, fileSupporter, Name, bufferCount, changed);
    }

    public static bool operator <(BufferedTable left, BufferedTable right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(BufferedTable left, BufferedTable right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(BufferedTable left, BufferedTable right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(BufferedTable left, BufferedTable right)
    {
        return left.CompareTo(right) >= 0;
    }

    public static bool operator ==(BufferedTable left, BufferedTable right)
    {
        if (left is null)
        {
            return right is null;
        }

        return left.Equals(right);
    }

    public static bool operator !=(BufferedTable left, BufferedTable right)
    {
        return !(left == right);
    }
}
