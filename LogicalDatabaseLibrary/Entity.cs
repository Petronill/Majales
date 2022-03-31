﻿using LogicalDatabaseLibrary.Attrs;
using System.Collections;

namespace LogicalDatabaseLibrary;

public class Entity : IEnumerable<IAttr>, IEquatable<Entity>
{
    protected IAttr[] attrs;

    public Entity(params IAttr[] attributes)
    {
        attrs = attributes;
    }

    public int Count()
    {
        return attrs.Length;
    }

    public IAttr? this[int i]
    {
        get => (i >= 0 && i < attrs.Length) ? attrs[i] : null;
    }

    public IAttr? this[string name]
    {
        get => this[GetIndex(name)];
    }

    public int GetIndex(string name)
    {
        for (int i = 0; i < attrs.Length; i++)
        {
            if (attrs[i].GetName() == name)
            {
                return i;
            }
        }
        return -1;
    }

    public bool Equals(Entity? other)
    {
        if (other == null || this.Count() == other.Count())
        {
            return false;
        }

        for (int i = 0; i < this.Count(); i++)
        {
            if (!attrs[i].Equals(other[i]))
            {
                return false;
            }
        }
        return true;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Entity);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(attrs);
    }

    public IEnumerator<IAttr> GetEnumerator()
    {
        return ((IEnumerable<IAttr>)attrs).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return attrs.GetEnumerator();
    }
}
