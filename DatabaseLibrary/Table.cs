using DatabaseDefinitions;
using FileSupportLibrary;
using MiscLibrary;
using MiscLibrary.Sanitizing;
using MiscLibrary.ArrayUtils;
using System.Collections;
using System.Text;
using LogicalDatabaseLibrary;
using DatabaseLibrary.Indexes;

namespace DatabaseLibrary;

public class Table : IEnumerable<Row>, IQuietEnumerable<Row>, IComparable<Table>, IInputSanitizable<Line>, IInputSanitizable<TableLine>
{
    protected TableHead head;
    protected bool pageReady;
    protected int currentPage;
    protected int pages;
    protected TableLine[] rows = Array.Empty<TableLine>();
    protected IFileSupport fileSupporter;

    public string Name { get; init; }

    public TableMeta Meta { get => new() { TableName = Name, Head = head }; }

    public event RowUpdatedHandler? RowUpdated;
    public event RowUpdatedHandler? RowRequested;
    public event RowUpdatedHandler? RowDeleted;
    public event TableReorganizationHandler? TableCleared;

    protected void OnRowUpdated(RowUpdateArgs args) => RowUpdated?.Invoke(this, args);
    protected void OnRowRequested(RowUpdateArgs args) => RowRequested?.Invoke(this, args);
    protected void OnRowDeleted(RowUpdateArgs args) => RowDeleted?.Invoke(this, args);
    protected void OnTableCleared(EventArgs args) => TableCleared?.Invoke(this, args);

    public Table(string name, IFileSupport fileSupport, bool preload = false)
    {
        Name = name;
        fileSupporter = fileSupport;
        InitTable(preload);
    }

    public static TableProvider<Table> GetTableProvider(bool preload = false)
    {
        return (name, fileSupport) => new Table(name, fileSupport, preload);
    }

    protected virtual void InitTable(bool preload)
    {
        if (fileSupporter.GetInfo(Name, out TableHead loadedHead) && (pages = fileSupporter.AllPages(new() { TableName = Name, Head = loadedHead })) > 0)
        {
            this.head = loadedHead;
            pageReady = false;
            if (preload)
            {
                GetPageReady();
            }
        }
        else
        {
            throw new FileNotFoundException();
        }
    }

    protected virtual void LoadPage(int page)
    {
        if (pageReady && page == currentPage)
        {
            return;
        }

        if (fileSupporter.GetPageLines(Name, page, out string[] lines))
        {
            currentPage = page;
            pageReady = true;

            int i = 0;
            Array.Resize(ref rows, lines.Length);
            for (int j = 0; j < lines.Length; j++)
            {
                if (rows[i] is null)
                {
                    rows[i] = new TableLine();
                }

                TableLine? tmp = head.Entity.FromTokens(lines[j].Split(head.Separator));
                if (tmp is not null)
                {
                    rows[i] = tmp;
                    i++;
                }
            }
            Array.Resize(ref rows, i);
        }
        else
        {
            throw new FileNotFoundException();
        }
    }

    protected virtual void LoadNextPage()
    {
        LoadPage((currentPage - head.StartPage + 1) % pages + head.StartPage);
    }

    protected virtual void GetPageReady()
    {
        if (!pageReady)
        {
            LoadPage(head.StartPage);
        }
    }

    protected virtual bool FindId(int id, out int lineNumber)
    {
        GetPageReady();
        int stop = currentPage;
        do
        {
            for (lineNumber = 0; lineNumber < rows.Length; lineNumber++)
            {
                if (rows[lineNumber].GetId() == id)
                {
                    return true;
                }
            }
            LoadNextPage();
        } while (currentPage != stop);

        lineNumber = -1;
        return false;
    }

