using LogicalDatabaseLibrary;

namespace DatabaseDefinitions;

public delegate V? AttrSeparator<V>(Line line);

public static class AttrSeparatorFactory
{
    public static AttrSeparator<V> GetSeparator<V>(int index)
    {
        return (line) => { return (V?)line?[index]; };
    }

    public static PropSeparator<V> AttrToProp<V>(AttrSeparator<V> separator)
    {
        return (row) => { return separator(row.Line); };
    }
}
