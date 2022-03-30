using DatabaseDefinitions;

namespace DatabaseLibrary;

public interface IIndex<K, V>
{
    public abstract V? this[K key] { get; }

    public abstract void Clear();
}

public interface IDirectIndex<K, V> : IIndex<K, V>
{
    public new abstract V? this[K key] { get; set; }
}

public interface ILazyIndex<K, V> : IIndex<K, V> { }
public interface IEagerIndex<K, V> : IIndex<K, V>, IEnumerable<V>, IEnumerable<KeyValuePair<K, V>>
{
    public abstract int Size { get; }

}

