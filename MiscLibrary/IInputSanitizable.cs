using MiscLibrary;

namespace MiscLibrary.Sanitizing;

public interface IInputSanitizable<in T>
{
    public abstract bool Check(T input);
}

public interface IInputSanitizable<TDomain, in TType>
{
    public abstract bool Check(TDomain domain, TType input);
}
