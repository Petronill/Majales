using ArrayUtilsLibrary;

namespace LogicalDatabaseLibrary;

public class TableLine : Line
{
    public TableLine()
    {
    }
    
    public TableLine(object?[] content) : base(content)
    {
    }
    
    public int GetId()
    {
        return (int)content[0];
    }
}
