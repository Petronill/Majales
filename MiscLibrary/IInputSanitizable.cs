using MiscLibrary;

namespace MiscLibrary.Sanitizing;

public interface IInputSanitizable<in I>
{
    public abstract bool Check(I input);
}

public interface IInputSanitizable<D, in I>
{
    public abstract bool Check(D domain, I input);
}
