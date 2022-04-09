namespace PasswordManagementLibrary;

public interface IPasswordManager
{
    public abstract int MinLength { get; }
    public abstract bool VerifyPassword(string username, string password);
    public abstract bool NewPassword(string username, string password);
    public abstract bool ChangePassword(string username, string oldPassword, string newPassword);
}
