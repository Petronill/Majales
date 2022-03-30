using DatabaseDefinitions;
using FileSupportLibrary;
using System.Collections;
using System.Text;

namespace DatabaseLibrary;

public delegate T TableProvider<T>(string name, IFileSupport fileSupporter) where T : Table;

public class DatabaseIndex<T> : ILazyIndex<string, T>, IEnumerable<T>, IEnumerable<KeyValuePair<string, T>>, IQuietEnumerable<string, T> where T: Table
{
    private readonly Dictionary<string, T> tables = new();
    protected IFileSupport fileSupporter;
    protected TableProvider<T> provider;

    public DatabaseIndex(IFileSupport fileSupport, TableProvider<T> tableProvider)
    {
        fileSupporter = fileSupport;
        provider = tableProvider;
    }

    public T? this[string name]
    {
        get
        {
            if (tables.TryGetValue(name, out var table))
            {
                return table;
            }
            return TryLoad(name);
        }
    }

    public T? Add(string tableName, TableHead head)
    {
        T? t = tables[tableName];
        if (t != null)
        {
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
        }
        return stara;
    }

    public T? Delete(string tableName)
    {
        T? stara = this[tableName];
        if (stara != null && fileSupporter.DeleteTable(tableName))
        {
            tables.Remove(tableName);
        }
        return stara;
    }

    private T? TryLoad(string name)
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
