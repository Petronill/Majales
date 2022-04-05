namespace DatabaseDefinitions;

public class PropUdpateArgs<V> : EventArgs
{
    public int Id { get; init; }
    public V? Prop { get; init; }
}

public struct SeparatorCrate<V>
{
    public PropSeparator<V> Separator { get; init; }
    public MetaSeparator<V> MetaSeparator { get; init; }
}

public delegate V PropSeparator<V>(Row row);
public delegate RowMeta? MetaSeparator<V>(V? prop);
public delegate bool PropPredicate<V>(V? prop);
public delegate int PropComparator<V>(V f, V s);
public delegate void PropUpdatedHandler<V>(object sender, PropUdpateArgs<V> e);
