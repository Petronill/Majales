using DatabaseDefinitions;

namespace DatabaseLibrary;

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

public interface IPropIndex
{
    public abstract RowMeta? GetMeta(int id);
}

public abstract class PropIndex<V> : IPropIndex, IDirectIndex<int, V>
{
    protected readonly Dictionary<int, V> index = new();
    public PropSeparator<V> Separator { get; init; }
    public MetaSeparator<V> MetaSeparator { get; init; }

    public event PropUpdatedHandler<V>? PropUpdated;
    public event PropUpdatedHandler<V>? PropDeleted;
    public event TableReorganizationHandler? IndexCleared;
    protected RowUpdatedHandler RowUpdated;
    protected RowUpdatedHandler RowRequested;
    protected RowUpdatedHandler RowDeleted;
    protected TableReorganizationHandler TableCleared;

    protected virtual void OnPropUpdated(PropUdpateArgs<V> args) => PropUpdated?.Invoke(this, args);
    protected virtual void OnPropDeleted(PropUdpateArgs<V> args) => PropDeleted?.Invoke(this, args);
    protected virtual void OnIndexCleared(EventArgs args) => IndexCleared?.Invoke(this, args);

    public PropIndex(SeparatorCrate<V> separators)
    {
        Separator = separators.Separator;
        MetaSeparator = separators.MetaSeparator;
    }

    public bool ContainsKey(int id)
    {
        return index.ContainsKey(id);
    }

    protected bool ContainsProp(Row row)
    {
        return ContainsProp(Separator(row));
    }

    protected bool ContainsProp(V prop)
    {
        return index.ContainsValue(prop);
    }

    protected bool ContainsProp(PropPredicate<V> predicate)
    {
        return First(predicate) > -1;
    }

    public V? this[int id]
    {
        get
        {
            if (ContainsKey(id))
            {
                return index[id];
            }

            return default;
        }

        set
        {
            if (value != null)
            {
                index[id] = value;
                OnPropUpdated(new PropUdpateArgs<V> { Id = id, Prop = value });
            }
        }
    }

    public V? this[Row row]
    {
        get
        {
            return this[LineFormat.GetId(row)];
        }

        set
        {
            this[LineFormat.GetId(row)] = value;
        }
    }

    public void Update(Row row)
    {
        this[row] = Separator(row);
    }

    public void Remove(int id)
    {
        index.Remove(id);
        OnPropDeleted(new PropUdpateArgs<V> { Id = id });
    }

    public void Remove(Row row)
    {
        Remove(LineFormat.GetId(row));
    }

    public RowMeta? GetMeta(int id)
    {
        return ContainsKey(id) ? MetaSeparator(this[id]) : null;
    }

    protected V? ExtremeValue(PropComparator<V> comparator)
    {
        V ext = index.FirstOrDefault().Value;
        foreach (var pair in index)
        {
            if (comparator(pair.Value, ext) < 0)
            {
                ext = pair.Value;
            }
        }
        return ext;
    }

    protected int First(PropPredicate<V> predicate)
    {
        foreach (var prop in index)
        {
            if (predicate(prop.Value))
            {
                return prop.Key;
            }
        }

        return -1;
    }

    protected int First(V prop)
    {
        return First((p) => p.Equals(prop));
    }

    protected int First(Row row)
    {
        return First((p) => p.Equals(Separator(row)));
    }

    protected int Last(PropPredicate<V> predicate)
    {
        int id = -1;
        foreach (var prop in index)
        {
            if (predicate(prop.Value))
            {
                id = prop.Key;
            }
        }
        return id;
    }

    protected int Last(V prop)
    {
        return Last((p) => p.Equals(prop));
    }

    protected int Last(Row row)
    {
        return Last((p) => p.Equals(Separator(row)));
    }

    public abstract void Select(PropPredicate<V> predicate);

    public void Clear()
    {
        index.Clear();
        OnIndexCleared(new EventArgs());
    }
}

