﻿using DatabaseDefinitions;

namespace FileSupportLibrary;

public class FileSupporter : IFileSupport
{
    protected string pthspr = "\\";   //path filename separator
    protected string flext = ".csv";  //common page file extension
    protected string inffl = "info.bin";  //info file
    protected int defaultStartPage = 0;
    public string PathSeparator { get => pthspr; set => pthspr = value; }
    public string FileExtension { get => flext; set => flext = value; }
    public string InfoFile { get => inffl; set => inffl = value; }
    public int DefaultStartPage { get => defaultStartPage; set => defaultStartPage = 0; }
    public string Workspace { get; set; }

    public FileSupporter(string workdir)
    {
        Workspace = workdir;
    }

    protected string FullPageName(string tableName, int page)
    {
        return FullTableName(tableName) + pthspr + page + flext;
    }

    protected string FullInfofileName(string tableName)
    {
        return FullTableName(tableName) + pthspr + inffl;
    }

    protected string FullTableName(string tableName)
    {
        return Workspace + pthspr + tableName;
    }

    public int AllPages(TableMeta tableMeta)
    {
        int count = 0;

        while (ExistsPage(tableMeta.TableName, tableMeta.Head.StartPage + count))
        {
            count++;
        }
        return count;
    }

    public string[] AllTables()
    {
        LinkedList<string> tables = new();
        foreach (string dir in Directory.GetDirectories(Workspace))
        {
            string[] tmps = dir.Split(pthspr);
            string tmp = tmps[tmps.Length - 1];
            if (ExistsTable(tmp))
            {
                tables.AddLast(tmp);
            }
        }
        return tables.ToArray();
    }

    public bool ExistsFile(string filename)
    {
        return File.Exists(Workspace + pthspr + filename);
    }

    public bool ExistsDirectory(string name)
    {
        return Directory.Exists(Workspace + pthspr + name);
    }

    public bool ExistsTable(string name)
    {
        return ExistsDirectory(name) && ExistsFile(name + pthspr + inffl);
    }

    public bool ExistsPage(string tableName, int page)
    {
        return ExistsTable(tableName) && ExistsFile(tableName + pthspr + page + flext);
    }

