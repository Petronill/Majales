using ClientLibrary;
using LogicalDatabaseLibrary;
using DatabaseDefinitions;

namespace PasswordManagementLibrary;

public class LocalPasswordStorage : IPasswordStorage
{
    private readonly IDatabaseManager dtb;
    private static readonly string dtbName = ".secrets";
    private static readonly string tblSalts = ".salts";
    private static readonly string attrSalt = "salt";
    private static readonly string tblHashes = ".hashes";
    private static readonly string attrHash = "hash";

    public LocalPasswordStorage(IDatabaseManager manager)
    {
        dtb = manager;
        OpenPasswordDatabase();
    }

    private void OpenPasswordDatabase()
    {
        if (!dtb.OpenDatabase(new() { Name = dtbName, Path = Path.Combine(GlobalDefinitions.WorkingDirectory, dtbName) })
            && !CreatePasswordDatabase())
        {
            throw new FileNotFoundException("Unable to open password database");
        }
        if (dtb.Contains((dm) => dm.Name == dtbName, (tm) => tm.Name == tblSalts) < 1
            && !CreateTable(tblSalts, attrSalt))
        {
            throw new FileNotFoundException("Unable to open password database");
        }
        if (dtb.Contains((dm) => dm.Name == dtbName, (tm) => tm.Name == tblHashes) < 1
            && !CreateTable(tblHashes, attrHash))
        {
            throw new FileNotFoundException("Unable to open password database");
        }
    }

    private bool CreatePasswordDatabase()
    {
        return dtb.NewDatabase(new() { Name = dtbName, Path = Path.Combine(GlobalDefinitions.WorkingDirectory, dtbName) })
            && CreateTable(tblSalts, attrSalt) && CreateTable(tblHashes, attrHash);
    }

    private bool CreateTable(string tblName, string attrName)
    {
        return dtb.AddTable(
            (dm) => dm.Name == ".secrets",
            new()
            {
                TableName = tblName,
                Head = new()
                {
                    Separator = ";",
                    LineLimit = 100,
                    MaxId = 0,
                    StartPage = 0,
                    Entity = new TableEntity(new Attr("username"), new Attr(attrName))
                }
            }
        ) > 0;
    }

    public virtual TableLine? GetUserData(string tblName, string username)
    {
        TableLine[] lines = dtb.GetTableLines(
           (dm) => dm.Name == dtbName,
           (tm) => tm.Name == tblName,
           (r) => (string)r.Line[1] == username
        );

        return (lines.Length > 0) ? lines[0] : null;
    }

    public virtual string? GetSalt(string username)
    {
        return (string?)GetUserData(tblSalts, username)?[2];
    }

    public virtual string? GetHash(string username)
    {
        return (string?)GetUserData(tblHashes, username)?[2];
    }

    public virtual bool AddSalt(string username, string salt)
    {
        return dtb.AddLine(
                (dm) => dm.Name == dtbName,
                (tm) => tm.Name == tblSalts,
                new(0, username, salt)
            ) > 0;
    }

    public virtual bool UpdateHash(string username, string hash)
    {
        return dtb.SetAttr(
                (dm) => dm.Name == dtbName,
                (tm) => tm.Name == tblHashes,
                (r) => (string)r.Line[1] == username,
                2, hash
            ) > 0
            || dtb.AddLine(
                (dm) => dm.Name == dtbName,
                (tm) => tm.Name == tblHashes,
                new(0, username, hash)
            ) > 0;
    }
}
