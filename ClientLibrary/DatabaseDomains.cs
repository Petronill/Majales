using MiscLibrary;
using DatabaseDefinitions;
using DatabaseLibrary;
using DatabaseLibrary.Indexes;
using System.Collections;

namespace ClientLibrary;

public interface IDatabaseDomain<T> : Domain<DatabaseIndex<T>> where T : Table
{
    public new abstract IEnumerator<DatabaseIndex<T>> GetEnumerator();
}

public class DatabaseDomain<T> : IDatabaseDomain<T> where T : Table
{
    readonly IndexTree<DatabaseIndex<T>, string, T> databases;
    readonly DatabaseSelector dtbsel;

    public DatabaseDomain(IndexTree<DatabaseIndex<T>, string, T> databases, DatabaseSelector dtbsel)
    {
        this.databases = databases;
        this.dtbsel = dtbsel;
    }

    public virtual IEnumerator<DatabaseIndex<T>> GetEnumerator()
    {
        foreach (var dtb in databases)
        {
            if (dtbsel(new() { Name = dtb.Key, Path = dtb.Value.Path }))
            {
                yield return dtb.Value;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}

public interface IDatabaseMetaDomain : Domain<DatabaseMeta>
{
    public new abstract IEnumerator<DatabaseMeta> GetEnumerator();
}

public class DatabaseMetaDomain<T> : IDatabaseMetaDomain where T : Table
{
    readonly IndexTree<DatabaseIndex<T>, string, T> databases;
    readonly DatabaseSelector dtbsel;

    public DatabaseMetaDomain(IndexTree<DatabaseIndex<T>, string, T> databases, DatabaseSelector dtbsel)
    {
        this.databases = databases;
        this.dtbsel = dtbsel;
    }

    public virtual IEnumerator<DatabaseMeta> GetEnumerator()
    {
        foreach (var dtb in databases)
        {
            DatabaseMeta dtbMeta = new() { Name = dtb.Key, Path = dtb.Value.Path };
            if (dtbsel(dtbMeta))
            {
                yield return dtbMeta;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}

public interface ITableDomain<T> : Domain<T> where T : Table
{
    public new abstract IEnumerator<T> GetEnumerator();
}

public class TableDomain<T> : ITableDomain<T> where T : Table
{
    readonly IDatabaseDomain<T> domain;
    readonly TableSelector tblsel;

    public TableDomain(IDatabaseDomain<T> domain, TableSelector tblsel)
    {
        this.domain = domain;
        this.tblsel = tblsel;
    }

    public virtual IEnumerator<T> GetEnumerator()
    {
        foreach (var dtb in domain)
        {
            foreach (T table in dtb)
            {
                if (tblsel(table))
                {
                    yield return table;
                }
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}

public class SingleTableDomain<T> : ITableDomain<T> where T : Table
{
    readonly DatabaseIndex<T> dtb;
    readonly TableSelector tblsel;

    public SingleTableDomain(DatabaseIndex<T> dtb, TableSelector tblsel)
    {
        this.dtb = dtb;
        this.tblsel = tblsel;
    }

    public virtual IEnumerator<T> GetEnumerator()
    {
        foreach (T table in dtb)
        {
            if (tblsel(table))
            {
                yield return table;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}

public interface IRowDomain : Domain<Row>
{
    public new abstract IEnumerator<Row> GetEnumerator();
}

public class RowDomain<T> : IRowDomain where T : Table
{
    readonly ITableDomain<T> domain;
    readonly RowSelector rowsel;

    public RowDomain(ITableDomain<T> domain, RowSelector rowsel)
    {
        this.domain = domain;
        this.rowsel = rowsel;
    }

    public virtual IEnumerator<Row> GetEnumerator()
    {
        foreach (T table in domain)
        {
            foreach (Row r in table)
            {
                if (rowsel(r))
                {
                    yield return r;
                }
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}

public class SingleRowDomain : IRowDomain
{
    readonly Table table;
    readonly RowSelector rowsel;

    public SingleRowDomain(Table table, RowSelector rowsel)
    {
        this.table = table;
        this.rowsel = rowsel;
    }

    public virtual IEnumerator<Row> GetEnumerator()
    {
        foreach (Row r in table)
        {
            if (rowsel(r))
            {
                yield return r;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}