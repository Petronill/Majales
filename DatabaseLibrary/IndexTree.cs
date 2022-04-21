using System.Collections;

namespace DatabaseLibrary.Indexes;

public delegate void IndexTreeUpdateHandler(object sender, EventArgs args);
public delegate void IndexTreeReorganizationHandler(object sender, EventArgs args);

public class IndexTree<TIndex, TKey, Tval> : IDirectIndex<string, TIndex>, ILazyIndex<string, TIndex>, IEnumerable<KeyValuePair<string, TIndex>>, IEnumerable<TIndex> where TIndex : IIndex<TKey, Tval>
{
    protected readonly Dictionary<string, TIndex> indexes = new();

    public event IndexTreeUpdateHandler? IndexUpdated;
    public event IndexTreeUpdateHandler? IndexDeleted;
    public event IndexTreeReorganizationHandler? IndexCleared;

    protected void OnIndexUpdated(EventArgs args) => IndexUpdated?.Invoke(this, args);
    protected void OnIndexDeleted(EventArgs args) => IndexDeleted?.Invoke(this, args);
    protected void OnIndexCleared(EventArgs args) => IndexCleared?.Invoke(this, args);

    public virtual bool ContainsKey(string key)
    {
        return indexes.ContainsKey(key);
    }

    public virtual bool ContainsValue(TIndex index)
    {
        return indexes.ContainsValue(index);
    }

    public virtual TIndex? this[string key]
    {
        get
        {
            if (key is null || !ContainsKey(key))
            {
                return default;
            }
            return indexes[key];
        }

        set
        {
            if (value is not null && !indexes.ContainsKey(key))
            {
                indexes[key] = value;
                OnIndexUpdated(EventArgs.Empty);
            }
        }
    }

    public virtual Tval? this[string treeKey, TKey indexKey]
    {
        get
        {
            if (indexKey is null)
            {
                return default;
            }

            TIndex? index = this[treeKey];
            return (index is null) ? default : index[indexKey];
        }
    }

    public virtual void Add(string key, TIndex index)
    {
        if (!ContainsKey(key))
        {
            indexes.Add(key, index);
            OnIndexUpdated(EventArgs.Empty);
        }
    }

    public virtual void Update(string key, TIndex index)
    {
        if (ContainsKey(key))
        {
            this[key] = index;
        }
        else
        {
            Add(key, index);
        }
    }

    public virtual TIndex? Remove(string key)
    {
        TIndex? index = indexes[key];
        if (indexes.Remove(key))
        {
            OnIndexDeleted(EventArgs.Empty);
        }
        return index;
    }

    public virtual void Clear()
    {
        indexes.Clear();
        OnIndexCleared(EventArgs.Empty);
    }

    public IEnumerator<KeyValuePair<string, TIndex>> GetEnumerator()
    {
        return indexes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return indexes.GetEnumerator();
    }

    IEnumerator<TIndex> IEnumerable<TIndex>.GetEnumerator()
    {
        return indexes.Values.GetEnumerator();
    }
}
