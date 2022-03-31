using ArrayUtilsLibrary;

namespace LogicalDatabaseLibrary;

public class TableLine : Line
{
    public TableLine(TableEntity entity) : base(entity)
    {
    }

    public TableLine(int id, Line line)
    {
        Entity = new TableEntity(line.Entity.ToArray());
        object?[] tmp = line.ToArray();
        ArrayUtils.Prepend(ref tmp, id);
        content = tmp;
    }

    public int GetId()
    {
        return (int)content[0];
    }
}