    public bool GetInfo(string tableName, out TableHead head)
    {
        head = new();

        if (!ExistsTable(tableName))
        {
            return false;
        }

        try
        {
            head = IFileSupport.BinaryDeserialize<TableHead>(FullInfofileName(tableName));
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public bool UpdateInfo(TableMeta tableMeta)
    {
        if (!ExistsTable(tableMeta.TableName))
        {
            return false;
        }

        try
        {
            IFileSupport.BinarySerialize(FullInfofileName(tableMeta.TableName), tableMeta.Head);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public bool CreateTable(TableMeta tableMeta)
    {
        if (!ExistsTable(tableMeta.TableName))
        {
            try
            {
                Directory.CreateDirectory(FullTableName(tableMeta.TableName));
                File.Create(FullTableName(tableMeta.TableName) + pthspr + inffl);
                IFileSupport.BinarySerialize(FullInfofileName(tableMeta.TableName), tableMeta.Head);
                File.Create(FullPageName(tableMeta.TableName, tableMeta.Head.StartPage));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        return false;
    }

    public bool DeleteTable(string tableName)
    {
        if (ExistsTable(tableName))
        {
            try
            {
                Directory.Delete(FullTableName(tableName), true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        return false;
    }

    public int DeleteTables(string[] tableNames)
    {
        int count = 0;
        foreach (string t in tableNames)
        {
            if (DeleteTable(t))
            {
                count++;
            }
        }
        return count;
    }

    public bool GetLines(string tableName, int page, out string[] lines)
    {
        lines = Array.Empty<string>();
        if (!ExistsPage(tableName, page))
        {
            return false;
        }

        LinkedList<string> lineList = new();
        try
        {
            using StreamReader sr = File.OpenText(FullPageName(tableName, page));
            string? line = sr.ReadLine();
            while (line is not null)
            {
                lineList.AddLast(line);
                line = sr.ReadLine();
            }
        }
        catch (Exception)
        {
            return false;
        }
        lines = lineList.ToArray();

        return true;
    }

    public bool GetPageLines(string tableName, int page, out string[] lines)
    {
        bool res = GetLines(tableName, page, out lines);
        if (!res && !ExistsPage(tableName, page))
        {
            try
            {
                File.Create(FullPageName(tableName, page));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        return res;
    }

    public bool IsPageEmpty(string tableName, int page)
    {
        return !ExistsPage(tableName, page) || (new FileInfo(FullPageName(tableName, page)).Length == 0);
    }

    public int AllLines(string tableName, int page)
    {
        int count = 0;
        if (ExistsPage(tableName, page))
        {

            try
            {
                using StreamReader sr = File.OpenText(FullPageName(tableName, page));
                string? line;
                while ((line = sr.ReadLine()) is not null)
                {
                    if (line.Trim().Length > 0) { 
                        count++;
                    }
                }
            }
            catch (Exception)
            {
                return count;
            }
        }

        return count;
    }

    public bool AppendLine(string tableName, int page, string line)
    {
        if (!ExistsPage(tableName, page))
        {
            try
            {
                using StreamWriter sw = File.CreateText(FullPageName(tableName, page));
                sw.WriteLine(line);
            }
            catch (Exception)
            {
                return false;
            }
        }
        else
        {
            try
            {
                using StreamWriter sw = File.AppendText(FullPageName(tableName, page));
                sw.WriteLine(line);
            }
            catch (Exception)
            {
                return false;
            }
        }
        return true;
    }

    public bool UpdateLine(string tableName, int page, string line, int number)
    {
        if (ExistsPage(tableName, page))
        {
            try
            {
                string[] lines = File.ReadAllLines(FullPageName(tableName, page));
                lines[number] = line;
                File.WriteAllLines(FullPageName(tableName, page), lines);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        return false;
    }

    public bool DeleteLine(string tableName, int page, int number)
    {
        if (ExistsPage(tableName, page))
        {
            try
            {
                var tmpFile = Path.GetTempFileName();
                var linesToKeep = File.ReadLines(FullPageName(tableName, page)).Where((l, i) => i != number);
                File.WriteAllLines(tmpFile, linesToKeep);

                File.Delete(FullPageName(tableName, page));
                File.Move(tmpFile, FullPageName(tableName, page));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        return false;
    }
    
    public int AppendLines(string tableName, int page, string[] lines)
    {
        if (!ExistsPage(tableName, page))
        {
            return WriteLines(tableName, page, lines);
        }
        else
        {
            try
            {
                File.AppendAllLines(FullPageName(tableName, page), lines);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        return lines.Length;
    }

    public int WriteLines(string tableName, int page, string[] lines)
    {
        try
        {
            File.WriteAllLines(FullPageName(tableName, page), lines);
        }
        catch (Exception)
        {
            return 0;
        }
        return lines.Length;
    }

    public int DeleteLines(string tableName, int page, int[] numbers)
    {
        if (ExistsPage(tableName, page))
        {
            try
            {
                int it = 0;
                Array.Sort(numbers);
                var linesToKeep = File.ReadLines(FullPageName(tableName, page)).Where((l, i) => { 
                    if (numbers[it] == i)
                    {
                        it++;
                        return false;
                    }
                    return true;
                });

                var tmpFile = Path.GetTempFileName();
                File.WriteAllLines(tmpFile, linesToKeep);

                File.Delete(FullPageName(tableName, page));
                File.Move(tmpFile, FullPageName(tableName, page));
                return numbers.Length;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        return 0;
    }

    public bool DeletePage(string tableName, int page)
    {
        if (ExistsPage(tableName, page))
        {
            try
            {
                File.Delete(FullPageName(tableName, page));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        return false;
    }

    public int DeletePages(string tableName, int[] pages)
    {
        int count = 0;
        foreach (int p in pages)
        {
            if (DeletePage(tableName, p))
            {
                count++;
            }
        }
        return count;
    }

    public bool DeletePageIfEmpty(string tableName, int page)
    {
        return (IsPageEmpty(tableName, page)) ? DeletePage(tableName, page) : false;
    }

    public int DeletePagesIfEmpty(string tableName, int[] pages)
    {
        int count = 0;
        foreach (int page in pages)
        {
            if (DeletePageIfEmpty(tableName, page))
            {
                count++;
            }
        }
        
        return count;
    }

    public int DeleteAllEmptyPages(TableMeta tableMeta)
    {
        return DeletePagesIfEmpty(tableMeta.TableName, Enumerable.Range(tableMeta.Head.StartPage, AllPages(tableMeta)).ToArray());
    }
}
