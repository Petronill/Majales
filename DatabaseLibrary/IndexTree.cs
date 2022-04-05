using System.Collections;

namespace DatabaseLibrary.Indexes;

public delegate void IndexTreeUpdateHandler(object sender, EventArgs args);
public delegate void IndexTreeReorganizationHandler(object sender, EventArgs args);

public class IndexTree<I, K, V> : IDirectIndex<string, I>, ILazyIndex<string, I>, IEnumerable<KeyValuePair<string, I>>, IEnumerable<I> where I : IIndex<K, V>
{
    private readonly Dictionary<string, I> indexes = new();

    public event IndexTreeUpdateHandler? IndexUpdated;
    public event IndexTreeUpdateHandler? IndexDeleted;
    public event IndexTreeReorganizationHandler? IndexCleared;

    protected void OnIndexUpdated(EventArgs args) => IndexUpdated?.Invoke(this, args);
    protected void OnIndexDeleted(EventArgs args) => IndexDeleted?.Invoke(this, args);
    protected void OnIndexCleared(EventArgs args) => IndexCleared?.Invoke(this, args);

    public bool ContainsKey(string key)
    {
        return indexes.ContainsKey(key);
    }

    public bool ContainsValue(I index)
    {
        return indexes.ContainsValue(index);
    }

    public I? this[string key]
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

    public V? this[string treeKey, K indexKey]
    {
        get
        {
            if (indexKey is null)
            {
                return default;
            }

            I? index = this[treeKey];
            return (index is null) ? default : index[indexKey];
        }
    }

    public void Add(string key, I index)
    {
        if (!ContainsKey(key))
        {
            indexes.Add(key, index);
            OnIndexUpdated(EventArgs.Empty);
        }
    }

    public void Update(string key, I index)
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

    public I? Remove(string key)
    {
        I? index = indexes[key];
        if (indexes.Remove(key))
        {
            OnIndexDeleted(EventArgs.Empty);
        }
        return index;
    }

    public void Clear()
    {
        indexes.Clear();
        OnIndexCleared(EventArgs.Empty);
    }

    public IEnumerator<KeyValuePair<string, I>> GetEnumerator()
    {
        return indexes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return indexes.GetEnumerator();
    }

    IEnumerator<I> IEnumerable<I>.GetEnumerator()
    {
        return indexes.Values.GetEnumerator();
    }
}
