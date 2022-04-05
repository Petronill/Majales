using DatabaseDefinitions;

namespace FileSupportLibrary;

public interface IDatabaseSupport
{
    public abstract bool Exists(DatabaseMeta meta);

    public abstract bool Create(DatabaseMeta meta);
    public abstract bool Delete(DatabaseMeta meta);
}
