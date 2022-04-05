using DatabaseDefinitions;
using FileSupportLibrary;
using LogicalDatabaseLibrary;
using System.Collections;
using System.Text;

namespace DatabaseLibrary.Indexes;

public delegate void DatabaseIndexReorganizationHandler(object sender, EventArgs e);

public class DatabaseIndex<T> : ILazyIndex<string, T>, IEnumerable<T>, IEnumerable<KeyValuePair<string, T>>, IQuietEnumerable<string, T> where T: Table
{
    private readonly Dictionary<string, T> tables = new();
    protected IFileSupport fileSupporter;
    protected TableProvider<T> provider;

    public string Path { get => fileSupporter.Workspace; }

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

    public DatabaseIndex(IFileSupport fileSupport, TableProvider<T> newTableProvider)
    {
        fileSupporter = fileSupport;
        provider = newTableProvider;
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

    public TableLine? this[string name, int id]
    {
        get
        {
            return this[name]?[id];
        }
    }

    public TableLine? this[string name, int id, IPropIndex index]
    {
        get
        {
            return this[name]?[id, index];
        }
    }

    public T? Add(TableMeta meta)
    {
        T? t = tables[meta.TableName];
        if (t is not null)
        {
            OnTableUpdated(new TableUpdateArgs { Name = meta.TableName });
            return t;
        }
        return Create(meta);
    }

    public bool TryCreate(TableMeta meta)
    {
        return meta.TableName.Trim().Length != 0 && this[meta.TableName] is null && fileSupporter.CreateTable(meta);
    }

    public T? Create(TableMeta meta)
    {
        if (TryCreate(meta))
        {
            try
            {
                OnTableCreated(new TableUpdateArgs { Name = meta.TableName });
                return this[meta.TableName];
            }
            catch (Exception)
            {
                fileSupporter.DeleteTable(meta.TableName);
            }
        }
        return null;
    }

    public T? Remove(string tableName)
    {
        T? stara = this[tableName];
        if (stara is not null)
        {
            tables.Remove(tableName);
            OnTableRemoved(new TableUpdateArgs { Name = tableName });
        }
        return stara;
    }

    public T? Delete(string tableName)
    {
        T? stara = this[tableName];
        if (stara is not null && fileSupporter.DeleteTable(tableName))
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
