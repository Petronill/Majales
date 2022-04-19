namespace MiscLibrary;

public interface Domain<D> : IEnumerable<D>
{
    public new abstract IEnumerator<D> GetEnumerator();
}
