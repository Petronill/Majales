using DatabaseDefinitions;

using System.Collections;

namespace DatabaseLibrary.Indexes;

public class EagerPropIndex<V> : PropIndex<V>, IEagerIndex<int, V>
{
    public int Size { get => index.Count; }

    public EagerPropIndex(Table table, SeparatorCrate<V> separators, bool clearing = false) : base(separators)
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

    protected EagerPropIndex(SeparatorCrate<V> separators) : base(separators)
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

    public new bool ContainsProp(V prop)
    {
        return index.ContainsValue(prop);
    }

    public new bool ContainsProp(PropPredicate<V> predicate)
    {
        return First(predicate) > -1;
    }

    public new V? ExtremeValue(PropComparator<V> comparator)
    {
        return base.ExtremeValue(comparator);
    }

    public new int First(PropPredicate<V> predicate)
    {
        return base.First(predicate);
    }

    public new int First(V prop)
    {
        return base.First(prop);
    }

    public new int First(Row row)
    {
        return base.First(row);
    }

    public new int Last(PropPredicate<V> predicate)
    {
        return base.Last(predicate);
    }

    public new int Last(V prop)
    {
        return base.Last(prop);
    }

    public new int Last(Row row)
    {
        return base.Last(row);
    }

    public virtual EagerPropIndex<V> Where(PropPredicate<V> predicate)
    {
        EagerPropIndex<V> restricted = new(new SeparatorCrate<V> { Separator = Separator, MetaSeparator = MetaSeparator });

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
    }

    public IEnumerator<KeyValuePair<int, V>> GetEnumerator()
    {
        return index.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return index.GetEnumerator();
    }

    IEnumerator<V> IEnumerable<V>.GetEnumerator()
    {
        return index.Values.GetEnumerator();
    }
}
