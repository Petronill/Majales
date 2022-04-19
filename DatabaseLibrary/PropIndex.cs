﻿using DatabaseDefinitions;

namespace DatabaseLibrary.Indexes;

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

    public virtual bool ContainsKey(int id)
    {
        return index.ContainsKey(id);
    }

    public virtual bool ContainsKey(Row row)
    {
        return index.ContainsKey(row.Line.GetId());
    }

    protected virtual bool ContainsProp(Row row)
    {
        return ContainsProp(Separator(row));
    }

    protected virtual bool ContainsProp(V prop)
    {
        return index.ContainsValue(prop);
    }

    protected virtual bool ContainsProp(PropPredicate<V> predicate)
    {
        return First(predicate) > -1;
    }

    public virtual V? this[int id]
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
            if (value is not null && ContainsKey(id))
            {
                index[id] = value;
                OnPropUpdated(new PropUdpateArgs<V> { Id = id, Prop = value });
            }
        }
    }

    public virtual V? this[Row row]
    {
        get
        {
            return this[row.Line.GetId()];
        }

        set
        {
            this[row.Line.GetId()] = value;
        }
    }

    public virtual void Add(int id, V prop)
    {
        if (!ContainsKey(id))
        {
            index.Add(id, prop);
            OnPropUpdated(new PropUdpateArgs<V> { Id = id, Prop = prop });
        }
    }

    public virtual void Add(Row row)
    {
        int id = row.Line.GetId();
        V prop = Separator(row);
        Add(id, prop);
    }

    public virtual void Update(int id, V prop)
    {
        if (ContainsKey(id))
        {
            this[id] = prop;
        }
        else
        {
            Add(id, prop);
        }
    }

    public virtual void Update(Row row)
    {
        int id = row.Line.GetId();
        V prop = Separator(row);
        Update(id, prop);
    }

    public virtual void Remove(int id)
    {
        if (index.Remove(id))
        {
            OnPropDeleted(new PropUdpateArgs<V> { Id = id });
        }
    }

    public virtual void Remove(Row row)
    {
        Remove(row.Line.GetId());
    }

    public virtual RowMeta? GetMeta(int id)
    {
        return ContainsKey(id) ? MetaSeparator(this[id]) : null;
    }

    protected virtual V? ExtremeValue(PropComparator<V> comparator)
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

    protected virtual int First(PropPredicate<V> predicate)
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

    protected virtual int First(V prop)
    {
        return First((p) => p.Equals(prop));
    }

    protected virtual int First(Row row)
    {
        return First((p) => p.Equals(Separator(row)));
    }

    protected virtual int Last(PropPredicate<V> predicate)
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

    protected virtual int Last(V prop)
    {
        return Last((p) => p.Equals(prop));
    }

    protected virtual int Last(Row row)
    {
        return Last((p) => p.Equals(Separator(row)));
    }

    public abstract void Select(PropPredicate<V> predicate);

    public virtual void Clear()
    {
        index.Clear();
        OnIndexCleared(new EventArgs());
    }
}

