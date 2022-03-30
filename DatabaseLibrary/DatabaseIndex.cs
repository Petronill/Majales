using DatabaseDefinitions;
using FileSupportLibrary;
using System.Collections;
using System.Text;

namespace DatabaseLibrary;

public delegate T TableProvider<T>(string name, IFileSupport fileSupporter) where T : Table;

public class TableUpdateArgs : EventArgs
{
    public string Name { get; init; }
}

public delegate void TableUpdatedHandler(object sender, TableUpdateArgs e);
public delegate void DatabaseIndexReorganizationHandler(object sender, EventArgs e);

public class DatabaseIndex<T> : ILazyIndex<string, T>, IEnumerable<T>, IEnumerable<KeyValuePair<string, T>>, IQuietEnumerable<string, T> where T: Table
{
    private readonly Dictionary<string, T> tables = new();
    protected IFileSupport fileSupporter;
    protected TableProvider<T> provider;

    public event TableUpdatedHandler? TableCreated;
    public event TableUpdatedHandler? TableUpdated;
    public event TableUpdatedHandler? TableRequested;
    public event TableUpdatedHandler? TableRemoved;
    public event TableUpdatedHandler? TableDeleted;
    public event DatabaseIndexReorganizationHandler? DatabaseIndexCleared;

    protected void OnTableCreated(TableUpdateArgs args) => TableCreated?.Invoke(this, args);
    protected void OnTableUpdated(TableUpdateArgs args) => TableUpdated?.Invoke(this, args);
    protected void OnTableRequested(TableUpdateArgs args) => TableRequested?.Invoke(this, args);
    protected void OnTableRemoved(TableUpdateArgs args) => TableRemoved?.Invoke(this, args);
    protected void OnTableDeleted(TableUpdateArgs args) => TableDeleted?.Invoke(this, args);
    protected void OnDatabaseIndexCleared(EventArgs args) => DatabaseIndexCleared?.Invoke(this, args);

    public DatabaseIndex(IFileSupport fileSupport, TableProvider<T> tableProvider)
    {
        fileSupporter = fileSupport;
        provider = tableProvider;
    }

    protected bool ContainsTable(string name)
    {
        return tables.ContainsKey(name);
    }

    protected bool ContainsTable(T table)
    {
        return tables.ContainsValue(table);
    }

    protected T? TryLoad(string name)
    {
        if (fileSupporter.ExistsTable(name))
        {
            try
            {
                T loaded = provider(name, fileSupporter);
                if (tables.TryAdd(name, loaded))
                {
                    tables.Remove(name);
                    tables.TryAdd(name, loaded);
                }
                return loaded;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }
        return null;
    }

    public T? this[string name]
    {
        get
        {
            OnTableRequested(new TableUpdateArgs { Name = name });
            if (ContainsTable(name))
            {
                return tables[name];
            }
            return TryLoad(name);
        }
    }

    public string? this[string name, int id]
    {
        get
        {
            return this[name]?[id];
        }
    }

    public string? this[string name, string id]
    {
        get
        {
            return this[name]?[id];
        }
    }

    public string? this[string name, int id, IPropIndex index]
    {
        get
        {
            return this[name]?[id, index];
        }
    }

    public string? this[string name, string id, IPropIndex index]
    {
        get
        {
            return this[name]?[id, index];
        }
    }

    public T? Add(string tableName, TableHead head)
    {
        T? t = tables[tableName];
        if (t != null)
        {
            OnTableUpdated(new TableUpdateArgs { Name = tableName });
            return t;
        }
        return Create(tableName, head);
    }

    public bool TryCreate(string tableName, TableHead head)
    {
        return this[tableName] == null && fileSupporter.CreateTable(tableName, head);
    }

    public T? Create(string tableName, TableHead head)
    {
        if (TryCreate(tableName, head))
        {
            try
            {
                OnTableCreated(new TableUpdateArgs { Name = tableName });
                return this[tableName];
            }
            catch (Exception)
            {
                fileSupporter.DeleteTable(tableName);
            }
        }
        return null;
    }

    public T? Remove(string tableName)
    {
        T? stara = this[tableName];
        if (stara != null)
        {
            tables.Remove(tableName);
            OnTableRemoved(new TableUpdateArgs { Name = tableName });
        }
        return stara;
    }

    public T? Delete(string tableName)
    {
        T? stara = this[tableName];
        if (stara != null && fileSupporter.DeleteTable(tableName))
        {
            tables.Remove(tableName);
            OnTableDeleted(new TableUpdateArgs { Name = tableName });
        }
        return stara;
    }

    public int HowManyTables()
    {
        return fileSupporter.AllTables().Length;
    }

    private void TryLoadAll()
    {
        foreach (string t in fileSupporter.AllTables())
        {
            TryLoad(t);
        }
    }

    public void Clear()
    {
        tables.Clear();
        OnDatabaseIndexCleared(EventArgs.Empty);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (T t in this)
        {
            sb.Append(t.Name);
            sb.Append('\n');
        }
        return sb.ToString();
    }

    public IEnumerator<T> GetEnumerator()
    {
        TryLoadAll();
        return tables.Values.GetEnumerator();
    }

    IEnumerator<KeyValuePair<string, T>> IEnumerable<KeyValuePair<string, T>>.GetEnumerator()
    {
        TryLoadAll();
        return tables.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<KeyValuePair<string, T>> GetQuietENumerator()
    {
        return tables.GetEnumerator();
    }

    IEnumerator<T> IQuietEnumerable<T>.GetQuietEnumerator()
    {
        return tables.Values.GetEnumerator();
    }
}
