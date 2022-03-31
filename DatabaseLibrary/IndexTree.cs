using System.Collections;

namespace DatabaseLibrary;

public delegate void IndexTreeUpdateHandler(object sender, EventArgs args);
public delegate void IndexTreeReorganizationHandler(object sender, EventArgs args);

public class IndexTree<K, V> : IDirectIndex<string, IIndex<K, V>>, ILazyIndex<string, IIndex<K, V>>, IEnumerable<KeyValuePair<string, IIndex<K, V>>>, IEnumerable<IIndex<K, V>>
{
    private readonly Dictionary<string, IIndex<K, V>> indexes = new();

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

    public bool ContainsValue(IIndex<K, V> index)
    {
        return indexes.ContainsValue(index);
    }

    public IIndex<K, V>? this[string key]
    {
        get
        {
            if (key == null || !ContainsKey(key))
            {
                return null;
            }
            return indexes[key];
        }

        set
        {
            if (value != null && !indexes.ContainsKey(key))
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
            if (indexKey == null)
            {
                return default;
            }

            IIndex<K, V>? index = this[treeKey];
            return (index == null) ? default(V) : index[indexKey];
        }
    }

    public void Add(string key, IIndex<K, V> index)
    {
        if (!ContainsKey(key))
        {
            indexes.Add(key, index);
            OnIndexUpdated(EventArgs.Empty);
        }
    }

    public void Update(string key, IIndex<K, V> index)
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

    public IIndex<K, V>? Remove(string key)
    {
        IIndex<K, V>? index = indexes[key];
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

    public IEnumerator<KeyValuePair<string, IIndex<K, V>>> GetEnumerator()
    {
        return indexes.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return indexes.GetEnumerator();
    }

    IEnumerator<IIndex<K, V>> IEnumerable<IIndex<K, V>>.GetEnumerator()
    {
        return indexes.Values.GetEnumerator();
    }
}
