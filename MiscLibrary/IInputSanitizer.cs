namespace MiscLibrary.Sanitizing;

public interface IInputSanitizer<T> : IInputSanitizable<T>
{
    public abstract T Sanitize(T input);
}

public interface IInputSanitizer<TDomain, TType> : IInputSanitizable<TDomain, TType>
{
    public abstract TType Sanitize(TDomain domain, TType input);
}
