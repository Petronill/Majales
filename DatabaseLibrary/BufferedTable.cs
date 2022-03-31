using ArrayUtilsLibrary;
using DatabaseDefinitions;
using FileSupportLibrary;

namespace DatabaseLibrary.Tables;


public delegate void BufferFlushedHandler(object sender, EventArgs args);

public class BufferedTable : Table, IEquatable<BufferedTable>, IComparable<BufferedTable>
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

    public static new TableProvider<BufferedTable> GetTableProvider()
    {
        return (name, fileSupport) => new BufferedTable(name, fileSupport);
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
            if (fileSupporter.WriteLines(Name, currentPage, rows) != rows.Length)
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

    public override string? this[int id]
    {
        get
        {
            return base[id];
        }

        set
        {
            if (value != null)
            {
                if (FindId(id, out int lineNumber))
                {
                    rows[lineNumber] = value;
                    OnRowUpdated(new RowUpdateArgs { Row = new Row { Line = this[lineNumber], Meta = new RowMeta { PageNumber = currentPage, LineNumber = lineNumber } } });
                    BufferDecrement();
                }
                else
                {
                    Add(value);
                }
            }
        }
    }

    public override void Add(string line)
    {
        int page = FindFreeSpace();
        int lineNumber = 0;
        if (page == currentPage)
        {
            lineNumber = rows.Length;
            ArrayUtils.Append(ref rows, line);
            BufferDecrement();
        }
        else
        {
            fileSupporter.AppendLine(Name, page, line);
        }
        OnRowUpdated(new RowUpdateArgs { Row = new Row { Line = this[lineNumber], Meta = new RowMeta { PageNumber = currentPage, LineNumber = lineNumber } } });
    }

    protected override void Remove(int id, int lineNumber)
    {
        ArrayUtils.Delete(ref rows, lineNumber);
        OnRowDeleted(new RowUpdateArgs { Row = new Row { Line = this[lineNumber], Meta = new RowMeta { PageNumber = currentPage, LineNumber = lineNumber } } });
        BufferDecrement();
    }

    public override void Clear()
    {
        Flush();
        base.Clear();
    }

    public bool Equals(BufferedTable? other)
    {
        return base.Equals(other) && bufferCount == other.bufferCount;
    }

    public int CompareTo(BufferedTable? other)
    {
        return base.CompareTo(other);
    }
}
