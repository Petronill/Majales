using DatabaseDefinitions;
using DatabaseLibrary;
using DatabaseLibrary.Indexes;

namespace ClientLibrary;

public class SimpleDomainFactory<T> where T : Table
{
    readonly IndexTree<DatabaseIndex<T>, string, T> databases;

    public SimpleDomainFactory(IndexTree<DatabaseIndex<T>, string, T> databases)
    {
        this.databases = databases;
    }

    public IDatabaseDomain<T> GetDatabaseDomain(DatabaseSelector dtbsel)
    {
        return new DatabaseDomain<T>(databases, dtbsel);
    }

    public IDatabaseMetaDomain GetDatabaseMetaDomain(DatabaseSelector dtbsel)
    {
        return new DatabaseMetaDomain<T>(databases, dtbsel);
    }

    public ITableDomain<T> GetTableDomain(DatabaseSelector dtbsel, TableSelector tblsel)
    {
        return new TableDomain<T>(GetDatabaseDomain(dtbsel), tblsel);
    }

    public ITableDomain<T> GetTableDomain(DatabaseIndex<T> dtb, TableSelector tblsel)
    {
        return new SingleTableDomain<T>(dtb, tblsel);
    }

    public IRowDomain GetRowDomain(DatabaseSelector dtbsel, TableSelector tblsel, RowSelector rowsel)
    {
        return new RowDomain<T>(GetTableDomain(dtbsel, tblsel), rowsel);
    }

    public IRowDomain GetRowDomain(DatabaseIndex<T> dtb, TableSelector tblsel, RowSelector rowsel)
    {
        return new RowDomain<T>(GetTableDomain(dtb, tblsel), rowsel);
    }

    public IRowDomain GetRowDomain(T tbl, RowSelector rowsel)
    {
        return new SingleRowDomain(tbl, rowsel);
    }
}
