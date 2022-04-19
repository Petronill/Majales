using DatabaseDefinitions;
using DatabaseLibrary;
using DatabaseLibrary.Indexes;
using LogicalDatabaseLibrary;
using FileSupportLibrary;
using MiscLibrary;
using MiscLibrary.Sanitizing;

namespace ClientLibrary;

public class SimpleManager : IDatabaseManager
{
    protected IndexTree<DatabaseIndex<BufferedTable>, string, BufferedTable> databases = new();
    protected SimpleDomainFactory<BufferedTable> domainFactory;
    protected IDatabaseSupport supporter;

    public SimpleManager()
    {
        supporter = new DatabaseSupporter();
        domainFactory = new(databases);
    }

    public SimpleManager(IDatabaseSupport databaseSupporter)
    {
        supporter = databaseSupporter;
        domainFactory = new(databases);
    }

    protected static void AssertSanitizedTableLine(BufferedTable table, TableLine line)
    {
        if (!table.Check(line))
        {
            throw new UnsanitizedInputException();
        }
    }

    protected static TableSelector TableSelectorExtension(TableSelector tblsel, int attrIndex, object? value)
    {
        return (tbl) =>
        {
            return tblsel(tbl) && tbl.Meta.Head.Entity[attrIndex] is not null && tbl.Meta.Head.Entity[attrIndex].Check(value);
        };
    }

    protected static TableSelector TableSelectorExtension(TableSelector tblsel, string attrName, object? value)
    {
        return (tbl) =>
        {
            return tblsel(tbl) && tbl.Meta.Head.Entity[attrName] is not null && tbl.Meta.Head.Entity[attrName].Check(value);
        };
    }

    public virtual bool NewDatabase(DatabaseMeta dtbmeta)
    {
        if (databases.ContainsKey(dtbmeta.Name) || !supporter.Create(dtbmeta))
        {
            return false;
        }

        databases.Add(dtbmeta.Name, new DatabaseIndex<BufferedTable>(new FileSupporter(dtbmeta.Path), BufferedTable.GetTableProvider(1)));
        return true;
    }

    public virtual int AddLine(DatabaseSelector dtbsel, TableSelector tblsel, TableLine line)
    {
        int count = 0;
        foreach (var table in domainFactory.GetTableDomain(dtbsel, tblsel))
        {
            AssertSanitizedTableLine(table, line);
            table.Add(line);
            count++;
        }

        return count;
    }

    public virtual int AddTable(DatabaseSelector dtbsel, TableMeta table)
    {
        int count = 0;
        foreach (var dtb in domainFactory.GetDatabaseDomain(dtbsel))
        {
            if (dtb.Add(table) is not null)
            {
                count++;
            }
        }
        return count;
    }

    public virtual int CloseDatabase(DatabaseSelector dtbsel)
    {
        int count = 0;
        foreach (var dtb in domainFactory.GetDatabaseMetaDomain(dtbsel))
        {
            if (databases.Remove(dtb.Name) is not null)
            {
                count++;
            }
        }
        return count;
    }

    public virtual int DeleteDatabase(DatabaseSelector dtbsel)
    {
        int count = 0;
        foreach (var meta in domainFactory.GetDatabaseMetaDomain(dtbsel))
        {
            if (databases.Remove(meta.Name) is not null)
            {
                supporter.Delete(meta);
                count++;
            }
        }
        return count;
    }

    public virtual int DeleteLine(DatabaseSelector dtbsel, TableSelector tblsel, RowSelector rowsel)
    {
        int count = 0;
        foreach (var table in domainFactory.GetTableDomain(dtbsel, tblsel))
        {
            foreach (var row in domainFactory.GetRowDomain(table, rowsel))
            {
                if (table.Remove(row.Line.GetId()))
                {
                    count++;
                }
            }
        }
        return count;
    }

    public virtual int DeleteLine(DatabaseSelector dtbsel, TableSelector tblsel, int lineId)
    {
        int count = 0;
        foreach (var table in domainFactory.GetTableDomain(dtbsel, tblsel))
        {
            if (table.Remove(lineId))
            {
                count++;
            }
        }
        return count;
    }

    public virtual int DeleteTable(DatabaseSelector dtbsel, TableSelector tblsel)
    {
        int count = 0;
        foreach (var dtb in domainFactory.GetDatabaseDomain(dtbsel))
        {
            foreach (var table in domainFactory.GetTableDomain(dtb, tblsel))
            {
                if (dtb.Delete(table.Name) is not null)
                {
                    count++;
                }
            }
        }
        return count;
    }

    public virtual DatabaseMeta[] GetAllDatabases()
    {
        List<DatabaseMeta> dtbs = new();

        foreach (var dtb in databases)
        {
            dtbs.Add(new() { Name = dtb.Key, Path = dtb.Value.Path });
        }

        return dtbs.ToArray();
    }

    public virtual TableLine[] GetAllLines(DatabaseSelector dtbsel, TableSelector tblsel)
    {
        List<TableLine> lines = new();

        foreach (var table in domainFactory.GetTableDomain(dtbsel, tblsel))
        {
            foreach (Row row in table)
            {
                lines.Add(row.Line);
            }
        }

        return lines.ToArray();
    }

    public virtual Table[] GetAllTables(DatabaseSelector dtbsel)
    {
        List<Table> tables = new();

        foreach (var dtb in domainFactory.GetDatabaseDomain(dtbsel))
        {
            foreach (var table in dtb)
            {
                tables.Add(table);
            }
        }

        return tables.ToArray();
    }

