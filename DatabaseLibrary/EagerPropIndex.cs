using DatabaseDefinitions;

using System.Collections;

namespace DatabaseLibrary.Indexes;

public class EagerPropIndex<T> : PropIndex<T>, IEagerIndex<int, T>
{
    public int Size { get => index.Count; }

    public EagerPropIndex(Table table, SeparatorCrate<T> separators, bool clearing = false) : base(separators)
    {
        foreach (Row row in table)
        {
            this[row.Line.GetId()] = Separator(row);
        }

        InitHandlers();
        table.RowUpdated += RowUpdated;
        table.RowDeleted += RowDeleted;
        if (clearing)
        {
            table.TableCleared += TableCleared;
        }
    }

    protected EagerPropIndex(SeparatorCrate<T> separators) : base(separators)
    {
        InitHandlers();
    }

    protected virtual void InitHandlers()
    {
        RowUpdated = (s, e) => Update(e.Row);
        RowDeleted = (s, e) => Remove(e.Row);
        TableCleared = (s, e) => Clear();
    }

    public new bool ContainsProp(Row row)
    {
        return ContainsProp(Separator(row));
    }

    public new bool ContainsProp(T prop)
    {
        return index.ContainsValue(prop);
    }

    public new bool ContainsProp(PropPredicate<T> predicate)
    {
        return First(predicate) > -1;
    }

    public new T? ExtremeValue(PropComparator<T> comparator)
    {
        return base.ExtremeValue(comparator);
    }

    public new int First(PropPredicate<T> predicate)
    {
        return base.First(predicate);
    }

    public new int First(T prop)
    {
        return base.First(prop);
    }

    public new int First(Row row)
    {
        return base.First(row);
    }

    public new int Last(PropPredicate<T> predicate)
    {
        return base.Last(predicate);
    }

    public new int Last(T prop)
    {
        return base.Last(prop);
    }

    public new int Last(Row row)
    {
        return base.Last(row);
    }

    public virtual EagerPropIndex<T> Where(PropPredicate<T> predicate)
    {
        EagerPropIndex<T> restricted = new(new SeparatorCrate<T> { Separator = Separator, MetaSeparator = MetaSeparator });

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
    }

    public IEnumerator<KeyValuePair<int, T>> GetEnumerator()
    {
        return index.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return index.GetEnumerator();
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return index.Values.GetEnumerator();
    }
}
