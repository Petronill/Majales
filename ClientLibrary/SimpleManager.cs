using DatabaseDefinitions;
using DatabaseLibrary;
using DatabaseLibrary.Indexes;
using LogicalDatabaseLibrary;
using FileSupportLibrary;

namespace ClientLibrary;

public class SimpleManager : IDatabaseManager
{
    protected IndexTree<DatabaseIndex<BufferedTable>, string, BufferedTable> databases = new();
    protected IDatabaseSupport supporter;

    public SimpleManager()
    {
        supporter = new DatabaseSupporter();
    }

    public SimpleManager(IDatabaseSupport databaseSupporter)
    {
        supporter = databaseSupporter;
    }
    
    public bool NewDatabase(DatabaseMeta dtbmeta)
    {
        if (databases.ContainsKey(dtbmeta.Name) || !supporter.Create(dtbmeta))
        {
            return false;
        }

        databases.Add(dtbmeta.Name, new DatabaseIndex<BufferedTable>(new FileSupporter(dtbmeta.Path), BufferedTable.GetTableProvider(1)));
        return true;
    }

    public int AddLine(DatabaseSelector dtbsel, TableSelector tblsel, TableLine line)
    {
        int count = 0;
        foreach (var dtb in databases)
        {
            if (dtbsel(new() { Name = dtb.Key, Path = dtb.Value.Path }))
            {
                foreach (var table in dtb.Value)
                {
                    if (tblsel(table))
                    {
                        table.Add(line);
                        count++;
                    }
                }
            }
        }

        return count;
    }

    public int AddTable(DatabaseSelector dtbsel, TableMeta table)
    {
        int count = 0;
        foreach (var dtb in databases)
        {
            if (dtbsel(new() { Name = dtb.Key, Path = dtb.Value.Path }) && dtb.Value.Add(table) is not null)
            {
                count++;
            }
        }
        return count;
    }

    public int CloseDatabase(DatabaseSelector dtbsel)
    {
        int count = 0;
        foreach (var dtb in databases)
        {
            if (dtbsel(new() { Name = dtb.Key, Path = dtb.Value.Path }) && databases.Remove(dtb.Key) is not null)
            {
                count++;
            }
        }

        return count;
    }

    public int DeleteDatabase(DatabaseSelector dtbsel)
    {
        int count = 0;
        foreach (var dtb in databases)
        {
            DatabaseMeta meta = new() { Name = dtb.Key, Path = dtb.Value.Path };
            if (dtbsel(meta) && databases.Remove(dtb.Key) is not null)
            {
                supporter.Delete(meta);
                count++;
            }
        }

        return count;
    }

    public int DeleteLine(DatabaseSelector dtbsel, TableSelector tblsel, RowSelector rowsel)
    {
        int count = 0;
        foreach (var dtb in databases)
        {
            if (dtbsel(new() { Name = dtb.Key, Path = dtb.Value.Path }))
            {
                foreach (var table in dtb.Value)
                {
                    if (tblsel(table))
                    {
                        foreach (var row in table)
                        {
                            if (rowsel(row) && table.Remove(row.Line.GetId()))
                            {
                                count++;
                            }
                        }
                    }
                }
            }
        }

        return count;
    }