    public virtual DatabaseMeta[] GetDatabases(DatabaseSelector dtbsel)
    {
        List<DatabaseMeta> dtbs = new();

        foreach (var meta in domainFactory.GetDatabaseMetaDomain(dtbsel))
        {
            dtbs.Add(meta);
        }

        return dtbs.ToArray();
    }

    public virtual Table[] GetDatabaseTables(DatabaseSelector dtbsel, TableSelector tblsel)
    {
        List<Table> tables = new();

        foreach (var table in domainFactory.GetTableDomain(dtbsel, tblsel))
        {
            tables.Add(table);
        }

        return tables.ToArray();
    }

    public virtual TableLine[] GetTableLines(DatabaseSelector dtbsel, TableSelector tblsel, RowSelector rowsel)
    {
        List<TableLine> lines = new();

        foreach (Row row in domainFactory.GetRowDomain(dtbsel, tblsel, rowsel))
        {
            lines.Add(row.Line);
        }

        return lines.ToArray();
    }

    public virtual TableLine? GetLine(string databaseName, string tableName, int lineId)
    {
        if (databases.ContainsKey(databaseName))
        {
            BufferedTable? table = databases[databaseName]?[tableName];
            if (table is not null)
            {
                return table[lineId];
            }
        }

        return null;
    }

    public virtual T? GetAttr<T>(string databaseName, string tableName, int lineId, AttrSeparator<T> attrsep)
    {
        TableLine? line = GetLine(databaseName, tableName, lineId);
        return (line is not null) ? attrsep(line) : default;
    }

    public virtual bool OpenDatabase(DatabaseMeta dtbmeta)
    {
        if (databases.ContainsKey(dtbmeta.Name) || !supporter.Exists(dtbmeta))
        {
            return false;
        }

        databases.Add(dtbmeta.Name, new DatabaseIndex<BufferedTable>(new FileSupporter(dtbmeta.Path), BufferedTable.GetTableProvider()));
        return true;
    }

    public virtual int SetLine(DatabaseSelector dtbsel, TableSelector tblsel, TableLine line)
    {
        int count = 0;
        foreach (var table in domainFactory.GetTableDomain(dtbsel, tblsel))
        {
            AssertSanitizedTableLine(table, line);
            table[line.GetId()] = line;
            count++;
        }
        return count;
    }

    public virtual int UpdateLine(DatabaseSelector dtbsel, TableSelector tblsel, TableLine line)
    {
        int count = 0;
        foreach (var table in domainFactory.GetTableDomain(dtbsel, tblsel))
        {
            AssertSanitizedTableLine(table, line);
            table.Update(line);
            count++;
        }
        return count;
    }

    public virtual int SetAttr(DatabaseSelector dtbsel, TableSelector tblsel, RowSelector rowsel, int attrIndex, object? value)
    {
        int count = 0;
        foreach (var table in domainFactory.GetTableDomain(dtbsel, TableSelectorExtension(tblsel, attrIndex, value)))
        {
            foreach (var row in domainFactory.GetRowDomain(table, rowsel))
            {
                TableLine line = row.Line;
                line[attrIndex] = value;
                table[line.GetId()] = line;
                count++;
            }
        }
        return count;
    }

    public virtual int SetAttr(DatabaseSelector dtbsel, TableSelector tblsel, int lineId, int attrIndex, object? value)
    {
        int count = 0;
        foreach (var table in domainFactory.GetTableDomain(dtbsel, TableSelectorExtension(tblsel, attrIndex, value)))
        {
            TableLine? r = table?[lineId];
            if (r is not null)
            {
                r[attrIndex] = value;
                table[lineId] = r;
                count++;
            }
        }
        return count;
    }

    public virtual int SetAttr(DatabaseSelector dtbsel, TableSelector tblsel, RowSelector rowsel, string attrName, object? value)
    {
        int count = 0;
        foreach (var table in domainFactory.GetTableDomain(dtbsel, TableSelectorExtension(tblsel, attrName, value)))
        {
            foreach (var row in domainFactory.GetRowDomain(table, rowsel))
            {
                TableLine line = row.Line;
                line[table.Meta.Head.Entity.GetIndex(attrName)] = value;
                table[line.GetId()] = line;
                count++;
            }
        }
        return count;
    }

    public virtual int SetAttr(DatabaseSelector dtbsel, TableSelector tblsel, int lineId, string attrName, object? value)
    {
        int count = 0;
        foreach (var table in domainFactory.GetTableDomain(dtbsel, TableSelectorExtension(tblsel, attrName, value)))
        {
            TableLine? r = table?[lineId];
            if (r is not null)
            {
                r[table.Meta.Head.Entity.GetIndex(attrName)] = value;
                table[lineId] = r;
                count++;
            }
        }
        return count;
    }

    public virtual int Contains(DatabaseSelector dtbsel)
    {
        return domainFactory.GetDatabaseDomain(dtbsel).Count();
    }

    public virtual int Contains(DatabaseSelector dtbsel, TableSelector tblsel)
    {
        return domainFactory.GetTableDomain(dtbsel, tblsel).Count();
    }

    public virtual int Contains(DatabaseSelector dtbsel, TableSelector tblsel, RowSelector rowsel)
    {
        return domainFactory.GetRowDomain(dtbsel, tblsel, rowsel).Count();
    }

    public bool Check(Domain<Table> domain, TableLine input)
    {
        foreach (var table in domain)
        {
            if (!table.Check(input))
            {
                return false;
            }
        }
        return true;
    }
}
