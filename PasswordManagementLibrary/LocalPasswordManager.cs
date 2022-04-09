using System.Security.Cryptography;
using System.Text;
using ClientLibrary;
using LogicalDatabaseLibrary;

namespace PasswordManagementLibrary;

public class LocalPasswordManager : IPasswordManager
{
    private static readonly int saltLength = 32;
    private readonly IDatabaseManager dtb;
    private static readonly string dtbName = ".secrets";
    private static readonly string tblSalts = ".salts";
    private static readonly string tblHashes = ".hashes";

    public int MinLength { get => 8; }

    public LocalPasswordManager() : this(new SimpleManager())
    {
    }

    public LocalPasswordManager(IDatabaseManager manager) {
        dtb = manager;
        if (!dtb.OpenDatabase(new() { Name = dtbName, Path = Path.Combine(Directory.GetCurrentDirectory(), dtbName) }))
        {
            throw new FileNotFoundException("Unable to open password database");
        }
    }
    
    private static string ComputeHash(string data)
    {
        using SHA512 sha512 = SHA512.Create();
        return Convert.ToBase64String(sha512.ComputeHash(Encoding.UTF8.GetBytes(data)));
    }

    private static string ComputeHash(string password, string salt)
    {
        return ComputeHash(password + salt);
    }

    private TableLine? GetUserData(string tblName, string username)
    {
        TableLine[] lines = dtb.GetTableLines(
           (dm) => dm.Name == dtbName,
           (tm) => tm.Name == tblName,
           (r) => (string)r.Line[1] == username
        );

        if (lines.Length == 1)
        {
            return lines[0];
        }
        return null;
    }

    private string? GetSalt(string username)
    {
        return (string?)GetUserData(tblSalts, username)?[2];
    }

    private string? GetHash(string username)
    {
        return (string?)GetUserData(tblHashes, username)?[2];
    }

    private static string NewSalt()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(saltLength));
    }

    public bool ChangePassword(string username, string oldPassword, string newPassword)
    {
        if (oldPassword.Trim() == newPassword.Trim())
        {
            throw new ArgumentException("Old password cannot be the same as the new one");
        }
        return VerifyPassword(username, oldPassword) && NewPassword(username, newPassword);
    }

    public bool VerifyPassword(string username, string password)
    {
        password = password.Trim();
        if (password.Length < MinLength)
        {
            throw new ArgumentException("Password too short");
        }

        string? salt = GetSalt(username);
        string? hash = GetHash(username);
        return salt is not null && hash is not null && hash.Equals(ComputeHash(password, salt));
    }

    public bool NewPassword(string username, string password)
    {
        password = password.Trim();
        if (password.Length < MinLength)
        {
            throw new ArgumentException("Password too short");
        }
        
        bool uspech = false;
        string? salt;
        
        TableLine? saltLine = GetUserData(tblSalts, username);

        if (saltLine is null)
        {
            salt = NewSalt();
            uspech = dtb.AddLine(
                (dm) => dm.Name == dtbName,
                (tm) => tm.Name == tblSalts,
                new(0, username, salt)
            ) == 1;
        }
        else
        {
            salt = (string?)saltLine[2];
            uspech = salt is not null;
        }

        if (!uspech)
        {
            return false;
        }

        string hash = ComputeHash(password, salt);
        return dtb.SetAttr(
                (dm) => dm.Name == dtbName,
                (tm) => tm.Name == tblHashes,
                (r) => (string)r.Line[1] == username,
                2, hash
            ) == 1
            || dtb.AddLine(
                (dm) => dm.Name == dtbName,
                (tm) => tm.Name == tblHashes,
                new(0, username, hash)
            ) == 1;
    }
}
