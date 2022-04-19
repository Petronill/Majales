namespace PasswordManagementLibrary;

public interface IPasswordStorage
{
    public abstract string? GetSalt(string username);
    public abstract string? GetHash(string username);
    public abstract bool AddSalt(string username, string salt);
    public abstract bool UpdateHash(string username, string hash);
}
