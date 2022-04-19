namespace LogicalDatabaseLibrary;

public class TableLine : Line
{
    public TableLine()
    {
    }

    public TableLine(params object?[] content) : base(content)
    {
    }
    
    public virtual int GetId()
    {
        return (int)content[0];
    }
}
