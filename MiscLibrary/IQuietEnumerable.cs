namespace MiscLibrary;

public interface IQuietEnumerable<out T>
{
    public IEnumerator<T> GetQuietEnumerator();
}

public interface IQuietEnumerable<TKey, TVal> : IQuietEnumerable<TVal>
{
    public IEnumerator<KeyValuePair<TKey, TVal>> GetQuietENumerator();
}