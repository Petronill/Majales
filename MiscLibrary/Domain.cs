namespace MiscLibrary;

public interface Domain<T> : IEnumerable<T>
{
    public new abstract IEnumerator<T> GetEnumerator();
}
