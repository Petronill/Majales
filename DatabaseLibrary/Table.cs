using DatabaseDefinitions;
using FileSupportLibrary;
using ArrayUtilsLibrary;
using System.Collections;
using System.Text;

namespace DatabaseLibrary;

public class RowUpdateArgs : EventArgs
{
    public Row Row { get; init; }
}

public delegate void RowUpdatedHandler(object sender, RowUpdateArgs args);
public delegate void TableReorganizationHandler(object sender, EventArgs args);

public class Table : IEnumerable<Row>, IQuietEnumerable<Row>, IEquatable<Table>, IComparable<Table>
{
    protected TableHead head;
    protected bool pageReady;
    protected int currentPage;
    protected int pages;
    protected string[] rows = Array.Empty<string>();
    protected IFileSupport fileSupporter;

    public string Name { get; init; }

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

    public static TableProvider<Table> GetTableProvider()
    {
        return (name, fileSupport) => new Table(name, fileSupport);
    }

    protected virtual void InitTable(bool preload)
    {
        if (fileSupporter.GetPageInfo(Name, out TableHead loadedHead) && (pages = fileSupporter.AllPages(Name, loadedHead)) > 0)
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
            rows = lines;
            pageReady = true;
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
                if (LineFormat.GetId(rows[lineNumber]) == id)
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
        if (tmp == null)
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
                if (LineFormat.GetId(rows[lineNumber]) == id)
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

    public virtual string? this[int id]
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
            if (value != null)
            {
                if (FindId(id, out int lineNumber))
                {
                    rows[lineNumber] = value;
                    OnRowUpdated(new RowUpdateArgs { Row = new Row { Line = this[lineNumber], Meta = new RowMeta { PageNumber = currentPage, LineNumber = lineNumber } } });
                    fileSupporter.UpdateLine(Name, currentPage, value, lineNumber);
                }
                else
                {
                    Add(value);
                }
            }
        }
    }

    public virtual string? this[int id, IPropIndex index]
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
            if (value != null)
            {
                if (FindId(id, index, out int lineNumber))
                {
                    rows[lineNumber] = value;
                    OnRowUpdated(new RowUpdateArgs { Row = new Row { Line = this[lineNumber], Meta = new RowMeta { PageNumber = currentPage, LineNumber = lineNumber } } });
                    fileSupporter.UpdateLine(Name, currentPage, value, lineNumber);
                }
                else
                {
                    Add(value);
                }
            }
        }
    }

    public virtual string? this[string id]
    {
        get
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            return this[LineFormat.IdStringToInt(id)];

        }

        set
        {
            this[LineFormat.IdStringToInt(id)] = value;
        }
    }

    public virtual string? this[string id, IPropIndex index]
    {
        get
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            return this[LineFormat.IdStringToInt(id), index];

        }

        set
        {
            this[LineFormat.IdStringToInt(id), index] = value;
        }
    }

    public virtual void Update(string line)
    {
        this[LineFormat.GetId(line)] = line;
    }

    public virtual void Update(string line, IPropIndex index)
    {
        this[LineFormat.GetId(line)] = line;
    }

    public virtual void Add(string line)
    {
        int page = FindFreeSpace();
        fileSupporter.AppendLine(Name, page, line);
        int lineNumber = 0;
        if (page == currentPage)
        {
            lineNumber = rows.Length;
            ArrayUtils.Append(ref rows, line);
        }
        OnRowUpdated(new RowUpdateArgs { Row = new Row { Line = this[lineNumber], Meta = new RowMeta { PageNumber = currentPage, LineNumber = lineNumber } } });
    }

    protected virtual void Remove(int id, int lineNumber)
    {
        fileSupporter.DeleteLine(Name, currentPage, lineNumber);
        ArrayUtils.Delete(ref rows, lineNumber);
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

    public virtual void Remove(string id)
    {
        Remove(LineFormat.IdStringToInt(id));
    }

    public virtual void Remove(string id, IPropIndex index)
    {
        Remove(LineFormat.IdStringToInt(id), index);
    }

    public virtual string[] AllProps(string line)
    {
        return line == null ? Array.Empty<string>() : line.Split(head.Separator);
    }

    public virtual int PropNumber(string line)
    {
        return AllProps(line).Length;
    }

    public virtual string PropAtIndex(string line, int index)
    {
        return AllProps(line)[index];
    }
    public virtual string PropsInRange(string line, int startIndex, int count)
    {
        return string.Join(head.Separator, AllProps(line), startIndex, count);
    }

    public virtual string[] AllProps(Row row)
    {
        return AllProps(row.Line);
    }

    public virtual int PropNumber(Row row)
    {
        return PropNumber(row.Line);
    }

    public virtual string PropAtIndex(Row row, int index)
    {
        return PropAtIndex(row.Line, index);
    }
    public virtual string PropsInRange(Row row, int startIndex, int count)
    {
        return PropsInRange(row.Line, startIndex, count);
    }

    public virtual void Clear()
    {
        pageReady = false;
        rows = Array.Empty<string>();
        OnTableCleared(new EventArgs());
    }

    public virtual string PageToString()
    {
        return string.Join(head.Separator, rows);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (Row r in this)
        {
            sb.Append(r.Line);
            sb.Append('\n');
        }
        return sb.ToString();
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

    public bool Equals(Table? other)
    {
        return other != null && this.Name == other.Name && head.Equals(other.head);
    }

    public int CompareTo(Table? other)
    {
        return (other == null) ? -1 : Name.CompareTo(other.Name);
    }
}
