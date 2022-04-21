using DatabaseDefinitions;
using MiscLibrary;

namespace DatabaseLibrary.Indexes;

public class LazyPropIndex<T> : PropIndex<T>, ILazyIndex<int, T>, IQuietEnumerable<int, T>
{
    public LazyPropIndex(Table table, SeparatorCrate<T> separators, bool clearing = false) : base(separators)
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

    protected LazyPropIndex(SeparatorCrate<T> separators) : base(separators)
    {
        InitHandlers();
    }

    protected virtual void InitHandlers()
    {
        RowUpdated = (s, e) => Update(e.Row);
        RowRequested = (s, e) => Update(e.Row);
        RowDeleted = (s, e) => Remove(e.Row);
        TableCleared = (s, e) => Clear();
    }

    public virtual LazyPropIndex<T> Where(PropPredicate<T> predicate)
    {
        LazyPropIndex<T> restricted = new(new SeparatorCrate<T> { Separator = Separator, MetaSeparator = MetaSeparator });

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

    public override void Select(PropPredicate<T> predicate)
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

    public IEnumerator<KeyValuePair<int, T>> GetQuietENumerator()
    {
        return index.GetEnumerator();
    }

    IEnumerator<T> IQuietEnumerable<T>.GetQuietEnumerator()
    {
        return index.Values.GetEnumerator();
    }

    
}