    protected virtual bool FindId(int id, IPropIndex index, out int lineNumber)
    {
        RowMeta? tmp = index.GetMeta(id);
        if (tmp is null)
        {
            return FindId(id, out lineNumber);
        }

        RowMeta meta = tmp.Value;
        if (meta.PageNumber >= head.StartPage || meta.PageNumber < head.StartPage + pages)
        {
            LoadPage(meta.PageNumber);
        }
        else
        {
            GetPageReady();
            if (meta.LineNumber < 0 || meta.LineNumber > head.LineLimit)
            {
                return FindId(id, out lineNumber);
            }
        }
        int startLine = meta.LineNumber < 0 || meta.LineNumber > head.LineLimit ? 0 : meta.LineNumber;

        int stop = currentPage;
        do
        {
            for (int aux = startLine; aux < rows.Length + startLine; aux++)
            {
                lineNumber = aux % rows.Length;
                if (rows[lineNumber].GetId() == id)
                {
                    return true;
                }
            }
            LoadNextPage();
        } while (currentPage != stop);

        lineNumber = -1;
        return false;
    }

    protected virtual int FindFreeSpace()
    {
        int stop = currentPage;
        do
        {
            if (rows.Length + 1 < head.LineLimit)
            {
                return currentPage;
            }
            LoadNextPage();
        } while (currentPage != stop);

        pages++;
        return head.StartPage + pages - 1;
    }

    public virtual TableLine? this[int id]
    {
        get
        {
            if (FindId(id, out int lineNumber))
            {
                OnRowRequested(new RowUpdateArgs { Row = new Row { Line = this[lineNumber], Meta = new RowMeta { PageNumber = currentPage, LineNumber = lineNumber } } });
                return rows[lineNumber];
            }
            return null;
        }

        set
        {
            if (value is not null && FindId(id, out int lineNumber))
            {
                rows[lineNumber] = value;
                fileSupporter.UpdateLine(Name, currentPage, fileSupporter.Sanitize(head.Entity.ToString(value, head.Separator)), lineNumber);
                OnRowUpdated(new RowUpdateArgs { Row = new Row { Line = this[lineNumber], Meta = new RowMeta { PageNumber = currentPage, LineNumber = lineNumber } } });
            }
        }
    }

    public virtual TableLine? this[int id, IPropIndex index]
    {
        get
        {
            if (FindId(id, index, out int lineNumber))
            {
                OnRowRequested(new RowUpdateArgs { Row = new Row { Line = this[lineNumber], Meta = new RowMeta { PageNumber = currentPage, LineNumber = lineNumber } } });
                return rows[lineNumber];
            }
            return null;
        }

        set
        {
            if (value is not null && FindId(id, index, out int lineNumber))
            {
                rows[lineNumber] = value;
                fileSupporter.UpdateLine(Name, currentPage, fileSupporter.Sanitize(head.Entity.ToString(value, head.Separator)), lineNumber);
                OnRowUpdated(new RowUpdateArgs { Row = new Row { Line = this[lineNumber], Meta = new RowMeta { PageNumber = currentPage, LineNumber = lineNumber } } });
            }
        }
    }

    public virtual void Add(TableLine line)
    {
        if (!Check(line))
        {
            throw new UnsanitizedInputException();
        }
        
        line[head.Entity.GetIndex("rowId")] = ++head.MaxId;
        fileSupporter.UpdateInfo(Meta);

        int page = FindFreeSpace();
        fileSupporter.AppendLine(Name, page, fileSupporter.Sanitize(head.Entity.ToString(line, head.Separator)));
        int lineNumber = 0;
        if (page == currentPage)
        {
            lineNumber = rows.Length;
            ArrayUtils.Append(ref rows, line);
        }
        OnRowUpdated(new RowUpdateArgs { Row = new Row { Line = this[lineNumber], Meta = new RowMeta { PageNumber = currentPage, LineNumber = lineNumber } } });
    }

    public virtual void Add(Line line)
    {
        if (!Check(line))
        {
            throw new UnsanitizedInputException();
        }

        TableLine? tmp = head.Entity.FromLine((int)head.Entity["rowId"].Default(), line);
        if (tmp is not null)
        {
            Add(tmp);
        }
    }

