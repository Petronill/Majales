using DatabaseDefinitions;

namespace FileSupportLibrary;

public class DatabaseSupporter : IDatabaseSupport
{

    public bool Create(DatabaseMeta meta)
    {
        if (!Exists(meta))
        {
            return true;
        }

        try
        {
            Directory.CreateDirectory(meta.Path);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Delete(DatabaseMeta meta)
    {
        if (!Exists(meta))
        {
            return true;
        }

        try
        {
            Directory.Delete(meta.Path);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Exists(DatabaseMeta meta)
    {
        return Directory.Exists(meta.Path);
    }
}