    public int DeleteLine(DatabaseSelector dtbsel, TableSelector tblsel, int lineId)
    {
        int count = 0;
        foreach (var dtb in databases)
        {
            if (dtbsel(new() { Name = dtb.Key, Path = dtb.Value.Path }))
            {
                foreach (var table in dtb.Value)
                {
                    if (tblsel(table) && table.Remove(lineId))
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    public int DeleteTable(DatabaseSelector dtbsel, TableSelector tblsel)
    {
        int count = 0;
        foreach (var dtb in databases)
        {
            if (dtbsel(new() { Name = dtb.Key, Path = dtb.Value.Path }))
            {
                foreach (var table in dtb.Value)
                {
                    if (tblsel(table) && dtb.Value.Delete(table.Name) is not null)
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    public DatabaseMeta[] GetAllDatabases()
    {
        List<DatabaseMeta> dtbs = new();

        foreach (var dtb in databases)
        {
            dtbs.Add(new() { Name = dtb.Key, Path = dtb.Value.Path });
        }

        return dtbs.ToArray();
    }

    public TableLine[] GetAllLines(DatabaseSelector dtbsel, TableSelector tblsel)
    {
        List<TableLine> lines = new();

        foreach (var dtb in databases)
        {
            if (dtbsel(new() { Name = dtb.Key, Path = dtb.Value.Path }))
            {
                foreach (var table in dtb.Value)
                {
                    if (tblsel(table))
                    {
                        foreach (Row row in table)
                        {
                            lines.Add(row.Line);
                        }
                    }
                }
            }
        }

        return lines.ToArray();
    }

    public Table[] GetAllTables(DatabaseSelector dtbsel)
    {
        List<Table> tables = new();

        foreach (var dtb in databases)
        {
            if (dtbsel(new() { Name = dtb.Key, Path = dtb.Value.Path }))
            {
                foreach (var table in dtb.Value)
                {
                    tables.Add(table);
                }
            }
        }

        return tables.ToArray();
    }

    public DatabaseMeta[] GetDatabases(DatabaseSelector dtbsel)
    {
        List<DatabaseMeta> dtbs = new();

        foreach (var dtb in databases)
        {
            DatabaseMeta meta = new() { Name = dtb.Key, Path = dtb.Value.Path };
            if (dtbsel(meta))
            {
                dtbs.Add(meta);
            }
        }

        return dtbs.ToArray();
    }

    public Table[] GetDatabaseTables(DatabaseSelector dtbsel, TableSelector tblsel)
    {
        List<Table> tables = new();

        foreach (var dtb in databases)
        {
            if (dtbsel(new() { Name = dtb.Key, Path = dtb.Value.Path }))
            {
                foreach (var table in dtb.Value)
                {
                    if (tblsel(table))
                    {
                        tables.Add(table);
                    }
                }
            }
        }

        return tables.ToArray();
    }

    public TableLine[] GetTableLines(DatabaseSelector dtbsel, TableSelector tblsel, RowSelector rowsel)
    {
        List<TableLine> lines = new();

        foreach (var dtb in databases)
        {
            if (dtbsel(new() { Name = dtb.Key, Path = dtb.Value.Path }))
            {
                foreach (var table in dtb.Value)
                {
                    if (tblsel(table))
                    {
                        foreach (Row row in table)
                        {
                            if (rowsel(row))
                            {
                                lines.Add(row.Line);
                            }
                        }
                    }
                }
            }
        }

        return lines.ToArray();
    }

    public TableLine? GetLine(string databaseName, string tableName, int lineId)
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

    public T? GetAttr<T>(string databaseName, string tableName, int lineId, AttrSeparator<T> attrsep)
    {
        TableLine? line = GetLine(databaseName, tableName, lineId);
        return (line is not null) ? attrsep(line) : default;
    }

    public bool OpenDatabase(DatabaseMeta dtbmeta)
    {
        if (databases.ContainsKey(dtbmeta.Name) || !supporter.Exists(dtbmeta))
        {
            return false;
        }

        databases.Add(dtbmeta.Name, new DatabaseIndex<BufferedTable>(new FileSupporter(dtbmeta.Path), BufferedTable.GetTableProvider()));
        return true;
    }

    public int SetLine(DatabaseSelector dtbsel, TableSelector tblsel, TableLine line)
    {
        int count = 0;
        foreach (var dtb in databases)
        {
            if (dtbsel(new() { Name = dtb.Key, Path = dtb.Value.Path }))
            {
                foreach (var table in dtb.Value)
                {
                    if (tblsel(table))
                    {
                        table[line.GetId()] = line;
                        count++;
                    }
                }
            }
        }

        return count;
    }

    public int UpdateLine(DatabaseSelector dtbsel, TableSelector tblsel, TableLine line)
    {
        int count = 0;
        foreach (var dtb in databases)
        {
            if (dtbsel(new() { Name = dtb.Key, Path = dtb.Value.Path }))
            {
                foreach (var table in dtb.Value)
                {
                    if (tblsel(table))
                    {
                        table.Update(line);
                        count++;
                    }
                }
            }
        }

        return count;
    }

    public int SetAttr(DatabaseSelector dtbsel, TableSelector tblsel, RowSelector rowsel, int attrIndex, object? value)
    {
        int count = 0;
        foreach (var dtb in databases)
        {
            if (dtbsel(new() { Name = dtb.Key, Path = dtb.Value.Path }))
            {
                foreach (var table in dtb.Value)
                {
                    if (tblsel(table) && table.Meta.Head.Entity[attrIndex] is not null && table.Meta.Head.Entity[attrIndex].Check(value))
                    {
                        foreach (var row in table)
                        {
                            if (rowsel(row))
                            {
                                TableLine line = row.Line;
                                line[attrIndex] = value;
                                table[line.GetId()] = line;
                                count++;
                            }
                        }
                    }
                }
            }
        }

        return count;
    }

    public int SetAttr(DatabaseSelector dtbsel, TableSelector tblsel, int lineId, int attrIndex, object? value)
    {
        int count = 0;
        foreach (var dtb in databases)
        {
            if (dtbsel(new() { Name = dtb.Key, Path = dtb.Value.Path }))
            {
                foreach (var table in dtb.Value)
                {
                    if (tblsel(table) && table.Meta.Head.Entity[attrIndex] is not null && table.Meta.Head.Entity[attrIndex].Check(value))
                    {
                        TableLine? r = table?[lineId];
                        if (r is not null)
                        {
                            r[attrIndex] = value;
                            table[lineId] = r;
                            count++;
                        }
                    }
                }
            }
        }

        return count;
    }

    public int SetAttr(DatabaseSelector dtbsel, TableSelector tblsel, RowSelector rowsel, string attrName, object? value)
    {
        int count = 0;
        foreach (var dtb in databases)
        {
            if (dtbsel(new() { Name = dtb.Key, Path = dtb.Value.Path }))
            {
                foreach (var table in dtb.Value)
                {
                    if (tblsel(table) && table.Meta.Head.Entity[attrName] is not null && table.Meta.Head.Entity[attrName].Check(value))
                    {
                        foreach (var row in table)
                        {
                            if (rowsel(row))
                            {
                                TableLine line = row.Line;
                                line[table.Meta.Head.Entity.GetIndex(attrName)] = value;
                                table[line.GetId()] = line;
                                count++;
                            }
                        }
                    }
                }
            }
        }

        return count;
    }

    public int SetAttr(DatabaseSelector dtbsel, TableSelector tblsel, int lineId, string attrName, object? value)
    {
        int count = 0;
        foreach (var dtb in databases)
        {
            if (dtbsel(new() { Name = dtb.Key, Path = dtb.Value.Path }))
            {
                foreach (var table in dtb.Value)
                {
                    if (tblsel(table) && table.Meta.Head.Entity[attrName] is not null && table.Meta.Head.Entity[attrName].Check(value))
                    {
                        TableLine? r = table?[lineId];
                        if (r is not null)
                        {
                            r[table.Meta.Head.Entity.GetIndex(attrName)] = value;
                            table[lineId] = r;
                            count++;
                        }
                    }
                }
            }
        }

        return count;
    }

    public int Contains(DatabaseSelector dtbsel)
    {
        int count = 0;
        foreach (var dtb in databases)
        {
            if (dtbsel(new() { Name = dtb.Key, Path = dtb.Value.Path }))
            {
                count++;
            }
        }

        return count;
    }

    public int Contains(DatabaseSelector dtbsel, TableSelector tblsel)
    {
        int count = 0;
        foreach (var dtb in databases)
        {
            if (dtbsel(new() { Name = dtb.Key, Path = dtb.Value.Path }))
            {
                foreach (var table in dtb.Value)
                {
                    if (tblsel(table))
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    public int Contains(DatabaseSelector dtbsel, TableSelector tblsel, RowSelector rowsel)
    {
        int count = 0;
        foreach (var dtb in databases)
        {
            if (dtbsel(new() { Name = dtb.Key, Path = dtb.Value.Path }))
            {
                foreach (var table in dtb.Value)
                {
                    if (tblsel(table))
                    {
                        foreach (var row in table)
                        {
                            if (rowsel(row))
                            {
                                count++;
                            }
                        }
                    }
                }
            }
        }

        return count;
    }
}
