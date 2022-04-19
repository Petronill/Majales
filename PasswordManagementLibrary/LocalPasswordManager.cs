using System.Security.Cryptography;
using System.Text;

namespace PasswordManagementLibrary;

public class LocalPasswordManager : IPasswordManager
{
    private static readonly int saltLength = 32;
    private readonly IPasswordStorage storage;

    public int MinLength { get => 8; }

    public LocalPasswordManager(IPasswordStorage storage) {
        this.storage = storage;
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

        string? salt = storage.GetSalt(username);
        string? hash = storage.GetHash(username);
        return salt is not null && hash is not null && hash.Equals(ComputeHash(password, salt));
    }

    public bool NewPassword(string username, string password)
    {
        password = password.Trim();
        if (password.Length < MinLength)
        {
            throw new ArgumentException("Password too short");
        }
        
        string? salt = storage.GetSalt(username);
        if (salt is null)
        {
            salt = NewSalt();
            if (!storage.AddSalt(username, salt))
            {
                return false;
            }
        }

        return storage.UpdateHash(username, ComputeHash(password, salt));
    }
}
