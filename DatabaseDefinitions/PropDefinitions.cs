namespace DatabaseDefinitions;

public class PropUdpateArgs<T> : EventArgs
{
    public int Id { get; init; }
    public T? Prop { get; init; }
}

public struct SeparatorCrate<T>
{
    public PropSeparator<T> Separator { get; init; }
    public MetaSeparator<T> MetaSeparator { get; init; }
}

public delegate T? PropSeparator<T>(Row row);
public delegate RowMeta? MetaSeparator<T>(T? prop);
public delegate bool PropPredicate<T>(T? prop);
public delegate int PropComparator<T>(T f, T s);
public delegate void PropUpdatedHandler<T>(object sender, PropUdpateArgs<T> e);
