using DatabaseDefinitions;

namespace DatabaseLibrary.Indexes;

public class LazyPropIndex<V> : PropIndex<V>, ILazyIndex<int, V>, IQuietEnumerable<int, V>
{
    public LazyPropIndex(Table table, SeparatorCrate<V> separators, bool clearing = false) : base(separators)
    {
        InitHandlers();
        table.RowUpdated += RowUpdated;
        table.RowRequested += RowRequested;
        table.RowDeleted += RowDeleted;
        if (clearing)
        {
            table.TableCleared += TableCleared;
        }
    }

    protected LazyPropIndex(SeparatorCrate<V> separators) : base(separators)
    {
        InitHandlers();
    }

    protected void InitHandlers()
    {
        RowUpdated = (s, e) => Update(e.Row);
        RowRequested = (s, e) => Update(e.Row);
        RowDeleted = (s, e) => Remove(e.Row);
        TableCleared = (s, e) => Clear();
    }

    public LazyPropIndex<V> Where(PropPredicate<V> predicate)
    {
        LazyPropIndex<V> restricted = new(new SeparatorCrate<V> { Separator = Separator, MetaSeparator = MetaSeparator });

        foreach (var pair in index)
        {
            if (predicate(pair.Value))
            {
                restricted.index.Add(pair.Key, pair.Value);
            }
        }

        PropUpdated += (s, e) =>
        {
            if (predicate(e.Prop))
            {
                restricted[e.Id] = e.Prop;
            }
        };
        PropDeleted += (s, e) => restricted.Remove(e.Id);
        IndexCleared += (s, e) => restricted.Clear();
        return restricted;
    }

    public override void Select(PropPredicate<V> predicate)
    {

        foreach (var pair in index)
        {
            if (!predicate(pair.Value))
            {
                index.Remove(pair.Key);
            }
        }

        RowUpdated = (s, e) =>
        {
            if (predicate(Separator(e.Row)))
            {
                Update(e.Row);
            }
        };
        RowRequested = (s, e) =>
        {
            if (predicate(Separator(e.Row)))
            {
                Update(e.Row);
            }
        };
    }

    public IEnumerator<KeyValuePair<int, V>> GetQuietENumerator()
    {
        return index.GetEnumerator();
    }

    IEnumerator<V> IQuietEnumerable<V>.GetQuietEnumerator()
    {
        return index.Values.GetEnumerator();
    }

    
}
