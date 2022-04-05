namespace DatabaseDefinitions;

public struct DatabaseMeta
{
    public string Name { get; init; }
    public string Path { get; init; }
}

public delegate bool DatabaseSelector(DatabaseMeta meta);