    public virtual void Update(TableLine line)
    {
        if (!Check(line))
        {
            throw new UnsanitizedInputException();
        }

        if (FindId(line.GetId(), out int lineNumber))
        {
            OnRowRequested(new RowUpdateArgs { Row = new Row { Line = this[lineNumber], Meta = new RowMeta { PageNumber = currentPage, LineNumber = lineNumber } } });
            rows[lineNumber] = line;
            fileSupporter.UpdateLine(Name, currentPage, fileSupporter.Sanitize(head.Entity.ToString(line, head.Separator)), lineNumber);
            OnRowUpdated(new RowUpdateArgs { Row = new Row { Line = this[lineNumber], Meta = new RowMeta { PageNumber = currentPage, LineNumber = lineNumber } } });
        }
        else
        {
            Add(line);
        }
    }

    protected virtual void Remove(int id, int lineNumber)
    {
        fileSupporter.DeleteLine(Name, currentPage, lineNumber);
        ArrayUtils.Delete(ref rows, lineNumber);
        if (id == head.MaxId)
        {
            head.MaxId--;
            fileSupporter.UpdateInfo(Meta);
        }
        OnRowDeleted(new RowUpdateArgs { Row = new Row { Line = this[lineNumber], Meta = new RowMeta { PageNumber = currentPage, LineNumber = lineNumber } } });
    }

    public virtual bool Remove(int id)
    {
        if (FindId(id, out int lineNumber))
        {
            Remove(id, lineNumber);
            return true;
        }
        return false;
    }

    public virtual bool Remove(int id, IPropIndex index)
    {
        if (FindId(id, index, out int lineNumber))
        {
            Remove(id, lineNumber);
            return true;
        }
        return false;
    }

    public virtual void Clear()
    {
        pageReady = false;
        rows = Array.Empty<TableLine>();
        OnTableCleared(new EventArgs());
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        foreach (Row r in this)
        {
            sb.Append(r.Line);
            sb.Append('\n');
        }
        return sb.ToString();
    }

    public string[] ToStrings()
    {
        return rows.Select((tl) => head.Entity.ToString(tl, head.Separator)).ToArray();
    }

    protected string[] ToSanitizedStrings()
    {
        return rows.Select((tl) => fileSupporter.Sanitize(head.Entity.ToString(tl, head.Separator))).ToArray();
    }

    public IEnumerator<Row> GetEnumerator()
    {
        GetPageReady();
        int stop = currentPage;
        do
        {
            for (int i = 0; i < rows.Length; i++)
            {
                yield return new Row { Line = rows[i], Meta = new RowMeta { PageNumber = currentPage, LineNumber = i } };
            }
            LoadNextPage();
        } while (currentPage != stop);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<Row> GetQuietEnumerator()
    {
        GetPageReady();
        for (int i = 0; i < rows.Length; i++)
        {
            yield return new Row { Line = rows[i], Meta = new RowMeta { PageNumber = currentPage, LineNumber = i } };
        }
    }

    public override bool Equals(object obj)
    {
        return obj is Table other && this.Name == other.Name && head.Equals(other.head);
    }

    public int CompareTo(Table? other)
    {
        return (other is null) ? -1 : Name.CompareTo(other.Name);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(head, pages, fileSupporter, Name);
    }

    public bool Check(Line input)
    {
        if (input is null)
        {
            return false;
        }

        int i = 0;
        string? stringProp;
        for (; i < input.Count - 1; ++i)
        {
            stringProp = head.Entity[i]?.ToString(input[i]);
            if (stringProp is null || !fileSupporter.Check(stringProp) || stringProp.Contains(head.Separator))
            {
                return false;
            }
        }

        stringProp = head.Entity[i]?.ToString(input[i]);
        return stringProp is not null && fileSupporter.Check(stringProp);
    }
    
    public bool Check(TableLine input)
    {
        return Check((Line)input);
    }

    public static bool operator <(Table left, Table right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(Table left, Table right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(Table left, Table right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(Table left, Table right)
    {
        return left.CompareTo(right) >= 0;
    }

    public static bool operator ==(Table left, Table right)
    {
        if (left is null)
        {
            return right is null;
        }

        return left.Equals(right);
    }

    public static bool operator !=(Table left, Table right)
    {
        return !(left == right);
    }
}
