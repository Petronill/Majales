using LogicalDatabaseLibrary;

namespace DatabaseDefinitions;

public delegate T? AttrSeparator<T>(Line line);

public static class AttrSeparatorFactory
{
    public static AttrSeparator<T> GetSeparator<T>(int index)
    {
        return (line) => { return (T?)line?[index]; };
    }

    public static PropSeparator<T> AttrToProp<T>(AttrSeparator<T> separator)
    {
        return (row) => { return separator(row.Line); };
    }
}
