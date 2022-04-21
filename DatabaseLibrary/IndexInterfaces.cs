namespace DatabaseLibrary.Indexes;

public interface IIndex<TKey, TVal>
{
    public abstract TVal? this[TKey key] { get; }

    public abstract void Clear();
}

public interface IDirectIndex<TKey, TVal> : IIndex<TKey, TVal>
{
    public new abstract TVal? this[TKey key] { get; set; }
}

public interface ILazyIndex<TKey, TVal> : IIndex<TKey, TVal> { }
public interface IEagerIndex<TKey, TVal> : IIndex<TKey, TVal>, IEnumerable<TVal>, IEnumerable<KeyValuePair<TKey, TVal>>
{
    public abstract int Size { get; }

}

