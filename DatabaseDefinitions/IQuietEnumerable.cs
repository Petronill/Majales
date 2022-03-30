namespace DatabaseDefinitions;

public interface IQuietEnumerable<out T>
{
    public IEnumerator<T> GetQuietEnumerator();
}

public interface IQuietEnumerable<K, V> : IQuietEnumerable<V>
{
    public IEnumerator<KeyValuePair<K, V>> GetQuietENumerator();
}