using System.Collections;

namespace DatabaseLibrary;

public class IndexTree<K, V> : IDirectIndex<string, IIndex<K, V>>, ILazyIndex<string, IIndex<K, V>>, IEnumerable<KeyValuePair<string, IIndex<K, V>>>, IEnumerable<IIndex<K, V>>
{
    private readonly Dictionary<string, IIndex<K, V>> indexes = new();
    public IIndex<K, V>? this[string key]
    {
        get
        {
            if (key == null || !indexes.ContainsKey(key))
            {
                return null;
            }
            return indexes[key];
        }

        set
        {
            if (value != null)
            {
                indexes[key] = value;
            }
        }
    }

    public void Clear()
    {
        indexes.Clear();
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
